using CuentaVotos.Entities.Resultados;
using CuentaVotos.Entities.Shared;

namespace CuentaVotos.Repository
{
    public interface IResultadosRepository
    {
        ModelResult<List<ResultadoModel>> ListaRegistro(int idPuesto, int idMesa, int idCargo);
        ModelResult<string> Guardar(string userCode, int idPuesto, int idMesa, int idCargo, List<ResultadoModel> resultados);

        ModelResult<List<ResultadoGeneralModel>> Resultados(int idCargo, int idPuesto, int idMesa);

        ModelResult<object> EliminarResultados(int idCargo, int idPuesto);
    }
}
