using BunnyCDN.Net.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Storage
{
    public class BunnyStorage : ICuentaVotosStorage
    {
        private readonly BunnyStorageConfig _config;
        BunnyCDNStorage _bunny;
        public BunnyStorage(BunnyStorageConfig config)
        {
            _config = config;
            _bunny = new BunnyCDNStorage(config.StorageZone, config.Password, config.Region);

        }
        public async Task<UploadResult> Upload(UploadRequest request)
        {
            var res = new UploadResult();

            var path = $"/{request.StorageZone}/{request.FileName}";
            if (!string.IsNullOrEmpty(request.Path))
            {
                path = $"/{request.StorageZone}/{request.Path}/{request.FileName}";
            }

            try
            {
                switch (request.Source)
                {
                    case SourceType.File:
                        await Upload(request.LocalPath, path);
                        break;
                    case SourceType.Stream:
                        await Upload(request.File, path);
                        break;
                    default:
                        throw new Exception("No se ha especificado el tipo de origen del archivo. Propiedad Source del parametro UploadRequest");
                }

                res.IsSuccess = true;
                res.Path = $"{request.Path}/{request.FileName}";
                res.Container = request.StorageZone;
                res.HostName = _config.HostName;
                res.Size = request.File.Length;
                res.CDNUrlBase = _config.CDNUrlBase;
                
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Exception = ex;
            }

            return res;
        }

        private async Task Upload(string localPath, string path)
        {
            await _bunny.UploadAsync(localPath, path);
        }

        private async Task Upload(Stream content, string path)
        {
            await _bunny.UploadAsync(content, path);
        }

        public async Task<DownloadResult> Download(DownloadRequest request)
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri(Uri.EscapeDataString(_config.HostName));
            http.DefaultRequestHeaders.Add("AccessKey", _bunny.ApiAccessKey);
            try
            {
                var response = await http.GetAsync(request.Path);
                if (!response.IsSuccessStatusCode)
                {
                    return new DownloadResult
                    {
                        IsSuccess = false,
                        Message = $"No se ha encontrado el archivo {request.Path}",
                    };
                }

                var res = new DownloadResult
                {
                    IsSuccess = true,
                    File = await response.Content.ReadAsStreamAsync(),
                    Path = request.Path,
                    HostName = _config.HostName,
                };
                return res;
            }
            catch (Exception ex)
            {
                var res = new DownloadResult
                {
                    IsSuccess = false,
                    Message = "Error al descargar el archivo",
                    Exception = ex
                };
                return res;
            }
        }
        public Task<DeleteResult> Delete(DeleteRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<FileListResult> List(FileListRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
