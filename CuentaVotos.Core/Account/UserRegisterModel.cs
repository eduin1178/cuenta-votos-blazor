using CuentaVotos.Entiies.Account;
using CuentaVotos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Core.Account
{
    public class UserRegisterModel
    {
        private readonly IUserRepository _userRepository;

        public UserRegisterModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User User { get; set; } = new User();

        public string RegisterMessage { get; set; }

        public void Register()
        {
            var res = _userRepository.Create(User);

            RegisterMessage = res.Message;

            User = new User();
        }
    }
}
