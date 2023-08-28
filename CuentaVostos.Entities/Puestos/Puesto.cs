using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVostos.Entities.Puestos
{
    public class Puesto
    {
        public int Id { get; set; }
        public Guid Code { get; set; } = Guid.NewGuid();
        public int Number { get; set; }
        public string Name { get; set; }
        
    }
}
