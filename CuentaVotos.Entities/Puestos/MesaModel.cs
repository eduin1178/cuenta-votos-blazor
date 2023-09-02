using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Puestos
{
    public class MesaModel
    {
        public int Id { get; set; }
        public String Code { get; set; }
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public string UserFullName => $"{UserFirstName} {UserLastName}";

        public int PuestoId { get; set; }
        public int PuestoNumber { get; set; }
        public string Puesto { get; set; } = string.Empty;

    }
}
