using System;
using System.IO;
using Serenity.Abstractions;
using Serenity.Web;

namespace AdvanceCRM.Common.LargeFileUpload
{
    public static class UploadStorageExtensions
    {
        public static void CopyFile(this IUploadStorage storage, string sourcePath, string destinationPath, bool? autoRename = false)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));
            if (sourcePath == null)
                throw new ArgumentNullException(nameof(sourcePath));
            if (destinationPath == null)
                throw new ArgumentNullException(nameof(destinationPath));

            if (!storage.FileExists(sourcePath))
                throw new FileNotFoundException($"Source file not found: {sourcePath}", sourcePath);

            using var input = storage.OpenFile(sourcePath);
            storage.WriteFile(destinationPath, input, autoRename);
        }
    }
}
