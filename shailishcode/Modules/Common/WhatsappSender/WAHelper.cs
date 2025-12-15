using AdvanceCRM.Contacts;

using AdvanceCRM.Settings;
using MailChimp.Net.Models;
using Serenity.Data;
using Serenity;
using Serenity.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Net;

namespace AdvanceCRM.Common
{
    public static class WAHelper
    {
        public static string SendWA(string phone, string msg)
        {
            var SMSConfig = new WaConfigrationRow();

            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<ContactsRow>())
            {
                var s = WaConfigrationRow.Fields;
                SMSConfig = connection.TryById<WaConfigrationRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.MessageApi)
                    .Select(s.ApiKey)
                    .Select(s.Mobile)
                    //.Select(s.SenderId)
                    //.Select(s.Key)
                    .Select(s.SuccessResponse)
                    );
            }

            String apiStr = SMSConfig.MessageApi;
            String apikey = SMSConfig.ApiKey;
            String mobile = SMSConfig.Mobile;
           // String senderid = SMSConfig.SenderId;
           // String key = SMSConfig.Key == null ? "" : SMSConfig.Key;

            String uri = apiStr.Trim().Replace("<apikey>", apikey.Trim()).Replace("<mobile>", mobile.Trim()).Replace("<contacts>", phone).Replace("<msg>", msg.Trim());

            ////String uri = "http://bulksms.triocorporation.in/api/sendmsg.php?user=" + userid.Trim() + "&pass=" + password.Trim() + "&sender=" + senderid.Trim() + "&phone=" + phone + "&text=" + msg.Trim() + "&priority=ndnd&stype=normal";
            ////String uri = "http://byebyesms.com/app/smsapi/index.php?username="+userid.Trim()+"&password="+password.Trim()+"&campaign=8056&routeid=7&type=text&contacts=" + phone + "&senderid=" + senderid.Trim() + "&msg=" + msg.Trim();
            ////String uri = "http://www.technicalsms.com/vendorsms/pushsms.aspx?user=" + userid.Trim() + "&password=" + password.Trim() + "&msisdn=" + phone + "&sid=" + senderid.Trim() + "&msg=" + msg.Trim() + "&fl=0&gwid=2";

            //string fromMob = "919xxxxxxxxx";
            //string toMob = txtMobileNo.Text.Trim();
            //string msg = "This is first WhatsApp Message Whatsapp API";

            //WhatsApp wa = new WhatsApp(fromMob, "RequiredPassword", "SD", false, false);

            //try
            //{
            //    wa.OnConnectSuccess += () =>
            //    {
            //        wa.OnLoginSuccess += (phoneNumber, data) =>
            //        {
            //            wa.SendMessage(toMob, msg);

            //        };

            //        wa.OnLoginFailed += (data) =>
            //        {
            //            msg = "Login Failed" + data;
            //        };
            //    };

            //    wa.OnConnectFailed += (ex) =>
            //    {
            //        msg = "Connection Failed" + ex;
            //    };
            //    wa.Connect();
            //}
            //catch { }
            try
            {
                // Create a new 'HttpWebRequest' Object to the mentioned URL.
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

                // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                myHttpWebRequest.Timeout = 15000;

                HttpWebResponse myHttpWebResponse;
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                string response = reader.ReadToEnd();
                if (response.Contains(SMSConfig.SuccessResponse))
                    return "SMS sent successfully";
                else
                    throw new Exception("Invalid Response");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public static string SendBizWA(string phone, string tempid)
        {
            var SMSConfig = new BizWaConfigRow();

            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<ContactsRow>())
            {
                //var s = SMSConfigurationRow.Fields;
                //SMSConfig = connection.TryById<SMSConfigurationRow>(1, q => q
                //    .SelectTableFields()
                //    .Select(s.BulkAPI)
                //    .Select(s.Username)
                //    .Select(s.Password)
                //    .Select(s.SenderId)
                //    .Select(s.Key)
                //    .Select(s.SuccessResponse)
                //    );

                var cn = BizWaConfigRow.Fields;
                SMSConfig = connection.TryById<BizWaConfigRow>(1, q => q
                     .SelectTableFields()
                     .Select(cn.Id)
                     .Select(cn.WhatsAppNo)
                     .Select(cn.PhoneNoId)
                      .Select(cn.Wbaid)
                       .Select(cn.Accesstoken)
                     );
            }

            String whatsappno = SMSConfig.WhatsAppNo;
            String phoneid = SMSConfig.PhoneNoId;
            String wbid = SMSConfig.Wbaid;
            String accesstoken = SMSConfig.Accesstoken;
           
            try
            {
                string uri = "https://graph.facebook.com/v15.0/"+SMSConfig.PhoneNoId+"/messages"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";

                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                //  var request = new HttpRequestMessage(HttpMethod.Post, "https://api.interakt.ai/v1/public/apis/users/?offset=1&limit=100");
                myHttpWebRequest.Headers.Add("Authorization", "Bearer " + SMSConfig.Accesstoken.ToString().Trim());// EAAIhUMhNpPcBANaFsKlUL4AXKOfBC3yRxJ110PBCBY0qcQrq1hfw1QM2ACEuqpx6O3UV6fnZCZCumWCjM62ZBX7TdI6lEp12VLr1zmBzoFVkyeGLYj7PwAcwLYeF1EibZCdmJdKZCWb2KA072Cq4dSCigaZAETk7bd36QDM5wko9TZBDDIs7ZBtV");
                myHttpWebRequest.ContentType = "application/json";
                myHttpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    string json = "{\n    \"messaging_product\": \"whatsapp\",\n    \"to\": \"" + phone + "\",\n    \"type\": \"template\",\n    \"template\": {\n        \"name\": \"" + tempid + "\",\n        \"language\": {\n            \"code\": \"en_US\"\n        }\n    }\n}";
                    streamWriter.Write(json);
                }

                // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                myHttpWebRequest.Timeout = 15000;

                HttpWebResponse myHttpWebResponse;
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                //string response = reader.ReadToEnd();
                if (myHttpWebResponse.StatusCode.ToString().Contains("OK"))
                    return "Whatsapp sent successfully";
                else
                    throw new Exception("Invalid Response");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        //public static string SendScheduleSMS(string phone, string msg, DateTime time)
        //{
        //    var SMSConfig = new SMSConfigurationRow();

        //    using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<ContactsRow>())
        //    {
        //        var s = SMSConfigurationRow.Fields;
        //        SMSConfig = connection.TryById<SMSConfigurationRow>(1, q => q
        //            .SelectTableFields()
        //            .Select(s.ScheduleAPI)
        //            .Select(s.Username)
        //            .Select(s.Password)
        //            .Select(s.SenderId)
        //            .Select(s.Key)
        //            .Select(s.SuccessResponse)
        //            );
        //    }

        //    String apiStr = SMSConfig.ScheduleAPI;
        //    String userid = SMSConfig.Username;
        //    String password = SMSConfig.Password;
        //    String senderid = SMSConfig.SenderId;
        //    String key = SMSConfig.Key == null ? "" : SMSConfig.Key;
        //    String datetime = time.ToString("yyyy-MM-dd HH:mm");

        //    String uri = apiStr.Trim().Replace("<user>", userid.Trim()).Replace("<password>", password.Trim()).Replace("<sender>", senderid.Trim()).Replace("<key>", key.Trim()).Replace("<contacts>", phone).Replace("<msg>", msg.Trim()).Replace("<time>", datetime);

        //    //String uri = "http://bulksms.triocorporation.in/api/sendmsg.php?user=" + userid.Trim() + "&pass=" + password.Trim() + "&sender=" + senderid.Trim() + "&phone=" + phone + "&text=" + msg.Trim() + "&priority=ndnd&stype=normal";
        //    //String uri = "http://byebyesms.com/app/smsapi/index.php?username="+userid.Trim()+"&password="+password.Trim()+"&campaign=8056&routeid=7&type=text&contacts=" + phone + "&senderid=" + senderid.Trim() + "&msg=" + msg.Trim();
        //    //String uri = "http://www.technicalsms.com/vendorsms/pushsms.aspx?user=" + userid.Trim() + "&password=" + password.Trim() + "&msisdn=" + phone + "&sid=" + senderid.Trim() + "&msg=" + msg.Trim() + "&fl=0&gwid=2";

        //    try
        //    {
        //        // Create a new 'HttpWebRequest' Object to the mentioned URL.
        //        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

        //        // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
        //        myHttpWebRequest.Timeout = 15000;

        //        HttpWebResponse myHttpWebResponse;
        //        myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

        //        StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());

        //        string response = reader.ReadToEnd();
        //        if (response.Contains(SMSConfig.SuccessResponse))
        //            return "SMS sent successfully";
        //        else
        //            throw new Exception("Invalid Response");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //}
    }
}