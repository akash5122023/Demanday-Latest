using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;

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

            _uploadsRoot = ResolveUploadRoot(configuration?["UploadSettings:Path"], env);
        }

        public static string ResolveUploadRoot(string configuredPath, IWebHostEnvironment env)
        {
            if (env == null)
                throw new ArgumentNullException(nameof(env));

            var candidates = new List<string>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            void AddCandidate(string candidate)
            {
                if (!string.IsNullOrEmpty(candidate) && seen.Add(candidate))
                    candidates.Add(candidate);
            }

            AddCandidate(ResolvePath(configuredPath, env, allowDefaultFallback: false));
            AddCandidate(ResolvePath(null, env, allowDefaultFallback: true));
            AddCandidate(Path.Combine(Path.GetTempPath(), "AdvanceCRM", "uploads"));

            Exception lastError = null;
            foreach (var candidate in candidates)
            {
                string testFile = null;
                try
                {
                    Directory.CreateDirectory(candidate);
                    testFile = Path.Combine(candidate,
                        $".write-test-{Guid.NewGuid():N}.tmp");

                    using (var stream = new FileStream(testFile,
                        FileMode.Create, FileAccess.Write, FileShare.Delete))
                    {
                        stream.WriteByte(0);
                    }

                    TryDeleteFile(testFile);
                    return candidate;
                }
                catch (Exception ex) when (IsRecoverablePathException(ex))
                {
                    TryDeleteFile(testFile);
                    lastError = ex;
                }
            }

            if (lastError != null)
                throw new InvalidOperationException("Unable to initialize upload directory.", lastError);

            throw new InvalidOperationException("Unable to initialize upload directory.");
        }

        private static bool IsRecoverablePathException(Exception ex)
        {
            return ex is UnauthorizedAccessException ||
                ex is IOException ||
                ex is SecurityException ||
                ex is ArgumentException ||
                ex is NotSupportedException ||
                ex is PathTooLongException;
        }

        private static void TryDeleteFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
            }
        }

        private static string ResolvePath(string path, IWebHostEnvironment env, bool allowDefaultFallback)
        {
            if (string.IsNullOrEmpty(path))
            {
                if (!allowDefaultFallback)
                    return null;

                path = Path.Combine(env.WebRootPath ?? env.ContentRootPath, "uploads");
            }
            else if (path.StartsWith("~"))
            {
                path = Path.Combine(env.ContentRootPath, path.TrimStart('~', '/').Replace('/', Path.DirectorySeparatorChar));
            }

            if (!Path.IsPathRooted(path))
                path = Path.Combine(env.ContentRootPath, path);

            return Path.GetFullPath(path);
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
