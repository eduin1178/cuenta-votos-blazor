using CuentaVotos.Entities.Resultados;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadosController : ControllerBase
    {
        private readonly IResultadosRepository _resultadosRepository;

        public ResultadosController(IResultadosRepository resultadosRepository)
        {
            _resultadosRepository = resultadosRepository;
        }

        [HttpGet("{idPuesto}/{idMesa}/{idCargo}")]
        public IActionResult ListaRegistro(int idPuesto, int idMesa, int idCargo)
        {
            var res = _resultadosRepository.ListaRegistro(idPuesto, idMesa, idCargo);
            return Ok(res);
        }

        //ModelResult<string> Guardar(int idPuesto, int idMesa, int idCargo, List<ResultadoModel> resultados);
        //ModelResult<string> Confirmar(int idPuesto, int idMesa, int idCargo);

        //ModelResult<List<ResultadoMesaModel>> ResultadosMesa(int idCargo, int idPuesto, int idMesa);
        //ModelResult<List<ResultadoPuestoModel>> ResultadosPuesto(int idCargo, int idPuesto);
        //ModelResult<List<ResultadoGeneralModel>> ResultadosGenales(int idCargo);
    }
}
