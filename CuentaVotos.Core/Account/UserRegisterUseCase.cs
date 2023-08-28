using CuentaVotos.Entities.Account;
using CuentaVotos.Entiies.Shared;
using CuentaVotos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Core.Account
{
    public class UserRegisterUseCase
    {
        private readonly IAccountRepository _accountRepository;

        public UserRegisterUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        
        }
        
        public UserCreate UserCreate { get; set; } = new UserCreate();
        
        public ModelResult<UserProfile> Register()
        {
            return _accountRepository.Register(UserCreate);
        }
    }
}
