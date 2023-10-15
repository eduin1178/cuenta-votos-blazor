using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Storage
{
    public interface ICuentaVotosStorage
    {
        Task<FileListResult> List(FileListRequest request);
        Task<UploadResult> Upload(UploadRequest request);
        Task<DownloadResult> Download(DownloadRequest request);
        Task<DeleteResult> Delete(DeleteRequest request);
    }
}
