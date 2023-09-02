using CuentaVotos.Entities.Account;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;

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
