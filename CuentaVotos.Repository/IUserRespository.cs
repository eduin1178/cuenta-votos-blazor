using CuentaVotos.Entities.Account;
using CuentaVotos.Entities.Shared;

namespace CuentaVotos.Repository
{
    public interface IUserRespository
    {
        ModelResult<IEnumerable<UserModel>> List(int? roleId = null, int? stateId = null);
        ModelResult<string> ChangeState(int userId, int newState);
        ModelResult<string> ChangeRol(int userId, int newRole);
        ModelResult<string> Delete(int userId);
        ModelResult<string> RestorePassword(UserRestorePassword model);

    }
}
