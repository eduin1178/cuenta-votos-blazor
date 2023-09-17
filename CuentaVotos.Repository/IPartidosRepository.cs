using CuentaVotos.Entities.Procesos;
using CuentaVotos.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Repository
{
    public interface IPartidosRepository
    {
        ModelResult<List<Partido>> Lista();        
        ModelResult<object> Crear(Partido partido);
        ModelResult<object> Actualizar(int idPartido, Partido partido);
        ModelResult<object> Eliminar(int idPartido);
    }
}
