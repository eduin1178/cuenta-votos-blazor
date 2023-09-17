using CuentaVotos.Entities.Procesos;
using CuentaVotos.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Repository
{
    public interface ICargosRepository
    {
        ModelResult<List<Cargo>> Lista();
        ModelResult<object> Crear(Cargo model);
        ModelResult<object> Actualizar(int idCargo, Cargo model);
        ModelResult<object> Eliminar(int idCargo);
    }
}
