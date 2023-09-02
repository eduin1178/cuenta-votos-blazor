using CuentaVotos.Entities.Shared;
using CuentaVotos.Entities.Account;
using CuentaVotos.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _config;
        public AccountController(IAccountRepository accountRepository, IConfiguration config)
        {
            _accountRepository = accountRepository;
            _config = config;
        }

        [HttpGet("Profile")]
        public IActionResult Profile()
        {

            if (string.IsNullOrEmpty(User.Identity?.Name))
            {
                return Unauthorized();
            }

            var res = _accountRepository.Profile(User.Identity.Name.ToString());
            return Ok(res);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(UserLogin model)
        {
            var res = _accountRepository.Login(model);
            if (res.IsSuccess)
            {
                var token = CreateToken(res.Model!);
                var result = new ModelResult<AccessTokenModel>
                {
                    IsSuccess = true,
                    Message = "OK",
                    Model = token,
                };

                return Ok(result);
            }
            return Ok(res);
        }

        private AccessTokenModel CreateToken(UserProfile model)
        {
            var issuer = _config.GetValue<string>("Jwt:Issuer");
            var audience = _config.GetValue<string>("Jwt:Audience");
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Jwt:Key"));
            var duration = _config.GetValue<int>("Jwt:ExpireMinutes");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, model.Codigo.ToString()),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Name, model.FirstName),
                new Claim(ClaimTypes.Surname, model.LastName),
                new Claim(ClaimTypes.Role, model.RoleId.ToString()),
             }),
                Expires = DateTime.UtcNow.AddMinutes(duration),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return new AccessTokenModel
            {
                AccessToken = jwtToken,
                ExpireMinutes = duration,
                Created = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(duration),
            };
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserCreate model)
        {
            var res = _accountRepository.Register(model);
            return Ok(res);
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(UserChangePassword model)
        {
            var userCode = User.Identity?.Name;
            if (string.IsNullOrEmpty(userCode))
            {
                return Unauthorized();
            }
            var res = _accountRepository.ChangePassword(userCode, model);
            return Ok(res);
        }

        [HttpPost("UpdateProfile")]
        public IActionResult UpdateProfile(UserProfile model)
        {
            var userCode = User.Identity?.Name;
            if (string.IsNullOrWhiteSpace(userCode))
            {
                return Unauthorized();
            }
            var res = _accountRepository.UpdateProfile(userCode, model);
            return Ok(res);
        }
    }
}
