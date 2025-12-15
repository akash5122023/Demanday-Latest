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

using System.Net;
using System.Data;
using Newtonsoft.Json;

namespace AdvanceCRM.Common
{
    public static class SMSHelper
    {
        public static string SendSMS(string phone, string msg,string templateid)
        {
            var SMSConfig = new SMSConfigurationRow();

            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<ContactsRow>())
            {
                var s = SMSConfigurationRow.Fields;
                SMSConfig = connection.TryById<SMSConfigurationRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.API)
                    .Select(s.Username)
                    .Select(s.Password)
                    .Select(s.SenderId)
                    .Select(s.Key)
                    .Select(s.SuccessResponse)
                    );
            }

            String apiStr = SMSConfig.API;
            String userid = SMSConfig.Username;
            String password = SMSConfig.Password;
            String senderid = SMSConfig.SenderId;
            String key = SMSConfig.Key == null ? "" : SMSConfig.Key;

            String uri = apiStr.Trim().Replace("<user>", userid.Trim()).Replace("<password>", password.Trim()).Replace("<sender>", senderid.Trim()).Replace("<key>", key.Trim()).Replace("<contacts>", phone).Replace("<msg>", msg.Trim()).Replace("<template_id>", templateid);

            //String uri = "http://bulksms.triocorporation.in/api/sendmsg.php?user=" + userid.Trim() + "&pass=" + password.Trim() + "&sender=" + senderid.Trim() + "&phone=" + phone + "&text=" + msg.Trim() + "&priority=ndnd&stype=normal";
            //String uri = "http://byebyesms.com/app/smsapi/index.php?username="+userid.Trim()+"&password="+password.Trim()+"&campaign=8056&routeid=7&type=text&contacts=" + phone + "&senderid=" + senderid.Trim() + "&msg=" + msg.Trim();
            //String uri = "http://www.technicalsms.com/vendorsms/pushsms.aspx?user=" + userid.Trim() + "&password=" + password.Trim() + "&msisdn=" + phone + "&sid=" + senderid.Trim() + "&msg=" + msg.Trim() + "&fl=0&gwid=2";
            
            try
            {
                // Create a new 'HttpWebRequest' Object to the mentioned URL.
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

                // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                myHttpWebRequest.Timeout = 15000;

                HttpWebResponse myHttpWebResponse;
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                var code = ((int)myHttpWebResponse.StatusCode);

                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                string response = reader.ReadToEnd();

                ////// 
                // ServicePointManager.Expect100Continue = true;
                // ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                // string json = (new WebClient()).DownloadString(uri);
                //string abc = JsonConvert.DeserializeObject<String>(json);


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

        public static string SendWati(string phone, string msg)
        {
            var Config = new WatiConfigRow();

            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<ContactsRow>())
            {
                var s = WatiConfigRow.Fields;
                Config = connection.TryById<WatiConfigRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.Url)
                    .Select(s.Token)
                   
                    );
            }
            string uri = Config.Url + "/api/v1/sendSessionMessage/"+phone+"?messageText="+msg; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";
            try
            {
                HttpWebResponse myHttpWebResponse;
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                //  myHttpWebRequest.Headers.Add("x-api-key: L3WEMukeez1uFPZhiEv6a6vDf2pu9pJPjtziFqN7");
                myHttpWebRequest.Headers.Add("authorization:Bearer " + Config.Token.ToString().Trim());
                myHttpWebRequest.ContentType = "application/json; charset=utf-8";
                myHttpWebRequest.Headers.Add("cache-control", "no-cache");
                myHttpWebRequest.Method = "POST";

                //  HttpWebResponse myHttpWebResponse;
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                string response = reader.ReadToEnd();

               // if (response.Contains(Config.SuccessResponse))
                    return "Whatapp sent successfully";
                //else
                //    throw new Exception("Invalid Response");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        //public static string SendIntractWa(string phone, string msg)
        public static string SendIntractWa(string phone, string Template, string variable,string imageUrl)
        {
            var Config = new InteraktConfigRow();

            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<ContactsRow>())
            {
                var s = InteraktConfigRow.Fields;
                Config = connection.TryById<InteraktConfigRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.SecretKey)
                    );
            }
            string uri = "https://api.interakt.ai/v1/public/message/";
            try
            {
                HttpWebResponse myHttpWebResponse;
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                myHttpWebRequest.Headers.Add("Authorization: Basic " + Config.SecretKey.ToString().Trim());
                myHttpWebRequest.ContentType = "application/json";
                myHttpWebRequest.Method = "POST";

                var jsonBody = $@"
                    {{
                    ""fullPhoneNumber"": ""{phone}"",
                    ""type"": ""Template"",
                    ""template"": {{
                    ""name"": ""{Template}"",
                    ""languageCode"": ""en"",
                    ""headerValues"": [ ""{imageUrl}"" ],
                    ""bodyValues"": [ ""{variable}"" ]
                    }}
                    }}";


                using (StreamWriter streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonBody);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                try
                {
                    using (HttpWebResponse httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
                    {
                        using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            string responseContent = streamReader.ReadToEnd();
                            Console.WriteLine("Response: " + responseContent);
                        }
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse errorResponse)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string errorDetails = reader.ReadToEnd();
                            Console.WriteLine($"Error Details: {errorDetails}");
                        }
                    }
                    Console.WriteLine($"WebException: {ex.Message}");
                }

                return "Whatapp sent successfully";
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public static string SendBulkSMS(List<string> phone, string msg)
        {
            var SMSConfig = new SMSConfigurationRow();

            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<ContactsRow>())
            {
                var s = SMSConfigurationRow.Fields;
                SMSConfig = connection.TryById<SMSConfigurationRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.BulkAPI)
                    .Select(s.Username)
                    .Select(s.Password)
                    .Select(s.SenderId)
                    .Select(s.Key)
                    .Select(s.SuccessResponse)
                    );
            }

            String apiStr = SMSConfig.BulkAPI;
            String userid = SMSConfig.Username;
            String password = SMSConfig.Password;
            String senderid = SMSConfig.SenderId;
            String key = SMSConfig.Key == null ? "" : SMSConfig.Key;

            String uri = apiStr.Trim().Replace("<user>", userid.Trim()).Replace("<password>", password.Trim()).Replace("<sender>", senderid.Trim()).Replace("<key>", key.Trim()).Replace("<contacts>", String.Join(",", phone)).Replace("<msg>", msg.Trim());

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

        public static string SendScheduleSMS(string phone, string msg, DateTime time, string templateid)
        {
            var SMSConfig = new SMSConfigurationRow();

            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<ContactsRow>())
            {
                var s = SMSConfigurationRow.Fields;
                SMSConfig = connection.TryById<SMSConfigurationRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.ScheduleAPI)
                    .Select(s.Username)
                    .Select(s.Password)
                    .Select(s.SenderId)
                    .Select(s.Key)
                    .Select(s.SuccessResponse)
                    );
            }

            String apiStr = SMSConfig.ScheduleAPI;
            String userid = SMSConfig.Username;
            String password = SMSConfig.Password;
            String senderid = SMSConfig.SenderId;
            String key = SMSConfig.Key == null ? "" : SMSConfig.Key;
            String datetime = time.ToString("yyyy-MM-dd HH:mm");

            String uri = apiStr.Trim().Replace("<user>", userid.Trim()).Replace("<password>", password.Trim()).Replace("<sender>", senderid.Trim()).Replace("<key>", key.Trim()).Replace("<contacts>", phone).Replace("<msg>", msg.Trim()).Replace("<time>", datetime).Replace("<template_id>", templateid); 

            //String uri = "http://bulksms.triocorporation.in/api/sendmsg.php?user=" + userid.Trim() + "&pass=" + password.Trim() + "&sender=" + senderid.Trim() + "&phone=" + phone + "&text=" + msg.Trim() + "&priority=ndnd&stype=normal";
            //String uri = "http://byebyesms.com/app/smsapi/index.php?username="+userid.Trim()+"&password="+password.Trim()+"&campaign=8056&routeid=7&type=text&contacts=" + phone + "&senderid=" + senderid.Trim() + "&msg=" + msg.Trim();
            //String uri = "http://www.technicalsms.com/vendorsms/pushsms.aspx?user=" + userid.Trim() + "&password=" + password.Trim() + "&msisdn=" + phone + "&sid=" + senderid.Trim() + "&msg=" + msg.Trim() + "&fl=0&gwid=2";

            try
            {
                //throw new Exception(uri); 
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
    }
}