using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AdvanceCRM.Web.Helpers
{
    public static class UploadHelper
    {
        private static string _uploadsRoot;
        private static readonly FileExtensionContentTypeProvider _mimeProvider = new FileExtensionContentTypeProvider();

        public static void Configure(IConfiguration configuration, IWebHostEnvironment env)
        {
            if (_uploadsRoot != null)
                return;

            var path = configuration["UploadSettings:Path"];
            if (string.IsNullOrEmpty(path))
            {
                path = Path.Combine(env.WebRootPath ?? env.ContentRootPath, "uploads");
            }
            else if (path.StartsWith("~"))
            {
                path = Path.Combine(env.ContentRootPath, path.TrimStart('~', '/').Replace('/', Path.DirectorySeparatorChar));
            }
            if (!Path.IsPathRooted(path))
                path = Path.Combine(env.ContentRootPath, path);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            _uploadsRoot = path;
        }

        public static void CheckFileNameSecurity(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (fileName.IndexOf("..", StringComparison.Ordinal) >= 0)
                throw new ArgumentOutOfRangeException(nameof(fileName));
        }

        public static string DbFilePath(string dbFileName)
        {
            CheckFileNameSecurity(dbFileName);
            if (_uploadsRoot == null)
                throw new InvalidOperationException("UploadHelper.Configure must be called before using DbFilePath");

            return Path.Combine(_uploadsRoot, dbFileName.Replace('/', Path.DirectorySeparatorChar));
        }

        public static string GetMimeType(string path)
        {
            if (!_mimeProvider.TryGetContentType(path, out var mime))
                mime = "application/octet-stream";
            return mime;
        }
    }
}
