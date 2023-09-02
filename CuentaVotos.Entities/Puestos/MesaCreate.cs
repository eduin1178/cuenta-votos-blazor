using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Puestos
{
    public class MesaCreate
    {
        public int PuestoId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; } = null!;
        public int? UserId { get; set; }
    }
}
