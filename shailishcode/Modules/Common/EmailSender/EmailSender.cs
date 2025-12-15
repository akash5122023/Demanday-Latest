using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using Serenity.Abstractions;
using Serenity.Data;
using System;
using System.IO;

namespace AdvanceCRM.Common
{
    public class EmailSender : IEmailSender
    {
        private IWebHostEnvironment host;
        private SmtpSettings smtp;
        private ISqlConnections connections;
        private IUserAccessor userAccessor;

        public EmailSender(IWebHostEnvironment host, IOptions<SmtpSettings> smtp,
            ISqlConnections connections, IUserAccessor userAccessor)
        {
            this.host = (host ?? throw new ArgumentNullException(nameof(host)));
            this.smtp = (smtp ?? throw new ArgumentNullException(nameof(smtp))).Value;
            this.connections = connections ?? throw new ArgumentNullException(nameof(connections));
            this.userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

        public void Send(MimeMessage message, bool skipQueue)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (message.From.Count == 0)
            {
                var fromAddress = !string.IsNullOrWhiteSpace(smtp.From) ? smtp.From :
                    (!string.IsNullOrWhiteSpace(smtp.Username) ? smtp.Username : null);

                if (!string.IsNullOrEmpty(fromAddress))
                    message.From.Add(MailboxAddress.Parse(fromAddress));
            }

            if (!string.IsNullOrEmpty(smtp.Host))
            {
                using var client = new SmtpClient();
                var port = smtp.Port > 0 ? smtp.Port : 25;
                client.Connect(smtp.Host, port, smtp.UseSsl);
                if (!string.IsNullOrWhiteSpace(smtp.Username))
                    client.Authenticate(smtp.Username, smtp.Password ?? string.Empty);
                client.Send(message);
                client.Disconnect(true);
            }
            else
            {
                var pickupPath = string.IsNullOrEmpty(smtp.PickupPath) ?
                    Path.Combine(host.ContentRootPath, "App_Data", "Mail") :
                    Path.Combine(host.ContentRootPath, smtp.PickupPath);
                if (!Directory.Exists(pickupPath))
                    Directory.CreateDirectory(pickupPath);
                message.WriteTo(Path.Combine(pickupPath, DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".eml"));
            }
        }
    }
}