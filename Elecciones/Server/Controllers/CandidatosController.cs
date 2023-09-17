using CuentaVotos.Entities.Procesos;
using CuentaVotos.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatosController : ControllerBase
    {
        private readonly ICandidatosRepository _candidatosRepository;

        public CandidatosController(ICandidatosRepository candidatosRepository)
        {
            _candidatosRepository = candidatosRepository;
        }
        [HttpGet("ListaPorCargo/{idCargo}")]
        public IActionResult Lista(int idCargo, int? idPartido = null)
        {
            var res = _candidatosRepository.Lista(idCargo, idPartido);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Crear(Candidato candidato)
        {
            var res = _candidatosRepository.Crear(candidato);
            return Ok(res);
        }

        [HttpPost("{idCandidato}")]
        public IActionResult Actualizar(int idCandidato, Candidato candidato)
        {
            var res = _candidatosRepository.Actualizar(idCandidato, candidato);
            return Ok(res);
        }

        [HttpDelete("{idCandidato}")]
        public IActionResult Eliminar(int idCandidato)
        {
            var res = _candidatosRepository.Eliminar(idCandidato);
            return Ok(res);
        }
    }
}
