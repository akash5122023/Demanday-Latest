
using System;
using System.Linq;

using MailChimp.Net;
using System.Threading.Tasks;
using MailChimp.Net.Core;
using System.Net;
using MailChimp.Net.Models;
using System.Collections.Generic;
using AdvanceCRM.Modules.MailChimp.Models;
using Serenity.Data;
using System.Collections.ObjectModel;
using System.Collections;
using AdvanceCRM.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;


//namespace MailChipNetDemo.Controllers
namespace AdvanceCRM.Modules.MailChimp
{
    [Route("MailChimp"), Route("{action=GetList}")]
    [ReadPermission("MailChimp:Inbox")]

    public class MailChimpController : Controller
    {

        private readonly ISqlConnections _connections;
        private readonly MailChimpManager _manager;
        private string assignKey()
        {
            MailChimpConfigurationRow config;
            using (var connection = _connections.NewFor<MailChimpConfigurationRow>())
            {
                var s = MailChimpConfigurationRow.Fields;
                config = connection.TryById<MailChimpConfigurationRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.ApiKey));
            }
            return config?.ApiKey ?? string.Empty;
        }
        private readonly ILogger<MailChimpController> _logger;
        public MailChimpController(ISqlConnections connections, IOptions<MailChimpSettings> mailChimpConfig, ILogger<MailChimpController> logger)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            if (mailChimpConfig == null)
                throw new ArgumentNullException(nameof(mailChimpConfig));

            var apiKey = mailChimpConfig.Value.ApiKey;
            if (string.IsNullOrWhiteSpace(apiKey) || apiKey == "APIKey")
            {
                apiKey = assignKey();
            }

            _manager = new MailChimpManager(apiKey);
        }



        public object Mailchimp { get; private set; }
        //Get List
        [HttpGet, Route("~/MailChimp/GetList")]
        public async Task<IActionResult> GetList(string sort)
        {
            var listoptions = new ListRequest
            {
                Limit = 1000
            };
            IList<global::MailChimp.Net.Models.List> model = new List<global::MailChimp.Net.Models.List>();
            try
            {
                model = (await _manager.Lists.GetAllAsync(listoptions)).ToList();
                if (sort == "BydateAsc")
                {
                    model = model.OrderBy(x => x.DateCreated).ToList();
                }
                else if (sort == "BydateDesc")
                {
                    model = model.OrderByDescending(x => x.DateCreated).ToList();
                }
                else if (sort == "Asc")
                {
                    model = model.OrderBy(x => x.Name).ToList();
                }
                else if (sort == "Desc")
                {
                    model = model.OrderByDescending(x => x.Name).ToList();
                }
                else
                {
                    model = model.OrderByDescending(x => x.DateCreated).ToList();
                }
                return View(MVC.Views.MailChimp.Views.GetList, model);
            }
            catch (MailChimpException mce)
            {

                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return View(MVC.Views.MailChimp.Views.GetList, model);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.GetList, model);
            }
        }

        //create list
        [HttpGet]
        public IActionResult CreateList()
        {
            ModelList Lmodel = new ModelList();

            return View(MVC.Views.MailChimp.Views.CreateList, Lmodel);
        }

        //create list
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateList(ModelList Lmodel)
        {
            var list = new List
            {
                Name = Lmodel.Name,
                Contact = new Contact
                {
                    Company = Lmodel.Company,
                    Address1 = Lmodel.Address1,
                    Address2 = Lmodel.Address2,
                    City = Lmodel.City,
                    State = Lmodel.State,
                    Zip = Lmodel.Zip,
                    Country = Lmodel.ListCountry,
                    Phone = Lmodel.Phone
                },
                PermissionReminder = Lmodel.PermissionReminder,
                CampaignDefaults = new CampaignDefaults
                {
                    FromEmail = Lmodel.FromEmail,
                    FromName = Lmodel.FromName,
                    Subject = "Email message from dynamically created List",
                    Language = "en"
                },
                EmailTypeOption = true,
                NotifyOnSubscribe = Lmodel.NotifyOnSubscribe,
                NotifyOnUnsubscribe = Lmodel.NotifyOnUnsubscribe
            };
            try
            {
                if (ModelState.IsValid)
                {
                    var model = await _manager.Lists.AddOrUpdateAsync(list);
                    TempData["message"] = "List added successfully";
                    return RedirectToAction(nameof(GetList));
                }
                return View(MVC.Views.MailChimp.Views.CreateList, Lmodel);
                //return View(Lmodel);             

            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return View(MVC.Views.MailChimp.Views.CreateList, Lmodel);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.CreateList, Lmodel);
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Edit list
        [HttpGet]
        public async Task<IActionResult> EditList(string id)
        {
            ModelList Lmodel = new ModelList();
            var model = (await _manager.Lists.GetAsync(id));
            try
            {
                Lmodel.Id = model.Id;
                Lmodel.Name = model.Name;
                Lmodel.Address1 = model.Contact.Address1;
                Lmodel.Address2 = model.Contact.Address2;
                Lmodel.ListCountry = model.Contact.Country;
                Lmodel.City = model.Contact.City;
                Lmodel.State = model.Contact.State;
                Lmodel.Zip = model.Contact.Zip;
                Lmodel.Phone = model.Contact.Phone;
                Lmodel.Company = model.Contact.Company;
                Lmodel.PermissionReminder = model.PermissionReminder;
                Lmodel.FromEmail = model.CampaignDefaults.FromEmail;
                Lmodel.FromName = model.CampaignDefaults.FromName;
                Lmodel.Subject = "Email message from dynamically created List";
                Lmodel.Language = "en";
                Lmodel.NotifyOnSubscribe = model.NotifyOnSubscribe;
                Lmodel.NotifyOnUnsubscribe = model.NotifyOnUnsubscribe;
                return View(MVC.Views.MailChimp.Views.EditList, Lmodel);
                //return View(Lmodel);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return View(MVC.Views.MailChimp.Views.EditList, Lmodel);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.EditList, Lmodel);
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Edit List
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditList(ModelList Lmodel)
        {
            var list = new List
            {
                Id = Lmodel.Id,
                Name = Lmodel.Name,
                Contact = new Contact
                {
                    Company = Lmodel.Company,
                    Address1 = Lmodel.Address1,
                    Address2 = Lmodel.Address2,
                    City = Lmodel.City,
                    State = Lmodel.State,
                    Zip = Lmodel.Zip,
                    Country = Lmodel.ListCountry,
                    Phone = Lmodel.Phone
                },
                PermissionReminder = Lmodel.PermissionReminder,
                CampaignDefaults = new CampaignDefaults
                {
                    FromEmail = Lmodel.FromEmail,
                    FromName = Lmodel.FromName,
                    Subject = "Email message from dynamically created List",
                    Language = "en"
                },
                EmailTypeOption = true,
                NotifyOnUnsubscribe = Lmodel.NotifyOnUnsubscribe,
                NotifyOnSubscribe = Lmodel.NotifyOnSubscribe
            };
            try
            {
                if (ModelState.IsValid)
                {
                    var model = await _manager.Lists.AddOrUpdateAsync(list);
                    TempData["message"] = "List Updated successfully";
                    return RedirectToAction(nameof(GetList));
                }
                return View(MVC.Views.MailChimp.Views.EditList, Lmodel);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return View(MVC.Views.MailChimp.Views.EditList, Lmodel);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.EditList, Lmodel);
                // return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Delete single List 
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _manager.Lists.DeleteAsync(id);
                TempData["message"] = "List deleted successfully";
                return RedirectToAction(nameof(GetList));
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Detail.ToString();
                return RedirectToAction(nameof(GetList));
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return RedirectToAction(nameof(GetList));
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        public async Task<IActionResult> DetailList(string id)
        {
            var model = (await _manager.Lists.GetAsync(id));
            try
            {
                ViewBag.ListObject = model;
                return PartialView(MVC.Views.MailChimp.Views.DetailList);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return View(MVC.Views.MailChimp.Views.DetailList, model);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.DetailList, model);
            }

        }

        //Get Subscriber List
        public async Task<IActionResult> Members(string id)
        {
            try
            {
                var model = await _manager.Members.GetAllAsync(id, new MemberRequest { Limit = 1000 });
                //var model = await _manager.Members.GetAllAsync(id, new MemberRequest { Limit = 100, Status = Status.Subscribed });               
                //return View(model);
                return View(MVC.Views.MailChimp.Views.Members, model);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return RedirectToAction(MVC.Views.MailChimp.Views.Members, new {id=id});
                return RedirectToAction(nameof(Members), new { id = id });
                // return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return RedirectToAction(MVC.Views.MailChimp.Views.Members, new { id = id });
                return RedirectToAction(nameof(Members), new { id = id });
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }
        //Add Subscriber
        [HttpGet]
        public IActionResult AddSubscriber(string id)
        {
            ModelSubscriber SModel = new ModelSubscriber();
            SModel.ListId = id;
            return View(MVC.Views.MailChimp.Views.AddSubscriber, SModel);
            //return View(SModel);

        }
        //Add Subscriber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubscriber(string id, ModelSubscriber SModel)
        {
            try
            {
                SModel.ListId = id;
                var member = new Member
                {
                    EmailAddress = SModel.EmailAddress,
                    Status = Status.Subscribed,
                    StatusIfNew = Status.Subscribed,
                    ListId = SModel.ListId,
                    EmailType = "html",
                    IpSignup = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    TimestampSignup = DateTime.UtcNow.ToString("s"),
                    MergeFields = new Dictionary<string, object>
                {
                    {"FNAME",SModel.FirstName },
                    {"LNAME",SModel.LastName }
                }
                };
                if (ModelState.IsValid)
                {
                    var result = await _manager.Members.AddOrUpdateAsync(SModel.ListId, member);
                    TempData["message"] = "Subscriber added successfully";
                    return RedirectToAction(nameof(GetList));
                }
                //return View(SModel);               
                return View(MVC.Views.MailChimp.Views.AddSubscriber, SModel);
            }
            catch (MailChimpException mce)
            {
                if (!string.IsNullOrEmpty(mce.Title))
                {
                    TempData["Errormessage"] = mce.Title;

                }
                return RedirectToAction(nameof(AddSubscriber), new { id = id, SModel });
                //return View(MVC.Views.MailChimp.Views.AddSubscriber);
                //return View("AddSubscriber",new { id=id,SModel});               
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View("AddSubscriber", new { id = id, SModel });
                return RedirectToAction(nameof(AddSubscriber), new { id = id, SModel });

            }
        }

        //Add Subscriber
        [HttpGet, Route("~/MailChimp/NewSubscriber")]
        public async Task<IActionResult> NewSubscriber()
        {
            ModelSubscriber SModel = new ModelSubscriber();
            var listoptions = new ListRequest
            {
                Limit = 1000
            };
            try
            {
                var model = await _manager.Lists.GetAllAsync(listoptions);
                ViewBag.ListData = model;
                foreach (var role in ViewBag.ListData)
                {
                    SModel.ListId = role.Id;
                    SModel.Description = role.Name;
                }
                ViewBag.ListData1 = SModel;
                // return View(SModel);
                return View(MVC.Views.MailChimp.Views.NewSubscriber, SModel);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return View(SModel);  
                return View(MVC.Views.MailChimp.Views.NewSubscriber, SModel);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(SModel);               
                return View(MVC.Views.MailChimp.Views.NewSubscriber, SModel);
            }

        }

        //Add Subscriber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewSubscriber(ModelSubscriber SModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var member = new Member
                    {
                        EmailAddress = SModel.EmailAddress,
                        Status = Status.Subscribed,
                        StatusIfNew = Status.Subscribed,
                        ListId = SModel.ListId,
                        EmailType = "html",
                        IpSignup = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        TimestampSignup = SModel.TimestampSignup,
                        MergeFields = new Dictionary<string, object>
                {
                    {"FNAME",SModel.FirstName },
                    {"LNAME",SModel.LastName }
                }
                    };
                    var result = await _manager.Members.AddOrUpdateAsync(SModel.ListId, member);
                    TempData["message"] = "Subscriber added successfully";
                    return RedirectToAction(nameof(GetList));
                }
                var listoptions = new ListRequest
                {
                    Limit = 1000
                };
                var model = await _manager.Lists.GetAllAsync(listoptions);
                ViewBag.ListData = model;
                //return View("NewSubscriber");
                return View(MVC.Views.MailChimp.Views.NewSubscriber);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return View(SModel);
                return View(MVC.Views.MailChimp.Views.NewSubscriber, SModel);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(SModel);
                return View(MVC.Views.MailChimp.Views.NewSubscriber, SModel);
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Details of Member
        public async Task<IActionResult> DetailSubscriber(string EmailId, string listId)
        {
            var model = await _manager.Members.GetAsync(listId, EmailId);
            try
            {
                return View(MVC.Views.MailChimp.Views.DetailSubscriber, model);
            }
            catch (MailChimpException mce)
            {

                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return View(MVC.Views.MailChimp.Views.DetailSubscriber, model);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.DetailSubscriber, model);
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Update Subscriber
        [HttpGet]
        public async Task<IActionResult> UpdateSubscriber(string listId, string EmailAddress)
        {
            ModelSubscriber SModel = new ModelSubscriber();
            try
            {
                var model = await _manager.Members.GetAsync(listId, EmailAddress);
                SModel.Id = model.Id;
                SModel.EmailAddress = model.EmailAddress;
                SModel.ListId = model.ListId;
                SModel.FirstName = model.MergeFields["FNAME"].ToString();
                SModel.LastName = model.MergeFields["LNAME"].ToString();
                return View(MVC.Views.MailChimp.Views.UpdateSubscriber, SModel);
            }
            catch (MailChimpException mce)
            {

                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return View(MVC.Views.MailChimp.Views.UpdateSubscriber, SModel);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.UpdateSubscriber, SModel);
            }
        }

        //Update Subscriber
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSubscriber(ModelSubscriber SModel)
        {
            try
            {
                //SModel.EmailAddress = EmailAddress;
                //SModel.ListId = listId;
                var member = new Member
                {
                    Id = SModel.Id,
                    EmailAddress = SModel.EmailAddress,
                    Status = Status.Subscribed,
                    ListId = SModel.ListId,
                    EmailType = "html",
                    IpSignup = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    TimestampSignup = DateTime.UtcNow.ToString("s"),
                    MergeFields = new Dictionary<string, object>
                    {
                        {"FNAME",SModel.FirstName },
                        {"LNAME",SModel.LastName }
                    }
                };
                if (ModelState.IsValid)
                {
                    var result = await _manager.Members.AddOrUpdateAsync(SModel.ListId, member);
                    TempData["message"] = "Subscriber Updated successfully";
                    return RedirectToAction(nameof(Members), new { id = SModel.ListId });
                }
                //return View(SModel);
                return View(MVC.Views.MailChimp.Views.UpdateSubscriber, SModel);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return View(SModel);              
                return View(MVC.Views.MailChimp.Views.UpdateSubscriber, SModel);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(SModel);    
                return View(MVC.Views.MailChimp.Views.UpdateSubscriber, SModel);
            }
        }


        //Deleting Subscriber
        public async Task<IActionResult> DeleteSubscriber(string listId, string EmailAddress)
        {
            try
            {
                await _manager.Members.DeleteAsync(listId, EmailAddress);
                TempData["message"] = "Subscriber deleted successfully";
                return RedirectToAction(nameof(Members), new { id = listId });
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return RedirectToAction(nameof(Members), new { id = listId });
                // return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return RedirectToAction(nameof(Members), new { id = listId });
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Unsubscribe member        
        public async Task<IActionResult> Unsubscribe(string listId, string EmailAddress, string flag)
        {
            ModelSubscriber SModel = new ModelSubscriber();
            var model = await _manager.Members.GetAsync(listId, EmailAddress);
            SModel.Id = model.Id;
            SModel.EmailAddress = model.EmailAddress;
            SModel.ListId = model.ListId;
            SModel.FirstName = model.MergeFields["FNAME"].ToString();
            SModel.LastName = model.MergeFields["LNAME"].ToString();
            SModel.TimestampSignup = model.TimestampSignup;
            var member = new Member { };
            if (flag == "Unsubscribed")
            {
                member = new Member
                {
                    Id = SModel.Id,
                    EmailAddress = SModel.EmailAddress,
                    Status = Status.Unsubscribed,
                    ListId = SModel.ListId,
                    EmailType = "html",
                    IpSignup = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    TimestampSignup = SModel.TimestampSignup,
                    MergeFields = new Dictionary<string, object>
                {
                    {"FNAME", SModel.FirstName },
                    {"LNAME", SModel.LastName }
                }
                };
            }
            else
            {
                member = new Member
                {
                    Id = SModel.Id,
                    EmailAddress = SModel.EmailAddress,
                    Status = Status.Subscribed,
                    ListId = SModel.ListId,
                    EmailType = "html",
                    IpSignup = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    TimestampSignup = SModel.TimestampSignup,
                    MergeFields = new Dictionary<string, object>
                {
                    {"FNAME", SModel.FirstName },
                    {"LNAME", SModel.LastName }
                }
                };

            }
            try
            {
                var result = await _manager.Members.AddOrUpdateAsync(SModel.ListId, member);
                TempData["message"] = flag + " " + "successfully";
                return RedirectToAction(nameof(GetList));

            }
            catch (MailChimpException mce)
            {
                if (mce.Errors.Count > 0)
                {
                    TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                }
                else
                {
                    TempData["Errormessage"] = mce.Detail.ToString();
                }
                return RedirectToAction(nameof(DetailSubscriber), new { EmailId = EmailAddress, listId = listId });
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return RedirectToAction(nameof(DetailSubscriber), new { EmailId = EmailAddress, listId = listId });
            }
        }

        //TODO:Search Subscriber later now not implemented
        //Search Member
        [HttpGet]
        public async Task<IActionResult> SearchSubscriber(string search)
        {
            var searchoptions = new MemberSearchRequest
            {

            };

            var model = await _manager.Members.SearchAsync(searchoptions);
            return View();
            //return View(MVC.Views.MailChimp.Views.SearchSubscriber);
        }

        // Send Single Campaign
        public async Task<IActionResult> SendCampaign(string campaignId)
        {
            try
            {
                await _manager.Campaigns.SendAsync(campaignId);
                TempData["message"] = "Campaign Send successfully";
                return RedirectToAction(nameof(SentCampaigns), new { sort = "Recent", status = "" });
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return RedirectToAction(nameof(SentCampaigns), new { sort = "Recent", status = "" });
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return RedirectToAction(nameof(SentCampaigns), new { sort = "Recent", status = "" });
            }

        }

        //List of all Sent Campaigns
        [HttpGet, Route("~/MailChimp/SentCampaigns")]
        public async Task<IActionResult> SentCampaigns(string sort, string status)
        {
            IEnumerable<Campaign> model = new List<Campaign>();
            try
            {
                var options = new CampaignRequest
                {
                };
                if (status == "Draft")
                {
                    options = new CampaignRequest
                    {
                        Status = CampaignStatus.Save
                    };

                }
                model = await _manager.Campaigns.GetAllAsync();
                if (sort == "" || sort == null || sort == "Recent")
                {
                    model = model.OrderByDescending(x => x.CreateTime).ToList();
                }

                else if (sort == "Asc")
                {

                    model = model.OrderBy(x => x.Settings.Title).ToList();

                }
                else if (sort == "Desc")
                {
                    model = model.OrderByDescending(x => x.Settings.Title).ToList();

                }
                else if (sort == "Sent")
                {
                    options = new CampaignRequest
                    {
                        Status = CampaignStatus.Sent,
                        SortOrder = CampaignSortOrder.ASC,
                        //Limit = 1000
                    };
                    model = await _manager.Campaigns.GetAllAsync(options);
                }
                else if (sort == "Draft")
                {
                    options = new CampaignRequest
                    {
                        Status = CampaignStatus.Save,
                        SortOrder = CampaignSortOrder.DESC,
                        // Limit = 1000
                    };
                    model = await _manager.Campaigns.GetAllAsync(options);
                }
                else if (sort == "CreatedDate")
                {
                    model = model.OrderBy(x => x.CreateTime).ToList();

                }
                else if (sort == "Schedule")
                {
                    options = new CampaignRequest
                    {
                        Status = CampaignStatus.Schedule,
                        SortOrder = CampaignSortOrder.ASC,
                        // Limit = 1000
                    };
                    model = await _manager.Campaigns.GetAllAsync(options);

                }
                return View(MVC.Views.MailChimp.Views.SentCampaigns, model);

            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return View(MVC.Views.MailChimp.Views.SentCampaigns, model);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.SentCampaigns, model);
            }
        }

        //Details of Campaign
        public async Task<IActionResult> CampaignDetails(string id)
        {
            try
            {
                var model = await _manager.Campaigns.GetAsync(id);
                var ListDetails = await _manager.Lists.GetAsync(model.Recipients.ListId.ToString());
                ViewBag.CampaignObject = model;
                ViewBag.ListObject = ListDetails;
                //return PartialView("CampaignDetails");
                return PartialView(MVC.Views.MailChimp.Views.CampaignDetails);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return RedirectToAction(nameof(CampaignDetails),new { id= id });
                return RedirectToAction(MVC.Views.MailChimp.Views.CampaignDetails, new { id = id });

            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return RedirectToAction(nameof(CampaignDetails), new { id = id });
                return RedirectToAction(MVC.Views.MailChimp.Views.CampaignDetails, new { id = id });
            }

        }

        //Send CheckList for Campaign
        [HttpGet]
        public async Task<IActionResult> SendCheckList(string CampaignId)
        {
            ModelCheckList ModelCheck = new ModelCheckList();
            try
            {
                ModelCheck.campaignId = CampaignId;
                //ModelCheck.templateId = TemplateId;              
                var Campaigndetail = await _manager.Campaigns.GetAsync(ModelCheck.campaignId);
                int index = Campaigndetail.DashboardLink.LastIndexOf('=');
                ModelCheck.TemplateResolveId = Campaigndetail.DashboardLink.Substring(index + 1);
                var result = await _manager.Campaigns.SendChecklistAsync(CampaignId);
                ViewBag.CheckList = result;
                if (result.IsReady == true)
                {
                    ModelCheck.CheckReady = result.IsReady;

                }
                // return View(ModelCheck);
                return View(MVC.Views.MailChimp.Views.SendCheckList, ModelCheck);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return RedirectToAction(nameof(SendCheckList), new { CampaignId = CampaignId });
                //return View(ModelCheck); 
                return View(MVC.Views.MailChimp.Views.SendCheckList, ModelCheck);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(ModelCheck);
                return View(MVC.Views.MailChimp.Views.SendCheckList, ModelCheck);
            }
        }

        //Send CheckList for Campaign
        public IActionResult SendCheckListCamapign(string CampaignId, string status)
        {
            try
            {
                if (status == "Send")
                {
                    TempData["message"] = "Campaign Updated successfully";
                    return RedirectToAction(nameof(SendCampaign), new { campaignId = CampaignId });
                }
                else
                {
                    return RedirectToAction(nameof(SentCampaigns), new { campaignId = CampaignId, status = "Draft" });
                }
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return RedirectToAction(nameof(SendCheckListCamapign), new { CampaignId = CampaignId, status=status });
                return RedirectToAction(nameof(SendCheckList), new { CampaignId = CampaignId });
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return RedirectToAction(nameof(SendCheckList), new { CampaignId = CampaignId });
            }
        }

        //Schedule Campaign
        //TODO:TimeZone
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ScheduleCampaign(string campaignId, DateTime ScheduleDate)
        {
            ScheduleDate = ScheduleDate.AddHours(-5).AddMinutes(-30);
            var OptionRequest = new CampaignScheduleRequest
            {
                ScheduleTime = ScheduleDate.ToString()
            };
            try
            {
                await _manager.Campaigns.ScheduleAsync(campaignId, OptionRequest);
                return RedirectToAction(nameof(SentCampaigns), new { campaignId = campaignId, status = "Schedule" });

            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return RedirectToAction(nameof(SendCheckList), new { CampaignId = campaignId });
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return RedirectToAction(nameof(SendCheckList), new { CampaignId = campaignId });
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //TODO:Replicate Campaign Only After Discussion 
        //Replicate Campaign
        public async Task<IActionResult> ReplicateCampaign()
        {
            try
            {
                await _manager.Campaigns.ReplicateCampaignAsync("7d395fc162");
                return RedirectToAction(nameof(SentCampaigns));
            }
            catch (MailChimpException mce)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Get List of all Reports 
        [HttpGet, Route("~/MailChimp/Reports")]
        public async Task<IActionResult> GetReportsList()
        {
            try
            {
                var model = await _manager.Reports.GetAllReportsAsync();
                return View(MVC.Views.MailChimp.Views.GetReportsList, model);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault()?.Message ?? mce.Message;
                return View(MVC.Views.MailChimp.Views.GetReportsList, new List<Report>());
                // return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.GetReportsList, new List<Report>());
                // return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }

        }

        //TODO:Unsubscribe List later after discussion with Vinay
        //Get UnSubscriber List
        public async Task<IActionResult> UnSubscriberList()
        {
            try
            {
                var model = await _manager.Reports.GetUnsubscriberAsync("51f58c9b96", "heena@landmarksol.com");
                return View(model);

            }
            catch (MailChimpException mce)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.ServiceUnavailable, ex.Message);
            }

        }

        //Get overview of campaign 
        //TODO:Map and Barchart
        public async Task<IActionResult> GetCampaignSummary(string CampaignId)
        {
            ModelReport MReport = new ModelReport();
            try
            {
                var ReportAsync = await _manager.Reports.GetReportAsync(CampaignId);
                MReport.Id = ReportAsync.Id;
                MReport.CampaignTitle = ReportAsync.CampaignTitle;
                MReport.SubjectLine = ReportAsync.SubjectLine;
                MReport.SendTime = ReportAsync.SendTime;
                //MReport.ListStatsOpenRate = ReportAsync.ListStats.OpenRate;
                MReport.ListStatsOpenRate = (ReportAsync.Opens.OpenRate) * 100;
                MReport.ListStatsClickRate = (ReportAsync.Clicks.ClickRate) * 100;
                MReport.AbuseReports = ReportAsync.AbuseReports;
                MReport.LastClick = ReportAsync.Clicks.LastClick;
                MReport.Unsubscribed = ReportAsync.Unsubscribed;
                MReport.UniqueOpens = ReportAsync.Opens.UniqueOpens;
                MReport.UniqueClicks = ReportAsync.Clicks.UniqueClicks;
                MReport.ForwardsCount = ReportAsync.Forwards.ForwardsCount;
                MReport.LastOpen = ReportAsync.Opens.LastOpen;
                MReport.OpensTotal = ReportAsync.Opens.OpensTotal;
                MReport.ClicksTotal = ReportAsync.Clicks.ClicksTotal;
                MReport.EmailsSent = ReportAsync.EmailsSent;
                var Details = await _manager.Campaigns.GetAsync(CampaignId);
                MReport.ViewEmail = Details.ArchiveUrl;
                var ListDetails = await _manager.Lists.GetAsync(Details.Recipients.ListId.ToString());
                MReport.ListName = ListDetails.Name;
                MReport.MemberCount = ListDetails.Stats.MemberCount;
                var SentToRecipients = await _manager.Reports.GetSentToRecipientsAsync(CampaignId);
                var sortorderdesc = SentToRecipients.OrderByDescending(x => x.OpenCount).Where(x => x.OpenCount > 0).ToList();
                ViewBag.SentToRecipients = sortorderdesc;

                //TODO:EmailActivities,AbuseReport,LocationDetails,ClickReport,Domain Performance,Activity Report
                //var LocationsAsync = await _manager.Reports.GetLocationsAsync(CampaignId);
                //ViewBag.LocationsAsync = LocationsAsync;
                //var AllReportsAsync = await _manager.Reports.GetAllReportsAsync();
                //ViewData["AllReportsAsync"] = AllReportsAsync;
                //var ResponseAsync = await _manager.Reports.GetResponseAsync();
                //ViewData["ResponseAsync"] = ResponseAsync;
                //var AbuseReportAsync = await _manager.Reports.GetClickReportMembersAsync()
                //ViewData["AbuseReportAsync"] = AbuseReportAsync;
                //var AbuseReportAsync = await _manager.Reports.GetAbuseReportAsync(CampaignId);
                //ViewData["AbuseReportAsync"] = AbuseReportAsync;
                //var model = await _manager.Reports.GetEmailActivitiesResponseAsync(CampaignId);
                //ViewData["EmailActivitiesResponse"] = model;
                //var CampAdvice = await _manager.Reports.GetCampaignAdviceAsync(CampaignId);
                //ViewData["CampaignAdviceSync"] = CampAdvice;
                //var ClickReport = await _manager.Reports.GetClickReportAsync(CampaignId);
                //ViewData["ClickReportAsync"] = ClickReport;
                //var ClickReportDetails = await _manager.Reports.GetClickReportDetailsAsync(CampaignId, "LinkId");
                //ViewData["ClickReportDetails"] = ClickReportDetails;
                //var DomainPerformance = await _manager.Reports.GetDomainPerformanceAsync(CampaignId);
                //ViewData["DomainPerformance"] = DomainPerformance;               
                //var EeplUrl = await _manager.Reports.GetEepUrlReportAsync(CampaignId);
                //ViewData["EeplUrlReport"] = EeplUrl; 
                //return View(MReport);
                return View(MVC.Views.MailChimp.Views.GetCampaignSummary, MReport);

            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                // return View(MReport);
                return View(MVC.Views.MailChimp.Views.GetCampaignSummary, MReport);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(MReport);
                return View(MVC.Views.MailChimp.Views.GetCampaignSummary, MReport);
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }

        }

        //Recipient list
        [HttpGet]
        public async Task<IActionResult> RecipientList(string CampaignId, string flag)
        {
            try
            {
                ViewBag.flag = flag;
                ViewBag.CampaignId = CampaignId;
                var SentToRecipients = await _manager.Reports.GetSentToRecipientsAsync(CampaignId);
                var Details = await _manager.Campaigns.GetAsync(CampaignId);
                var RecipientsDetails = await _manager.Members.GetAllAsync(Details.Recipients.ListId.ToString());
                ViewBag.SentToRecipients = SentToRecipients;
                ViewBag.RecipientsDetails = RecipientsDetails;
                //return View();
                return View(MVC.Views.MailChimp.Views.RecipientList);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return RedirectToAction(nameof(RecipientList), new { CampaignId = CampaignId, flag = flag });
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return RedirectToAction(nameof(RecipientList), new { CampaignId = CampaignId, flag = flag });

            }

        }

        //Subscriber Open Activity
        [HttpGet]
        public async Task<IActionResult> SubscriberActivity(string CampaignId, string EmailAddress)
        {

            try
            {
                var EmailActivityAsync = await _manager.Reports.GetEmailActivityAsync(CampaignId, EmailAddress);
                ViewBag.EmailActivityAsync = EmailActivityAsync;
                return View(MVC.Views.MailChimp.Views.SubscriberActivity);
                // return View();
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return RedirectToAction(nameof(SubscriberActivity), new { CampaignId = CampaignId, EmailAddress = EmailAddress });
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return RedirectToAction(nameof(SubscriberActivity), new { CampaignId = CampaignId, EmailAddress = EmailAddress });
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Getting Default values of campaign
        [HttpGet]
        public async Task<JsonResult> GetDefaultCampaign(string id)
        {
            var model = (await _manager.Lists.GetAsync(id));
            try
            {
                ViewBag.ListObject = model;

            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return View(model);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(model);
            }
            return Json(model);
            // return new JsonResult { Data = model, JsonRequestBehavior.AllowGet };
        }

        //Get list of list
        [HttpGet]
        public async Task<IActionResult> GetListofList()
        {
            ModelCampaign CModel = new ModelCampaign();
            //List<IEnumerable> ModelList;
            try
            {
                ViewBag.Data = (List<IEnumerable>)await _manager.Lists.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return ViewBag.Data;
        }
        //Create Campaign
        //TODO:Facebook and Twitter integration to campaigns
        [HttpGet]
        public async Task<IActionResult> CreateCampaigns(int TemplateId)
        {
            ModelCampaign CModel = new ModelCampaign();
            try
            {

                var listoptions = new ListRequest
                {
                    Limit = 1000
                };
                var ModelList = await _manager.Lists.GetAllAsync(listoptions);
                var NewList = ModelList.OrderBy(x => x.Stats.MemberCount).Where(x => x.Stats.MemberCount > 0).ToList();
                ViewBag.ListData = NewList;
                if (TemplateId != 0)
                {
                    CModel.TemplateId = TemplateId;
                }
                var ModelTemplate = await _manager.Templates.GetAllAsync();
                ViewBag.TemplateData = ModelTemplate;
                //return View(CModel);
                return View(MVC.Views.MailChimp.Views.CreateCampaigns, CModel);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return View(CModel);
                return View(MVC.Views.MailChimp.Views.CreateCampaigns, CModel);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(CModel);
                return View(MVC.Views.MailChimp.Views.CreateCampaigns, CModel);
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }

        }

        //Create Campaign
        //TODO:Salesforce,GoogleAnalytics,Ecommerce
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCampaigns(ModelCampaign CModel)
        {
            try
            {
                var campaign = new Campaign
                {
                    Type = CampaignType.Regular,
                    Recipients = new Recipient
                    {
                        ListId = CModel.ListId
                    },
                    Settings = new Setting
                    {
                        SubjectLine = CModel.SubjectLine,
                        Title = CModel.Title,
                        FromName = CModel.FromName,
                        ReplyTo = CModel.ReplyTo,
                        TemplateId = CModel.TemplateId
                    },
                    Tracking = new Tracking
                    {
                        Opens = true,
                        HtmlClicks = true,
                        TextClicks = true
                    },
                    SocialCard = new SocialCard
                    {
                        ImageUrl = "http://cdn.smosh.com/sites/default/files/legacy.images/smosh-pit/122010/lolcat-link.jpg",
                        Description = "I'm learning how to make dynamic MailChimp campaigns via the API.",
                        Title = "Using the MailChimp API in .NET via the MailChimp.NET.V3 wrapper"
                    },
                    ReportSummary = new ReportSummary(),
                    DeliveryStatus = new DeliveryStatus()
                };
                if (ModelState.IsValid)
                {
                    var result = await _manager.Campaigns.AddOrUpdateAsync(campaign);
                    TempData["message"] = "Campaign created successfully";
                    return RedirectToAction(nameof(SendCheckList), new { CampaignId = result.Id });
                }
                //return View(CModel);
                return View(MVC.Views.MailChimp.Views.CreateCampaigns, CModel);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return View(CModel);
                return View(MVC.Views.MailChimp.Views.CreateCampaigns, CModel);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(CModel);
                return View(MVC.Views.MailChimp.Views.CreateCampaigns, CModel);
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Update Campaign
        [HttpGet]
        public async Task<IActionResult> UpdateCampaigns(string id)
        {
            ModelCampaign CModel = new ModelCampaign();
            try
            {
                var listoptions = new ListRequest
                {
                    Limit = 1000
                };
                var ModelList = await _manager.Lists.GetAllAsync(listoptions);
                ViewBag.ListData = ModelList;
                var ModelTemplate = await _manager.Templates.GetAllAsync();
                ViewBag.TemplateData = ModelTemplate;
                var model = await _manager.Campaigns.GetAsync(id);
                CModel.Id = model.Id;
                CModel.Title = model.Settings.Title;
                CModel.ListId = model.Recipients.ListId;
                CModel.SubjectLine = model.Settings.SubjectLine;
                CModel.FromName = model.Settings.FromName;
                CModel.ReplyTo = model.Settings.ReplyTo;
                CModel.TemplateId = model.Settings.TemplateId;
                // return View(CModel);
                return View(MVC.Views.MailChimp.Views.UpdateCampaigns, CModel);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return View(MVC.Views.MailChimp.Views.UpdateCampaigns, CModel);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return View(MVC.Views.MailChimp.Views.UpdateCampaigns, CModel);
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Update Campaign
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCampaigns(ModelCampaign CModel)
        {
            try
            {
                var campaign = new Campaign
                {
                    Id = CModel.Id,
                    Type = CampaignType.Regular,
                    Recipients = new Recipient
                    {
                        ListId = CModel.ListId
                    },
                    Settings = new Setting
                    {
                        SubjectLine = CModel.SubjectLine,
                        Title = CModel.Title,
                        FromName = CModel.FromName,
                        ReplyTo = CModel.ReplyTo,
                        TemplateId = CModel.TemplateId,
                        Authenticate = true
                    }
                };
                if (ModelState.IsValid)
                {
                    var result = await _manager.Campaigns.AddOrUpdateAsync(campaign);
                    return RedirectToAction(nameof(SendCheckList), new { CampaignId = result.Id });
                }
                //return View(CModel);
                return View(MVC.Views.MailChimp.Views.UpdateCampaigns, CModel);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                //return View(CModel);
                return View(MVC.Views.MailChimp.Views.UpdateCampaigns, CModel);
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(CModel);
                return View(MVC.Views.MailChimp.Views.UpdateCampaigns, CModel);
                // return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        //Deleting Campaign
        public async Task<IActionResult> DeleteCampaign(string Id)
        {
            try
            {
                await _manager.Campaigns.DeleteAsync(Id);
                return RedirectToAction(nameof(SentCampaigns), new { sort = "Recent" });
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();
                return RedirectToAction(nameof(SentCampaigns), new { sort = "Recent" });
                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return RedirectToAction(nameof(SentCampaigns), new { sort = "Recent" });
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        public async Task<IActionResult> TemplateList(ModelTemplate TModel)
        {
            var ModelTemplate = await _manager.Templates.GetAllAsync();
            foreach (var item in ModelTemplate)
            {
                TModel.Id = item.Id;
                TModel.Name = item.Name;
            }
            ViewBag.TemplateData = ModelTemplate;
            return PartialView(TModel);
        }

        //TODO:Template Editor       
        [HttpGet, Route("~/MailChimp/GetTemplateList")]
        public async Task<IActionResult> GetTemplateList(string sort)
        {
            try
            {
                var model = await _manager.Templates.GetAllAsync();
                if (sort == "A-Z")
                {
                    model = model.OrderBy(x => x.Name).ToList();

                }
                else if (sort == "Z-A")
                {
                    model = model.OrderByDescending(x => x.Name).ToList();

                }
                else if (sort == "oldest first")
                {
                    model = model.OrderBy(x => x.DateCreated).ToList();

                }
                else if (sort == "newest first")
                {
                    model = model.OrderByDescending(x => x.DateCreated).ToList();
                }
                //return View(model);
                return View(MVC.Views.MailChimp.Views.GetTemplateList, model);
            }
            catch (MailChimpException mce)
            {
                TempData["Errormessage"] = mce.Errors.FirstOrDefault().Message.ToString();

                return View(MVC.Views.MailChimp.Views.GetTemplateList, Enumerable.Empty<global::MailChimp.Net.Models.Template>());

                //return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;

                return View(MVC.Views.MailChimp.Views.GetTemplateList, Enumerable.Empty<global::MailChimp.Net.Models.Template>());

                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }

        }



    }

    public class ISO3166Country
    {
        public ISO3166Country(string name, string alpha2, string alpha3, int numericCode)
        {
            this.Name = name;
            this.Alpha2 = alpha2;
            this.Alpha3 = alpha3;
            this.NumericCode = numericCode;
        }

        public string Name { get; private set; }

        public string Alpha2 { get; private set; }

        public string Alpha3 { get; private set; }

        public int NumericCode { get; private set; }



        #region Build Collection
        public static Collection<dynamic> BuildCollection()
        {
            Collection<dynamic> collection = new Collection<dynamic>();

            // This collection built from Wikipedia entry on ISO3166-1 on 9th Feb 2016

            collection.Add(new ISO3166Country("Afghanistan", "AF", "AFG", 4));
            collection.Add(new ISO3166Country("Åland Islands", "AX", "ALA", 248));
            collection.Add(new ISO3166Country("Albania", "AL", "ALB", 8));
            collection.Add(new ISO3166Country("Algeria", "DZ", "DZA", 12));
            collection.Add(new ISO3166Country("American Samoa", "AS", "ASM", 16));
            collection.Add(new ISO3166Country("Andorra", "AD", "AND", 20));
            collection.Add(new ISO3166Country("Angola", "AO", "AGO", 24));
            collection.Add(new ISO3166Country("Anguilla", "AI", "AIA", 660));
            collection.Add(new ISO3166Country("Antarctica", "AQ", "ATA", 10));
            collection.Add(new ISO3166Country("Antigua and Barbuda", "AG", "ATG", 28));
            collection.Add(new ISO3166Country("Argentina", "AR", "ARG", 32));
            collection.Add(new ISO3166Country("Armenia", "AM", "ARM", 51));
            collection.Add(new ISO3166Country("Aruba", "AW", "ABW", 533));
            collection.Add(new ISO3166Country("Australia", "AU", "AUS", 36));
            collection.Add(new ISO3166Country("Austria", "AT", "AUT", 40));
            collection.Add(new ISO3166Country("Azerbaijan", "AZ", "AZE", 31));
            collection.Add(new ISO3166Country("Bahamas", "BS", "BHS", 44));
            collection.Add(new ISO3166Country("Bahrain", "BH", "BHR", 48));
            collection.Add(new ISO3166Country("Bangladesh", "BD", "BGD", 50));
            collection.Add(new ISO3166Country("Barbados", "BB", "BRB", 52));
            collection.Add(new ISO3166Country("Belarus", "BY", "BLR", 112));
            collection.Add(new ISO3166Country("Belgium", "BE", "BEL", 56));
            collection.Add(new ISO3166Country("Belize", "BZ", "BLZ", 84));
            collection.Add(new ISO3166Country("Benin", "BJ", "BEN", 204));
            collection.Add(new ISO3166Country("Bermuda", "BM", "BMU", 60));
            collection.Add(new ISO3166Country("Bhutan", "BT", "BTN", 64));
            collection.Add(new ISO3166Country("Bolivia (Plurinational State of)", "BO", "BOL", 68));
            collection.Add(new ISO3166Country("Bonaire, Sint Eustatius and Saba", "BQ", "BES", 535));
            collection.Add(new ISO3166Country("Bosnia and Herzegovina", "BA", "BIH", 70));
            collection.Add(new ISO3166Country("Botswana", "BW", "BWA", 72));
            collection.Add(new ISO3166Country("Bouvet Island", "BV", "BVT", 74));
            collection.Add(new ISO3166Country("Brazil", "BR", "BRA", 76));
            collection.Add(new ISO3166Country("British Indian Ocean Territory", "IO", "IOT", 86));
            collection.Add(new ISO3166Country("Brunei Darussalam", "BN", "BRN", 96));
            collection.Add(new ISO3166Country("Bulgaria", "BG", "BGR", 100));
            collection.Add(new ISO3166Country("Burkina Faso", "BF", "BFA", 854));
            collection.Add(new ISO3166Country("Burundi", "BI", "BDI", 108));
            collection.Add(new ISO3166Country("Cabo Verde", "CV", "CPV", 132));
            collection.Add(new ISO3166Country("Cambodia", "KH", "KHM", 116));
            collection.Add(new ISO3166Country("Cameroon", "CM", "CMR", 120));
            collection.Add(new ISO3166Country("Canada", "CA", "CAN", 124));
            collection.Add(new ISO3166Country("Cayman Islands", "KY", "CYM", 136));
            collection.Add(new ISO3166Country("Central African Republic", "CF", "CAF", 140));
            collection.Add(new ISO3166Country("Chad", "TD", "TCD", 148));
            collection.Add(new ISO3166Country("Chile", "CL", "CHL", 152));
            collection.Add(new ISO3166Country("China", "CN", "CHN", 156));
            collection.Add(new ISO3166Country("Christmas Island", "CX", "CXR", 162));
            collection.Add(new ISO3166Country("Cocos (Keeling) Islands", "CC", "CCK", 166));
            collection.Add(new ISO3166Country("Colombia", "CO", "COL", 170));
            collection.Add(new ISO3166Country("Comoros", "KM", "COM", 174));
            collection.Add(new ISO3166Country("Congo", "CG", "COG", 178));
            collection.Add(new ISO3166Country("Congo (Democratic Republic of the)", "CD", "COD", 180));
            collection.Add(new ISO3166Country("Cook Islands", "CK", "COK", 184));
            collection.Add(new ISO3166Country("Costa Rica", "CR", "CRI", 188));
            collection.Add(new ISO3166Country("Côte d'Ivoire", "CI", "CIV", 384));
            collection.Add(new ISO3166Country("Croatia", "HR", "HRV", 191));
            collection.Add(new ISO3166Country("Cuba", "CU", "CUB", 192));
            collection.Add(new ISO3166Country("Curaçao", "CW", "CUW", 531));
            collection.Add(new ISO3166Country("Cyprus", "CY", "CYP", 196));
            collection.Add(new ISO3166Country("Czech Republic", "CZ", "CZE", 203));
            collection.Add(new ISO3166Country("Denmark", "DK", "DNK", 208));
            collection.Add(new ISO3166Country("Djibouti", "DJ", "DJI", 262));
            collection.Add(new ISO3166Country("Dominica", "DM", "DMA", 212));
            collection.Add(new ISO3166Country("Dominican Republic", "DO", "DOM", 214));
            collection.Add(new ISO3166Country("Ecuador", "EC", "ECU", 218));
            collection.Add(new ISO3166Country("Egypt", "EG", "EGY", 818));
            collection.Add(new ISO3166Country("El Salvador", "SV", "SLV", 222));
            collection.Add(new ISO3166Country("Equatorial Guinea", "GQ", "GNQ", 226));
            collection.Add(new ISO3166Country("Eritrea", "ER", "ERI", 232));
            collection.Add(new ISO3166Country("Estonia", "EE", "EST", 233));
            collection.Add(new ISO3166Country("Ethiopia", "ET", "ETH", 231));
            collection.Add(new ISO3166Country("Falkland Islands (Malvinas)", "FK", "FLK", 238));
            collection.Add(new ISO3166Country("Faroe Islands", "FO", "FRO", 234));
            collection.Add(new ISO3166Country("Fiji", "FJ", "FJI", 242));
            collection.Add(new ISO3166Country("Finland", "FI", "FIN", 246));
            collection.Add(new ISO3166Country("France", "FR", "FRA", 250));
            collection.Add(new ISO3166Country("French Guiana", "GF", "GUF", 254));
            collection.Add(new ISO3166Country("French Polynesia", "PF", "PYF", 258));
            collection.Add(new ISO3166Country("French Southern Territories", "TF", "ATF", 260));
            collection.Add(new ISO3166Country("Gabon", "GA", "GAB", 266));
            collection.Add(new ISO3166Country("Gambia", "GM", "GMB", 270));
            collection.Add(new ISO3166Country("Georgia", "GE", "GEO", 268));
            collection.Add(new ISO3166Country("Germany", "DE", "DEU", 276));
            collection.Add(new ISO3166Country("Ghana", "GH", "GHA", 288));
            collection.Add(new ISO3166Country("Gibraltar", "GI", "GIB", 292));
            collection.Add(new ISO3166Country("Greece", "GR", "GRC", 300));
            collection.Add(new ISO3166Country("Greenland", "GL", "GRL", 304));
            collection.Add(new ISO3166Country("Grenada", "GD", "GRD", 308));
            collection.Add(new ISO3166Country("Guadeloupe", "GP", "GLP", 312));
            collection.Add(new ISO3166Country("Guam", "GU", "GUM", 316));
            collection.Add(new ISO3166Country("Guatemala", "GT", "GTM", 320));
            collection.Add(new ISO3166Country("Guernsey", "GG", "GGY", 831));
            collection.Add(new ISO3166Country("Guinea", "GN", "GIN", 324));
            collection.Add(new ISO3166Country("Guinea-Bissau", "GW", "GNB", 624));
            collection.Add(new ISO3166Country("Guyana", "GY", "GUY", 328));
            collection.Add(new ISO3166Country("Haiti", "HT", "HTI", 332));
            collection.Add(new ISO3166Country("Heard Island and McDonald Islands", "HM", "HMD", 334));
            collection.Add(new ISO3166Country("Holy See", "VA", "VAT", 336));
            collection.Add(new ISO3166Country("Honduras", "HN", "HND", 340));
            collection.Add(new ISO3166Country("Hong Kong", "HK", "HKG", 344));
            collection.Add(new ISO3166Country("Hungary", "HU", "HUN", 348));
            collection.Add(new ISO3166Country("Iceland", "IS", "ISL", 352));
            collection.Add(new ISO3166Country("India", "IN", "IND", 356));
            collection.Add(new ISO3166Country("Indonesia", "ID", "IDN", 360));
            collection.Add(new ISO3166Country("Iran (Islamic Republic of)", "IR", "IRN", 364));
            collection.Add(new ISO3166Country("Iraq", "IQ", "IRQ", 368));
            collection.Add(new ISO3166Country("Ireland", "IE", "IRL", 372));
            collection.Add(new ISO3166Country("Isle of Man", "IM", "IMN", 833));
            collection.Add(new ISO3166Country("Israel", "IL", "ISR", 376));
            collection.Add(new ISO3166Country("Italy", "IT", "ITA", 380));
            collection.Add(new ISO3166Country("Jamaica", "JM", "JAM", 388));
            collection.Add(new ISO3166Country("Japan", "JP", "JPN", 392));
            collection.Add(new ISO3166Country("Jersey", "JE", "JEY", 832));
            collection.Add(new ISO3166Country("Jordan", "JO", "JOR", 400));
            collection.Add(new ISO3166Country("Kazakhstan", "KZ", "KAZ", 398));
            collection.Add(new ISO3166Country("Kenya", "KE", "KEN", 404));
            collection.Add(new ISO3166Country("Kiribati", "KI", "KIR", 296));
            collection.Add(new ISO3166Country("Korea (Democratic People's Republic of)", "KP", "PRK", 408));
            collection.Add(new ISO3166Country("Korea (Republic of)", "KR", "KOR", 410));
            collection.Add(new ISO3166Country("Kuwait", "KW", "KWT", 414));
            collection.Add(new ISO3166Country("Kyrgyzstan", "KG", "KGZ", 417));
            collection.Add(new ISO3166Country("Lao People's Democratic Republic", "LA", "LAO", 418));
            collection.Add(new ISO3166Country("Latvia", "LV", "LVA", 428));
            collection.Add(new ISO3166Country("Lebanon", "LB", "LBN", 422));
            collection.Add(new ISO3166Country("Lesotho", "LS", "LSO", 426));
            collection.Add(new ISO3166Country("Liberia", "LR", "LBR", 430));
            collection.Add(new ISO3166Country("Libya", "LY", "LBY", 434));
            collection.Add(new ISO3166Country("Liechtenstein", "LI", "LIE", 438));
            collection.Add(new ISO3166Country("Lithuania", "LT", "LTU", 440));
            collection.Add(new ISO3166Country("Luxembourg", "LU", "LUX", 442));
            collection.Add(new ISO3166Country("Macao", "MO", "MAC", 446));
            collection.Add(new ISO3166Country("Macedonia (the former Yugoslav Republic of)", "MK", "MKD", 807));
            collection.Add(new ISO3166Country("Madagascar", "MG", "MDG", 450));
            collection.Add(new ISO3166Country("Malawi", "MW", "MWI", 454));
            collection.Add(new ISO3166Country("Malaysia", "MY", "MYS", 458));
            collection.Add(new ISO3166Country("Maldives", "MV", "MDV", 462));
            collection.Add(new ISO3166Country("Mali", "ML", "MLI", 466));
            collection.Add(new ISO3166Country("Malta", "MT", "MLT", 470));
            collection.Add(new ISO3166Country("Marshall Islands", "MH", "MHL", 584));
            collection.Add(new ISO3166Country("Martinique", "MQ", "MTQ", 474));
            collection.Add(new ISO3166Country("Mauritania", "MR", "MRT", 478));
            collection.Add(new ISO3166Country("Mauritius", "MU", "MUS", 480));
            collection.Add(new ISO3166Country("Mayotte", "YT", "MYT", 175));
            collection.Add(new ISO3166Country("Mexico", "MX", "MEX", 484));
            collection.Add(new ISO3166Country("Micronesia (Federated States of)", "FM", "FSM", 583));
            collection.Add(new ISO3166Country("Moldova (Republic of)", "MD", "MDA", 498));
            collection.Add(new ISO3166Country("Monaco", "MC", "MCO", 492));
            collection.Add(new ISO3166Country("Mongolia", "MN", "MNG", 496));
            collection.Add(new ISO3166Country("Montenegro", "ME", "MNE", 499));
            collection.Add(new ISO3166Country("Montserrat", "MS", "MSR", 500));
            collection.Add(new ISO3166Country("Morocco", "MA", "MAR", 504));
            collection.Add(new ISO3166Country("Mozambique", "MZ", "MOZ", 508));
            collection.Add(new ISO3166Country("Myanmar", "MM", "MMR", 104));
            collection.Add(new ISO3166Country("Namibia", "NA", "NAM", 516));
            collection.Add(new ISO3166Country("Nauru", "NR", "NRU", 520));
            collection.Add(new ISO3166Country("Nepal", "NP", "NPL", 524));
            collection.Add(new ISO3166Country("Netherlands", "NL", "NLD", 528));
            collection.Add(new ISO3166Country("New Caledonia", "NC", "NCL", 540));
            collection.Add(new ISO3166Country("New Zealand", "NZ", "NZL", 554));
            collection.Add(new ISO3166Country("Nicaragua", "NI", "NIC", 558));
            collection.Add(new ISO3166Country("Niger", "NE", "NER", 562));
            collection.Add(new ISO3166Country("Nigeria", "NG", "NGA", 566));
            collection.Add(new ISO3166Country("Niue", "NU", "NIU", 570));
            collection.Add(new ISO3166Country("Norfolk Island", "NF", "NFK", 574));
            collection.Add(new ISO3166Country("Northern Mariana Islands", "MP", "MNP", 580));
            collection.Add(new ISO3166Country("Norway", "NO", "NOR", 578));
            collection.Add(new ISO3166Country("Oman", "OM", "OMN", 512));
            collection.Add(new ISO3166Country("Pakistan", "PK", "PAK", 586));
            collection.Add(new ISO3166Country("Palau", "PW", "PLW", 585));
            collection.Add(new ISO3166Country("Palestine, State of", "PS", "PSE", 275));
            collection.Add(new ISO3166Country("Panama", "PA", "PAN", 591));
            collection.Add(new ISO3166Country("Papua New Guinea", "PG", "PNG", 598));
            collection.Add(new ISO3166Country("Paraguay", "PY", "PRY", 600));
            collection.Add(new ISO3166Country("Peru", "PE", "PER", 604));
            collection.Add(new ISO3166Country("Philippines", "PH", "PHL", 608));
            collection.Add(new ISO3166Country("Pitcairn", "PN", "PCN", 612));
            collection.Add(new ISO3166Country("Poland", "PL", "POL", 616));
            collection.Add(new ISO3166Country("Portugal", "PT", "PRT", 620));
            collection.Add(new ISO3166Country("Puerto Rico", "PR", "PRI", 630));
            collection.Add(new ISO3166Country("Qatar", "QA", "QAT", 634));
            collection.Add(new ISO3166Country("Réunion", "RE", "REU", 638));
            collection.Add(new ISO3166Country("Romania", "RO", "ROU", 642));
            collection.Add(new ISO3166Country("Russian Federation", "RU", "RUS", 643));
            collection.Add(new ISO3166Country("Rwanda", "RW", "RWA", 646));
            collection.Add(new ISO3166Country("Saint Barthélemy", "BL", "BLM", 652));
            collection.Add(new ISO3166Country("Saint Helena, Ascension and Tristan da Cunha", "SH", "SHN", 654));
            collection.Add(new ISO3166Country("Saint Kitts and Nevis", "KN", "KNA", 659));
            collection.Add(new ISO3166Country("Saint Lucia", "LC", "LCA", 662));
            collection.Add(new ISO3166Country("Saint Martin (French part)", "MF", "MAF", 663));
            collection.Add(new ISO3166Country("Saint Pierre and Miquelon", "PM", "SPM", 666));
            collection.Add(new ISO3166Country("Saint Vincent and the Grenadines", "VC", "VCT", 670));
            collection.Add(new ISO3166Country("Samoa", "WS", "WSM", 882));
            collection.Add(new ISO3166Country("San Marino", "SM", "SMR", 674));
            collection.Add(new ISO3166Country("Sao Tome and Principe", "ST", "STP", 678));
            collection.Add(new ISO3166Country("Saudi Arabia", "SA", "SAU", 682));
            collection.Add(new ISO3166Country("Senegal", "SN", "SEN", 686));
            collection.Add(new ISO3166Country("Serbia", "RS", "SRB", 688));
            collection.Add(new ISO3166Country("Seychelles", "SC", "SYC", 690));
            collection.Add(new ISO3166Country("Sierra Leone", "SL", "SLE", 694));
            collection.Add(new ISO3166Country("Singapore", "SG", "SGP", 702));
            collection.Add(new ISO3166Country("Sint Maarten (Dutch part)", "SX", "SXM", 534));
            collection.Add(new ISO3166Country("Slovakia", "SK", "SVK", 703));
            collection.Add(new ISO3166Country("Slovenia", "SI", "SVN", 705));
            collection.Add(new ISO3166Country("Solomon Islands", "SB", "SLB", 90));
            collection.Add(new ISO3166Country("Somalia", "SO", "SOM", 706));
            collection.Add(new ISO3166Country("South Africa", "ZA", "ZAF", 710));
            collection.Add(new ISO3166Country("South Georgia and the South Sandwich Islands", "GS", "SGS", 239));
            collection.Add(new ISO3166Country("South Sudan", "SS", "SSD", 728));
            collection.Add(new ISO3166Country("Spain", "ES", "ESP", 724));
            collection.Add(new ISO3166Country("Sri Lanka", "LK", "LKA", 144));
            collection.Add(new ISO3166Country("Sudan", "SD", "SDN", 729));
            collection.Add(new ISO3166Country("Suriname", "SR", "SUR", 740));
            collection.Add(new ISO3166Country("Svalbard and Jan Mayen", "SJ", "SJM", 744));
            collection.Add(new ISO3166Country("Swaziland", "SZ", "SWZ", 748));
            collection.Add(new ISO3166Country("Sweden", "SE", "SWE", 752));
            collection.Add(new ISO3166Country("Switzerland", "CH", "CHE", 756));
            collection.Add(new ISO3166Country("Syrian Arab Republic", "SY", "SYR", 760));
            collection.Add(new ISO3166Country("Taiwan, Province of China[a]", "TW", "TWN", 158));
            collection.Add(new ISO3166Country("Tajikistan", "TJ", "TJK", 762));
            collection.Add(new ISO3166Country("Tanzania, United Republic of", "TZ", "TZA", 834));
            collection.Add(new ISO3166Country("Thailand", "TH", "THA", 764));
            collection.Add(new ISO3166Country("Timor-Leste", "TL", "TLS", 626));
            collection.Add(new ISO3166Country("Togo", "TG", "TGO", 768));
            collection.Add(new ISO3166Country("Tokelau", "TK", "TKL", 772));
            collection.Add(new ISO3166Country("Tonga", "TO", "TON", 776));
            collection.Add(new ISO3166Country("Trinidad and Tobago", "TT", "TTO", 780));
            collection.Add(new ISO3166Country("Tunisia", "TN", "TUN", 788));
            collection.Add(new ISO3166Country("Turkey", "TR", "TUR", 792));
            collection.Add(new ISO3166Country("Turkmenistan", "TM", "TKM", 795));
            collection.Add(new ISO3166Country("Turks and Caicos Islands", "TC", "TCA", 796));
            collection.Add(new ISO3166Country("Tuvalu", "TV", "TUV", 798));
            collection.Add(new ISO3166Country("Uganda", "UG", "UGA", 800));
            collection.Add(new ISO3166Country("Ukraine", "UA", "UKR", 804));
            collection.Add(new ISO3166Country("United Arab Emirates", "AE", "ARE", 784));
            collection.Add(new ISO3166Country("United Kingdom of Great Britain and Northern Ireland", "GB", "GBR", 826));
            collection.Add(new ISO3166Country("United States of America", "US", "USA", 840));
            collection.Add(new ISO3166Country("United States Minor Outlying Islands", "UM", "UMI", 581));
            collection.Add(new ISO3166Country("Uruguay", "UY", "URY", 858));
            collection.Add(new ISO3166Country("Uzbekistan", "UZ", "UZB", 860));
            collection.Add(new ISO3166Country("Vanuatu", "VU", "VUT", 548));
            collection.Add(new ISO3166Country("Venezuela (Bolivarian Republic of)", "VE", "VEN", 862));
            collection.Add(new ISO3166Country("Viet Nam", "VN", "VNM", 704));
            collection.Add(new ISO3166Country("Virgin Islands (British)", "VG", "VGB", 92));
            collection.Add(new ISO3166Country("Virgin Islands (U.S.)", "VI", "VIR", 850));
            collection.Add(new ISO3166Country("Wallis and Futuna", "WF", "WLF", 876));
            collection.Add(new ISO3166Country("Western Sahara", "EH", "ESH", 732));
            collection.Add(new ISO3166Country("Yemen", "YE", "YEM", 887));
            collection.Add(new ISO3166Country("Zambia", "ZM", "ZMB", 894));
            collection.Add(new ISO3166Country("Zimbabwe", "ZW", "ZWE", 716));

            return collection;
        }
        #endregion
    }
}

