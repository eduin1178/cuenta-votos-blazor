using CuentaVotos.Entities.Resultados;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpPost("{idPuesto}/{idMesa}/{idCargo}")]
        public IActionResult Guardar(int idPuesto, int idMesa, int idCargo, List<ResultadoModel> resultados)
        {
            var userCode = User.Identity.Name;
            var res = _resultadosRepository.Guardar(userCode, idPuesto, idMesa, idCargo, resultados);
            return Ok(res);
        }
        //ModelResult<string> Confirmar(int idPuesto, int idMesa, int idCargo);

        //ModelResult<List<ResultadoMesaModel>> ResultadosMesa(int idCargo, int idPuesto, int idMesa);
        //ModelResult<List<ResultadoPuestoModel>> ResultadosPuesto(int idCargo, int idPuesto);
        [HttpGet("Consolidado/{idCargo}/{idPuesto}/{idMesa}")]
        public IActionResult Resultados(int idCargo, int idPuesto, int idMesa)
        {
            var res = _resultadosRepository.Resultados(idCargo, idPuesto, idMesa);
            return Ok(res);
        }
    }
}
