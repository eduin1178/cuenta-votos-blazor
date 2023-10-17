using CuentaVotos.Entities;
using CuentaVotos.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Repository
{
    public interface IProcesoRespository
    {
        ModelResult<Proceso> GetProceso(int idProceso);
        ModelResult<object> GuardarProceso(Proceso model);
    }
}
