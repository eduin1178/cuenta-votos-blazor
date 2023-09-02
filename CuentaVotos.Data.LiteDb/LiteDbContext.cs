using CuentaVotos.Entities.Puestos;
using CuentaVotos.Entities.Account;
using System.Xml.Linq;

namespace CuentaVotos.Data.LiteDb
{
    public class LiteDbContext : ALiteDbContext
    {

        public LiteDbContext(string connectionString) :base(connectionString)
        {
            Users = new LiteDbSet<User>(InternalDatabase);
            Users.ConfigureIndices(x=>x.Id, true);
            Users.ConfigureIndices(x=>x.Email, true);
            Users.ConfigureIndices(x=>x.Codigo, true);

            Puestos = new LiteDbSet<Puesto>(InternalDatabase);
            Puestos.ConfigureIndices(x=>x.Id, true);
            Puestos.ConfigureIndices(x=>x.Code, true);


            Mesas = new LiteDbSet<Mesa>(InternalDatabase);
            Mesas.ConfigureIndices(x => x.Id, true);
            Mesas.ConfigureIndices(x => x.Code, true);
            Mesas.ConfigureIndices(x => x.PuestoId, false);
            Mesas.ConfigureIndices(x => x.UserId, false);

        }

        public readonly LiteDbSet<User> Users;
        public readonly LiteDbSet<Puesto> Puestos;
        public readonly LiteDbSet<Mesa> Mesas;

    }
}
