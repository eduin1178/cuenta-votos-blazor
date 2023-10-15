using CuentaVotos.Entities.Puestos;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestosController : ControllerBase
    {
        private readonly IPuestoRepository _puestoRepository;

        public PuestosController(IPuestoRepository puestoRepository)
        {
            _puestoRepository = puestoRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            var res = _puestoRepository.List();
            return Ok(res);
        }

        [HttpGet("{puestoId}")]
        public IActionResult One(int puestoId)
        {
            var res = _puestoRepository.One(puestoId);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Create(PuestoCreate model)
        {
            var res = _puestoRepository.Create(model);
            return Ok(res);
        }

        [HttpPost("{puestoId}")]
        public IActionResult Update(int puestoId, Puesto model)
        {
            var res = _puestoRepository.Update(puestoId, model);
            return Ok(res);
        }

        [HttpDelete("{puestoId}")]
        public IActionResult Delete(int puestoId)
        {
            var res = _puestoRepository.Delete(puestoId);
            return Ok(res); 
        }

        [HttpPost("AgregarMesa/{puestoId}")]
        public IActionResult AddTable(int puestoId, Mesa model)
        {
            var res = _puestoRepository.AddTable(puestoId, model);
            return Ok(res);
        }
        [HttpDelete("QuitarMesa/{puestoId}/{mesaId}")]
        public IActionResult RemoveTable(int puestoId, int mesaId)
        {
            var res = _puestoRepository.RemoveTable(puestoId, mesaId);
            return Ok(res);
        }

        [HttpGet("Acta/{idMesa}")]
        public IActionResult UpdateUrlActa(int idMesa, string urlActa)
        {
            var res = _puestoRepository.UpdateUrlActa(idMesa, urlActa);
            return Ok(res);
        }

    }
}
