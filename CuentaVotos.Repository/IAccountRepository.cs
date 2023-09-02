using CuentaVotos.Entities.Account;
using CuentaVotos.Entities.Shared;

namespace CuentaVotos.Repository
{
    public interface IAccountRepository
    {
        ModelResult<UserProfile> Login(UserLogin userLogin);
        ModelResult<UserProfile> Register(UserCreate user);
        ModelResult<UserProfile> Profile(string userCode);
        ModelResult<string> ChangePassword(string userCode, UserChangePassword userChangePassword);
        ModelResult<string> UpdateProfile(string userCode, UserProfile profile);
    }
}
