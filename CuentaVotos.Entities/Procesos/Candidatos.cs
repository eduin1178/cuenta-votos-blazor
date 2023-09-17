using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Procesos
{
    public class Candidato
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Nombre { get; set; }
        public string FotoUrl { get; set; }
        public int PartidoId { get; set; }
        public int CargoId { get; set; }
    }

}
