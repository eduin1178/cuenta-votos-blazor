using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Procesos
{
    public class CandidatoModel : Candidato
    {
        public string Partido { get; set; }
        public string LogoPartido { get; set; }
        public string ColorPartido { get; set; }
        public string Cargo { get; set; }
    }
}
