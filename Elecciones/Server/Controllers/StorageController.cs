using CuentaVotos.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace Elecciones.Server.Controllers
{
    /// <summary>
    /// Gestión de archivos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StorageController : ControllerBase
    {
        private readonly ICuentaVotosStorage _storage;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="storage"></param>
        public StorageController(ICuentaVotosStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Subir un archivo
        /// </summary>
        /// <param name="container"></param>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpPost("Upload/{container}")]
        public async Task<IActionResult> Upload([FromRoute] string container,
            [FromForm] IFormFile file,
            [FromQuery] string path = null)
        {
            UploadRequest request = new UploadRequest();
            request.Type = StorageType.Bunny;
            request.Source = SourceType.Stream;
            request.File = file.OpenReadStream();
            request.Path = path;
            request.StorageZone = container;
            request.FileName = file.FileName;
            var res = await _storage.Upload(request);
            return Ok(res);
        }

        /// <summary>
        /// Descargar un archivo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="nameToFileDownload"></param>
        /// <returns></returns>
        [HttpGet("Download")]
        public async Task<IActionResult> Download([FromQuery] string path, [FromQuery] string nameToFileDownload = null)
        {
            var res = await _storage.Download(new DownloadRequest
            {
                Type = StorageType.Bunny,
                Path = path
            });

            if (res.IsSuccess)
            {
                var filenName = Path.GetFileName(path);
                if (!string.IsNullOrEmpty(nameToFileDownload))
                {
                    filenName = nameToFileDownload;
                }
                return File(res.File, "application/octet-stream", filenName);
            }
            else
            {
                return Ok(res);
            }
        }

        /// <summary>
        /// Descargar un archivo publico
        /// </summary>
        /// <param name="path"></param>
        /// <param name="nameToFileDownload"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Preview")]
        public async Task<IActionResult> Preview([FromQuery] string path, [FromQuery] string nameToFileDownload = null)
        {
            var res = await _storage.Download(new DownloadRequest
            {
                Type = StorageType.Bunny,
                Path = path
            });

            if (res.IsSuccess)
            {
                var filenName = Path.GetFileName(path);
                if (!string.IsNullOrEmpty(nameToFileDownload))
                {
                    filenName = nameToFileDownload;
                }
                return File(res.File, "application/octet-stream", filenName);
            }
            else
            {
                return Ok(res);
            }
        }

    }
}
