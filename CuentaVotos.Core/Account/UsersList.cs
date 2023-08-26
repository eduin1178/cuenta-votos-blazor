using CuentaVotos.Entiies.Account;
using CuentaVotos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Core.Account
{
    public class UsersList
    {
        private readonly IUserRepository _userRepository;

        public UsersList(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string ResponseMessage { get; set; }

        public void Load()
        {
            var res = _userRepository.List();
            if (res.IsSuccess)
            {
                Users = res.Model;
            }
            else
            {
                Users = new List<User> ();
                ResponseMessage= res.Message;
            }
        }

        public List<User>? Users { get; set; }
    }
}
