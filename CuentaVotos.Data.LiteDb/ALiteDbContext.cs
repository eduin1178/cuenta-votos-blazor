using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Data.LiteDb
{
    public abstract class ALiteDbContext : IDisposable
    {
        protected LiteDatabase InternalDatabase;

        protected ALiteDbContext(string databasePath)
        {
            InternalDatabase = new LiteDatabase(databasePath);
        }

        public void Dispose()
        {
            InternalDatabase.Dispose();
        }
    }
}
