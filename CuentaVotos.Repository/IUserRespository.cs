using CuentaVotos.Entities.Account;
using CuentaVotos.Entiies.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Repository
{
    public interface IUserRespository
    {
        ModelResult<IEnumerable<UserModel>> List(int? roleId = null, int? stateId = null);        
        ResultBase ChangeState(int userId, int newState);
        ResultBase ChangeRol(int userId, int newRole);
        ResultBase Delete(int userId);
        ResultBase RestorePassword(int userId, string newPassword);

    }
}
