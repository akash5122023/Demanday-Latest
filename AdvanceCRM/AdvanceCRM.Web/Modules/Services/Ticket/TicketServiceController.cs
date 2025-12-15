using AdvanceCRM.Administration;
using AdvanceCRM.Web.Helpers;
using AdvanceCRM.Contacts;
using AdvanceCRM.Template;
using AdvanceCRM.Products;
using AdvanceCRM.Services;
using Serenity.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using AdvanceCRM.Common;

namespace AdvanceCRM.Services
{
    [EnableCors("MyPolicy")]
    [Route("Services/TicketService")]
    public class TicketServiceController : Controller
    {
        
            private readonly ISqlConnections _connections;
            private readonly IConfiguration _configuration;
            private readonly IWebHostEnvironment _env;

            public TicketServiceController(ISqlConnections connections, IConfiguration configuration, IWebHostEnvironment env)
            {
                _connections = connections;
                _configuration = configuration;
                _env = env;
            }

            // GET: Ticket
            public async Task<IActionResult> Index()
        {
            var model = LocalCache.GetLocalStoreOnly(
                "LodgeComplaint",
                TimeSpan.FromSeconds(1),
                ProductsRow.Fields.GenerationKey,
                () => new ModelLodgeComplaint());

            if (model.ProductList == null)
            {
                using var cn = _connections.NewFor<ProductsRow>();
                cn.Open();
                var list = await cn.QueryAsync<ProductsRow>(
                    "SELECT Id, Name FROM [dbo].[Products]");
                model.ProductList = list.ToList();

                LocalCache.Remove("LodgeComplaint:" + ProductsRow.Fields.GenerationKey);
                LocalCache.GetLocalStoreOnly(
                    "LodgeComplaint",
                    TimeSpan.FromSeconds(1),
                    ProductsRow.Fields.GenerationKey,
                    () => model);
            }

            ViewBag.ListData = model.ProductList;
            return View(MVC.Views.Services.Ticket.LodgeComplaint, model);
        }

        [HttpPost, Route("ValidateAndGenerateOTP")]
        public async Task<IActionResult> ValidateAndGenerateOTP(string phone)
        {
            var model = new ModelLodgeComplaint();

            using var cn = _connections.NewFor<ContactsRow>();
            cn.Open();
            var contactRow = await cn.QueryFirstOrDefaultAsync<ContactsRow>(
                "SELECT TOP 1 * FROM Contacts WHERE Phone = @phone",
                new { phone });

            model.CMSList = (await cn.QueryAsync<CMSRow>(
                @"SELECT Id, ContactsName, ContactsId, ContactsPhone, TicketNo, Date,
                         ComplaintComplaintType, Instructions, Status, AdditionalInfo,
                         ProductsName, AssignedToDisplayName
                  FROM CMS WHERE ContactsPhone = @phone", new { phone })).ToList();
            ViewBag.listdetails = model.CMSList;

            if (contactRow == null)
                return Json("This phone number is not registered with us");

            var randomNum = new Random().Next(10000, 99999);

            try
            {
                using var logCn = _connections.NewFor<OptLogRow>();
                logCn.Open();
                await logCn.ExecuteAsync(
                    "INSERT INTO OPTLog(Phone,OPT,Validity) VALUES(@phone,@otp,@validity)",
                    new { phone, otp = randomNum, validity = DateTime.Now });

                using var tplCn = _connections.NewFor<OtherTemplatesRow>();
                tplCn.Open();
                var template = await tplCn.QueryFirstOrDefaultAsync<OtherTemplatesRow>(
                    "SELECT TOP 1 OTPSMSTemplate AS OtpsmsTemplate, OTPSMSTemplateId AS OtpsmsTemplateId FROM OtherTemplates");

                var str = SendCustomSMS(phone, randomNum.ToString(), "OTP", "", 0,
                    template.OtpsmsTemplate, template.OtpsmsTemplateId);

                if (!str.Contains("SMS"))
                    return Json("Unable to send OTP via SMS");
            }
            catch
            {
                return Json("Unable to send OTP via SMS");
            }

            return Json(contactRow.Name);
        }

        [HttpPost, Route("Validated")]
        public async Task<IActionResult> Validated(string otp)
        {
            using var cn = _connections.NewFor<OptLogRow>();
            cn.Open();
            var entry = await cn.QueryFirstOrDefaultAsync<OptLogRow>(
                "SELECT TOP 1 Phone, Opt, Validity FROM OPTLog WHERE Opt = @otp",
                new { otp });

            if (entry == null)
                return Json("Invalid OTP");

            if (DateTime.Now.Subtract(entry.Validity.Value).TotalMinutes > 15)
                return Json("OTP Expired");

            using var delCn = _connections.NewFor<OptLogRow>();
            delCn.Open();
            await delCn.ExecuteAsync(
                "DELETE FROM OPTLog WHERE Validity < @cutoff",
                new { cutoff = DateTime.Now.AddDays(-1) });

            return Json("Valid OTP");
        }

        [HttpGet, Route("LodgeComplaint")]
        public async Task<IActionResult> LodgeComplaint()
        {
            var model = LocalCache.GetLocalStoreOnly(
                "LodgeComplaint",
                TimeSpan.FromSeconds(1),
                ProductsRow.Fields.GenerationKey,
                () => new ModelLodgeComplaint());

            if (model.ProductList == null || model.CMSList == null)
            {
                using var cn = _connections.NewFor<ProductsRow>();
                cn.Open();
                var cmsQuery = @"SELECT Id, ContactsName, ContactsId, ContactsPhone, TicketNo,
                                    Date, ComplaintComplaintType, Instructions, Status,
                                    AdditionalInfo, ProductsName, AssignedToDisplayName
                                 FROM CMS";
                var cms = await cn.QueryAsync<CMSRow>(cmsQuery);
                model.CMSList = cms.ToList();

                var products = await cn.QueryAsync<ProductsRow>(
                    "SELECT Id, Name FROM [dbo].[Products]");
                model.ProductList = products.ToList();

                LocalCache.Remove("LodgeComplaint:" + ProductsRow.Fields.GenerationKey);
                LocalCache.GetLocalStoreOnly(
                    "LodgeComplaint",
                    TimeSpan.FromSeconds(1),
                    ProductsRow.Fields.GenerationKey,
                    () => model);
            }

            ViewBag.ListData = model.ProductList;
            ViewBag.listdetails = model.CMSList;
            return View(MVC.Views.Services.Ticket.LodgeComplaint, model);
        }


        [HttpPost, Route("LodgeComplaint")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LodgeComplaint(ModelLodgeComplaint model)
        {
            try
            {
                using var cn = _connections.NewFor<OptLogRow>();
                cn.Open();

                var prodFields = ProductsRow.Fields;
                model.ProductList = cn.List<ProductsRow>(q => q
                    .SelectTableFields()
                    .Select(prodFields.Id)
                    .Select(prodFields.Name));

                ViewBag.ListData = model.ProductList;

                await cn.ExecuteAsync(
                    @"INSERT INTO Ticket(Phone,Name,ProductsId,ComplaintDetails,Priority,Date)
                      VALUES(@Phone,@Name,@ProductsId,@ComplaintDetails,@Priority,@Date)",
                    new
                    {
                        Phone = model.Phone,
                        Name = model.Name,
                        ProductsId = model.ProductId,
                        ComplaintDetails = model.ComplaintDetails,
                        Priority = model.PriorityId,
                        Date = DateTime.Now
                    });

                var product = await cn.QueryFirstOrDefaultAsync<ProductsRow>(
                    "SELECT TOP 1 Name FROM Products WHERE Id = @Id",
                    new { Id = model.ProductId });

                var last = await cn.QueryFirstOrDefaultAsync<TicketRow>(
                    "SELECT TOP 1 Id FROM Ticket ORDER BY Id DESC");

                var template = await cn.QueryFirstOrDefaultAsync<OtherTemplatesRow>(
                    "SELECT TOP 1 TicketSmsTemplate AS TicketSmsTemplate, TicketSmsTemplateId AS TicketSmsTemplateId FROM OtherTemplates");

                var msg = SendCustomSMS(model.Phone, "", "Success", product.Name, last.Id,
                    template.TicketSmsTemplate, template.TicketSmsTemplateId);
                TempData["message"] = "Your complaint for product " + product.Name +
                    " is registered successfully.Your complaint number is " + last.Id.ToString();

            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }
            return RedirectToAction("LodgeComplaint", model);
        }

        public String SendCustomSMS(string Phone, string OTP, string Type, string ProductName, Int32? ComplaintId,string message,string tempid)
        {
            String response;
            String msg=message;
            String TempId = tempid;

            if (Type == "OTP")
            {
                msg = msg.Replace("#otp", OTP);
                //msg = "OTP for complaint registration is: " + OTP;
            }
            else
            {
                msg = msg.Replace("#product", ProductName);
                msg = msg.Replace("#complaintno",Convert.ToString(ComplaintId));
              //  msg = "Your complaint for product " + ProductName + " is registered successfully.\nYour complaint number is " + ComplaintId;
            }
            try
            {
                response = SMSHelper.SendSMS(Phone, msg,TempId);
            }
            catch (Exception ex)
            {
                response = "Error: " + ex.Message.ToString();
            }

            return response;
        }

    }
}