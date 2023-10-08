using CuentaVotos.Entities.Account;
using CuentaVotos.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRespository _userRespository;

        public UsersController(IUserRespository userRespository)
        {
            _userRespository = userRespository;
        }

        [HttpGet]
        public IActionResult List(int? roleId = null, int? stateId = null)
        {
            var res = _userRespository.List(roleId, stateId);
            return Ok(res);
        }

        [HttpGet("ChangeState/{userId}/{newState}")]
        public IActionResult ChangeState(int userId, int newState)
        {
            var res = _userRespository.ChangeState(userId, newState);
            return Ok(res);
        }

        [HttpGet("ChangeRol/{userId}/{newRole}")]
        public IActionResult ChangeRol(int userId, int newRole)
        {
            var res = _userRespository.ChangeRol(userId, newRole);
            return Ok(res);
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            var res = _userRespository.Delete(userId);
            return Ok(res);
        }

        [HttpPost("RestorePassword")]
        public IActionResult RestorePassword(UserRestorePassword model)
        {
            var res = _userRespository.RestorePassword(model);
            return Ok(res);
        }

        [HttpGet("Puestos")]
        public IActionResult Puestos()
        {
            var userCode = User.Identity.Name;
            var res = _userRespository.Puestos(userCode);
            return  Ok(res);
        }
    }
}
