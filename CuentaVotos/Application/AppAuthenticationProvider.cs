
using CuentaVotos.Api;
using CuentaVotos.Entities.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace CuentaVotos.Application
{
    public class AppAuthenticationProvider : AuthenticationStateProvider
    {
        ProtectedLocalStorage _localStorage;
        ApiClient _client;
        private AuthenticationState Anonimo = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        public AppAuthenticationProvider(ProtectedLocalStorage localStorage, ApiClient client)
        {
            _localStorage = localStorage;
            _client = client;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userToken = await _localStorage.GetAsync<AccessTokenModel>("CuentaVotosAuth");
                var userAuth = userToken.Success ? userToken.Value : null;
                if (userAuth == null)
                {
                    return await Task.FromResult(Anonimo);
                }
                var ahora = DateTime.Now;
                if (userAuth.Expires < ahora)
                {
                    //TODO: Refreshtoken
                    return await Task.FromResult(Anonimo);
                }
                _client.SetAuthorization(userAuth.AccessToken);
                
                var userProfile = await _localStorage.GetAsync<UserProfile>("CuentaVotosProfile");
                var userSession = userProfile.Success ? userProfile.Value : null;
                if (userSession == null)
                {
                    return await Task.FromResult(Anonimo);
                }

                var user = SignIn(userSession);

                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(user)));
            }
            catch (Exception)
            {

                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }
        }

     
        private ClaimsPrincipal SignIn(UserProfile userInfo)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userInfo.Codigo.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, userInfo.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, userInfo.LastName));
            claims.Add(new Claim(ClaimTypes.Email, userInfo.Email));
            claims.Add(new Claim(ClaimTypes.Role, userInfo.RoleId.ToString()));

            var claimIdentity = new ClaimsIdentity(claims, "CuentaVotosAuth");

            var authenticatedUser = new ClaimsPrincipal(claimIdentity);
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            NotifyAuthenticationStateChanged(authState);

            return authenticatedUser;
        }
    }
}
