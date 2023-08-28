using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVostos.Entities.Puestos
{
    public class Mesa
    {
        public int Id { get; set; }
        public Guid Code { get; set; } = Guid.NewGuid();
        public int Number { get; set; }
        public string Name { get; set; }
        public int PuestoId { get; set; }
        public int? UserId { get; set; }
    }
}
