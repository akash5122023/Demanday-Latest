
namespace AdvanceCRM.Common.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Modules.Common;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Template;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    

    [Route("Services/Common/[action]")]
    [ConnectionKey("Default"), ServiceAuthorize("?")]
    public class CommonController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _env;

        public CommonController(
            ISqlConnections connections,
            IWebHostEnvironment env)
        {
            _connections = connections;
            _env = env;
        }

        [HttpPost, Route("UploadImage")]
        public FileUploadResponse UploadImage(IUnitOfWork uow, FileUploadRequest request)
        {
            if (string.IsNullOrEmpty(request.ImagePath) || string.IsNullOrEmpty(request.FileName))
                throw new ArgumentNullException("Image or FileName");

            //var uniqueFileName = $"{Guid.NewGuid()}_{request.FileName}";
            var uniqueFileName = request.FileName;
            var uploadPath = Path.Combine(_env.ContentRootPath, "Common", "intractIMg");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, uniqueFileName);
            var fileBytes = Convert.FromBase64String(request.ImagePath);
            System.IO.File.WriteAllBytes(filePath, fileBytes);  // Correct method for writing bytes to a file

            StartCleanupTask(uploadPath);

            return new FileUploadResponse
            {
                FilePath = "/Common/intractIMg/" + uniqueFileName
            };
        }
        // Background cleanup method (deletes files older than 1 minute)
        public static void StartCleanupTask(string directoryPath)
        {
            Task.Run(() =>
            {
                try
                {
                    var now = DateTime.UtcNow;
                    var files = Directory.GetFiles(directoryPath);

                    foreach (var file in files)
                    {
                        var fileInfo = new FileInfo(file);
                        if (now - fileInfo.CreationTimeUtc > TimeSpan.FromMinutes(1))
                        {
                            fileInfo.Delete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception if needed
                    Console.WriteLine("Cleanup error: " + ex.Message);
                }
            });
        }
        //Send Mail
        [HttpPost, ServiceAuthorize("?")]
        public StandardResponse SendMail(IUnitOfWork uow, SendMailRequest request)
        {

            var response = new StandardResponse();
            var User = new UserRow();

            using (var connection = _connections.NewFor<UserRow>())
            {
                var u = UserRow.Fields;
                User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    .SelectTableFields()
                    .Select(u.Host)
                    .Select(u.Port)
                    .Select(u.SSL)
                    .Select(u.EmailId)
                    .Select(u.EmailPassword));
            }

            try
            {

                MailMessage mm = new MailMessage();
                var addr = new MailAddress(User.EmailId, User.DisplayName);

                mm.From = addr;
                mm.Sender = addr;
                mm.To.Add(request.EmailId);
                mm.Subject = request.Subject;
                var msg = request.Message;

                mm.Body = msg;
                mm.IsBodyHtml = true;

                response.Status = EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);

            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost, ServiceAuthorize("?")]
        public StandardResponse SendWati(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            String msg = request.SMSType;


            response.Status = SMSHelper.SendWati(request.Phone, msg);
            return response;
        }

        [HttpPost, ServiceAuthorize("?")]
        public StandardResponse SendIntractWa(IUnitOfWork uow, SendIntractSMSRequest request)
        {
            var response = new StandardResponse();

            if (string.IsNullOrEmpty(request.Phone))
                throw new ArgumentNullException("Phone number cannot be null or empty.");

            if (string.IsNullOrEmpty(request.Template))
                throw new ArgumentNullException("Message template cannot be null or empty.");

            if (string.IsNullOrEmpty(request.ImageUrl))
                throw new ArgumentNullException("Image URL cannot be null or empty.");

            if (request.Variable == null || !request.Variable.Any())
                throw new ArgumentNullException("Body values cannot be null or empty.");

            try
            {
                response.Status = SMSHelper.SendIntractWa(request.Phone, request.Template, request.Variable, request.ImageUrl);
            }
            catch (Exception ex)
            {
                response.Status = $"Failed to send message: {ex.Message}";
            }

            return response;
        }

        //Send SMS for SMS Sender
        [HttpPost, ServiceAuthorize("?")]
        public StandardResponse SendSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            String msg = request.SMSType;
            String temId = request.TemplateID;

            response.Status = SMSHelper.SendSMS(request.Phone, msg, temId);
            return response;
        }

        [HttpPost, ServiceAuthorize("?")]
        public StandardResponse SendBizMail(IUnitOfWork uow, SendMailRequest request)
        {

            var response = new StandardResponse();
            var User = new UserRow();

            using (var connection = _connections.NewFor<UserRow>())
            {
                var u = UserRow.Fields;
                User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    .SelectTableFields()
                    .Select(u.Host)
                    .Select(u.Port)
                    .Select(u.SSL)
                    .Select(u.EmailId)
                    .Select(u.EmailPassword));
            }

            try
            {

                MailMessage mm = new MailMessage();
                var addr = new MailAddress(User.EmailId, User.DisplayName);

                mm.From = addr;
                mm.Sender = addr;
                mm.To.Add(request.EmailId);
                mm.Subject = request.Subject;
                var msg = request.Message;

                mm.Body = msg;
                mm.IsBodyHtml = true;

                response.Status = EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);

            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }



        [HttpPost, Route("GetTemplateImage")]
        public GetTemplateImageResponse GetTemplateImage(IDbConnection connection, GetTemplateImageRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.TemplateName))
                throw new ArgumentNullException(nameof(request));

            var templateRow = connection.TryFirst<IntractTemplateRow>(q =>
                q.Select(IntractTemplateRow.Fields.header_handle_file_url)
                 .Where(IntractTemplateRow.Fields.Name == request.TemplateName));

            return new GetTemplateImageResponse
            {
                ImageUrl = templateRow?.header_handle_file_url ?? "" // Ensure there's no null value
            };
        }

        public class GetTemplateImageRequest : ServiceRequest
        {
            public string TemplateName { get; set; }
        }

        public class GetTemplateImageResponse : ServiceResponse
        {
            public string ImageUrl { get; set; }
        }
    }
}