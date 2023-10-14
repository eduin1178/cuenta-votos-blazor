using CuentaVotos.Entities.Resultados;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using Elecciones.Server.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResultadosController : ControllerBase
    {
        private readonly IResultadosRepository _resultadosRepository;
        private readonly IHubContext<NotifyResultHub> _notifyResultHub;

        public ResultadosController(IResultadosRepository resultadosRepository, IHubContext<NotifyResultHub> notifyResultHub)
        {
            _resultadosRepository = resultadosRepository;
            _notifyResultHub = notifyResultHub;
        }

        [HttpGet("{idPuesto}/{idMesa}/{idCargo}")]
        public IActionResult ListaRegistro(int idPuesto, int idMesa, int idCargo)
        {
            var res = _resultadosRepository.ListaRegistro(idPuesto, idMesa, idCargo);
            return Ok(res);
        }

        [HttpPost("{idPuesto}/{idMesa}/{idCargo}")]
        public async Task<IActionResult> Guardar(int idPuesto, int idMesa, int idCargo, List<ResultadoModel> resultados)
        {
            var userCode = User.Identity.Name;
            var res = _resultadosRepository.Guardar(userCode, idPuesto, idMesa, idCargo, resultados);
            if (res.IsSuccess)
            {
               await _notifyResultHub.Clients.All.SendAsync("NotifyResult", userCode, idCargo, idPuesto);
            }
            return Ok(res);
        }

        [HttpGet("Consolidado/{idCargo}/{idPuesto}/{idMesa}")]
        public IActionResult Resultados(int idCargo, int idPuesto, int idMesa)
        {
            var res = _resultadosRepository.Resultados(idCargo, idPuesto, idMesa);
            return Ok(res);
        }
    }
}
