using CuentaVotos.Entities.Account;
using CuentaVotos.Entiies.Shared;

namespace CuentaVotos.Repository
{
    public interface IAccountRepository
    {
        ModelResult<UserProfile> Login(UserLogin userLogin);
        ModelResult<UserProfile> Register(UserCreate user);
        ModelResult<UserProfile> Profile(string userCode);
        ResultBase ChangePassword(UserChangePassword userChangePassword);
    }
}
