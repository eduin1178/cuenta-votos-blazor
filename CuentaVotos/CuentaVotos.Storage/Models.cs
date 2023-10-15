using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Storage
{
    public class UploadRequest
    {
        public StorageType Type { get; set; } = StorageType.Bunny;
        public string StorageZone { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public SourceType Source { get; set; } = SourceType.Stream;
        public Stream File { get; set; }
        public string LocalPath { get; set; }
    }

    public class UploadResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public string HostName { get; set; }
        public string Container { get; set; }
        public string Url => $"{HostName}{Path}";
        public Exception Exception { get; set; }
        public string CDNUrlBase { get; set; }
    }


    public class DownloadRequest
    {
        public StorageType Type { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }


    public class DownloadResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string HostName { get; set; }
        public Stream File { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public Exception Exception { get; set; }
    }

    public class DeleteRequest
    {
        public StorageType Type { get; set; }
        public string Container { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }

    public class DeleteResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public Exception Exception { get; set; }
    }

    public class FileListRequest
    {
        public StorageType Type { get; set; }
        public string Container { get; set; }
        public string Path { get; set; }
    }

    public class FileListResult
    {
        public bool IsSuccess { get; set; }
        public int DirectoryCount { get; set; }
        public int FileCount { get; set; }
        public long TotalSize { get; set; }
        public List<FileStoreModel> Files { get; set; }
    }
    public class FileStoreModel
    {
        public string Code { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Container { get; set; }
        public string Path { get; set; }
        public string ObjectName { get; set; }
        public bool Size { get; set; }
        public bool IsDirectory { get; set; }
        public string ServerId { get; set; }
        public string StorageZoneId { get; set; }
        public string FullPath { get; set; }
    }

    public enum StorageType
    {
        Local,
        Bunny
    }

    public enum SourceType
    {
        File,
        Stream
    }
}
