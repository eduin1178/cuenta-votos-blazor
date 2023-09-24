using CuentaVotos.Core.Shared;
using CuentaVotos.Entities.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Elecciones.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IHostEnvironment _env;

        public FilesController(IHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> PostFile(IFormFile file)
        {

            long maxFileSize = 1024 * 1024 * 15;
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/Uploads");

            var uploadResult = new UploadResult();


            if (file.Length == 0)
            {
                return Ok(new ModelResult<UploadResult>
                {
                    IsSuccess = true,
                    Message = "OK",
                    Model = uploadResult,
                });

            }
            else if (file.Length > maxFileSize)
            {
                return Ok(new ModelResult<UploadResult>
                {
                    IsSuccess = false,
                    Message = "Error al subir el archivo",
                    Model = uploadResult,
                });
            }
            else
            {
                var path = Path.Combine(_env.ContentRootPath, "Uploads");
                var fullPath = Path.Combine(path, file.FileName);
                try
                {

                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception)
                    {

                    }

                    await using FileStream fs = new(fullPath, FileMode.Create);
                    await file.CopyToAsync(fs);
                    uploadResult.Uploaded = true;
                    uploadResult.StoredFileName = resourcePath + "/" + file.FileName;
                }
                catch (IOException)
                {
                    return Ok(new ModelResult<UploadResult>
                    {
                        IsSuccess = false,
                        Message = "Error al subir el archivo",
                        Model = uploadResult,
                    });
                }
            }




            return Ok(new ModelResult<UploadResult>
            {
                IsSuccess = true,
                Message = "OK",
                Model = uploadResult,
            });

        }
    }
}
