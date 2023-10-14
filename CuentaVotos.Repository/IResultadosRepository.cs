using CuentaVotos.Entities.Resultados;
using CuentaVotos.Entities.Shared;

namespace CuentaVotos.Repository
{
    public interface IResultadosRepository
    {
        ModelResult<List<ResultadoModel>> ListaRegistro(int idPuesto, int idMesa, int idCargo);
        ModelResult<string> Guardar(string userCode, int idPuesto, int idMesa, int idCargo, List<ResultadoModel> resultados);

        ModelResult<List<ResultadoMesaModel>> ResultadosMesa(int idCargo, int idPuesto, int idMesa);
        ModelResult<List<ResultadoPuestoModel>> ResultadosPuesto(int idCargo, int idPuesto);
        ModelResult<List<ResultadoGeneralModel>> ResultadosGenales(int idCargo);
    }
}
