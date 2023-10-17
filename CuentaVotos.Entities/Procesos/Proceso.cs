using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities
{
    public class Proceso
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime Inicia { get; set; }
        public DateTime Termina { get; set; }
        public bool Abierto
        {
            get
            {
                return DateTime.Now >= Inicia && DateTime.Now <= Termina;
            }
        }

        public string Estado
        {
            get
            {
                if (Abierto)
                {
                    return "Abierto";
                }
                else
                {
                    return "Cerrado";
                }
            }
        }
    }
}
