﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Puestos
{
    public class Puesto
    {
        public int Id { get; set; }
        public string Code { get; set; } = Guid.NewGuid().ToString();
        public int Number { get; set; }
        public string Name { get; set; }
        
    }
}