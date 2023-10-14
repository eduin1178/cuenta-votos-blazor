using System;
using System.Collections.Generic;
using System.Linq;
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

    public class ResultadoMesaModel
    {        
        public int IdPuesto { get; set; }
        public int IdMesa { get; set; }
        public int IdCargo { get; set; }
        public int IdPartido { get; set; }
        public string NombrePartido { get; set; }
        public string LogoPartido { get; set; }
        public int VotosPartido { get; set; }
        List<DetallesResultado> Detalles { get; set; } = new List<DetallesResultado>();
        public int VotosCandidato => Detalles.Sum(x => x.VotosCandidato);
        public bool Expandido { get; set; } = true;

    }
    public class ResultadoPuestoModel
    {        
        public int IdCargo { get; set; }
        public int IdPuesto { get; set; }
        public int IdPartido { get; set; }
        public string NombrePartido { get; set; }
        public string LogoPartido { get; set; }
        public int VotosPartido { get; set; }
        List<DetallesResultado> Detalles { get; set; } = new List<DetallesResultado>();
        public int VotosCandidato => Detalles.Sum(x => x.VotosCandidato);
        public bool Expandido { get; set; } = true;
    }

    public class ResultadoGeneralModel
    {
        public int IdCargo { get; set; }
        public int IdPartido { get; set; }
        public string NombrePartido { get; set; }
        public string LogoPartido { get; set; }
        public int VotosPartido { get; set; }
        List<DetallesResultado> Detalles { get; set; } = new List<DetallesResultado>();
        public int VotosCandidato => Detalles.Sum(x => x.VotosCandidato);
        public bool Expandido { get; set; } = true;
    }

    public class DetallesResultadoModel
    {
        public int IdCantidato { get; set; }
        public string NombreCandidato { get; set; }
        public int Numero { get; set; }
        public string FotoCandidatoUrl { get; set; }
        public int VotosCandidato { get; set; }
    }
}
