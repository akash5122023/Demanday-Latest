
namespace AdvanceCRM.Quotation.Repositories
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
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json.Linq;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Serenity;
    using Serenity.Extensions;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Net.Mail;
    using System.Text;
    using System.Threading;
    using System.Web;
    using MyRow = QuotationRow;
    using UserRow = Administration.UserRow;

    public class QuotationRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;
        private readonly IMemoryCache memoryCache;
        private CancellationTokenSource listCacheReset = new CancellationTokenSource();
        private readonly MemoryCacheEntryOptions userScopeCacheOptions;

        public QuotationRepository(IRequestContext context, ISqlConnections connections, IMemoryCache? memoryCache = null)
            : base(context)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
            Context = context ?? throw new ArgumentNullException(nameof(context));
            this.memoryCache = memoryCache ?? Dependency.TryResolve<IMemoryCache>() ?? throw new ArgumentNullException(nameof(memoryCache));
            userScopeCacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
        }


        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        public static List<int> getNotifyUsers(MyRow entity,IRequestContext context,ISqlConnections _connections)
        {
            var userIds = new List<int>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = new UserRow();
                var assigned = new UserRow();
                owner = conn.TryById<UserRow>(entity.OwnerId, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );

                assigned = conn.TryById<UserRow>(entity.AssignedId, q => q
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
            }

            userIds = userIds.Distinct().ToList();
            userIds.Remove(Convert.ToInt32(context.User.GetIdentifier()));

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
            MyRow existing = null;

            if (request?.EntityId != null)
            {
                using (var connection = _connections.NewFor<MyRow>())
                {
                    existing = connection.TryById<MyRow>(request.EntityId, q => q
                        .Select(fld.Id)
                        .Select(fld.OwnerId)
                        .Select(fld.AssignedId));
                }
            }

            var response = new MyDeleteHandler(this).Process(uow, request);

            if (existing != null)
                InvalidateUserScopeCache(existing.OwnerId, existing.AssignedId);

            ResetListCache();

            return response;
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler(Context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, QuotationListRequest request)
        {
            var cacheKey = GetListCacheKey(request, out var currentUserId);

            if (cacheKey != null && memoryCache.TryGetValue(cacheKey, out ListResponse<MyRow> cachedResponse) && cachedResponse != null)
                return CloneListResponse(cachedResponse);

            var response = new MyListHandler(this).Process(connection, request);

            if (cacheKey != null && currentUserId > 0 && response != null)
            {
                var cacheEntry = CloneListResponse(response);
                memoryCache.Set(cacheKey, cacheEntry, GetListCacheEntryOptions());
            }

            return response;
        }

        private MemoryCacheEntryOptions GetListCacheEntryOptions()
        {
            return new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(2))
                .AddExpirationToken(new CancellationChangeToken(listCacheReset.Token));
        }

        private static ListResponse<MyRow> CloneListResponse(ListResponse<MyRow> source)
        {
            var clone = new ListResponse<MyRow>
            {
                TotalCount = source.TotalCount,
                Skip = source.Skip,
                Take = source.Take,
                Values = source.Values
            };

            if (source.Entities != null)
            {
                clone.Entities = new List<MyRow>(source.Entities.Count);

                foreach (var entity in source.Entities)
                {
                    if (entity != null)
                        clone.Entities.Add((MyRow)entity.Clone());
                }
            }

            return clone;
        }

        private string? GetListCacheKey(QuotationListRequest request, out int userId)
        {
            userId = 0;

            var identifier = Context?.User?.GetIdentifier();
            if (string.IsNullOrEmpty(identifier) || !int.TryParse(identifier, out userId))
                return null;

            var builder = new StringBuilder();
            builder.Append("quotation:list:user:").Append(userId);
            builder.Append("|skip:").Append(request.Skip);
            builder.Append("|take:").Append(request.Take);
            builder.Append("|includeDeleted:").Append(request.IncludeDeleted);
            builder.Append("|columnSelection:").Append(request.ColumnSelection);
            builder.Append("|products:").Append(request.ProductsId?.ToString() ?? "null");
            builder.Append("|division:").Append(request.DivisionId?.ToString() ?? "null");
            builder.Append("|area:").Append(request.AreaId?.ToString() ?? "null");

            if (request.Sort != null && request.Sort.Length > 0)
            {
                builder.Append("|sort:");
                builder.Append(string.Join(',', request.Sort.Select(s => $"{s.Field}:{s.Descending}")));
            }

            if (!ReferenceEquals(request.Criteria, null))
                builder.Append("|criteria:").Append(request.Criteria);

            if (request.EqualityFilter != null && request.EqualityFilter.Count > 0)
            {
                var equalitySignature = string.Join(',', request.EqualityFilter
                    .OrderBy(x => x.Key, StringComparer.OrdinalIgnoreCase)
                    .Select(x => $"{x.Key}={x.Value}"));
                builder.Append("|eq:").Append(equalitySignature);
            }

            if (request.IncludeColumns != null && request.IncludeColumns.Count > 0)
            {
                builder.Append("|include:");
                builder.Append(string.Join(',', request.IncludeColumns.OrderBy(x => x, StringComparer.Ordinal)));
            }

            if (request.ExcludeColumns != null && request.ExcludeColumns.Count > 0)
            {
                builder.Append("|exclude:");
                builder.Append(string.Join(',', request.ExcludeColumns.OrderBy(x => x, StringComparer.Ordinal)));
            }

            return builder.ToString();
        }

        private void ResetListCache()
        {
            var previous = Interlocked.Exchange(ref listCacheReset, new CancellationTokenSource());

            try
            {
                previous.Cancel();
            }
            finally
            {
                previous.Dispose();
            }
        }

        private string GetUserScopeCacheKey(int userId) => $"quotation:user-scope:{userId}";

        private UserScopeCacheItem GetUserScope(int userId)
        {
            if (memoryCache.TryGetValue(GetUserScopeCacheKey(userId), out UserScopeCacheItem cached) && cached != null)
                return cached;

            var scope = LoadUserScope(userId);
            memoryCache.Set(GetUserScopeCacheKey(userId), scope, userScopeCacheOptions);
            return scope;
        }

        private UserScopeCacheItem LoadUserScope(int userId)
        {
            var subordinateIds = new List<int>();
            using (var connection = _connections.NewFor<UserRow>())
            {
                var u = UserRow.Fields;
                var rows = connection.List<UserRow>(q => q
                    .Select(u.UserId)
                    .Where(u.UpperLevel == userId || u.UpperLevel2 == userId || u.UpperLevel3 == userId || u.UpperLevel4 == userId || u.UpperLevel5 == userId));

                foreach (var row in rows)
                    if (row.UserId.HasValue)
                        subordinateIds.Add(row.UserId.Value);
            }

            var sharedQuotationIds = new List<int>();
            using (var connection = _connections.NewFor<MultiRepQuotationRow>())
            {
                var mr = MultiRepQuotationRow.Fields;
                var rows = connection.List<MultiRepQuotationRow>(q => q
                    .Select(mr.QuotationId)
                    .Where(mr.AssignedId == userId));

                foreach (var row in rows)
                    if (row.QuotationId.HasValue)
                        sharedQuotationIds.Add(row.QuotationId.Value);
            }

            return new UserScopeCacheItem(subordinateIds.Distinct().ToArray(), sharedQuotationIds.Distinct().ToArray());
        }

        private void InvalidateUserScopeCache(params int?[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                return;

            foreach (var userId in userIds)
            {
                if (userId.HasValue)
                    memoryCache.Remove(GetUserScopeCacheKey(userId.Value));
            }
        }

        private sealed class UserScopeCacheItem
        {
            public UserScopeCacheItem(int[] subordinateUserIds, int[] sharedQuotationIds)
            {
                SubordinateUserIds = subordinateUserIds ?? Array.Empty<int>();
                SharedQuotationIds = sharedQuotationIds ?? Array.Empty<int>();
            }

            public int[] SubordinateUserIds { get; }
            public int[] SharedQuotationIds { get; }
        }

        //Undelete option
        //private class MyUndeleteHandler : UndeleteRequestHandler<MyRow> { }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly QuotationRepository repository;

            public MySaveHandler(QuotationRepository repository)
                : base(repository.Context)
            {
                this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            protected override void AfterSave()
            {

                base.AfterSave();

                int type = 0; 
                string str = "";
                if (this.IsCreate)
                {                 

                    UserRow user;
                    using (var conn = repository._connections.NewFor<UserRow>())
                    {

                        var u = UserRow.Fields;

                        user = conn.TryById<UserRow>(Request.Entity.AssignedId, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }
                    type = 1;

                    str = "Quotation Created and Assigned to " + user.Username + "</b>"; ;
                }
                else if (this.IsUpdate)
                {
                    if (Old.Status != Request.Entity.Status)
                    {
                        str = "Status Changed to <b>" + Request.Entity.Status.GetDescription() + "</b>.<br/>";
                        type = 2;
                    }

                    if (Old.AssignedId != Request.Entity.AssignedId)
                    {
                        UserRow user;
                        using (var conn = repository._connections.NewFor<UserRow>())
                        {
                            var u = UserRow.Fields;

                            user = conn.TryById<UserRow>(Request.Entity.AssignedId, q => q
                            .SelectTableFields()
                            .Select(u.Username));
                        }

                        str = str + "Quotation Assigned to <b>" + user.Username + "</b>.<br/>";
                        type = 3;
                    }
                    if (Old.StageId != Request.Entity.StageId)
                    {
                        StageRow stage;
                        using (var conn = repository._connections.NewFor<StageRow>())
                        {
                            var u = StageRow.Fields;

                            stage = conn.TryById<StageRow>(Request.Entity.StageId, q => q
                            .SelectTableFields()
                            .Select(u.Stage));
                        }
                        str = "Stage Changed to <b>" + stage.Stage + "</b>.<br/>";
                        type = 4;
                    }
                }

                if (type != 0 && str.Length > 0)
                {
                    var Timeline = new TimelineRow();

                    Timeline.EntityType = Request.Entity.Table;
                    Timeline.EntityId = Row.Id;
                    Timeline.InsertDate = null;
                    Timeline.Type = type;
                    Timeline.Text = str;
                    Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

                    new TimelineRepository(Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                if (Request.Entity.Status == (Masters.StatusMaster)2)
                {
                    try
                    {
                        new SqlUpdate("dbo.QuotationFollowups")
                            .Set("Status", 2)
                            .Where("QuotationId=" + Request.Entity.Id)
                            .Execute(this.Connection, ExpectedRows.Ignore);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }
                }

                repository.InvalidateUserScopeCache(Request.Entity.OwnerId, Request.Entity.AssignedId, Old?.OwnerId, Old?.AssignedId);
                repository.ResetListCache();
            }

            protected override void BeforeSave()
            {
                base.BeforeSave();

                var connection = repository._connections.NewByKey("Default");

                var Company = new CompanyDetailsRow();

                var ct = ContactsRow.Fields;
                var Contact = connection.TryById<ContactsRow>(Request.Entity.ContactsId, q => q
                   .SelectTableFields()
                   .Select(ct.Name)
                   .Select(ct.Email));


                var e = CompanyDetailsRow.Fields;
                Company = connection.TryFirst<CompanyDetailsRow>(l => l
                    .Select(e.QuotationFollwupMandatory)
                    .Select(e.QuotationProductsMandatory)
                    .Select(e.QuoEditNo)
                ) ;

                if (this.IsCreate)
                {
                    if (Company.QuoEditNo.Value == false)
                    {
                        GetNextNumberResponse nextNumber = new QuotationController().GetNextNumber(connection, new GetNextNumberRequest());
                        Request.Entity.QuotationN = nextNumber.SerialN;
                        Request.Entity.QuotationNo = int.Parse(nextNumber.Serial);
                    }
                }

                if (this.IsUpdate && Old.Status == Masters.StatusMaster.Closed)
                {
                    if (!Context.Permissions.HasPermission("Quotation:Re-open Quotation"))
                    {
                        throw new Exception("Authorization failed to change status!");
                    }
                }

                if (Company.QuotationProductsMandatory.HasValue)
                {
                    if (Company.QuotationProductsMandatory.Value == true)
                    {
                        if (Request.Entity.Products.Count < 1)
                        {
                            throw new Exception("Kindly add atleast one product for this Quotation and then try saving");
                        }
                    }
                }

                /////MailWizz/// 
                var model = new MailModel();
                BizMailConfigRow Config;

                var menq = BizMailQuotationRow.Fields;

                var br = UserRow.Fields;
                var UData = new UserRow();

                using (var connection1 = repository._connections.NewFor<BizMailConfigRow>())
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

                    model.EnqRow = connection1.List<BizMailQuotationRow>(q => q
                          .SelectTableFields()
                          .Select(menq.Rule)
                          .Select(menq.QuotationStatus)
                          .Select(menq.BmListId)
                          .Select(menq.BmListListId)
                          .Select(menq.StageId)
                          .Select(menq.SourceId)
                          .Select(menq.ClosingType)
                          .Select(menq.Type)
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
                        var name = Contact.Name;
                        var mail = Contact.Email;
                        if (ruletype.Rule == Masters.MailRuleMaster.Status)
                        {
                            var stat = ruletype.QuotationStatus;
                            if (stat == Request.Entity.Status)
                            {
                                condition = true;
                                list = ruletype.BmListListId;
                            }
                        }
                        if (ruletype.Rule == Masters.MailRuleMaster.Stage)
                        {
                            var stage = ruletype.StageId;
                            if (stage == Request.Entity.StageId)
                            {
                                condition = true;
                                list = ruletype.BmListListId;
                            }
                        }
                        if (ruletype.Rule == Masters.MailRuleMaster.Source)
                        {
                            var sorce = ruletype.SourceId;
                            if (sorce == Request.Entity.SourceId)
                            {
                                condition = true;
                                list = ruletype.BmListListId;
                            }
                        }
                        if (ruletype.Rule == Masters.MailRuleMaster.Type)
                        {
                            var typ = ruletype.Type;
                            if (typ == Request.Entity.Type)
                            {
                                condition = true;
                                list = ruletype.BmListListId;
                            }
                        }
                        if (ruletype.Rule == Masters.MailRuleMaster.ClosingType)
                        {
                            var ctyp = ruletype.ClosingType;
                            if (ctyp == Request.Entity.ClosingType)
                            {
                                condition = true;
                                list = ruletype.BmListListId;
                            }
                        }


                        if (condition == true)
                        {
                            var client = new RestClient(Config.Apiurl + "/lists/" + list + "/subscribers");
                            var request = new RestRequest("", Method.Post);
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

                if (this.IsUpdate)
                {
                    if (Company.QuotationFollwupMandatory.HasValue && this.IsUpdate)
                    {
                        if (Company.QuotationFollwupMandatory.Value == true)
                        {
                            if (connection.Count<QuotationFollowupsRow>(new Criteria("QuotationId = " + Request.Entity.Id)) < 1)
                            {
                                throw new Exception("Kindly add atleast one followup for this Quotation and then try saving");
                            }
                        }
                    }
                }

                List<Int32> userIds = getNotifyUsers(Request.Entity,Context, repository._connections);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Quotation;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Quotation#edit/" + Request.Entity.Id;
                notify.Text = Context.User.Identity.Name+ " has" + (this.IsUpdate ? " Updated" : " Created") + " a Quotation";
                notify.UserList = userIds;

                new NotificationsRepository(Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
            }

            private string NumberToWords(int number)
            {
                if (number == 0)
                    return "zero";

                if (number < 0)
                    return "minus " + NumberToWords(Math.Abs(number));

                string words = "";
                if ((number / 1000000000) > 0)
                {
                    words += NumberToWords(number / 1000000000) + " Billion ";
                    number %= 1000000000;
                }

                if ((number / 10000000) > 0)
                {
                    words += NumberToWords(number / 10000000) + " Crore ";
                    number %= 10000000;
                }

                if ((number / 1000000) > 0)
                {
                    words += NumberToWords(number / 1000000) + " Million ";
                    number %= 1000000;
                }


                if ((number / 100000) > 0)
                {
                    words += NumberToWords(number / 100000) + " Lakh ";
                    number %= 100000;
                }


                if ((number / 1000) > 0)
                {
                    words += NumberToWords(number / 1000) + " Thousand ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    words += NumberToWords(number / 100) + " Hundred ";
                    number %= 100;
                }

                if (number > 0)
                {
                    if (words != "")
                        words += "and ";

                    var unitsMap = new[] { "zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                    var tensMap = new[] { "zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                    if (number < 20)
                        words += unitsMap[number];
                    else
                    {
                        words += tensMap[number / 10];
                        if ((number % 10) > 0)
                            words += "-" + unitsMap[number % 10];
                    }
                }

                return words;
            }

            //protected override void AfterSave()
            //{
            //    base.AfterSave();

            //    //Sending mail
            //    int cId = Request.Entity.ContactsId.Value;

            //    var model = new QuotationReportData();
            //    var q = MyRow.Fields;
            //    var c = ContactsRow.Fields;
            //    var qp = QuotationProductsRow.Fields;

            //    using (var connection = _connections.NewFor<MyRow>())
            //    {
            //        model.Contacts = connection.TryById<ContactsRow>(Request.Entity.ContactsId.Value, cnts => cnts
            //        .SelectTableFields()
            //        .Select(c.Id)
            //        .Select(c.Name)
            //        .Select(c.Email)
            //        .Select(c.Phone)
            //        );

            //        model.Quotation = connection.TryById<MyRow>(Request.Entity.Id.Value, qtn => qtn
            //        .SelectTableFields()
            //        .Select(q.Id)
            //        .Select(q.Date)
            //        );

            //        model.QuotationProducts = connection.List<QuotationProductsRow>(qps => qps
            //            .SelectTableFields()
            //            .Select(qp.ProductsCode)
            //            .Select(qp.ProductsName)
            //            .Select(qp.Price)
            //            .Select(qp.Quantity)
            //            .Select(qp.Discount)
            //            .Select(qp.TaxType1)
            //            .Select(qp.Percentage1)
            //            .Select(qp.TaxType2)
            //            .Select(qp.Percentage2)
            //            .Select(qp.LineTotal)
            //            .Where(qp.QuotationId == Request.Entity.Id.Value));

            //        var qt = QuotationTermsRow.Fields;
            //        model.QuotationTerms = connection.List<QuotationTermsRow>(qts => qts
            //            .SelectTableFields()
            //            .Select(qt.Terms)
            //            .Where(qt.QuotationId == Request.Entity.Id.Value));
            //    }

            //    var message = new MailMessage();
            //    var addr = new MailAddress("vinaykulkarni89@hotmail.com", Texts.Site.Layout.CompanyName);

            //    if ((model.Contacts.Email.Trim() != "") || (model.Contacts.Email != null))
            //    {
            //        message.To.Add(model.Contacts.Email);
            //        message.Subject = "Quotation";
            //        message.Body ="<html><head><title></title></head>" +
            //            "<body style=\"align - self:stretch; align - items:center; align - items:center\">" +
            //            "<table style=\"width: 100%; border-collapse:collapse\" border=\"1\">" +
            //            "<tr>" +
            //            "<td align=\"center\"><h3> Quotation </h3></td>" +
            //            "</tr>" +
            //            "</table>" +

            //            "<table style=\"width: 100% ; border-collapse:collapse\" border=\"1\">" +
            //            "<tr>" +
            //            "<td align=\"center\" style=\"width: 20 % \">" +
            //            "&nbsp; <img src=\""+ Texts.Site.Layout.LogoLink +"\" height=\"80\" width=\"80\" />&nbsp;" +
            //            "</td>" +
            //            "<td align=\"center\" style=\"width: 40 % \">" +
            //            "<h3>"
            //            ;
            //        message.Body = message.Body + Texts.Site.Layout.CompanyName;
            //        message.Body = message.Body + "</h3>";
            //        message.Body = message.Body + Texts.Site.Layout.CompanyAddress + "<br />";
            //        message.Body = message.Body + Texts.Site.Layout.CompanyPhone +
            //            "</td>" +
            //            "<td align=\"left\" style=\"width: 40 % \">" +
            //            "<strong>&nbsp; Quotaion No </strong>: ";
            //        message.Body = message.Body + DateTime.Now.Year + "/" + Request.Entity.Id.Value + ", " +
            //            "<strong> Date </strong>:" +
            //            model.Quotation.Date.Value.ToShortDateString() + "<br />" +
            //            "<strong>&nbsp; Name </strong> :" +
            //            model.Contacts.Name + "<br />" +
            //            "<strong>&nbsp; Phone </strong>: " + model.Contacts.Phone + "<br />" +
            //            "<strong>&nbsp; Address </strong>: " + model.Contacts.Address +
            //            "</td>" +
            //            "</tr>" +
            //            "</table>"
            //            ;
            //        //Products

            //        message.Body = message.Body +
            //            "<table style=\"width: 100%; border-collapse:collapse\" border=\"1\">" +
            //            "<tr>" +
            //            "<td align=\"center\"><strong>Sr. No.</strong></td>" +
            //            "<td align=\"center\"><strong>Code</strong></td>" +
            //            "<td align=\"center\"><strong>Product</strong></td>" +
            //            "<td align=\"center\"><strong>Price</strong></td>" +
            //            "<td align=\"center\"><strong>Quantity</strong></td>" +
            //            "<td align=\"center\"><strong>Discount</strong></td>" +
            //            "<td align=\"center\"><strong>Tax</strong></td>" +
            //            "<td align=\"center\"><strong>Total</strong></td>" +
            //            "<td align=\"center\"><strong>Grand Total</strong></td>" +
            //            "</tr>";
            //        int srno = 1; decimal Total = 0; decimal gTotal = 0;

            //        foreach (var item in model.QuotationProducts)
            //        {
            //            message.Body = message.Body + "<tr>" +
            //                "<td align=\"center\">" + srno + "</td>" +
            //                "<td align=\"Left\">&nbsp;" + item.ProductsCode + "</td>" +
            //                "<td align=\"left\">&nbsp;" + item.ProductsName + "</td>" +
            //                "<td align=\"right\">" + item.Price.Value + "&nbsp;</td>" +
            //                "<td align=\"right\">" + item.Quantity.Value + "&nbsp;</td>" +
            //                "<td align=\"right\">" + item.Discount.Value + "&nbsp;</td>" +
            //                "<td align=\"left\">";

            //                if (item.TaxType1 != null)
            //                {
            //                    message.Body = message.Body + item.TaxType1 + ":" + item.Percentage1;
            //                }
            //                if (item.TaxType1 != null && item.TaxType2 != null)
            //                {
            //                    message.Body = message.Body + ",";
            //                }
            //                if (item.TaxType2 != null)
            //                {
            //                    message.Body = message.Body + item.TaxType2 + ":" + item.Percentage2;
            //                }

            //            message.Body = message.Body + "</td>" +
            //                "<td align=\"right\">" + item.LineTotal.Value + "&nbsp;</td>" +
            //                "<td align=\"right\">" + item.LineTotal.Value + "&nbsp;</td>";

            //            Total = Total + item.LineTotal.Value;
            //            gTotal = gTotal + item.LineTotal.Value;
            //            srno++;

            //            message.Body = message.Body + "</tr>";
            //        }

            //        int diff = 10 - srno;

            //        for (int i = diff; i > 0; i--)
            //        {
            //            message.Body = message.Body +
            //                "<tr>"+
            //                "<td>&nbsp;</td>"+
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "<td>&nbsp;</td>" +
            //                "</tr>";
            //        }
            //        //Total

            //        message.Body = message.Body + "<tr>" +
            //            "<td colspan=\"8\" align=\"right\"><strong> Total:&nbsp;</strong></td>" +
            //            "<td align=\"right\"><strong>" + Total.ToString() + "&nbsp;</strong></td>" +
            //            "</tr>";

            //        //Grand total
            //        message.Body = message.Body +
            //            "<tr>" +
            //            "<td colspan=\"8\" align=\"right\"><strong> Grand Total:&nbsp;</strong></td>" +
            //            "<td align=\"right\"><strong>" + gTotal.ToString() + "&nbsp;</strong></td>" +
            //            "</tr>" +
            //            "<tr>" +
            //            "<td colspan=\"9\" bgcolor=\"wheat\">" +
            //            "<strong>Total in Words: &nbsp;</strong>" +
            //            NumberToWords((int)gTotal) +
            //            " Only </td>" +
            //            "</tr>" +
            //            "</table>" +

            //            //Terms

            //            "<h4>Terms &amp; Condition's</h4>" +
            //            "<table style=\"width: 100%; border-collapse:collapse\" border=\"1\">";
            //            int Tsrno = 1;

            //        foreach (var item in model.QuotationTerms)
            //        {
            //            message.Body = message.Body +
            //                "<tr>"+
            //                "<td align=\"Left\">"+Tsrno+ ".&nbsp;" + item.Terms+"</td>"+
            //                "</tr>";
            //            Tsrno++;
            //        }

            //        message.Body = message.Body +
            //            "</table>"+
            //            "</body>" +
            //            "</html>";


            //        message.IsBodyHtml = true;
            //        message.From = addr;
            //        message.Sender = addr;

            //        var mail = new SmtpClient();

            //        //Configuration
            //        NetworkCredential nc = new NetworkCredential("vinaykulkarni89@hotmail.com", "password");
            //        mail.Credentials = nc;
            //        mail.EnableSsl = true;
            //        mail.Host = "smtp.live.com";
            //        mail.Port = 587;

            //        mail.Send(message);
            //    }
            //    else
            //    {
            //        //throw new FileNotFoundException(@"[Email Id not found]");
            //    }            
            //}
        }
        private class MyDeleteHandler : DeleteRequestHandler<MyRow>
        {
            public MyDeleteHandler(QuotationRepository repository)
                : base(repository.Context)
            {
            }
        }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }

        private class MyListHandler : ListRequestHandler<MyRow, QuotationListRequest>
        {
            private readonly QuotationRepository repository;

            public MyListHandler(QuotationRepository repository)
                : base(repository.Context)
            {
                this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                var user = (UserDefinition)Context.User.ToUserDefinition();
                if (user == null)
                    return;

                var currentUserId = Convert.ToInt32(user.UserId);
                var hasFullAccess = currentUserId == 1 || user.Quotation;

                if (!hasFullAccess)
                {
                    var scope = repository.GetUserScope(currentUserId);
                    var visibleUserIds = new HashSet<int>(scope.SubordinateUserIds ?? Array.Empty<int>()) { currentUserId };

                    BaseCriteria accessCriteria = null;

                    if (visibleUserIds.Count > 0)
                        accessCriteria = fld.OwnerId.In(visibleUserIds) | fld.AssignedId.In(visibleUserIds);

                    if (scope.SharedQuotationIds.Length > 0)
                    {
                        var sharedCriteria = fld.Id.In(scope.SharedQuotationIds);
                        accessCriteria = ReferenceEquals(accessCriteria, null) ? sharedCriteria : accessCriteria | sharedCriteria;
                    }

                    if (!ReferenceEquals(accessCriteria, null))
                        query.Where(accessCriteria);
                    else
                        query.Where(fld.Id == -1);
                }

                ApplyProductAndAreaFilters(query);
            }

            private void ApplyProductAndAreaFilters(SqlQuery query)
            {
                if (Request.ProductsId != null)
                {
                    var qpProducts = QuotationProductsRow.Fields.As("qpProducts");
                    var subQuery = query.SubQuery()
                        .Select("1")
                        .From(qpProducts)
                        .Where(qpProducts.QuotationId == fld.Id & qpProducts.ProductsId == Request.ProductsId.Value);

                    query.Where(Criteria.Exists(subQuery.ToString()));
                }

                if (Request.DivisionId != null)
                {
                    var qpDivision = QuotationProductsRow.Fields.As("qpDivision");
                    var subQuery = query.SubQuery()
                        .Select("1")
                        .From(qpDivision)
                        .Where(qpDivision.QuotationId == fld.Id & qpDivision.ProductsDivisionId == Request.DivisionId.Value);

                    query.Where(Criteria.Exists(subQuery.ToString()));
                }

                if (Request.AreaId != null)
                {
                    query.Where(fld.ContactsAreaId == Request.AreaId.Value);
                }
            }
        }
        public class MailModel
        {
            public List<BizMailQuotationRow> EnqRow { get; set; }
        }

    }
}