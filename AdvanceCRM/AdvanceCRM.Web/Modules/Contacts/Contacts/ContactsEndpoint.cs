
namespace AdvanceCRM.Contacts.Endpoints
{
    using OfficeOpenXml;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    
    using MyRepository = Repositories.ContactsRepository;
    using MyRow =ContactsRow;
    using SubContact =SubContactsRow;
    using Mast = Masters;
    using System.Collections.Generic;
    
    using AdvanceCRM.Masters;
    using Nito.AsyncEx;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections;
    using AdvanceCRM.Contacts;
    using System.Globalization;
    using System.Configuration;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Common;
    using AdvanceCRM.Web.Helpers;

    [Route("Services/Contacts/Contacts/[action]")]
[ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
public class ContactsController : ServiceEndpoint
{
    private readonly ISqlConnections _connections;
    private readonly IWebHostEnvironment _env;

    public ContactsController(ISqlConnections connections, IWebHostEnvironment env)
    {
        _connections = connections;
        _env = env;
    }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context, _connections).Delete(uow, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ContactsListRequest request)
        {
            return new MyRepository(Context, _connections).List(connection, request);
        }

        //Send SMS for SMS Sender
        [HttpPost, ServiceAuthorize("SMS:BulkSMS")]
        public StandardResponse SendBulkSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            MyRow contact;

            var e = MyRow.Fields;

            using (var connection = _connections.NewFor<MyRow>())
            {
                contact = connection.TryById<MyRow>(request.Id, q => q
                    .Select(e.Id)
                    .Select(e.Name)
                    .Select(e.Phone)
                );
            }

            String msg = request.SMSType;
            String TempId = request.TemplateID;

            if (msg.Trim() == "#customername token can be used in message to auto add customers name")
            {
                throw new Exception("It seems you havent chnaged the default text, hence sending SMS is been cancelled");
            }

            var newmsg = msg.Replace("#customername", contact.Name);

            response.Status = SMSHelper.SendSMS(contact.Phone, newmsg, TempId);

            if (!response.Status.Contains(("SMS").ToUpper()))
            {
                throw new Exception("SMS Sending Failed!");
            }

            response.Status = "Successfull!!!";

            return response;
        }

        //Add to MailChimp

        private string assignKey()
        {
            MailChimpConfigurationRow MCConfig;

            using (var connection = _connections.NewFor<MailChimpConfigurationRow>())
            {
                var s = MailChimpConfigurationRow.Fields;
                MCConfig = connection.TryById<MailChimpConfigurationRow>(1, q => q
                .SelectTableFields()
                .Select(s.ApiKey)
                );
            }
            return MCConfig.ApiKey;
        }
        private MailChimp.Net.MailChimpManager CreateMailChimpManager()
        {
            return new MailChimp.Net.MailChimpManager(assignKey());
        }

        private async Task<IEnumerable> ListDetails(MailChimp.Net.MailChimpManager manager)
        {
            var listoptions = new MailChimp.Net.Core.ListRequest
            {
                Limit = 1000
            };
            var ModelList = await manager.Lists.GetAllAsync(listoptions);
            var NewList = ModelList.OrderBy(x => x.Stats.MemberCount).ToList();


            return ViewBag.ListData = NewList;
        }

        [HttpPost]
        public MailChimpResponse AddToMailChimp(MailChimpRequest request)
        {
            var response = new MailChimpResponse();

            var manager = CreateMailChimpManager();

            List<ContactsRow> contacts;
            List<ContactsRow> contacts1 = new List<MyRow>();


            var c = ContactsRow.Fields;

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                contacts = connection.List<ContactsRow>(q => q
                    .Select(c.Id)
                    .Select(c.Name)
                    .Select(c.Email)
                    .Where(c.Email.IsNotNull())
                );
            }

            foreach (var item in request.MailChimpIds)
            {
                contacts1.Add(contacts.Find(d => d.Id.ToString() == item));
            }

            contacts1.RemoveAll(x => x == null);


            var result1 = AsyncContext.Run(() => ListDetails(manager));

            var ContactListId = "";

            foreach (var item in ViewBag.ListData)
            {
                if (item.Name == request.ListName.Trim())
                {
                    ContactListId = item.Id;
                    break;
                }
            }

            if (ContactListId.IsNullOrEmpty())
            {
                response.MailChimpReturnResponse = "List '" + request.ListName + "' not found, please create this list in MailChimp section";
                return response;
            }

            foreach (var item in contacts1)
            {
                //Add mailchimp sync code here for this list to add it to MailChimp Contact List

                //Id of contacts list

                try
                {
                    var member = new MailChimp.Net.Models.Member
                    {
                        EmailAddress = item.Email,
                        Status = MailChimp.Net.Models.Status.Subscribed,
                        StatusIfNew = MailChimp.Net.Models.Status.Subscribed,
                        ListId = ContactListId,
                        EmailType = "html",
                        IpSignup = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        TimestampSignup = DateTime.UtcNow.ToString("s"),
                        MergeFields = new Dictionary<string, object>
                        {
                            {"FNAME", item.Name },
                            {"LNAME","" }
                        }
                    };
                    var result = AsyncContext.Run(() => manager.Members.AddOrUpdateAsync(ContactListId, member));
                }
                catch (Exception ex)
                {

                    response.MailChimpReturnResponse = "Error\n" + ex.Message.ToString();
                }
            }

            if (contacts1.Count() > 0)
            {
                response.MailChimpReturnResponse = "Success";
            }
            else
            {
                response.MailChimpReturnResponse = "In selected list no valid email Id where found";
            }

            return response;
        }

        //Excel import contacts
        //[HttpPost, ServiceAuthorize("Contacts:Import")]
        //public ExcelImportResponse ExcelImport(IUnitOfWork uow, ExcelImportRequest request)
        //{
        //    request.CheckNotNull();
        //    Check.NotNullOrWhiteSpace(request.FileName, "filename");

        //    UploadHelper.CheckFileNameSecurity(request.FileName);

        //    if (!request.FileName.StartsWith("temporary/"))
        //        throw new ArgumentOutOfRangeException("filename");

        //    ExcelPackage ep = new ExcelPackage();
        //    using (var fs = new FileStream(UploadHelper.DbFilePath(request.FileName), FileMode.Open, FileAccess.Read))
        //        ep.Load(fs);

        //    var c = MyRow.Fields;
        //    var sc = SubContact.Fields;
        //    var ct = CityRow.Fields;
        //    var s = StateRow.Fields;
        //    var a = AreaRow.Fields;
        //    var cat = CategoryRow.Fields;
        //    var g = GradeRow.Fields;
        //    var u = Administration.UserRow.Fields;


        //    var response = new ExcelImportResponse();
        //    response.ErrorList = new List<string>();

        //    var worksheet = ep.Workbook.Worksheets[1];
        //   var previousId = -1;
        //    for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
        //    {
        //        try
        //        {
        //            var contactName = Convert.ToString(worksheet.Cells[row, 2].Value ?? "");
        //            if (contactName.IsTrimmedEmpty())
        //                continue;

        //            var contactTypeStr = Convert.ToString(worksheet.Cells[row, 1].Value ?? "");
        //            if (contactTypeStr.Equals("3"))
        //                continue;

        //            int cTypeValue = 0;

        //            if (!string.IsNullOrWhiteSpace(contactTypeStr))
        //            {
        //                Masters.CTypeMaster cType = (Masters.CTypeMaster)Enum.Parse(typeof(Masters.CTypeMaster), contactTypeStr);
        //                cTypeValue = (int)cType;
        //            }

        //            if (cTypeValue == 0) // Sub Contact
        //            {
        //                var contact = uow.Connection.TryFirst<SubContact>(q => q
        //                    .Select(sc.Id)
        //                    .Where(sc.Name == contactName && sc.ContactsId == previousId));

        //                if (contact == null)
        //                    contact = new SubContact
        //                    {
        //                        Name = contactName
        //                    };
        //                else
        //                {
        //                    contact.TrackWithChecks = false;
        //                }

        //                contact.Phone = Convert.ToString(worksheet.Cells[row, 3].Value ?? "");
        //                contact.Email = Convert.ToString(worksheet.Cells[row, 4].Value ?? "");
        //                contact.Address = Convert.ToString(worksheet.Cells[row, 5].Value ?? "");
        //                contact.Designation = Convert.ToString(worksheet.Cells[row, 11].Value ?? "");
        //                contact.ResidentialPhone = Convert.ToString(worksheet.Cells[row, 12].Value ?? "");

        //                var genderType = Convert.ToString(worksheet.Cells[row, 15].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(genderType))
        //                {
        //                    Masters.GenderMaster gender = (Masters.GenderMaster)Enum.Parse(typeof(Masters.GenderMaster), genderType);
        //                    int genderTypeValue = (int)gender;

        //                    contact.Gender = (Masters.GenderMaster)genderTypeValue;
        //                }
        //                else
        //                    contact.Gender = null;


        //                if (previousId < 0)
        //                {
        //                    throw new Exception("Contact name not found or is null\nImporting has been stopped");
        //                }
        //                else
        //                {
        //                    contact.ContactsId = previousId;
        //                }

        //                //Inserting
        //                if (contact.Id == null)
        //                {
        //                    var sub = new SubContactsController();
        //                    sub.Create(uow, new SaveWithLocalizationRequest<SubContact>
        //                    {
        //                        Entity = contact
        //                    });

        //                    response.Inserted = response.Inserted + 1;
        //                }
        //                else
        //                {
        //                    var sub = new SubContactsController();
        //                    sub.Update(uow, new SaveWithLocalizationRequest<SubContact>
        //                    {
        //                        Entity = contact,
        //                        EntityId = contact.Id.Value
        //                    });

        //                    response.Updated = response.Updated + 1;
        //                }
        //            }
        //            else
        //            {
        //                var contact = uow.Connection.TryFirst<MyRow>(q => q
        //                    .Select(c.Id)
        //                    .Where(c.Name == contactName));

        //                if (contact == null)
        //                    contact = new MyRow
        //                    {
        //                        Name = contactName
        //                    };
        //                else
        //                {
        //                    // avoid assignment errors
        //                    contact.TrackWithChecks = false;
        //                }

        //                //Enum type
        //                var contactType = Convert.ToString(worksheet.Cells[row, 1].Value ?? "");
        //                if (contactType.Equals("3"))
        //                    continue;

        //                contact.ContactType = (Masters.CTypeMaster)cTypeValue;

        //                contact.Phone = Convert.ToString(worksheet.Cells[row, 3].Value ?? "");
        //                contact.Email = Convert.ToString(worksheet.Cells[row, 4].Value ?? "");
        //                contact.Address = Convert.ToString(worksheet.Cells[row, 5].Value ?? "");

        //                var City = Convert.ToString(worksheet.Cells[row, 6].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(City))
        //                {
        //                    var city = uow.Connection.TryFirst<Mast.CityRow>(q => q
        //                        .Select(ct.Id)
        //                        .Where(ct.City == City));

        //                    if (city == null)
        //                    {
        //                        response.ErrorList.Add("Error On Row " + row + ": City '" +
        //                            City + "' not found!");
        //                        continue;
        //                    }

        //                    contact.CityId = city.Id.Value;
        //                }
        //                else
        //                    contact.CityId = null;

        //                var State = Convert.ToString(worksheet.Cells[row, 7].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(State))
        //                {
        //                    var state = uow.Connection.TryFirst<Mast.StateRow>(q => q
        //                        .Select(s.Id)
        //                        .Where(s.State == State));

        //                    if (state == null)
        //                    {
        //                        response.ErrorList.Add("Error On Row " + row + ": State '" +
        //                            State + "' not found!");
        //                        continue;
        //                    }

        //                    contact.StateId = state.Id.Value;
        //                }
        //                else
        //                    contact.StateId = null;

        //                contact.Pin = Convert.ToString(worksheet.Cells[row, 8].Value ?? "");

        //                var countryType = Convert.ToString(worksheet.Cells[row, 9].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(countryType))
        //                {
        //                    Masters.CountryMaster country = (Masters.CountryMaster)Enum.Parse(typeof(Masters.CountryMaster), countryType);
        //                    int countryTypeValue = (int)country;

        //                    contact.Country = (Masters.CountryMaster)countryTypeValue;
        //                }
        //                else
        //                    contact.Country = null;

        //                contact.Website = Convert.ToString(worksheet.Cells[row, 10].Value ?? "");
        //                contact.AdditionalInfo = Convert.ToString(worksheet.Cells[row, 11].Value ?? "");
        //                contact.ResidentialPhone = Convert.ToString(worksheet.Cells[row, 12].Value ?? "");
        //                contact.OfficePhone = Convert.ToString(worksheet.Cells[row, 13].Value ?? "");

        //                var genderType = Convert.ToString(worksheet.Cells[row, 14].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(genderType))
        //                {
        //                    Masters.GenderMaster gender = (Masters.GenderMaster)Enum.Parse(typeof(Masters.GenderMaster), genderType);
        //                    int genderTypeValue = (int)gender;

        //                    contact.Gender = (Masters.GenderMaster)genderTypeValue;
        //                }
        //                else
        //                    contact.Gender = null;

        //                var Area = Convert.ToString(worksheet.Cells[row, 15].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(Area))
        //                {
        //                    var area = uow.Connection.TryFirst<Mast.AreaRow>(q => q
        //                        .Select(a.Id)
        //                        .Where(a.Area == Area));

        //                    if (area == null)
        //                    {
        //                        response.ErrorList.Add("Error On Row " + row + ": Area '" +
        //                            Area + "' not found!");
        //                        continue;
        //                    }

        //                    contact.AreaId = area.Id.Value;
        //                }
        //                else
        //                    contact.AreaId = null;

        //                var Category = Convert.ToString(worksheet.Cells[row, 16].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(Category))
        //                {
        //                    var category = uow.Connection.TryFirst<Mast.CategoryRow>(q => q
        //                        .Select(cat.Id)
        //                        .Where(cat.Category == Category));

        //                    if (category == null)
        //                    {
        //                        response.ErrorList.Add("Error On Row " + row + ": Category '" +
        //                            Category + "' not found!");
        //                        continue;
        //                    }

        //                    contact.CategoryId = category.Id.Value;
        //                }
        //                else
        //                    contact.CategoryId = null;

        //                var Grade = Convert.ToString(worksheet.Cells[row, 17].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(Grade))
        //                {
        //                    var grade = uow.Connection.TryFirst<Mast.GradeRow>(q => q
        //                        .Select(g.Id)
        //                        .Where(g.Grade == Grade));

        //                    if (grade == null)
        //                    {
        //                        response.ErrorList.Add("Error On Row " + row + ": Grade '" +
        //                            Grade + "' not found!");
        //                        continue;
        //                    }

        //                    contact.GradeId = grade.Id.Value;
        //                }
        //                else
        //                    contact.GradeId = null;


        //                var tType = Convert.ToString(worksheet.Cells[row, 18].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(tType))
        //                {
        //                    Masters.TypeMaster type = (Masters.TypeMaster)Enum.Parse(typeof(Masters.TypeMaster), tType);
        //                    int tTypeValue = (int)type;

        //                    contact.Type = (Masters.TypeMaster)tTypeValue;
        //                }
        //                else
        //                    contact.Type = null;

        //                var Owner = Convert.ToString(worksheet.Cells[row, 19].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(Owner))
        //                {
        //                    var owner = uow.Connection.TryFirst<Administration.UserRow>(q => q
        //                        .Select(u.UserId)
        //                        .Where(u.Username == Owner));

        //                    if (owner == null)
        //                    {
        //                        response.ErrorList.Add("Error On Row " + row + ": Owner Name '" +
        //                            Owner + "' not found!");
        //                        continue;
        //                    }

        //                    contact.OwnerId = owner.UserId.Value;
        //                }
        //                else
        //                    contact.OwnerId = null;

        //                var Assigned = Convert.ToString(worksheet.Cells[row, 20].Value ?? "");
        //                if (!string.IsNullOrWhiteSpace(Assigned))
        //                {
        //                    var assigned = uow.Connection.TryFirst<Administration.UserRow>(q => q
        //                        .Select(u.UserId)
        //                        .Where(u.Username == Assigned));

        //                    if (assigned == null)
        //                    {
        //                        response.ErrorList.Add("Error On Row " + row + ": Assigned Name '" +
        //                            Assigned + "' not found!");
        //                        continue;
        //                    }

        //                    contact.AssignedId = assigned.UserId.Value;
        //                }
        //                else
        //                    contact.AssignedId = null;

        //                contact.GSTIN = Convert.ToString(worksheet.Cells[row, 21].Value ?? "");
        //                contact.PANNo = Convert.ToString(worksheet.Cells[row, 22].Value ?? "");

        //                contact.CCEmails = Convert.ToString(worksheet.Cells[row, 23].Value ?? "");
        //                contact.BCCEmails = Convert.ToString(worksheet.Cells[row, 24].Value ?? "");
        //                contact.Whatsapp = Convert.ToString(worksheet.Cells[row, 25].Value ?? "");

        //                //Inserting
        //                if (contact.Id == null)
        //                {
        //                    SaveResponse changes = new MyRepository().Create(uow, new SaveWithLocalizationRequest<MyRow>
        //                    {
        //                        Entity = contact
        //                    });

        //                    response.Inserted = response.Inserted + 1;
        //                    previousId = Convert.ToInt32(changes.EntityId);
        //                }
        //                else
        //                {
        //                    new MyRepository().Update(uow, new SaveWithLocalizationRequest<MyRow>
        //                    {
        //                        Entity = contact,
        //                        EntityId = contact.Id.Value
        //                    });

        //                    response.Updated = response.Updated + 1;
        //                    previousId = contact.Id.Value;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            response.ErrorList.Add("Exception on Row " + row + ": " + ex.Message);
        //        }
        //    }

        //    return response;
        //}
        [HttpPost, ServiceAuthorize("Contacts:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, ExcelImportRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentNullException("filename");

            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/"))
                throw new ArgumentOutOfRangeException("filename");

            ExcelPackage ep = new ExcelPackage();
            using (var fs = new FileStream(UploadHelper.DbFilePath(request.FileName), FileMode.Open, FileAccess.Read))
                ep.Load(fs);

            var c = MyRow.Fields;
            var sc = SubContact.Fields;
            var ct = CityRow.Fields;
            var s = StateRow.Fields;
            var a = AreaRow.Fields;
            var cat = CategoryRow.Fields;
            var g = GradeRow.Fields;
            var u = Administration.UserRow.Fields;


            var response = new ExcelImportResponse();
            response.ErrorList = new List<string>();

            var worksheet = ep.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new ValidationError("Uploaded excel file does not contain any worksheet");
            var previousId = -1;
            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var contactName = Convert.ToString(worksheet.Cells[row, 2].Value ?? "");
                    if (contactName.IsTrimmedEmpty())
                        continue;

                    var contactTypeStr = Convert.ToString(worksheet.Cells[row, 1].Value ?? "");
                    if (contactTypeStr.Equals("3"))
                        continue;

                    int cTypeValue = 0;

                    if (!string.IsNullOrWhiteSpace(contactTypeStr))
                    {
                        Masters.CTypeMaster cType = (Masters.CTypeMaster)Enum.Parse(typeof(Masters.CTypeMaster), contactTypeStr);
                        cTypeValue = (int)cType;
                    }

                    if (cTypeValue == 0) // Sub Contact
                    {
                        var contact = uow.Connection.TryFirst<SubContact>(q => q
                            .Select(sc.Id)
                            .Where(sc.Name == contactName && sc.ContactsId == previousId));

                        if (contact == null)
                            contact = new SubContact
                            {
                                Name = contactName
                            };
                        else
                        {
                            contact.TrackWithChecks = false;
                        }

                        contact.Phone = Convert.ToString(worksheet.Cells[row, 3].Value ?? "");
                        contact.Email = Convert.ToString(worksheet.Cells[row, 4].Value ?? "");
                        contact.Address = Convert.ToString(worksheet.Cells[row, 5].Value ?? "");
                        contact.Designation = Convert.ToString(worksheet.Cells[row, 11].Value ?? "");
                        contact.ResidentialPhone = Convert.ToString(worksheet.Cells[row, 12].Value ?? "");

                        var genderType = Convert.ToString(worksheet.Cells[row, 15].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(genderType))
                        {
                            Masters.GenderMaster gender = (Masters.GenderMaster)Enum.Parse(typeof(Masters.GenderMaster), genderType);
                            int genderTypeValue = (int)gender;

                            contact.Gender = (Masters.GenderMaster)genderTypeValue;
                        }
                        else
                            contact.Gender = null;


                        if (previousId < 0)
                        {
                            throw new Exception("Contact name not found or is null\nImporting has been stopped");
                        }
                        else
                        {
                            contact.ContactsId = previousId;
                        }

                        //Inserting
                        if (contact.Id == null)
                        {
                            var sub = new SubContactsController();
                            sub.Create(uow, new SaveWithLocalizationRequest<SubContact>
                            {
                                Entity = contact
                            });

                            response.Inserted = response.Inserted + 1;
                        }
                        else
                        {
                            var sub = new SubContactsController();
                            sub.Update(uow, new SaveWithLocalizationRequest<SubContact>
                            {
                                Entity = contact,
                                EntityId = contact.Id.Value
                            });

                            response.Updated = response.Updated + 1;
                        }
                    }
                    else
                    {
                        var contact = uow.Connection.TryFirst<MyRow>(q => q
                            .Select(c.Id)
                            .Where(c.Name == contactName));

                        if (contact == null)
                            contact = new MyRow
                            {
                                Name = contactName
                            };
                        else
                        {
                            // avoid assignment errors
                            contact.TrackWithChecks = false;
                        }

                        //Enum type
                        var contactType = Convert.ToString(worksheet.Cells[row, 1].Value ?? "");
                        if (contactType.Equals("3"))
                            continue;

                        contact.ContactType = (Masters.CTypeMaster)cTypeValue;

                        contact.Phone = Convert.ToString(worksheet.Cells[row, 3].Value ?? "");
                        contact.Email = Convert.ToString(worksheet.Cells[row, 4].Value ?? "");
                        contact.Address = Convert.ToString(worksheet.Cells[row, 5].Value ?? "");

                        var City = Convert.ToString(worksheet.Cells[row, 6].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(City))
                        {
                            var city = uow.Connection.TryFirst<Mast.CityRow>(q => q
                                .Select(ct.Id)
                                .Where(ct.City == City));

                            if (city == null)
                            {
                                response.ErrorList.Add("Error On Row " + row + ": City '" +
                                    City + "' not found!");
                                continue;
                            }

                            contact.CityId = city.Id.Value;
                        }
                        else
                            contact.CityId = null;

                        var State = Convert.ToString(worksheet.Cells[row, 7].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(State))
                        {
                            var state = uow.Connection.TryFirst<Mast.StateRow>(q => q
                                .Select(s.Id)
                                .Where(s.State == State));

                            if (state == null)
                            {
                                response.ErrorList.Add("Error On Row " + row + ": State '" +
                                    State + "' not found!");
                                continue;
                            }

                            contact.StateId = state.Id.Value;
                        }
                        else
                            contact.StateId = null;

                        contact.Pin = Convert.ToString(worksheet.Cells[row, 8].Value ?? "");

                        var countryType = Convert.ToString(worksheet.Cells[row, 9].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(countryType))
                        {
                            Masters.CountryMaster country = (Masters.CountryMaster)Enum.Parse(typeof(Masters.CountryMaster), countryType);
                            int countryTypeValue = (int)country;

                            contact.Country = (Masters.CountryMaster)countryTypeValue;
                        }
                        else
                            contact.Country = null;

                        contact.Website = Convert.ToString(worksheet.Cells[row, 10].Value ?? "");
                        contact.AdditionalInfo = Convert.ToString(worksheet.Cells[row, 11].Value ?? "");
                        contact.ResidentialPhone = Convert.ToString(worksheet.Cells[row, 12].Value ?? "");
                        contact.OfficePhone = Convert.ToString(worksheet.Cells[row, 13].Value ?? "");

                        var genderType = Convert.ToString(worksheet.Cells[row, 14].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(genderType))
                        {
                            Masters.GenderMaster gender = (Masters.GenderMaster)Enum.Parse(typeof(Masters.GenderMaster), genderType);
                            int genderTypeValue = (int)gender;

                            contact.Gender = (Masters.GenderMaster)genderTypeValue;
                        }
                        else
                            contact.Gender = null;

                        var Area = Convert.ToString(worksheet.Cells[row, 15].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(Area))
                        {
                            var area = uow.Connection.TryFirst<Mast.AreaRow>(q => q
                                .Select(a.Id)
                                .Where(a.Area == Area));

                            if (area == null)
                            {
                                response.ErrorList.Add("Error On Row " + row + ": Area '" +
                                    Area + "' not found!");
                                continue;
                            }

                            contact.AreaId = area.Id.Value;
                        }
                        else
                            contact.AreaId = null;

                        var Category = Convert.ToString(worksheet.Cells[row, 16].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(Category))
                        {
                            var category = uow.Connection.TryFirst<Mast.CategoryRow>(q => q
                                .Select(cat.Id)
                                .Where(cat.Category == Category));

                            if (category == null)
                            {
                                response.ErrorList.Add("Error On Row " + row + ": Category '" +
                                    Category + "' not found!");
                                continue;
                            }

                            contact.CategoryId = category.Id.Value;
                        }
                        else
                            contact.CategoryId = null;

                        var Grade = Convert.ToString(worksheet.Cells[row, 17].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(Grade))
                        {
                            var grade = uow.Connection.TryFirst<Mast.GradeRow>(q => q
                                .Select(g.Id)
                                .Where(g.Grade == Grade));

                            if (grade == null)
                            {
                                response.ErrorList.Add("Error On Row " + row + ": Grade '" +
                                    Grade + "' not found!");
                                continue;
                            }

                            contact.GradeId = grade.Id.Value;
                        }
                        else
                            contact.GradeId = null;


                        var tType = Convert.ToString(worksheet.Cells[row, 18].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(tType))
                        {
                            Masters.TypeMaster type = (Masters.TypeMaster)Enum.Parse(typeof(Masters.TypeMaster), tType);
                            int tTypeValue = (int)type;

                            contact.Type = (Masters.TypeMaster)tTypeValue;
                        }
                        else
                            contact.Type = null;

                        var Owner = Convert.ToString(worksheet.Cells[row, 19].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(Owner))
                        {
                            var owner = uow.Connection.TryFirst<Administration.UserRow>(q => q
                                .Select(u.UserId)
                                .Where(u.Username == Owner));

                            if (owner == null)
                            {
                                response.ErrorList.Add("Error On Row " + row + ": Owner Name '" +
                                    Owner + "' not found!");
                                continue;
                            }

                            contact.OwnerId = owner.UserId.Value;
                        }
                        else
                            contact.OwnerId = null;

                        var Assigned = Convert.ToString(worksheet.Cells[row, 20].Value ?? "");
                        if (!string.IsNullOrWhiteSpace(Assigned))
                        {
                            var assigned = uow.Connection.TryFirst<Administration.UserRow>(q => q
                                .Select(u.UserId)
                                .Where(u.Username == Assigned));

                            if (assigned == null)
                            {
                                response.ErrorList.Add("Error On Row " + row + ": Assigned Name '" +
                                    Assigned + "' not found!");
                                continue;
                            }

                            contact.AssignedId = assigned.UserId.Value;
                        }
                        else
                            contact.AssignedId = null;

                        contact.GSTIN = Convert.ToString(worksheet.Cells[row, 21].Value ?? "");
                        contact.PANNo = Convert.ToString(worksheet.Cells[row, 22].Value ?? "");

                        contact.CCEmails = Convert.ToString(worksheet.Cells[row, 23].Value ?? "");
                        contact.BCCEmails = Convert.ToString(worksheet.Cells[row, 24].Value ?? "");

                        //Inserting
                        if (contact.Id == null)
                        {
                            SaveResponse changes = new MyRepository(Context, _connections).Create(uow, new SaveWithLocalizationRequest<MyRow>
                            {
                                Entity = contact
                            });

                            response.Inserted = response.Inserted + 1;
                            //previousId = (int)changes.EntityId;
                            previousId = contact.Id.Value;
                        }
                        else
                        {
                            new MyRepository(Context, _connections).Update(uow, new SaveWithLocalizationRequest<MyRow>
                            {
                                Entity = contact,
                                EntityId = contact.Id.Value
                            });

                            response.Updated = response.Updated + 1;
                            previousId = contact.Id.Value;
                        }
                    }

                }
                catch (Exception ex)
                {
                    response.ErrorList.Add("Exception on Row " + row + ": " + ex.Message);
                }
            }

            return response;
        }

        //Excel import subcontacts
        [HttpPost, ServiceAuthorize("Contacts:Import")]
        public ExcelImportResponse ExcelImportSubContacts(IUnitOfWork uow, ExcelImportRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentNullException("filename");

            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/"))
                throw new ArgumentOutOfRangeException("filename");

            ExcelPackage ep = new ExcelPackage();
            using (var fs = new FileStream(UploadHelper.DbFilePath(request.FileName), FileMode.Open, FileAccess.Read))
                ep.Load(fs);

            var c = MyRow.Fields;

            var response = new ExcelImportResponse();
            response.ErrorList = new List<string>();

            var worksheet = ep.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new ValidationError("Uploaded excel file does not contain any worksheet");
            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var contactName = Convert.ToString(worksheet.Cells[row, 1].Value ?? "");
                    if (contactName.IsTrimmedEmpty())
                        continue;

                    var contact = uow.Connection.TryFirst<SubContact>(q => q
                        .Select(c.Id)
                        .Where(c.Name == contactName));

                    if (contact == null)
                        contact = new SubContact
                        {
                            Name = contactName
                        };
                    else
                    {

                        contact.TrackWithChecks = false;
                    }

                    contact.Phone = Convert.ToString(worksheet.Cells[row, 2].Value ?? "");

                    contact.ResidentialPhone = Convert.ToString(worksheet.Cells[row, 3].Value ?? "");
                    contact.Email = Convert.ToString(worksheet.Cells[row, 4].Value ?? "");
                    contact.Designation = Convert.ToString(worksheet.Cells[row, 5].Value ?? "");
                    contact.Address = Convert.ToString(worksheet.Cells[row, 6].Value ?? "");

                    var genderType = Convert.ToString(worksheet.Cells[row, 7].Value ?? "");
                    if (!string.IsNullOrWhiteSpace(genderType))
                    {
                        Masters.GenderMaster gender = (Masters.GenderMaster)Enum.Parse(typeof(Masters.GenderMaster), genderType);
                        int genderTypeValue = (int)gender;

                        contact.Gender = (Masters.GenderMaster)genderTypeValue;
                    }
                    else
                        contact.Gender = null;

                    var religionType = Convert.ToString(worksheet.Cells[row, 8].Value ?? "");
                    if (!string.IsNullOrWhiteSpace(religionType))
                    {
                        Masters.ReligionMaster religion = (Masters.ReligionMaster)Enum.Parse(typeof(Masters.ReligionMaster), religionType);
                        int religionTypeValue = (int)religion;

                        contact.Religion = (Masters.ReligionMaster)religionTypeValue;
                    }
                    else
                        contact.Religion = null;

                    var maritalType = Convert.ToString(worksheet.Cells[row, 9].Value ?? "");
                    if (!string.IsNullOrWhiteSpace(maritalType))
                    {
                        Masters.MaritalMaster marital = (Masters.MaritalMaster)Enum.Parse(typeof(Masters.MaritalMaster), maritalType);
                        int maritalTypeValue = (int)marital;

                        contact.MaritalStatus = (Masters.MaritalMaster)maritalTypeValue;
                    }
                    else
                        contact.MaritalStatus = null;

                    if (!string.IsNullOrEmpty(Convert.ToString(worksheet.Cells[row, 10].Value ?? "")))
                    {
                        var str = worksheet.Cells[row, 10].Value;
                        //var strdt = DateTime.ParseExact(Convert.ToString(worksheet.Cells[row, 10].Value ?? ""), "MM/dd/yyyy", null);
                        contact.MarriageAnniversary = Convert.ToDateTime(str);
                    }
                    else
                    {
                        contact.MarriageAnniversary = null;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(worksheet.Cells[row, 11].Value ?? "")))
                    {
                        var str = worksheet.Cells[row, 11].Value;
                        contact.Birthdate = Convert.ToDateTime(str);
                    }
                    else
                    {
                        contact.Birthdate = null;
                    }

                    var Contact = Convert.ToString(worksheet.Cells[row, 12].Value ?? "");
                    if (!string.IsNullOrWhiteSpace(Contact))
                    {
                        var contactname = uow.Connection.TryFirst<MyRow>(q => q
                            .Select(c.Id)
                            .Where(c.Name == Contact));

                        if (contactname == null)
                        {
                            response.ErrorList.Add("Error On Row " + row + ": Conatct Name '" +
                                Contact + "' not found!");
                            continue;
                        }

                        contact.ContactsId = contactname.Id.Value;
                    }
                    else
                        throw new Exception("Contact name not found or is null\nImporting has been stopped");


                    //Inserting
                    if (contact.Id == null)
                    {
                        var sub = new SubContactsController();
                        sub.Create(uow, new SaveWithLocalizationRequest<SubContact>
                        {
                            Entity = contact
                        });

                        response.Inserted = response.Inserted + 1;
                    }
                    else
                    {
                        var sub = new SubContactsController();
                        sub.Update(uow, new SaveWithLocalizationRequest<SubContact>
                        {
                            Entity = contact,
                            EntityId = contact.Id.Value
                        });

                        response.Updated = response.Updated + 1;
                    }

                }
                catch (Exception ex)
                {
                    response.ErrorList.Add("Exception on Row " + row + ": " + ex.Message);
                }
            }

            return response;
        }
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context, _connections).Retrieve(connection, request);
        }

        [ServiceAuthorize("Contacts:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ContactsListRequest request)
        {
            var data = List(connection, request).Entities;
            List<SubContactsRow> subCon = null;
            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i].ContactType == Masters.CTypeMaster.Individual)
                    continue;

                using (var conn = _connections.NewFor<SubContactsRow>())
                {
                    var s = SubContactsRow.Fields;
                    subCon = conn.List<SubContactsRow>(q => q
                        .SelectTableFields()
                        .Where(s.ContactsId == data[i].Id.Value)
                    );
                }

                if (subCon != null)
                {
                    foreach (var item in subCon)
                    {
                        ContactsRow c = new ContactsRow();

                        c.ContactType = null;
                        c.Name = item.Name;
                        c.Phone = item.Phone;
                        c.Email = item.Email;
                        c.Address = item.Address;
                        c.ResidentialPhone = item.ResidentialPhone;
                        c.Gender = item.Gender;
                        c.AdditionalInfo = item.Designation;
                        data.Insert(++i, c);
                    }
                }
            }
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.ContactsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Contacts_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost, ServiceAuthorize("Contacts:Import")]
        public ActionResult DownloadTemplate(IDbConnection connection, RetrieveRequest request)
        {
            string templateFile = Path.Combine(_env.ContentRootPath, "Templates", "Contacts_Template.xlsx");
            byte[] bytes = System.IO.File.ReadAllBytes(templateFile);

            var Output = File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Contacts_Template.xlsx");
            return Output;
        }
    }
}
