
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
    using System.Security.Claims;
    using Serenity.Abstractions;
    using MyRow =BmListRow;

    public class BmListRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;

        public BmListRepository(IRequestContext context) : base(context) { }
        public BmListRepository(IRequestContext context, ISqlConnections connections) : base(context)
        {
            _connections = connections;
        }

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

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
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
                 .Where(br.UserId == Context.User.FindFirstValue(ClaimTypes.NameIdentifier))
                );

                    var s = BizMailConfigRow.Fields;
                    Config = connection.TryFirst<BizMailConfigRow>(q => q
                        .SelectTableFields()
                        .Select(s.Apiurl)
                        .Select(s.Apikey)
                       // .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );
                }


                var client = new RestClient(Config.Apiurl + "/lists");
                var request = new RestRequest(string.Empty, Method.Post);
                //request.AddHeader("postman-token", "a62f832c-c9e2-47a7-7769-2938f3b900ae");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("x-mw-public-key", Config.Apikey);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
              //  request.AddParameter("application/x-www-form-urlencoded", "campaign%5Bname%5D=" + Request.Entity.Name + "campaign%5Btype%5D=" + Request.Entity.Name + "campaign%5Bfrom_name%5D=" + Request.Entity.Name + "&campaign%5Bfrom_email%5D=" + Request.Entity.Name + "&campaign%5BSubject%5D=" + Request.Entity.Name + "&campaign%5Breply_to%5D=" + Request.Entity.ReplyTo + "&campaign%5Blist_uid%5D=" + Request.Entity.From + "&campaign%5BSend_at%5D=" + Request.Entity.CompanyName + "&campaign%5Btemplate%5D%5Btemplate_uid%5D=" + Request.Entity.CompanyName+ "&campaign%5Btemplate%5D%5Binline_css%5D=" + Request.Entity.CompanyName+ "&campaign%5Btemplate%5D%5Bauto_plain_text%5D=" + Request.Entity.CompanyName, ParameterType.RequestBody);
                request.AddParameter("application/x-www-form-urlencoded", "general%5Bname%5D=" + Request.Entity.Name + "&general%5Bdescription%5D=" + Request.Entity.Name + "&defaults%5Bfrom_name%5D=" + Request.Entity.Name + "&defaults%5Breply_to%5D=" + Request.Entity.ReplyTo + "&defaults%5Bfrom_email%5D=" + Request.Entity.From + "&company%5Bname%5D=" + Request.Entity.CompanyName + "&company%5Bcountry_id%5D=1&company%5Baddress_1%5D=" + Request.Entity.City + "&company%5Bcity%5D=" + Request.Entity.City + "&company%5Bzip_code%5D=123456", ParameterType.RequestBody);
                RestResponse response = client.Execute(request);
                var res = response;


            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
         private class MyListHandler : ListRequestHandler<MyRow> { public MyListHandler(IRequestContext context) : base(context) { } }
    }
}