using CuentaVostos.Entities.Puestos;
using CuentaVotos.Entiies.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Repository
{
    public interface IPuestoRepository
    {
        ModelResult<List<PuestoModel>> List();
        ModelResult<PuestoModel> One(int puestoId);
        ResultBase Create(int number, string name);
        ResultBase Update(int puestoId, int number, string name);
        ResultBase Delete(int puestoId);
    }
}
