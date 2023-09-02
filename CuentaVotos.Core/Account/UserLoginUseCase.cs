using CuentaVotos.Entities.Account;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Core.Account
{
    public class UserLoginUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public UserLoginUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            Repository = _accountRepository;
        }
        public UserLogin  UserLogin  { get; set; } = new UserLogin();

        public IAccountRepository Repository { get; set; }
        public ModelResult<UserProfile> Authenticate()
        {
           return _accountRepository.Login(UserLogin);
        }
    }
}
