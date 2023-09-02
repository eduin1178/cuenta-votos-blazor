using Blazored.LocalStorage;
using CuentaVotos.Entities.Account;
using Elecciones.Client.Api;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Elecciones.Client.Application
{
    public class AppAuthenticationProvider : AuthenticationStateProvider
    {

        private readonly ILocalStorageService _localStorage;
        ApiClient _client;
        private AuthenticationState Anonimo = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        public AppAuthenticationProvider(ILocalStorageService localStorage, ApiClient client)
        {
            _localStorage = localStorage;
            _client = client;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            try
            {
                var userToken = await _localStorage.GetItemAsync<AccessTokenModel>("CuentaVotosAuth");

                if (userToken == null)
                {
                    return await Task.FromResult(Anonimo);
                }
                var ahora = DateTime.Now;
                if (userToken.Expires < ahora)
                {
                    //TODO: Refreshtoken
                    return await Task.FromResult(Anonimo);
                }
                _client.SetAuthorization(userToken.AccessToken);

                var userProfile = await _localStorage.GetItemAsync<UserProfile>("CuentaVotosProfile");

                if (userProfile == null)
                {
                    return await Task.FromResult(Anonimo);
                }

                var user = SignIn(userProfile);

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
