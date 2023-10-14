using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Resultados
{
    public class Resultado
    {
        public int Id { get; set; }
        public Guid Code { get; set; } = Guid.NewGuid();
        public int IdPuesto { get; set; }
        public int IdMesa { get; set; }
        public int IdCargo { get; set; }
        public int IdPartido { get; set; }
        public int? VotosPartido { get; set; }
        public List<DetallesResultado> Detalles { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public DateTime Registrado { get; set; }
        public bool Confirmado { get; set; }
        public DateTime Confirmacion { get; set; }
    }
    public class DetallesResultado
    {
        public int IdCantidato { get; set; }
        public int VotosCandidato { get; set; }
    }
}
