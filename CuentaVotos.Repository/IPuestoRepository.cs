using CuentaVotos.Entities.Puestos;
using CuentaVotos.Entities.Shared;

namespace CuentaVotos.Repository
{
    public interface IPuestoRepository
    {
        ModelResult<List<PuestoModel>> List();
        ModelResult<PuestoModel> One(int puestoId);
        ModelResult<string> Create(PuestoCreate model);
        ModelResult<string> Update(int puestoId, Puesto model);
        ModelResult<string> Delete(int puestoId);

        ModelResult<PuestoModel> AddTable(int puestoId, Mesa model);
        ModelResult<PuestoModel> RemoveTable(int puestoId, int mesaId);
    }
}
