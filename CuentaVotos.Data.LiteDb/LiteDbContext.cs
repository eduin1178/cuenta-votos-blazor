using CuentaVotos.Entiies.Account;
using LiteDB;

namespace CuentaVotos.Data.LiteDb
{
    public class LiteDbContext : LiteDatabase
    {
        public LiteDbContext(string connectionString) : base(connectionString)
        {
            Users = this.GetCollection<User>("Users");
        }
        public ILiteCollection<User> Users { get; set; }

    }
}
