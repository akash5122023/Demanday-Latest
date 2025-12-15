using AdvanceCRM.Web.Helpers;
using AdvanceCRM.Products;
using Dapper;
using Serenity.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace AdvanceCRM.Quotation
{
    [EnableCors("MyPolicy")]
    [Route("Quotation/PrintQuotation")]
    public class PrintQuotationController : Controller
    {
        private readonly ISqlConnections _connections;

        public PrintQuotationController(ISqlConnections connections)
        {
            _connections = connections;
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {
            var model = LocalCache.GetLocalStoreOnly(
                "PrintQuotation",
                TimeSpan.FromSeconds(1),
                ProductsRow.Fields.GenerationKey,
                () => new ModelQuotation());

            if (model.ProductsList == null)
            {
                using var cn = _connections.NewFor<ProductsRow>();
                cn.Open();
                var list = await cn.QueryAsync<ProductsRow>(
                    "SELECT Id, Name FROM [dbo].[Products]");
                model.ProductsList = list.ToList();

                LocalCache.Remove("PrintQuotation:" + ProductsRow.Fields.GenerationKey);
                LocalCache.GetLocalStoreOnly(
                    "PrintQuotation",
                    TimeSpan.FromSeconds(1),
                    ProductsRow.Fields.GenerationKey,
                    () => model);
            }

            ViewBag.ListData = model.ProductsList;
            return View(MVC.Views.Quotation.Quotation_.PrintQuotation, model);
        }

        //[HttpPost, Route("~/Ticket/ValidateAndGenerateOTP")]
        //public ActionResult ValidateAndGenerateOTP(string Phone)
        //{
        //    var s = ContactsRow.Fields;
        //    var ContactRow = new ContactsRow();

        //    using (var connection = _connections.NewFor<ContactsRow>())
        //    {
        //        ContactRow = connection.TryFirst<ContactsRow>(q => q
        //            .SelectTableFields()
        //            .Select(s.Phone)
        //            .Select(s.Name)
        //            .Where(s.Phone == Phone)
        //            );
        //    }

        //    if (ContactRow == null)
        //    {
        //        return Json("This phone number is not registered with us", JsonRequestBehavior.AllowGet);
        //    }

        //    Random randm = new Random();

        //    var randomNum = randm.Next(10000, 99999);

        //    try
        //    {
        //        using (var connection = _connections.NewFor<OptLogRow>())
        //        {
        //            connection.Execute("INSERT INTO OPTLog(Phone,OPT,Validity) VALUES('" + Phone + "'," + randomNum + "," + DateTime.Now.ToSqlDate() + ")");
        //        }


        //        var str = SendCustomSMS(Phone, randomNum.ToString(), "OTP", "", 0);

        //        if (!str.Contains("SMS"))
        //        {
        //            return Json("Unable to send OTP via SMS", JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("Unable to send OTP via SMS", JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(ContactRow.Name, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost, Route("~/Ticket/Validated")]
        //public ActionResult Validated(string Otp)
        //{
        //    var s = OptLogRow.Fields;
        //    var OTP = new OptLogRow();

        //    using (var connection = _connections.NewFor<OptLogRow>())
        //    {
        //        OTP = connection.TryFirst<OptLogRow>(q => q
        //            .SelectTableFields()
        //            .Select(s.Phone)
        //            .Select(s.Opt)
        //            .Select(s.Validity)
        //            .Where(s.Opt == Otp)
        //            );
        //    }

        //    if (OTP == null)
        //    {
        //        return Json("Invalid OTP", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        var diff = OTP.Validity.Value.Subtract(DateTime.Now).TotalMinutes;
        //        if (diff > 15)
        //        {
        //            return Json("OTP Expired", JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //    using (var connection = _connections.NewFor<OptLogRow>())
        //    {
        //        //connection.Execute("DELETE FROM OPTLog WHERE Validity < "+DateTime.Now.AddDays(-1));
        //        string str = "DELETE FROM OPTLog WHERE Validity < " + DateTime.Now.AddDays(-1).ToShortDateString();
        //        connection.Execute(str);
        //    }
        //    return Json("Valid OTP", JsonRequestBehavior.AllowGet);
        //}

        [HttpGet("LodgeComplaint")]
        public Task<IActionResult> LodgeComplaint()
        {
            return Index();
        }


        //[HttpPost, Route("~/Ticket/LodgeComplaint")]
        //[ValidateAntiForgeryToken]
        //public ActionResult LodgeComplaint(ModelQuotation model)
        //{
        //    try
        //    {
        //        var Prod = ProductsRow.Fields;
        //        using (var connection = _connections.NewFor<OptLogRow>())
        //        {

        //            model.ProductsList = connection.List<ProductsRow>(us => us
        //            .SelectTableFields()
        //            .Select(Prod.Id)
        //            .Select(Prod.Name)
        //            );


        //            ViewBag.ListData = model.ProductsList;

        //            string str = "INSERT INTO Ticket(Phone,Name,ProductsId,ComplaintDetails,Priority) VALUES('" + model.Phone + "','" + model.Name + "','" + model.ProductId + "','" + model.ComplaintDetails + "','" + model.PriorityId + "')";
        //            connection.Execute(str);
        //            var p = ProductsRow.Fields;
        //            var Product_Name = connection.TryById<ProductsRow>(model.ProductId, q => q
        //            .SelectTableFields()
        //            .Select(p.Name)
        //            );

        //            var t = TicketRow.Fields;
        //            var LastId = connection.TryFirst<TicketRow>(l => l
        //                .Select(t.Id)
        //                .OrderBy(t.Id, desc: true)
        //                );
        //            var msg = SendCustomSMS(model.Phone, "", "Success", Product_Name.Name, LastId.Id);
        //            TempData["message"] = "Your complaint for product " + Product_Name.Name + " is registered successfully.Your complaint number is " + LastId.Id.ToString();

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Errormessage"] = ex.Message;
        //    }
        //    return RedirectToAction("LodgeComplaint", model);
        //}

        //public String SendCustomSMS(string Phone, string OTP, string Type, string ProductName, Int32? ComplaintId)
        //{
        //    String response;
        //    String msg;
        //    if (Type == "OTP")
        //    {
        //        msg = "OTP for complaint registration is: " + OTP;
        //    }
        //    else
        //    {
        //        msg = "Your complaint for product " + ProductName + " is registered successfully.\nYour complaint number is " + ComplaintId;
        //    }
        //    try
        //    {
        //        response = SMSHelper.SendSMS(Phone, msg);
        //    }
        //    catch (Exception ex)
        //    {
        //        response = "Error: " + ex.Message.ToString();
        //    }

        //    return response;
        //}

    }
}