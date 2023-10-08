﻿using CuentaVotos.Entities.Procesos;
using CuentaVotos.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Repository
{
    public interface ICandidatosRepository
    {
        ModelResult<List<CandidatoModel>> Lista(int idCargo);        
        ModelResult<object> Crear(Candidato candidato);
        ModelResult<object> Actualizar(int idCandidato, Candidato candidato);
        ModelResult<object> Eliminar(int idCandidato);
    }
}