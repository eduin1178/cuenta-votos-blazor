using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Resultados
{
    public class ResultadoModel
    {
        public int Id { get; set; }
        public Guid? Code { get; set; } = Guid.NewGuid();
        public int IdPuesto { get; set; }        
        public int IdMesa { get; set; }
        public int IdCargo { get; set; }
        public int IdPartido { get; set; }
        public string NombrePartido { get; set; }
        public string LogoPartido { get; set; }
        public string ColorPartido { get; set; }
        public int VotosPartido { get; set; }
        public List<DetallesResultadoModel> Detalles { get; set; } = new List<DetallesResultadoModel>();
        public int? VotosCandidato => Detalles.Sum(x=>x.VotosCandidato);
        public int? IdUsuarioRegistro { get; set; }
        public DateTime? Registrado { get; set; }
        public bool Expandido { get; set; } = true;
        public void ToggleExpandido()
        {
            Expandido = !Expandido;
        }   
    }


    public class ResultadoGeneralModel
    {
        public int IdCargo { get; set; }
        public int IdPartido { get; set; }
        public string NombrePartido { get; set; }
        public string LogoPartido { get; set; }
        public string ColorPartido { get; set; }
        public int VotosPartido { get; set; }
        public List<DetallesResultadoModel> Detalles { get; set; } = new List<DetallesResultadoModel>();
        public int VotosCandidato => Detalles.Sum(x => x.VotosCandidato);
        public bool Expandido { get; set; } = true;
        public void ToggleExpandido()
        {
            Expandido = !Expandido;
        }
    }

    public class DetallesResultadoModel
    {
        public int IdCantidato { get; set; }
        public string NombreCandidato { get; set; }
        public int? Numero { get; set; }
        public string FotoCandidatoUrl { get; set; }
        public int VotosCandidato { get; set; }
    }

    public class ReporteResultadosModel
    {
        public string Partido { get; set; }
        public string Candidato { get; set; }
        public string Puesto { get; set; }
        public string Mesa { get; set; }
        public int Votos { get; set; }
        public string Testigo { get; set; }
        public DateTime Registro { get; set; }
    }
}
