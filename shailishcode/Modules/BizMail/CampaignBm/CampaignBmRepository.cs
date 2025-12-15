
namespace AdvanceCRM.BizMail.Repositories
{
    using System;
    using System.Linq;
    using System.Net;
    //using System.Web.Script.Serialization;
    using Newtonsoft.Json;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System.Data;
    using System.Collections.Generic;
    using System.IO;
    
    using AdvanceCRM.BizMail;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Settings;
    using RestSharp;
    using System.Text;
    using System.Net.Http;
    using MyRow =CampaignBmRow;

    public class CampaignBmRepository : BaseRepository
    {
        protected ISqlConnections _connections { get; }

        public CampaignBmRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
        }

        public CampaignBmRepository(IRequestContext context) : base(context) { }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
             return new MySaveHandler(Context, _connections).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context, _connections).Process(uow, request, SaveRequestType.Update);
        }

        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyDeleteHandler(Context).Process(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler(Context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
             return new MyListHandler(Context).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow> {
            private readonly ISqlConnections _connections;
            public MySaveHandler(IRequestContext context, ISqlConnections connections) : base(context)
            {
                _connections = connections;
            }

            protected override void BeforeSave()
            {
                base.BeforeSave();

                var br = UserRow.Fields;
                var UData = new UserRow();

                BizMailConfigRow Config;

                using (var connection = _connections.NewFor<BizMailConfigRow>())
                {

                    UData = connection.First<UserRow>(q => q
                 .SelectTableFields()
                 .Select(br.CompanyId)
                 .Where(br.UserId == Context.User.GetIdentifier())
                );

                    var s = BizMailConfigRow.Fields;
                    Config = connection.TryFirst<BizMailConfigRow>(q => q
                        .SelectTableFields()
                        .Select(s.Apiurl)
                        .Select(s.Apikey)
                        // .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );
                }


                var client = new RestClient(Config.Apiurl + "/campaigns");
                var request = new RestRequest(string.Empty, Method.Post);
                //request.AddHeader("postman-token", "a62f832c-c9e2-47a7-7769-2938f3b900ae");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("x-mw-public-key", Config.Apikey);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(Request.Entity.Name));
                request.AddParameter("application/x-www-form-urlencoded", "campaign%5Bname%5D=" + Request.Entity.Name + "&campaign%5Btype%5D=autoresponder&campaign%5Bfrom_name%5D=" + Request.Entity.FromName + "&campaign%5Bfrom_email%5D=" + Request.Entity.FromEmail + "&campaign%5Bsubject%5D=" + Request.Entity.Subject + "&campaign%5Breply_to%5D=" + Request.Entity.ReplyTo + "&campaign%5Blist_uid%5D=" + Request.Entity.ListId + "&campaign%5BSend_at%5D=2022-07-18 10-12-22&campaign%5Btemplate%5D%5Bcontent%5D=" + encodedStr, ParameterType.RequestBody);

               // request.AddParameter("application/x-www-form-urlencoded", "campaign%5Bname%5D%3AAmit%0Acampaign%5Btype%5D%3Aautoresponder%0Acampaign%5Bfrom_name%5D%3Afname%0Acampaign%5Bfrom_email%5D%3Aamitk1116%40gmail.com%0Acampaign%5Bsubject%5D%3ASubject%0Acampaign%5Breply_to%5D%3Aamitk1116%40gmail.com%0Acampaign%5Blist_uid%5D%3Aot8811zhweca3%0Acampaign%5BSend_at%5D%3A2022-07-18%2010-12-22%0Acampaign%5Btemplate%5D%5Bcontent%5D%3AGood%20Morning", ParameterType.RequestBody);
                RestResponse response = client.Execute(request);
                var res = request;
                var res1 = response;

                //if (response==null)
                //{
                //    throw new Exception("Kindly add atleast one product for this Enquiry and then try saving");
                //}
                //else
                //{
                
                //throw new Exception(response.ToString());

                //}



            }

        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
         private class MyListHandler : ListRequestHandler<MyRow> { public MyListHandler(IRequestContext context) : base(context) { } }
    }
}