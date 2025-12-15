
namespace AdvanceCRM.Contacts.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Purchase;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Services;
    using RestSharp;
    using AdvanceCRM.BizMail;
    using AdvanceCRM.Settings;
    using Newtonsoft.Json.Linq;
    using AdvanceCRM.Web.Helpers;
    using Serenity;
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
    using MyRow =ContactsRow;

    public class ContactsRepository : BaseRepository
    {
        private readonly IRequestContext _context;
        private readonly ISqlConnections _connections;

        public ContactsRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _context = context;
            _connections = connections;
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        public List<Int32> GetNotifyUsers(MyRow Entity)
        {
            List<Int32> userIds = new List<Int32>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = new UserRow();
                var assigned = new UserRow();

                owner = conn.TryById<UserRow>(Entity.OwnerId, q => q
                .SelectTableFields()
                .Select(od.UserId)
                .Select(od.UpperLevel)
                .Select(od.UpperLevel2)
                .Select(od.UpperLevel3)
                .Select(od.UpperLevel4)
                .Select(od.UpperLevel5)
                );

                assigned = conn.TryById<UserRow>(Entity.AssignedId, q => q
                .SelectTableFields()
                .Select(od.UserId)
                .Select(od.UpperLevel)
                .Select(od.UpperLevel2)
                .Select(od.UpperLevel3)
                .Select(od.UpperLevel4)
                .Select(od.UpperLevel5)
                );

                if (owner.HasValue())
                {
                    userIds.Add(owner.UserId.Value);
                    userIds.Add(owner.UpperLevel.Value);
                    userIds.Add(owner.UpperLevel2.Value);
                    userIds.Add(owner.UpperLevel3.Value);
                    userIds.Add(owner.UpperLevel4.Value);
                    userIds.Add(owner.UpperLevel5.Value);
                }
                if (assigned.HasValue())
                {
                    userIds.Add(assigned.UserId.Value);
                    userIds.Add(assigned.UpperLevel.Value);
                    userIds.Add(assigned.UpperLevel2.Value);
                    userIds.Add(assigned.UpperLevel3.Value);
                    userIds.Add(assigned.UpperLevel4.Value);
                    userIds.Add(assigned.UpperLevel5.Value);
                }
                if (Entity.MultiAssignList.HasValue() && Entity.MultiAssignList.Count > 0)
                    userIds.AddRange(Entity.MultiAssignList);
            }

            userIds = userIds.Distinct().ToList();
            userIds.Remove(Convert.ToInt32(_context.User.GetIdentifier()));

            return userIds;
        }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
             return new MySaveHandler(this).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(this).Process(uow, request, SaveRequestType.Update);
        }

        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            var response = new MyDeleteHandler(Context).Process(uow, request);

            LocalCache.Remove("Lookup:Contacts.Contacts");

            return response;
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler(Context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ContactsListRequest request)
        {
             return new MyListHandler(this).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly ContactsRepository _repository;

            public MySaveHandler(ContactsRepository repository) : base(repository._context)
            {
                _repository = repository;
            }

            protected override void AfterSave()
            {
                base.AfterSave();

                List<Int32> userIds = _repository.GetNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Contacts;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Contacts#edit/" + Request.Entity.Id;

                notify.Text =Context.User.Identity.Name + " has" + (this.IsUpdate ? " Updated" : " Created") + " a Contact";
                notify.UserList = userIds;

                new NotificationsRepository(Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });

                LocalCache.Remove("Lookup:Contacts.Contacts");
            }

            protected override void BeforeSave()
            {
                base.BeforeSave();

                 var connection = _repository._connections.NewByKey("Default");

                var Company = new CompanyDetailsRow();

                //var ct = ContactsRow.Fields;
                //var Contact = connection.TryById<ContactsRow>(Request.Entity.ContactsId, q => q
                //   .SelectTableFields()
                //   .Select(ct.Name)
                //   .Select(ct.Email));

                /////MailWizz/// 
                var model = new MailModel();
                BizMailConfigRow Config;

                var menq = BizMailContactRow.Fields;

                var br = UserRow.Fields;
                var UData = new UserRow();

                  using (var connection1 = _repository._connections.NewFor<BizMailConfigRow>())
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

                    model.EnqRow = connection1.List<BizMailContactRow>(q => q
                          .SelectTableFields()
                          .Select(menq.Rule)
                          .Select(menq.BmListId)
                          .Select(menq.BmListListId)
                          .Select(menq.Status)
                          .Where(menq.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );

                }

                if (Config.Apiurl != null && Config.Apikey !=null)
                {

                    foreach (var ruletype in model.EnqRow)
                    {
                        bool condition = false;
                        dynamic list = null;
                        var name = Request.Entity.Name;
                        var mail = Request.Entity.Email;
                        condition = true;
                        list = ruletype.BmListListId;


                        if (condition == true)
                        {
                            var client = new RestClient(Config.Apiurl + "/lists/" + list + "/subscribers");
                              var request = new RestRequest(string.Empty,Method.Post);
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

            //protected override void BeforeSave()
            //{
            //    base.BeforeSave();
            //    var connection = SqlConnections.NewByKey("Default");

            //    var Company = new CompanyDetailsRow();

            //    var e = CompanyDetailsRow.Fields;
            //    Company = connection.TryFirst<CompanyDetailsRow>(l => l
            //        .Select(e.StateCompulsoryInContacts)
            //        .Select(e.CountryMandatory)
            //        .Select(e.PincodeMandatory)
            //        .Select(e.CityMandatory)
            //    );

            //    if (Company.CountryMandatory.HasValue)
            //    {
            //        if (Company.CountryMandatory.Value == true)
            //        {
            //            if (Request.Entity.Country.Value <= 0)//|| Request.Entity.StateId.Value=="")

            //            //if (Request.Entity.Products.Count < 1)
            //            {
            //                throw new Exception("Please Select Country");
            //            }
            //        }
            //    }


            //    //if (Company.StateCompulsoryInContacts.HasValue)
            //    //{
            //    //    if (Company.StateCompulsoryInContacts.Value == true)
            //    //    {
            //    //        if(MyRow.Fields.StateId <=0 )//|| Request.Entity.StateId.Value=="")

            //    //        //if (Request.Entity.Products.Count < 1)
            //    //        {
            //    //            throw new Exception("Please Select State");
            //    //        }
            //    //    }
            //    //}
            //    //if (Company.CityMandatory.HasValue)
            //    //{
            //    //    if (Company.CityMandatory.Value == true)
            //    //    {
            //    //        if (MyRow.Fields.CityId <= 0)//|| Request.Entity.StateId.Value=="")

            //    //        //if (Request.Entity.Products.Count < 1)
            //    //        {
            //    //            throw new Exception("Please Select City");
            //    //        }
            //    //    }
            //    //}
            //    ////if (Company.PincodeMandatory.HasValue)
            //    ////{
            //    ////    if (Company.PincodeMandatory.Value == true)
            //    ////    {
            //    ////        if (!Request.Entity.Pin.HasValue)//|| Request.Entity.StateId.Value=="")

            //    ////        //if (Request.Entity.Products.Count < 1)
            //    ////        {
            //    ////            throw new Exception("Please Select Pincode");
            //    ////        }
            //    ////    }
            //    ////}

            //}
        }
        private class MyDeleteHandler : DeleteRequestHandler<MyRow>
        {
            public MyDeleteHandler(IRequestContext context) : base(context) { }

        }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow, ContactsListRequest>
        {
            private readonly ContactsRepository _repository;

            public MyListHandler(ContactsRepository repository) : base(repository._context)
            {
                _repository = repository;
            }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

               int uid = 0;
                var User = new UserRow();
                var user = (UserDefinition)Context.User.ToUserDefinition();
                if (user.UserId != 1)
                {

                    using (var connection = _repository._connections.NewFor<UserRow>())
                    {
                        var qt = UserRow.Fields;
                        User = connection.TryFirst<UserRow>(q => q
                           .SelectTableFields()
                           .Select(qt.Enquiry)
                          .Where(qt.UserId == Convert.ToInt32(user.UserId))
                         );
                    }

                    if (User != null && User.Contacts == true)
                    {
                        uid = Convert.ToInt32(User.UserId);
                    }

                }
                //For products filter
                if (user.UserId == 1 || user.UserId == uid)
                {
                    if (Request.SubContactsId != null)
                    {
                        var od = SubContactsRow.Fields.As("od");
                        string x = query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.ContactsId == fld.Id &
                                od.Id == Request.SubContactsId.Value)
                            .ToString();
                        query.Where(Criteria.Exists(
                        query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.ContactsId == fld.Id &
                                od.Id == Request.SubContactsId.Value)
                            .ToString()));
                    }
                    else if (Request.Stage != null)
                    {
                        if (Request.Stage == Masters.ContactsStage.Enquiry)
                        {
                            var od = EnquiryRow.Fields.As("od");
                            query.Where(Criteria.Exists(query.SubQuery()
                            .Select("1").From(od)
                            .Where(od.ContactsId == fld.Id).ToString()));
                        }
                        else if (Request.Stage == Masters.ContactsStage.Quotation)
                        {
                            var od = QuotationRow.Fields.As("od");
                            query.Where(Criteria.Exists(query.SubQuery()
                            .Select("1").From(od)
                            .Where(od.ContactsId == fld.Id).ToString()));
                        }
                        else if (Request.Stage == Masters.ContactsStage.Sales)
                        {
                            var od = SalesRow.Fields.As("od");
                            query.Where(Criteria.Exists(query.SubQuery()
                            .Select("1").From(od)
                            .Where(od.ContactsId == fld.Id).ToString()));
                        }
                        else if (Request.Stage == Masters.ContactsStage.Purchase)
                        {
                            var od = PurchaseRow.Fields.As("od");
                            query.Where(Criteria.Exists(query.SubQuery()
                            .Select("1").From(od)
                            .Where(od.PurchaseFromId == fld.Id).ToString()));
                        }
                        else if (Request.Stage == Masters.ContactsStage.CMS)
                        {
                            var od = CMSRow.Fields.As("od");
                            query.Where(Criteria.Exists(query.SubQuery()
                            .Select("1").From(od)
                            .Where(od.ContactsId == fld.Id).ToString()));
                        }
                    }

                    return;
                }
                
                var data = new UsersList();

                using (var connection = _repository._connections.NewFor<UserRow>())
                {
                    var od = UserRow.Fields;
                    data.Users1 = connection.List<UserRow>(q => q
                        .SelectTableFields()
                        .Select(od.UserId)
                        .Where(od.UpperLevel == user.UserId || od.UpperLevel2 == user.UserId || od.UpperLevel3 == user.UserId || od.UpperLevel4 == user.UserId || od.UpperLevel5 == user.UserId)
                        );

                    var mr = MultiRepContactsRow.Fields;
                    data.Users2 = connection.List<MultiRepContactsRow>(q => q
                        .SelectTableFields()
                        .Select(mr.ContactsId)
                        .Where(mr.AssignedId == user.UserId)
                        );
                }

                var str = fld.OwnerId + " = " + user.UserId + " OR " + fld.AssignedId + " = " + user.UserId;

                var str1 = "";
                var str2 = "";

                foreach (var item in data.Users1)
                {
                    str1 = str1 + " OR " + fld.OwnerId + " = " + item.UserId.Value + " OR " + fld.AssignedId + " = " + item.UserId.Value;
                }

                foreach (var item in data.Users2)
                {
                    str2 = str2 + " OR " + fld.Id + " = " + item.ContactsId.Value;
                }

                if (Request.SubContactsId.HasValue)
                {
                    if (Request.SubContactsId != null)
                    {
                        var od = SubContactsRow.Fields.As("od");

                        query.Where(new Criteria("(" + str + str1 + str2 + ")").ToString(), Criteria.Exists(query.SubQuery()
                            .Select("1")
                            .From(od)
                            .Where(
                                od.ContactsId == fld.Id &
                                od.Id == Request.SubContactsId.Value)
                            .ToString()).ToString());
                    }
                }
                else if (Request.Stage != null)
                {
                    if (Request.Stage == Masters.ContactsStage.Enquiry)
                    {
                        var od = EnquiryRow.Fields.As("od");
                        query.Where(new Criteria("(" + str + str1 + str2 + ")").ToString(), Criteria.Exists(query.SubQuery()
                            .Select("1").From(od)
                            .Where(od.ContactsId == fld.Id)
                            .ToString()).ToString());
                    }
                    else if (Request.Stage == Masters.ContactsStage.Quotation)
                    {
                        var od = QuotationRow.Fields.As("od");
                        query.Where(new Criteria("(" + str + str1 + str2 + ")").ToString(), Criteria.Exists(query.SubQuery()
                            .Select("1").From(od)
                            .Where(od.ContactsId == fld.Id)
                            .ToString()).ToString());
                    }
                    else if (Request.Stage == Masters.ContactsStage.Sales)
                    {
                        var od = SalesRow.Fields.As("od");
                        query.Where(new Criteria("(" + str + str1 + str2 + ")").ToString(), Criteria.Exists(query.SubQuery()
                            .Select("1").From(od)
                            .Where(od.ContactsId == fld.Id)
                            .ToString()).ToString());
                    }
                    else if (Request.Stage == Masters.ContactsStage.Purchase)
                    {
                        var od = PurchaseRow.Fields.As("od");
                        query.Where(new Criteria("(" + str + str1 + str2 + ")").ToString(), Criteria.Exists(query.SubQuery()
                            .Select("1").From(od)
                            .Where(od.PurchaseFromId == fld.Id)
                            .ToString()).ToString());
                    }
                    else if (Request.Stage == Masters.ContactsStage.CMS)
                    {
                        var od = CMSRow.Fields.As("od");
                        query.Where(new Criteria("(" + str + str1 + str2 + ")").ToString(), Criteria.Exists(query.SubQuery()
                            .Select("1").From(od)
                            .Where(od.ContactsId == fld.Id)
                            .ToString()).ToString());
                    }
                }
                else
                {
                    query.Where(new Criteria("(" + str + str1 + str2 + ")"));
                }
                //query.Where((fld.OwnerId == user.UserId) | (fld.AssignedId == user.UserId) | (fld.OwnerId == item.UserId.Value) |(fld.AssignedId == item.UserId.Value));
            }

            public class UsersList
            {
                public List<UserRow> Users1 { get; set; }
                public List<MultiRepContactsRow> Users2 { get; set; }
            }
        }

        public class MailModel
        {
            public List<BizMailContactRow> EnqRow { get; set; }
        }
    }
}