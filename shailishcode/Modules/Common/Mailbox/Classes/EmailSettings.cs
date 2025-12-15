using AdvanceCRM.Administration;
using Serenity.Data;
using Serenity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Common.Mailbox.Classes
{
    public class EmailSettings
    {
        public EmailSettings()
        {
            Set_MailCredntials();
        }
        public string IMAPHost { get; set; }
        public int IMAPPort { get; set; }
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public void Set_MailCredntials()
        {
            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<UserRow>())
            {
                var s = UserRow.Fields;
                // Scheduler jobs might not have a request context so default to user id 1
                var Config = connection.TryById<UserRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.MCSMTPServer)
                    .Select(s.MCSMTPPort)
                    .Select(s.MCIMAPServer)
                    .Select(s.MCIMAPPort)
                    .Select(s.MCUsername)
                    .Select(s.MCPassword)
                    );

                this.IMAPHost = Config.MCIMAPServer;
                this.IMAPPort = Config.MCIMAPPort != null ? Config.MCIMAPPort.Value : 993;
                this.SMTPHost = Config.MCSMTPServer;
                this.SMTPPort = Config.MCSMTPPort != null ? Config.MCSMTPPort.Value : 465;
                this.UserName = Config.MCUsername;
                this.Password = Config.MCPassword;
            }
        }
    }


}