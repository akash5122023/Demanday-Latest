using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Serenity.Abstractions;
using Serenity.Services;
using Serenity.Web;
using Serenity;
using AdvanceCRM.Web.Helpers;

namespace AdvanceCRM.Common.Pages
{
    public class FileController : Controller
    {
        // These three get populated via DI
        protected IUploadStorage UploadStorage { get; }
        protected ITextLocalizer Localizer { get; }
        protected IExceptionLogger Logger { get; }

        public FileController(
            IUploadStorage uploadStorage,
            ITextLocalizer localizer,
            IExceptionLogger logger = null)    // optional for legacy compatibility
        {
            UploadStorage = uploadStorage
                ?? throw new ArgumentNullException(nameof(uploadStorage));
            Localizer = localizer
                ?? throw new ArgumentNullException(nameof(localizer));
            Logger = logger;
        }

        // GET upload/{*pathInfo}
        [HttpGet]
        [Route("upload/{**pathInfo}")]
        public IActionResult Read(string pathInfo)
        {
            if (string.IsNullOrEmpty(pathInfo))
                return NotFound();

            UploadHelper.CheckFileNameSecurity(pathInfo);
            var filePath = UploadHelper.DbFilePath(pathInfo);
            var mimeType = UploadHelper.GetMimeType(filePath);
            return PhysicalFile(filePath, mimeType);
        }

        // POST File/TemporaryUpload
        [HttpPost]
        [Route("File/TemporaryUpload")]
        public IActionResult TemporaryUpload()
        {
            // must be exactly one file
            if (Request.Form.Files.Count != 1)
                throw new ArgumentOutOfRangeException(nameof(Request.Form.Files));

            var file = Request.Form.Files[0];
            if (file == null || string.IsNullOrEmpty(file.FileName))
                throw new ArgumentNullException(nameof(file.FileName));

            // pass IUploadStorage and IExceptionLogger into UploadProcessor
            var processor = new UploadProcessor(UploadStorage, Logger)
            {
                ThumbWidth = 128,
                ThumbHeight = 96
            };

            using (var stream = file.OpenReadStream())
            {
                // note signature: ProcessStream(Stream, extension, ITextLocalizer)
                if (processor.ProcessStream(
                        stream,
                        Path.GetExtension(file.FileName),
                        Localizer))
                {
                    // TemporaryFile is set by ProcessStream()
                    var temporaryFile = processor.TemporaryFile;

                    // store original filename alongside the temp upload
                    var origPath = Path.ChangeExtension(
                        UploadHelper.DbFilePath(temporaryFile), ".orig");
                    System.IO.File.WriteAllText(origPath, file.FileName);

                    return Ok(new UploadResponse
                    {
                        TemporaryFile = temporaryFile,
                        Size = processor.FileSize,
                        IsImage = processor.IsImage,
                        Width = processor.ImageWidth,
                        Height = processor.ImageHeight
                    });
                }

                return BadRequest(new UploadResponse
                {
                    Error = new ServiceError
                    {
                        Code = "Exception",
                        Message = processor.ErrorMessage
                    }
                });
            }
        }

        private class UploadResponse : ServiceResponse
        {
            public string TemporaryFile { get; set; }
            public long Size { get; set; }
            public bool IsImage { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
    }
}
