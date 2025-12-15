
namespace AdvanceCRM.ThirdParty.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Quotation.Endpoints;
    using AdvanceCRM.Masters;
    using RestSharp;
    using AdvanceCRM.BizMail;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Contacts;
    using AdvanceCRM; // for ClaimsPrincipal extensions
    using Newtonsoft.Json.Linq;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Net.Mail;
    using System.Web;
    using MyRow = SIndiaMartDetailsRow;

    public class SIndiaMartDetailsRepository : BaseRepository
    {
        protected ISqlConnections _connections { get; }

        public SIndiaMartDetailsRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
        }

        public SIndiaMartDetailsRepository(IRequestContext context) : base(context)
        {
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

            public MySaveHandler(IRequestContext context, ISqlConnections connections)
                : base(context)
            {
                _connections = connections;
            }

            protected override void BeforeSave()
            {
                base.BeforeSave();

                var connection = _connections.NewByKey("Default");

                var Company = new CompanyDetailsRow();

                //var ct = ContactsRow.Fields;
                //var Contact = connection.TryById<ContactsRow>(Request.Entity.ContactsId, q => q
                //   .SelectTableFields()
                //   .Select(ct.Name)
                //   .Select(ct.Email));

                /////MailWizz/// 
                var model = new MailModel();
                BizMailConfigRow Config;

                var menq = BizMailIdiaMartRow.Fields;

                var br = UserRow.Fields;
                var UData = new UserRow();

                using (var connection1 = _connections.NewFor<BizMailConfigRow>())
                {
                    UData = connection1.First<UserRow>(q => q
                   .SelectTableFields()
                   .Select(br.CompanyId)
                   .Where(br.UserId == Context.User.GetIdentifier())
                  );

                    var s = BizMailConfigRow.Fields;
                    Config = connection1.TryFirst<BizMailConfigRow>(q => q
                        .SelectTableFields()
                        .Select(s.Apiurl)
                        .Select(s.Apikey)
                        //.Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );

                    model.EnqRow = connection1.List<BizMailIdiaMartRow>(q => q
                          .SelectTableFields()
                          .Select(menq.Rule)
                          .Select(menq.BmListId)
                          .Select(menq.BmListListId)
                          .Select(menq.Status)
                          .Where(menq.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );

                }
                if (Config.Apiurl != null && Config.Apikey != null)
                {
                    foreach (var ruletype in model.EnqRow)
                    {
                        bool condition = false;
                        dynamic list = null;
                        var name = Request.Entity.SenderName;
                        var mail = Request.Entity.SenderEmail;
                        condition = true;
                        list = ruletype.BmListListId;


                        if (condition == true)
                        {
                            var client = new RestClient(Config.Apiurl + "/lists/" + list + "/subscribers");
                            var request = new RestRequest(string.Empty, Method.Post);
                            //request.AddHeader("postman-token", "a62f832c-c9e2-47a7-7769-2938f3b900ae");
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("x-mw-public-key", Config.Apikey);
                            request.AddHeader("content-type", "application/x-www-form-urlencoded");
                            request.AddParameter("application/x-www-form-urlencoded", "EMAIL=" + mail + "&FNAME=" + name + "&LNAME=" + name + "", ParameterType.RequestBody);
                            RestResponse response = client.Execute(request);
                        }
                    }
                }
                ////// MailWizz End/////
            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow> { public MyListHandler(IRequestContext context) : base(context) { } }
        public class MailModel
        {
            public List<BizMailIdiaMartRow> EnqRow { get; set; }
        }
    }
}