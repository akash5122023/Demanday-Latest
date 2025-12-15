
namespace AdvanceCRM.Template.Endpoints
{
    using AdvanceCRM.Settings;
    using AdvanceCRM.Template;
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Serenity.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    
    using MyRepository = Repositories.IntractTemplateRepository;
    using MyRow = IntractTemplateRow;
    using Serenity.Extensions.DependencyInjection;

    [Route("Services/Template/IntractTemplate/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class IntractTemplateController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public IntractTemplateController(ISqlConnections connections)
        {
            _connections = connections;
        }

        public IntractTemplateController() : this(Dependency.Resolve<ISqlConnections>())
        {
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context).List(connection, request);
        }
        
        [HttpPost]
        public StandardResponse Sync(IUnitOfWork uow)
        {
            var response = new StandardResponse();
            InteraktConfigRow Config;

            using (var connection = _connections.NewFor<InteraktConfigRow>())
            {
                var s = InteraktConfigRow.Fields;
                Config = connection.TryFirst<InteraktConfigRow>(q => q
                    .SelectTableFields()
                    .Select(s.SecretKey));
            }

            string apiUrl = "https://api.interakt.ai/v1/public/track/organization/templates?language=all";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            myHttpWebRequest.ContentType = "application/json";
            myHttpWebRequest.Headers.Add("authorization: Basic " + Config.SecretKey.Trim());
            myHttpWebRequest.Timeout = 15000;

            try
            {
                using (HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
                {
                    var reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                    string jsonResponse = reader.ReadToEnd();
                    //var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                    //var Templateobj = apiResponse.Templates;
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                    var Templateobj = apiResponse.Results.Templates; // Access the list of templates


                    if (Templateobj != null)
                    {
                        var repository = new MyRepository(Context);
                        var uniqueRecords = new HashSet<string>();
                        var sqlInsertList = new List<string>();

                        foreach (var TemplateObject in Templateobj)
                        {
                            //var templateName = TemplateObject.Name.ToString();
                            var templateName = TemplateObject.Id.ToString();
                            if (uniqueRecords.Contains(templateName) || repository.Exists(uow, templateName))
                            {
                                // Skip duplicates
                                continue;
                            }

                            uniqueRecords.Add(templateName);

                            var TemplateRecord = new IntractTemplateDetail
                            {
                                Id = TemplateObject.Id,
                                CreatedAtUtc = TemplateObject.CreatedAtUtc,
                                Name = TemplateObject.Name,
                                Language = TemplateObject.Language,
                                Category = TemplateObject.Category,
                                TemplateCategoryLabel = TemplateObject.TemplateCategoryLabel,
                                HeaderFormat = TemplateObject.HeaderFormat,
                                Header = TemplateObject.Header,
                                Body = TemplateObject.Body,
                                Footer = TemplateObject.Footer,
                                Buttons = TemplateObject.Buttons,
                                AutosubmittedFor = TemplateObject.AutosubmittedFor,
                                DisplayName = TemplateObject.DisplayName,
                                ApprovalStatus = TemplateObject.ApprovalStatus,
                                WaTemplateId = TemplateObject.WaTemplateId,
                                VariablePresent = TemplateObject.VariablePresent,
                                header_handle_file_url = TemplateObject.header_handle_file_url
                            };

                            sqlInsertList.Add($@"
                    INSERT INTO IntractTemplate ([IntractId], [created_at_utc], [name], [language], [category], [template_category_label], [header_format], [header], [body], [footer], [buttons], [autosubmitted_for], [display_name], [approval_status], [wa_template_id], [variable_present],[header_handle_file_url])
                    VALUES (
                        '{TemplateRecord.Id}',
                        '{TemplateRecord.CreatedAtUtc}',
                        '{TemplateRecord.Name.Replace("'", "''")}',
                        '{TemplateRecord.Language}',
                        '{TemplateRecord.Category}',
                        '{TemplateRecord.TemplateCategoryLabel}',
                        '{TemplateRecord.HeaderFormat}',
                        '{TemplateRecord.Header}',
                        '{TemplateRecord.Body}',
                        '{TemplateRecord.Footer}',
                        '{TemplateRecord.Buttons}',
                        '{TemplateRecord.AutosubmittedFor}',
                        '{TemplateRecord.DisplayName}',
                        '{TemplateRecord.ApprovalStatus}',
                        '{TemplateRecord.WaTemplateId}',
                        '{TemplateRecord.VariablePresent}',
                        '{TemplateRecord.header_handle_file_url}'
                                            );
                ");
                        }

                        if (sqlInsertList.Any())
                        {
                            using (var innerConnection = _connections.NewFor<IntractTemplateRow>())
                            {
                                foreach (var sql in sqlInsertList)
                                {
                                    try
                                    {
                                        innerConnection.Execute(sql);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Log the exception
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse errorResponse)
                {
                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                    {
                        response.Status = reader.ReadToEnd();
                    }
                }
                else
                {
                    response.Status = ex.Message;
                }
                return response;
            }

            response.Status = "Sync completed";
            return response;
        }

        public class ApiResponse
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("has_next")]
            public bool HasNext { get; set; }

            [JsonProperty("results")]
            public Results Results { get; set; }
        }

        public class Results
        {
            [JsonProperty("templates")]
            public List<IntractTemplateDetail> Templates { get; set; }
        }
        public class IntractTemplateDetail
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("created_at_utc")]
            public string CreatedAtUtc { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("approval_status")]
            public string ApprovalStatus { get; set; }
            [JsonProperty("category")]
            public string Category { get; set; }
            [JsonProperty("language")]
            public string Language { get; set; }
            [JsonProperty("wa_template_id")]
            public string WaTemplateId { get; set; }
            [JsonProperty("template_category_label")]
            public string TemplateCategoryLabel { get; set; }
            [JsonProperty("header_format")]
            public string HeaderFormat { get; set; }
            [JsonProperty("header")]
            public string Header { get; set; }
            [JsonProperty("body")]
            public string Body { get; set; }
            [JsonProperty("footer")]
            public string Footer { get; set; }
            [JsonProperty("buttons")]
            public string Buttons { get; set; }
            [JsonProperty("autosubmitted_for")]
            public string AutosubmittedFor { get; set; }
            [JsonProperty("display_name")]
            public string DisplayName { get; set; }
            [JsonProperty("variable_present")]
            public string VariablePresent { get; set; }
            [JsonProperty("header_handle_file_url")]
            public string header_handle_file_url { get; set; }


        }
    }
}
