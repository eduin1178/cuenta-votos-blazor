using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Storage
{
    public class BunnyStorageConfig
    {
        public string Region { get; set; }
        public string StorageZone { get; set; }
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string ConnectionType { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string PasswordReadOnly { get; set; }
        public long FileSizeLimit { get; set; } = 1024;
    }
}
