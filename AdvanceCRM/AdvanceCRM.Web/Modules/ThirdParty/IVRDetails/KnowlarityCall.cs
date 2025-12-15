using AdvanceCRM.Settings;
using Serenity;
using Serenity.Data;
using Serenity.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
//using System.Web.Script.Serialization;

namespace AdvanceCRM.Modules.ThirdParty.IVRDetails
{
    public class KnowlarityCall
    {
        public static string ClickToCall(String knowlarity_no, Int32 agent_id, String customer_no )
        {
            try
            {
                dynamic status=string.Empty;
                IVRConfigurationRow IVRConfig;
                String agent_no;string cli_no;
                using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<IVRConfigurationRow>())
                {
                    var s = IVRConfigurationRow.Fields;
                    IVRConfig = connection.TryById<IVRConfigurationRow>(1, q => q
                        .SelectTableFields()
                        .Select(s.IVRNumber)
                        .Select(s.ApiKey)
                        .Select(s.Plan)
                        .Select(s.IVRType)
                        .Select(s.AppId)
                        .Select(s.AppSecret) 
                        .Select(s.Username)
                        .Select(s.Password)
                        .Select(s.CliNumber)
                        );

                    var a = KnowlarityAgentsRow.Fields;
                    var AgentDetails = connection.TryById<KnowlarityAgentsRow>(agent_id, q => q
                    .SelectTableFields()
                    .Select(a.Number));
                    agent_no = AgentDetails.Number;
                }

                if (IVRConfig.IVRType == Masters.IVRTypeMaster.Knowlarity)
                {

                    customer_no = "+91" + customer_no;
                    cli_no = "+91" + IVRConfig.CliNumber;

                    string apiURL = ("https://kpi.knowlarity.com/" + IVRConfig.Plan + "/v1/account/call/makecall");


                    //string apiURL = "https://kpi.knowlarity.com/Basic/v1/account/calllog?start_time=2016-01-01%2012%3A00%3A00%2B05%3A30&end_time=2017-07-17%2011%3A54%3A00%2B05%3A30&knowlarity_number=%2B919021023456&limit=200";

                    //String uri = apiURL + "?limit=10";

                    // Create a new 'HttpWebRequest' Object to the mentioned URL.
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                    //myHttpWebRequest.Headers.Add("channel", data.IVRConfig.Plan.ToString().Trim());
                    myHttpWebRequest.Headers.Add("x-api-key: L3WEMukeez1uFPZhiEv6a6vDf2pu9pJPjtziFqN7");
                    myHttpWebRequest.Headers.Add("authorization: " + IVRConfig.ApiKey.ToString().Trim());
                    myHttpWebRequest.ContentType = "application/json";
                    myHttpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                    {
                        //string json = "{\"k_number\":\"" + knowlarity_no.Trim() + "\"," +
                        //              "\"agent_number\":\"" + agent_no.Trim() + "\"," +
                        //              "\"customer_number\":\"" + customer_no.Trim() + "\"}";
                        if (IVRConfig.CliNumber != "")
                        {
                            string json = "{\"k_number\":\"" + knowlarity_no.Trim() + "\"," +
                                         "\"agent_number\":\"" + agent_no.Trim() + "\"," +
                                         "\"customer_number\":\"" + customer_no.Trim() + "\"," +
                                         "\"caller_id\":\"" + cli_no.Trim() + "\"}";
                            streamWriter.Write(json);
                        }
                        else
                        {
                            string json = "{\"k_number\":\"" + knowlarity_no.Trim() + "\"," +
                                          "\"agent_number\":\"" + agent_no.Trim() + "\"," +
                                          "\"customer_number\":\"" + customer_no.Trim() + "\"}";
                            streamWriter.Write(json);
                        }

                        //streamWriter.Write(json);
                    }
                    // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                    myHttpWebRequest.Timeout = 15000;

                    HttpWebResponse myHttpWebResponse;

                    myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                    var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                    string jsonResponse = reader.ReadToEnd();
                    using var doc = JsonDocument.Parse(jsonResponse);
                    if (doc.RootElement.TryGetProperty("success", out var prop))
                        status = prop.GetString();
                }
                else if(IVRConfig.IVRType==Masters.IVRTypeMaster.TeleCMI)
                {
                    customer_no = "91" + customer_no;

                    string apiURL = ("https://rest.telecmi.com/v2/webrtc/click2call");

                   
                    // Create a new 'HttpWebRequest' Object to the mentioned URL.
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                    //myHttpWebRequest.Headers.Add("channel", data.IVRConfig.Plan.ToString().Trim());
                    //myHttpWebRequest.Headers.Add("x-api-key: L3WEMukeez1uFPZhiEv6a6vDf2pu9pJPjtziFqN7");
                    //myHttpWebRequest.Headers.Add("authorization: " + IVRConfig.ApiKey.ToString().Trim());
                    myHttpWebRequest.ContentType = "application/json";
                    myHttpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                    {
                        string json = "{\"user_id\":\"" + agent_no.Trim() + "\"," +
                                       "\"secret\":\"" + IVRConfig.AppSecret.Trim() + "\"," +
                                      "\"to\":"+customer_no.Trim()+"," +                                      
                                      "\"webrtc\":false," +
                                       "\"followme\":true}";


                        streamWriter.Write(json);
                    }
                    // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                    myHttpWebRequest.Timeout = 15000;

                    HttpWebResponse myHttpWebResponse;

                    myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                    var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                    string jsonResponse = reader.ReadToEnd();
                    using var doc = JsonDocument.Parse(jsonResponse);
                    if (doc.RootElement.TryGetProperty("msg", out var prop))
                        status = prop.GetString();
                }
                else if (IVRConfig.IVRType == Masters.IVRTypeMaster.Cloud_Connect)
                {
                    customer_no = "91" + customer_no;

                    string apiURL = "https://crm2.cloud-connect.in/ccpl_api/v1.4/api/info/click2call";

                    // Create a new 'HttpWebRequest' object to the mentioned URL
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);

                    myHttpWebRequest.ContentType = "application/json";
                    myHttpWebRequest.Method = "POST";

                    // Construct the JSON payload
                    using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                    {
                        string json = "{"
                            + "\"token_id\":\"" + IVRConfig.Token_Id + "\","
                            + "\"phone_number\":\"" + customer_no.Trim() + "\","
                            + "\"extension_number\":\"" + agent_no.Trim() + "\","
                            + "\"extension_password\":\"9563e764830aa78d25341608a9705070\""
                            + "}";

                        streamWriter.Write(json);
                    }

                    // Set the 'Timeout' property of the HttpWebRequest to 15 seconds
                    myHttpWebRequest.Timeout = 15000;

                    try
                    {
                        // Get the response
                        using (HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
                        {
                            using (var reader = new StreamReader(myHttpWebResponse.GetResponseStream()))
                            {
                                string jsonResponse = reader.ReadToEnd();
                                using var doc = JsonDocument.Parse(jsonResponse);
                                var obj = doc.RootElement;
                                status = obj.GetProperty("msg").GetString(); // Extract the "msg" field from the response
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        if (ex.Response != null)
                        {
                            using (var errorResponse = (HttpWebResponse)ex.Response)
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                string error = reader.ReadToEnd();
                                Console.WriteLine("Error response: " + error);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Request failed: " + ex.Message);
                        }
                    }

                }


                else if (IVRConfig.IVRType == Masters.IVRTypeMaster.way2voice)
                {
                    customer_no = "91" + customer_no;

                    string apiURL = ("https://rest.telecmi.com/v2/webrtc/click2call");



                    
                    //HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                    ////myHttpWebRequest.Headers.Add("channel", data.IVRConfig.Plan.ToString().Trim());
                    ////myHttpWebRequest.Headers.Add("x-api-key: L3WEMukeez1uFPZhiEv6a6vDf2pu9pJPjtziFqN7");
                    ////myHttpWebRequest.Headers.Add("authorization: " + IVRConfig.ApiKey.ToString().Trim());
                    //myHttpWebRequest.ContentType = "application/json";
                    //myHttpWebRequest.Method = "POST";

                    //using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                    //{
                    //    string json = "{\"user_id\":\"" + agent_no.Trim() + "\"," +
                    //                   "\"secret\":\"" + IVRConfig.AppSecret.Trim() + "\"," +
                    //                  "\"to\":" + customer_no.Trim() + "," +
                    //                  "\"webrtc\":false," +
                    //                   "\"followme\":true}";


                    //    streamWriter.Write(json);
                    //}
                    //// Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                    //myHttpWebRequest.Timeout = 15000;

                    //HttpWebResponse myHttpWebResponse;

                    //myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                    //var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //var obj = js.Deserialize<dynamic>(reader.ReadToEnd());

                    status ="Its in process";
                }
                return status;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public static string CLIClickToCall(String knowlarity_no, Int32 agent_id, String customer_no)
        {
            try
            {
                dynamic status = string.Empty;
                IVRConfigurationRow IVRConfig;
                String agent_no;
                using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<IVRConfigurationRow>())
                {
                    var s = IVRConfigurationRow.Fields;
                    IVRConfig = connection.TryById<IVRConfigurationRow>(1, q => q
                        .SelectTableFields()
                        .Select(s.IVRNumber)
                        .Select(s.ApiKey)
                        .Select(s.Plan)
                        .Select(s.IVRType)
                        .Select(s.AppId)
                        .Select(s.AppSecret)
                        .Select(s.Username)
                        .Select(s.Password)
                        );

                    var a = KnowlarityAgentsRow.Fields;
                    var AgentDetails = connection.TryById<KnowlarityAgentsRow>(agent_id, q => q
                    .SelectTableFields()
                    .Select(a.Number));
                    agent_no = AgentDetails.Number;
                }

                if (IVRConfig.IVRType == Masters.IVRTypeMaster.Knowlarity)
                {

                    customer_no = "+91" + customer_no;

                    string apiURL = ("https://kpi.knowlarity.com/" + IVRConfig.Plan + "/v1/account/call/makecall");


                    //string apiURL = "https://kpi.knowlarity.com/Basic/v1/account/calllog?start_time=2016-01-01%2012%3A00%3A00%2B05%3A30&end_time=2017-07-17%2011%3A54%3A00%2B05%3A30&knowlarity_number=%2B919021023456&limit=200";

                    //String uri = apiURL + "?limit=10";

                    // Create a new 'HttpWebRequest' Object to the mentioned URL.
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                    //myHttpWebRequest.Headers.Add("channel", data.IVRConfig.Plan.ToString().Trim());
                    myHttpWebRequest.Headers.Add("x-api-key: L3WEMukeez1uFPZhiEv6a6vDf2pu9pJPjtziFqN7");
                    myHttpWebRequest.Headers.Add("authorization: " + IVRConfig.ApiKey.ToString().Trim());
                    myHttpWebRequest.ContentType = "application/json";
                    myHttpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                    {
                        string json = "{\"k_number\":\"" + knowlarity_no.Trim() + "\"," +
                                      "\"agent_number\":\"" + agent_no.Trim() + "\"," +
                                      "\"customer_number\":\"" + customer_no.Trim() + "\"," +
                                      "\"caller_id\":\"+912241948183\"}";

                        streamWriter.Write(json);
                    }
                    // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                    myHttpWebRequest.Timeout = 15000;

                    HttpWebResponse myHttpWebResponse;

                    myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                    var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                    string jsonResponse = reader.ReadToEnd();
                    using var doc = JsonDocument.Parse(jsonResponse);
                    var obj = doc.RootElement;

                    status = obj.GetProperty("success").GetString();
                }
                else if (IVRConfig.IVRType == Masters.IVRTypeMaster.TeleCMI)
                {
                    customer_no = "91" + customer_no;

                    string apiURL = ("https://rest.telecmi.com/v2/webrtc/click2call");


                    // Create a new 'HttpWebRequest' Object to the mentioned URL.
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                   
                    myHttpWebRequest.ContentType = "application/json";
                    myHttpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                    {
                        string json = "{\"user_id\":\"" + agent_no.Trim() + "\"," +
                                       "\"secret\":\"" + IVRConfig.AppSecret.Trim() + "\"," +
                                      "\"to\":" + customer_no.Trim() + "," +
                                      "\"webrtc\":false," +
                                       "\"followme\":true}";


                        streamWriter.Write(json);
                    }
                    // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                    myHttpWebRequest.Timeout = 15000;

                    HttpWebResponse myHttpWebResponse;

                    myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                    var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                    string jsonResponse = reader.ReadToEnd();
                    using var doc = JsonDocument.Parse(jsonResponse);
                    var obj = doc.RootElement;

                    status = obj.GetProperty("msg").GetString();
                }
                else if (IVRConfig.IVRType == Masters.IVRTypeMaster.way2voice)
                {
                    customer_no = "91" + customer_no;

                    string apiURL = ("https://rest.telecmi.com/v2/webrtc/click2call");
                    status = "Its in process";
                }
                return status;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}