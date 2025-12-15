using System.Net.Mail;
using Serenity.Services;
using System.Net;
using System;
using AdvanceCRM.Administration;
using Serenity.Data;
using System.Collections.Generic;
using System.Linq;

namespace AdvanceCRM.Common
{
    public class BizEmailHelper : ServiceRequest
    {
        public static string SendBizMail(MailMessage message, string email, string password, bool SSL, string host, int port)
        {
            string response;

            try
            {
                NetworkCredential nc = new NetworkCredential(email, password);
                using (var client = new SmtpClient())
                {
                    client.Credentials = nc;
                    client.EnableSsl = SSL;
                    client.Host = host;
                    client.Port = port;
                    client.Timeout = 10000;
                    client.Send(message);
                    client.Dispose();
                    message.Dispose();
                }

                response = "Mail sent successfully!!!";
            }
            catch (Exception ex)
            {
                response = "Error\n\n" + ex.Message.ToString();
            }
            return response;
        }

      
    }
}