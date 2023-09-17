using CuentaVotos.Entities.Procesos;
using CuentaVotos.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PartidosController : ControllerBase
    {
        private readonly IPartidosRepository _partidosRepository;

        public PartidosController(IPartidosRepository partidosRepository)
        {
            _partidosRepository = partidosRepository;
        }
        [HttpGet]
        public IActionResult Lista()
        {
            var res = _partidosRepository.Lista();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Crear(Partido partido)
        {
            var res = _partidosRepository.Crear(partido);
            return Ok(res);
        }

        [HttpPost("{idPartido}")]
        public IActionResult Actualizar(int idPartido, Partido partido)
        {
            var res = _partidosRepository.Actualizar(idPartido, partido);
            return Ok(res);
        }

        [HttpDelete("{idPartido}")]
        public IActionResult Eliminar(int idPartido)
        {
            var res = _partidosRepository.Eliminar(idPartido);
            return Ok(res);
        }
    }
}
