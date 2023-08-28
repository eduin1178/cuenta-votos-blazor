using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Account
{
    public class AccessTokenModel
    {
        public string AccessToken { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
