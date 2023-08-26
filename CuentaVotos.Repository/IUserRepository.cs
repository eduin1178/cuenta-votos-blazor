using CuentaVotos.Entiies.Account;
using CuentaVotos.Entiies.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Repository
{
    public interface IUserRepository 
    {
        ModelResult<User> Login(string email, string password);
        ResultBase Create(User user);

        ModelResult<List<User>> List();
    }
}
