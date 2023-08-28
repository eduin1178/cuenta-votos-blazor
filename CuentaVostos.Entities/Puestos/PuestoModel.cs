using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVostos.Entities.Puestos
{
    public class PuestoModel
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Mesas
        {
            get
            {
                return ListaMesas.Count;
            }
        }
        public List<MesaModel> ListaMesas { get; set; } = new List<MesaModel>();
    }
}
