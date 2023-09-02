using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Puestos
{
    public class UserTables
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string UserFirsName { get; set; }
        public string UserLastName { get; set; }
        public string UserFullName  => $"{UserFirsName} {UserLastName}";

        public List<MesaModel> Mesas { get; set; }
    }
}
