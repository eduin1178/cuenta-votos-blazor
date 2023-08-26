using CuentaVotos.Entiies.Account;
using CuentaVotos.Entiies.Shared;
using CuentaVotos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Core.Account.Login
{
    public class LoginModel
    {
        private readonly IUserRepository _userRepository;

        public LoginModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Email { get; set; } = "eduin1178@gmail.com";
        public string Password { get; set; } = "123";
        public bool RememberMe { get; set; }

        public string LoginMessage { get; set; } = string.Empty;
        

        public ResultBase Login()
        {
            var res =  _userRepository.Login(Email, Password);
            LoginMessage = res.Message; 

            return res;
        }

        public ResultBase Create()
        {
            var entity = new User();

            entity.Email = Email;
            entity.PasswordHash = Password;

            var res = _userRepository.Create(entity);

            return res;
        }

    }
}
