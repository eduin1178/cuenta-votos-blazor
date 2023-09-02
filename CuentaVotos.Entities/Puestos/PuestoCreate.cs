using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Puestos
{
    public class PuestoCreate
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public int TableCount { get; set; }
    }
}
