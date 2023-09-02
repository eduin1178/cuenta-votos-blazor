using CuentaVostos.Entities.Puestos;
using CuentaVotos.Entities.Shared;
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
        ModelResult<string> Create(int number, string name);
        ModelResult<string> Update(int puestoId, int number, string name);
        ModelResult<string> Delete(int puestoId);
    }
}
