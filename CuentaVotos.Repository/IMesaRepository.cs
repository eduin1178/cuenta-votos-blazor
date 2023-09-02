using CuentaVotos.Entities.Puestos;
using CuentaVotos.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Repository
{
    public interface IMesaRepository
    {
        ModelResult<List<MesaModel>> List();
        ModelResult<MesaModel> One(int mesaId);

        ModelResult<object> Create(MesaCreate model);
        ModelResult<object> Update(int mesaId, Mesa model);
        ModelResult<object> Delete(int mesaId);

    }
}
