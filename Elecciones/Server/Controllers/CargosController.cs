using CuentaVotos.Entities.Procesos;
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
    public class CargosController : ControllerBase
    {
        private readonly ICargosRepository _cargosRepository;

        public CargosController(ICargosRepository cargosRepository)
        {
            _cargosRepository = cargosRepository;
        }

        [HttpGet]
        public IActionResult Lista()
        {
            var res = _cargosRepository.Lista();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Crear(Cargo model)
        {
            var res = _cargosRepository.Crear(model);
            return Ok(res);
        }

        [HttpPost("{idCargo}")]
        public IActionResult Actualizar(int idCargo, Cargo model)
        {
            var res = _cargosRepository.Actualizar(idCargo, model);
            return Ok(res);
        }

        [HttpDelete("{idCargo}")]
        public IActionResult Eliminar(int idCargo)
        {
            var res = _cargosRepository.Eliminar(idCargo);
            return Ok(res);
        }
    }
}
