using AdvanceCRM.Contacts;
using Serenity.Data;
using System;
using System.Data;
using System.Net.Mail;
using System.Linq;
using AdvanceCRM.Settings;
using RestSharp;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using Serenity;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Common
{
    public static class MailHelper
    {
        public static string SendBulkMail(string custname,string to,string Subject, string Body, string attach)
        {
           
            var MailConfig = new BulkMailConfigRow();

            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<ContactsRow>())
            {
                var s = BulkMailConfigRow.Fields;
                MailConfig = connection.TryById<BulkMailConfigRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.Host)
                    .Select(s.Port)
                    .Select(s.Ssl)
                    .Select(s.EmailId)
                    .Select(s.EmailPassword)
                   
                    );
            }

            //String apiStr = MailConfig.API;
            //String userid = MailConfig.Username;
            //String password = MailConfig.Password;
            //String senderid = MailConfig.SenderId;
            //String key = MailConfig.Key == null ? "" : MailConfig.Key;

            //String uri = apiStr.Trim().Replace("<user>", userid.Trim()).Replace("<password>", password.Trim()).Replace("<sender>", senderid.Trim()).Replace("<key>", key.Trim()).Replace("<contacts>", phone).Replace("<msg>", msg.Trim());

            ////String uri = "http://bulkMail.triocorporation.in/api/sendmsg.php?user=" + userid.Trim() + "&pass=" + password.Trim() + "&sender=" + senderid.Trim() + "&phone=" + phone + "&text=" + msg.Trim() + "&priority=ndnd&stype=normal";
            ////String uri = "http://byebyeMail.com/app/Mailapi/index.php?username="+userid.Trim()+"&password="+password.Trim()+"&campaign=8056&routeid=7&type=text&contacts=" + phone + "&senderid=" + senderid.Trim() + "&msg=" + msg.Trim();
            ////String uri = "http://www.technicalMail.com/vendorMail/pushMail.aspx?user=" + userid.Trim() + "&password=" + password.Trim() + "&msisdn=" + phone + "&sid=" + senderid.Trim() + "&msg=" + msg.Trim() + "&fl=0&gwid=2";

            try
            {
                if (MailConfig.Host != null)
                {
                    MailMessage mm = new MailMessage();
                    var addr = new MailAddress(MailConfig.EmailId, MailConfig.EmailId);

                    mm.From = addr;
                    mm.Sender = addr;
                    mm.To.Add(to);
                    mm.Subject =Subject;
                    var msg = Body;
                    msg = msg.Replace("#username", Serenity.Authorization.UserDefinition?.DisplayName);
                    // msg = msg.Replace("#usermobile", Context.User.ToUserDefinition().Phone);
                    msg = msg.Replace("#customername", custname);
                    mm.Body = msg;

                    //if (attach != null)
                    //{
                    //    JArray att = JArray.Parse(attach);
                    //    foreach (var f in att)
                    //    {
                    //        if (f["Filename"].HasValue())
                    //        {
                    //            mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //        }
                    //    }
                    //}
                    mm.IsBodyHtml = true;

                    EmailHelper.Send(mm, MailConfig.EmailId, MailConfig.EmailPassword, (Boolean)MailConfig.Ssl, MailConfig.Host, MailConfig.Port.Value);
                   

                }
                return "Mail sent successfully";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public static string SendBulkMail(string custname, string to, string Subject, string Body,DateTime dt)
        {
            var MailConfig = new BizMailConfigRow();

            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<ContactsRow>())
            {
                var s = BizMailConfigRow.Fields;
                MailConfig = connection.TryById<BizMailConfigRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.ReplyToName)
                    .Select(s.ReplyToMail)
                    .Select(s.FromMail)
                    .Select(s.FromName)
                    .Select(s.Apikey)
                    .Select(s.Apiurl)
                    );
            }

            String apikey = MailConfig.Apikey;
            String apiurl = MailConfig.Apiurl;
            String ReplyToName = MailConfig.ReplyToName;
            String ReplyTomail = MailConfig.ReplyToMail;
            String FromMail = MailConfig.FromMail;
            String Fromname = MailConfig.FromName;
            dt = dt.AddHours(-5);
            string date = dt.ToString("yyyy-MM-dd HH:mm:ss");
           
            try
            {
                var client = new RestClient(apiurl + "/transactional-emails");
                var request = new RestRequest(string.Empty, Method.Post);
                
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("x-mw-public-key", apikey);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(Body));

                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Cookie", "PHPSESSID=an08teg84veb3vh6tbaf2ue1rp");
                request.AddParameter("email[to_name]", custname);
                request.AddParameter("email[to_email]", to);
                request.AddParameter("email[from_name]", Fromname);
                request.AddParameter("email[from_email]", FromMail);
                request.AddParameter("email[reply_to_name]", ReplyToName);
                request.AddParameter("email[reply_to_email]", ReplyTomail);
                request.AddParameter("email[subject]", Subject);
                request.AddParameter("email[body]", encodedStr);
                request.AddParameter("email[send_at]", date);
                RestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                //request.AddParameter("application/x-www-form-urlencoded", "email%5Bto_name%5D=" +to +"&email%5Bto_email%5D=" + custname + "&email%5Bfrom_name%5D=" + Fromname + "&email%5Bfrom_email%5D=" + FromMail + "&email%5Breply_to_name%5D=" + ReplyToName + "&email%5Breply_to_email%5D=" + ReplyTomail + "&email%5Bsubject%5D=" + Subject + "&email%5Bbody%5D=" + encodedStr + "&email%5Bsend_at%5D="+dt, ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);
                //var res = response.StatusCode();
                //var test = request.ToString();//


                //  string response = reader.ReadToEnd();
                //if (response.Contains(MailConfig.SuccessResponse))
                // "Mail sent successfully";
                //if (response.Content[0] != "error")
                //{
                   return "Mail sent successfully";
                //    //Callback(response.Data);
                //}
                //else
                //    throw new Exception(response.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        //    public static string SendScheduleMail(string phone, string msg, DateTime time)
        //    {
        //        var MailConfig = new MailConfigurationRow();

        //        using (var connection = _connections.NewFor<ContactsRow>())
        //        {
        //            var s = MailConfigurationRow.Fields;
        //            MailConfig = connection.TryById<MailConfigurationRow>(1, q => q
        //                .SelectTableFields()
        //                .Select(s.ScheduleAPI)
        //                .Select(s.Username)
        //                .Select(s.Password)
        //                .Select(s.SenderId)
        //                .Select(s.Key)
        //                .Select(s.SuccessResponse)
        //                );
        //        }

        //        String apiStr = MailConfig.ScheduleAPI;
        //        String userid = MailConfig.Username;
        //        String password = MailConfig.Password;
        //        String senderid = MailConfig.SenderId;
        //        String key = MailConfig.Key == null ? "" : MailConfig.Key;
        //        String datetime = time.ToString("yyyy-MM-dd HH:mm");

        //        String uri = apiStr.Trim().Replace("<user>", userid.Trim()).Replace("<password>", password.Trim()).Replace("<sender>", senderid.Trim()).Replace("<key>", key.Trim()).Replace("<contacts>", phone).Replace("<msg>", msg.Trim()).Replace("<time>", datetime);

        //        //String uri = "http://bulkMail.triocorporation.in/api/sendmsg.php?user=" + userid.Trim() + "&pass=" + password.Trim() + "&sender=" + senderid.Trim() + "&phone=" + phone + "&text=" + msg.Trim() + "&priority=ndnd&stype=normal";
        //        //String uri = "http://byebyeMail.com/app/Mailapi/index.php?username="+userid.Trim()+"&password="+password.Trim()+"&campaign=8056&routeid=7&type=text&contacts=" + phone + "&senderid=" + senderid.Trim() + "&msg=" + msg.Trim();
        //        //String uri = "http://www.technicalMail.com/vendorMail/pushMail.aspx?user=" + userid.Trim() + "&password=" + password.Trim() + "&msisdn=" + phone + "&sid=" + senderid.Trim() + "&msg=" + msg.Trim() + "&fl=0&gwid=2";

        //        try
        //        {
        //            // Create a new 'HttpWebRequest' Object to the mentioned URL.
        //            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

        //            // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
        //            myHttpWebRequest.Timeout = 15000;

        //            HttpWebResponse myHttpWebResponse;
        //            myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

        //            StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());

        //            string response = reader.ReadToEnd();
        //            if (response.Contains(MailConfig.SuccessResponse))
        //                return "Mail sent successfully";
        //            else
        //                throw new Exception("Invalid Response");
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message.ToString());
        //        }
        //    }
    }
}
