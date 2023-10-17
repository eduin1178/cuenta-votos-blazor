using CuentaVotos.Entities.Shared;
using CuentaVotos.Entities;
using CuentaVotos.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProcesosController : ControllerBase
    {
        private readonly IProcesoRespository _procesoRespository;

        public ProcesosController(IProcesoRespository procesoRespository)
        {
            _procesoRespository = procesoRespository;
        }

        [HttpGet("{idProceso}")]
        public  IActionResult GetProceso(int idProceso)
        {
            var res = _procesoRespository.GetProceso(idProceso);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult GuardarProceso(Proceso model)
        {
            var res = _procesoRespository.GuardarProceso(model);

            return Ok(res);
        }
    }
}
