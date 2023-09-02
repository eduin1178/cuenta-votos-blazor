using CuentaVotos.Entities.Puestos;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly IMesaRepository _mesaRepository;

        public MesasController(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        //ModelResult<List<MesaModel>> List();
        //ModelResult<MesaModel> One(int mesaId);

        [HttpPost]
        public IActionResult Create(MesaCreate model)
        {
            var res = _mesaRepository.Create(model);
            return Ok(res);
        }

        [HttpPost("{mesaId}")]
        public IActionResult Update(int mesaId, Mesa model)
        {
            var res = _mesaRepository.Update(mesaId, model);
            return Ok(res);
        }

        [HttpDelete("{mesaId}")]
        public IActionResult Delete(int mesaId)
        {
            var res = _mesaRepository.Delete(mesaId);
            return Ok(res);
        }


    }
}
