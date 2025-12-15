using AdvanceCRM.Administration;

using AdvanceCRM.Masters;
using AdvanceCRM.Purchase;
using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Serenity.Abstractions;

namespace AdvanceCRM.Modules.Reports.GST
{
    [Route("GSTR2")]
    public class GSTR2Controller : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IUserAccessor _userAccessor;
        private readonly IPermissionService _permissionService;
        private readonly IRequestContext _requestContext;
        private readonly IMemoryCache _memoryCache;
        private readonly ITypeSource _typeSource;
        private readonly IUserRetrieveService _userRetriever;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly ITextLocalizer _localizer;

        public GSTR2Controller(
            IUserAccessor userAccessor,
            ISqlConnections connections,
            IConfiguration configuration,
            IWebHostEnvironment env,
            IPermissionService permissionService,
            IRequestContext requestContext,
            IMemoryCache memoryCache,
            ITypeSource typeSource,
            IUserRetrieveService userRetriever,
            ITextLocalizer localizer)
        {
            _userAccessor = userAccessor;
            _permissionService = permissionService;
            _requestContext = requestContext;
            _memoryCache = memoryCache;
            _typeSource = typeSource;
            _userRetriever = userRetriever;
            _connections = connections;
            _configuration = configuration;
            _env = env;
            _localizer = localizer;
        }
        // GET: GSTR-2  Report for B2B     
        [HttpGet, Route("~/Reports/GSTR2B2BReport")]
        [ServiceAuthorize("Reports:GSTR2")]
        public ActionResult Index() => View(MVC.Views.Reports.GST.GSTR2B2BReportView);

        //GSTR-2 Partial View data for B2B
        public ActionResult LoadData(string StartDate, string EndDate)
        {
            try
            {

                var model = new GSTR2B2BPageModel();
                if (StartDate == null && EndDate == null)
                {
                    model.StartDate = DateTime.Now.AddMonths(-1);
                    model.EndDate = DateTime.Now;
                }
                else
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = Convert.ToDateTime(StartDate, culture);
                    model.EndDate = Convert.ToDateTime(EndDate, culture);
                }

                var Pur = PurchaseRow.Fields;
                var PurP = PurchaseProductsRow.Fields;
                var Stat = StateRow.Fields;
                var c = CompanyDetailsRow.Fields;

                using (var connection = _connections.NewFor<PurchaseRow>())
                {

                    //Purchase  List
                    model.PurchaseList = connection.List<PurchaseRow>(us => us
                            .SelectTableFields()
                            .Select(Pur.Id)
                            .Select(Pur.Total)
                            .Select(Pur.InvoiceDate)
                            .Select(Pur.PurchaseFromGSTIN)
                            .Select(Pur.PurchaseFromName)
                            .Select(Pur.InvoiceNo) //If this is empty then print Id
                            .Select(Pur.PurchaseFromStateId)
                            .Select(Pur.ReverseCharge)
                            .Select(Pur.InvoiceType)
                            .Select(Pur.ITCEligibility)
                            .Where(Pur.PurchaseFromGSTIN.IsNotNull())
                            .Where(Pur.InvoiceDate >= model.StartDate)
                            .Where(Pur.InvoiceDate <= model.EndDate)
                            );

                    //Purchase Product  List
                    model.PurchaseProductList = connection.List<PurchaseProductsRow>(us => us
                            .SelectTableFields()
                            .Select(PurP.Id)
                            .Select(PurP.GrandTotal)
                            .Select(PurP.PurchaseId)
                            .Select(PurP.Percentage1)
                            .Select(PurP.Percentage2)
                            .Select(PurP.Tax1Amount)
                            .Select(PurP.Tax2Amount)
                            .Where(PurP.PurchaseInvoiceDate >= model.StartDate)
                            .Where(PurP.PurchaseInvoiceDate <= model.EndDate)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );

                    //Company List
                    model.CompanyList = connection.List<CompanyDetailsRow>(us => us
                         .SelectTableFields()
                         .Select(c.StateId)
                         );
                }
                return PartialView(MVC.Views.Reports.GST.GSTR2B2BPartialView, model);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        //GSTR-2 Export to CSV for B2B
        [HttpPost]
        public FileContentResult ExportCSVB2B(string StartDate, string EndDate)
        {
            try
            {
                var model = new GSTR2B2BPageModel();
                if (StartDate == null && EndDate == null)
                {
                    model.StartDate = DateTime.Now.AddMonths(-1);
                    model.EndDate = DateTime.Now;
                }
                else
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = Convert.ToDateTime(StartDate, culture);
                    model.EndDate = Convert.ToDateTime(EndDate, culture);
                }

                var Pur = PurchaseRow.Fields;
                var PurP = PurchaseProductsRow.Fields;
                var Stat = StateRow.Fields;
                var c = CompanyDetailsRow.Fields;
                //var msg = "";

                using (var connection = _connections.NewFor<PurchaseRow>())
                {

                    //Purchase  List
                    model.PurchaseList = connection.List<PurchaseRow>(us => us
                            .SelectTableFields()
                            .Select(Pur.Id)
                            .Select(Pur.Total)
                            .Select(Pur.InvoiceDate)
                            .Select(Pur.PurchaseFromGSTIN)
                            .Select(Pur.PurchaseFromName)
                            .Select(Pur.InvoiceNo) //If this is empty then print Id
                            .Select(Pur.PurchaseFromStateId)
                            .Select(Pur.ReverseCharge)
                            .Select(Pur.InvoiceType)
                            .Select(Pur.ITCEligibility)
                            .Where(Pur.PurchaseFromGSTIN.IsNotNull())
                            .Where(Pur.InvoiceDate >= model.StartDate)
                            .Where(Pur.InvoiceDate <= model.EndDate)
                            );

                    //Purchase Product  List
                    model.PurchaseProductList = connection.List<PurchaseProductsRow>(us => us
                            .SelectTableFields()
                            .Select(PurP.Id)
                            .Select(PurP.GrandTotal)
                            .Select(PurP.PurchaseId)
                            .Select(PurP.Percentage1)
                            .Select(PurP.Percentage2)
                            .Select(PurP.Tax1Amount)
                            .Select(PurP.Tax2Amount)
                            .Where(PurP.PurchaseInvoiceDate >= model.StartDate)
                            .Where(PurP.PurchaseInvoiceDate <= model.EndDate)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );

                    //Company List
                    model.CompanyList = connection.List<CompanyDetailsRow>(us => us
                         .SelectTableFields()
                         .Select(c.StateId)
                         );

                    var sb = new StringBuilder();
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}", "GSTIN of Supplier", "Invoice No", "Invoice Date", "Invoice Value", "Place Of Supply", "Reverse Charge", "Invoice Type", "Rate", "Taxable Value", "Integrated Tax Paid", "Central Tax Paid", "State/UT Tax Paid", "Cess Paid", "Eligibility For ITC", "Availed ITC Integrated Tax", "Availed ITC Central Tax", "Availed ITC State/UT Tax", "Availed ITC Cess", Environment.NewLine);
                    foreach (var line in model.PurchaseList)
                    {

                        foreach (var item in model.PurchaseProductList.Where(x => x.PurchaseId == line.Id).GroupBy(y => y.Percentage1))
                        {

                            model.GSTIN = line.PurchaseFromGSTIN;
                            string invno;
                            if (!(line.InvoiceNo.IsNullOrEmpty()))
                            {
                                invno = line.InvoiceDate.Value.Year.ToString() + "/" + line.InvoiceNo.ToString();
                            }
                            else
                            {
                                invno = line.InvoiceDate.Value.Year.ToString() + "/" + line.Id.ToString();
                            }

                            model.InvoiceNo = invno;
                            model.InvoiceDate = line.InvoiceDate.Value.ToString("dd-MMM-yyyy");
                            model.Total = line.Total.Value.ToString("###0.00");

                            if (line.PurchaseFromStateId.HasValue)
                            {
                                model.State = model.StateList.FirstOrDefault(x => x.Id == line.PurchaseFromStateId).State;

                                if (model.State.ToUpper() == ("JAMMU AND KASHMIR"))
                                {
                                    model.State = "01-" + model.State;
                                }
                                else if (model.State.ToUpper() == "HIMACHAL PRADESH")
                                {
                                    model.State = "02-" + model.State;
                                }
                                else if (model.State.ToUpper() == "PUNJAB")
                                {
                                    model.State = "03-" + model.State;
                                }
                                else if (model.State.ToUpper() == "CHANDIGARH")
                                {
                                    model.State = "04-" + model.State;
                                }
                                else if (model.State.ToUpper() == "UTTARAKHAND")
                                {
                                    model.State = "05-" + model.State;
                                }
                                else if (model.State.ToUpper() == "HARYANA")
                                {
                                    model.State = "06-" + model.State;
                                }
                                else if (model.State.ToUpper() == "DELHI")
                                {
                                    model.State = "07-" + model.State;
                                }
                                else if (model.State.ToUpper() == "RAJASTHAN")
                                {
                                    model.State = "08-" + model.State;
                                }
                                else if (model.State.ToUpper() == "UTTAR  PRADESH")
                                {
                                    model.State = "09-" + model.State;
                                }
                                else if (model.State.ToUpper() == "BIHAR")
                                {
                                    model.State = "10-" + model.State;
                                }
                                else if (model.State.ToUpper() == "SIKKIM")
                                {
                                    model.State = "11-" + model.State;
                                }
                                else if (model.State.ToUpper() == "ARUNACHAL PRADESH")
                                {
                                    model.State = "12-" + model.State;
                                }
                                else if (model.State.ToUpper() == "NAGALAND")
                                {
                                    model.State = "13-" + model.State;
                                }
                                else if (model.State.ToUpper() == "MANIPUR")
                                {
                                    model.State = "14-" + model.State;
                                }
                                else if (model.State.ToUpper() == "MIZORAM")
                                {
                                    model.State = "15-" + model.State;
                                }
                                else if (model.State.ToUpper() == "TRIPURA")
                                {
                                    model.State = "16-" + model.State;
                                }
                                else if (model.State.ToUpper() == "MEGHLAYA")
                                {
                                    model.State = "17-" + model.State;
                                }
                                else if (model.State.ToUpper() == "ASSAM")
                                {
                                    model.State = "18-" + model.State;
                                }
                                else if (model.State.ToUpper() == "WEST BENGAL")
                                {
                                    model.State = "19-" + model.State;
                                }
                                else if (model.State.ToUpper() == "JHARKHAND")
                                {
                                    model.State = "20-" + model.State;
                                }
                                else if (model.State.ToUpper() == "ODISHA")
                                {
                                    model.State = "21-" + model.State;
                                }
                                else if (model.State.ToUpper() == "CHATTISGARH")
                                {
                                    model.State = "22-" + model.State;
                                }
                                else if (model.State.ToUpper() == "MADHYA PRADESH")
                                {
                                    model.State = "23-" + model.State;
                                }
                                else if (model.State.ToUpper() == "GUJARAT")
                                {
                                    model.State = "24-" + model.State;
                                }
                                else if (model.State.ToUpper() == "DAMAN AND DIU")
                                {
                                    model.State = "25-" + model.State;
                                }
                                else if (model.State.ToUpper() == "DADRA AND NAGAR HAVELI")
                                {
                                    model.State = "26-" + model.State;
                                }
                                else if (model.State.ToUpper() == "MAHARASHTRA")
                                {
                                    model.State = "27-" + model.State;
                                }
                                else if (model.State.ToUpper() == "ANDHRA PRADESH(BEFORE DIVISION)")
                                {
                                    model.State = "28-" + model.State;
                                }
                                else if (model.State.ToUpper() == "KARNATAKA")
                                {
                                    model.State = "29-" + model.State;
                                }
                                else if (model.State.ToUpper() == "GOA")
                                {
                                    model.State = "30-" + model.State;
                                }
                                else if (model.State.ToUpper() == "LAKSHWADEEP")
                                {
                                    model.State = "31-" + model.State;
                                }
                                else if (model.State.ToUpper() == "KERALA")
                                {
                                    model.State = "32-" + model.State;
                                }
                                else if (model.State.ToUpper() == "TAMIL NADU")
                                {
                                    model.State = "33-" + model.State;
                                }
                                else if (model.State.ToUpper() == "PUDUCHERRY")
                                {
                                    model.State = "34-" + model.State;
                                }
                                else if (model.State.ToUpper() == "ANDAMAN AND NICOBAR ISLANDS")
                                {
                                    model.State = "35-" + model.State;
                                }
                                else if (model.State.ToUpper() == "TELANGANA")
                                {
                                    model.State = "36-" + model.State;
                                }
                                else if (model.State.ToUpper() == "ANDHRA PRADESH (NEW)")
                                {
                                    model.State = "37-" + model.State;
                                }
                            }
                            else
                            {
                                model.State = "";
                            }
                            if (line.ReverseCharge.HasValue)
                            {
                                if (line.ReverseCharge == true)
                                {
                                    model.ReverseCharge = "Y";
                                }
                                else
                                {
                                    model.ReverseCharge = "N";
                                }
                            }

                            if (line.InvoiceType.HasValue)
                            {
                                //TODO: have to get enum text
                                model.InvoiceType = line.InvoiceType.Value.GetEnumDescription(_localizer);
                            }
                            double rate;
                            if (item.FirstOrDefault(x => x.PurchaseId == line.Id).Percentage1 < 1)
                            {
                                rate = (double)(item.FirstOrDefault(x => x.PurchaseId == line.Id).Percentage1);
                            }
                            else
                            {
                                rate = ((double)@item.FirstOrDefault(x => x.PurchaseId == line.Id).Percentage1) + ((double)@item.FirstOrDefault(x => x.PurchaseId == line.Id).Percentage2);
                            }
                            model.Rate = rate.ToString("###0.00");
                            model.TaxableValue = item.Sum(x => x.GrandTotal).Value.ToString("###0.00");

                            var tax1 = item.FirstOrDefault(x => x.PurchaseId == line.Id).Tax1Amount;
                            var tax2 = item.FirstOrDefault(x => x.PurchaseId == line.Id).Tax2Amount;

                            bool stateChanged = false;

                            var Id = model.CompanyList.FirstOrDefault().StateId;

                            if (Id == line.PurchaseFromStateId)
                            {
                                stateChanged = true;
                            }
                            else
                            {
                                stateChanged = false;
                            }
                            if (stateChanged == true)
                            {
                                model.Integrated_Tax_Paid = ((decimal)(tax1 + tax2)).ToString("###0.00");
                            }
                            if (stateChanged == false)
                            {
                                model.Central_Tax_Paid = ((decimal)(item.FirstOrDefault(x => x.PurchaseId == line.Id).Tax1Amount)).ToString("###0.00");
                            }
                            if (stateChanged == false)
                            {
                                model.State_UT_Tax_Paid = ((decimal)(item.FirstOrDefault(x => x.PurchaseId == line.Id).Tax2Amount)).ToString("###0.00");
                            }

                            model.Eligibility_For_ITC = line.ITCEligibility.Value.GetEnumDescription(_localizer);


                            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}", model.GSTIN, model.InvoiceNo, model.InvoiceDate, model.Total, model.State, model.ReverseCharge, model.InvoiceType, model.Rate, model.TaxableValue, model.Integrated_Tax_Paid, model.Central_Tax_Paid, model.State_UT_Tax_Paid, model.Cess_Paid, model.Eligibility_For_ITC, model.Availed_ITC_Integrated_Tax, model.Availed_ITC_Central_Tax, model.Availed_ITC_State_UT_Tax, model.Availed_ITC_Cess, Environment.NewLine);
                        }
                    }
                    //using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/GSTR2B2B.CSV"), false))
                    //{
                    //    _testData.WriteLine(sb.ToString()); // Write the file.
                    //}

                    //msg = "Exported to CSV Successfully";
                    var byteData = ASCIIEncoding.ASCII.GetBytes(sb.ToString());
                    return new FileContentResult(byteData, "text/csv");
                }
            }
            catch (Exception ex)
            {
                //TempData["Errormessage"] = ex.Message;
                return null;
            }
        }

        //GSTR-2 Report for B2BUR
        [HttpGet, Route("~/Reports/GSTR2B2BURReport")]
        public ActionResult GSTR2B2BURReport() => View(MVC.Views.Reports.GST.GSTR2B2BURReportView);

        public ActionResult LoadDataB2BBUR(string StartDate, string EndDate)
        {
            try
            {

                var model = new GSTR2B2BURPageModel();
                if (StartDate == null && EndDate == null)
                {
                    model.StartDate = DateTime.Now.AddMonths(-1);
                    model.EndDate = DateTime.Now;
                }
                else
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = Convert.ToDateTime(StartDate, culture);
                    model.EndDate = Convert.ToDateTime(EndDate, culture);
                }

                var Pur = PurchaseRow.Fields;
                var PurP = PurchaseProductsRow.Fields;
                var Stat = StateRow.Fields;
                var c = CompanyDetailsRow.Fields;

                using (var connection = _connections.NewFor<PurchaseRow>())
                {

                    //Purchase  List
                    model.PurchaseList = connection.List<PurchaseRow>(us => us
                            .SelectTableFields()
                            .Select(Pur.Id)
                            .Select(Pur.Total)
                            .Select(Pur.InvoiceDate)
                            .Select(Pur.PurchaseFromGSTIN)
                            .Select(Pur.PurchaseFromName)
                            .Select(Pur.InvoiceNo) //If this is empty then print Id
                            .Select(Pur.PurchaseFromStateId)
                            .Select(Pur.ITCEligibility)
                            .Where(Pur.PurchaseFromGSTIN.IsNull())
                            .Where(Pur.InvoiceDate >= model.StartDate)
                            .Where(Pur.InvoiceDate <= model.EndDate)
                            );

                    //Purchase Product  List
                    model.PurchaseProductList = connection.List<PurchaseProductsRow>(us => us
                            .SelectTableFields()
                            .Select(PurP.Id)
                            .Select(PurP.GrandTotal)
                            .Select(PurP.PurchaseId)
                            .Select(PurP.Percentage1)
                            .Select(PurP.Percentage2)
                            .Select(PurP.Tax1Amount)
                            .Select(PurP.Tax2Amount)
                            .Where(PurP.PurchaseInvoiceDate >= model.StartDate)
                            .Where(PurP.PurchaseInvoiceDate <= model.EndDate)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );

                    //Company List
                    model.CompanyList = connection.List<CompanyDetailsRow>(us => us
                         .SelectTableFields()
                         .Select(c.StateId)
                         );
                    return PartialView(MVC.Views.Reports.GST.GSTR2B2BURPartialView, model);
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }


        [HttpPost]
        public FileContentResult ExportCSVB2BUR(string StartDate, string EndDate)
        {
            try
            {
                var model = new GSTR2B2BURPageModel();
                if (StartDate == null && EndDate == null)
                {
                    model.StartDate = DateTime.Now.AddMonths(-1);
                    model.EndDate = DateTime.Now;
                }
                else
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = Convert.ToDateTime(StartDate, culture);
                    model.EndDate = Convert.ToDateTime(EndDate, culture);
                }

                var Pur = PurchaseRow.Fields;
                var PurP = PurchaseProductsRow.Fields;
                var Stat = StateRow.Fields;
                var c = CompanyDetailsRow.Fields;

                using (var connection = _connections.NewFor<PurchaseRow>())
                {

                    //Purchase  List
                    model.PurchaseList = connection.List<PurchaseRow>(us => us
                            .SelectTableFields()
                            .Select(Pur.Id)
                            .Select(Pur.Total)
                            .Select(Pur.InvoiceDate)
                            .Select(Pur.PurchaseFromGSTIN)
                            .Select(Pur.PurchaseFromName)
                            .Select(Pur.InvoiceNo) //If this is empty then print Id
                            .Select(Pur.PurchaseFromStateId)
                            .Select(Pur.ITCEligibility)
                            .Where(Pur.PurchaseFromGSTIN.IsNull())
                            .Where(Pur.InvoiceDate >= model.StartDate)
                            .Where(Pur.InvoiceDate <= model.EndDate)
                            );

                    //Purchase Product  List
                    model.PurchaseProductList = connection.List<PurchaseProductsRow>(us => us
                            .SelectTableFields()
                            .Select(PurP.Id)
                            .Select(PurP.GrandTotal)
                            .Select(PurP.PurchaseId)
                            .Select(PurP.Percentage1)
                            .Select(PurP.Percentage2)
                            .Select(PurP.Tax1Amount)
                            .Select(PurP.Tax2Amount)
                            .Where(PurP.PurchaseInvoiceDate >= model.StartDate)
                            .Where(PurP.PurchaseInvoiceDate <= model.EndDate)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );

                    //Company List
                    model.CompanyList = connection.List<CompanyDetailsRow>(us => us
                         .SelectTableFields()
                         .Select(c.StateId)
                         );

                    var sb = new StringBuilder();
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}", "Supplier Name", "Invoice No", "Invoice Date", "Invoice Value", "Place Of Supply", "Supply Type", "Rate", "Taxable Value", "Integrated Tax Paid", "Central Tax Paid", "State/UT Tax Paid", "Cess Paid", "Eligibility For ITC", "Availed ITC Integrated Tax", "Availed ITC Central Tax", "Availed ITC State/UT Tax", "Availed ITC Cess", Environment.NewLine);
                    foreach (var line in model.PurchaseList)
                    {

                        foreach (var item in model.PurchaseProductList.Where(x => x.PurchaseId == line.Id).GroupBy(y => y.Percentage1))
                        {
                            if ((line.PurchaseFromGSTIN.IsNullOrEmpty()))
                            {

                                model.Supplier_Name = line.PurchaseFromName;

                                #region State 

                                bool stateChanged = false;

                                var Id = model.CompanyList.FirstOrDefault().StateId;

                                if (Id == line.PurchaseFromStateId)
                                {
                                    stateChanged = true;
                                }
                                else
                                {
                                    stateChanged = false;
                                }

                                #endregion

                                string invno;
                                if (!(line.InvoiceNo.IsNullOrEmpty()))
                                {
                                    invno = line.InvoiceDate.Value.Year.ToString() + "/" + line.InvoiceNo.ToString();
                                }
                                else
                                {
                                    invno = line.InvoiceDate.Value.Year.ToString() + "/" + line.Id.ToString();
                                }
                                model.InvoiceNo = invno;
                                model.InvoiceDate = line.InvoiceDate.Value.ToString("dd-MMM-yyyy");
                                model.Total = line.Total.Value.ToString("###0.00");

                                if (line.PurchaseFromStateId.HasValue)
                                {
                                    model.State = model.StateList.FirstOrDefault(x => x.Id == line.PurchaseFromStateId).State;

                                    if (model.State.ToUpper() == ("JAMMU AND KASHMIR"))
                                    {
                                        model.State = "01-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "HIMACHAL PRADESH")
                                    {
                                        model.State = "02-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "PUNJAB")
                                    {
                                        model.State = "03-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "CHANDIGARH")
                                    {
                                        model.State = "04-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "UTTARAKHAND")
                                    {
                                        model.State = "05-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "HARYANA")
                                    {
                                        model.State = "06-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "DELHI")
                                    {
                                        model.State = "07-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "RAJASTHAN")
                                    {
                                        model.State = "08-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "UTTAR  PRADESH")
                                    {
                                        model.State = "09-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "BIHAR")
                                    {
                                        model.State = "10-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "SIKKIM")
                                    {
                                        model.State = "11-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "ARUNACHAL PRADESH")
                                    {
                                        model.State = "12-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "NAGALAND")
                                    {
                                        model.State = "13-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "MANIPUR")
                                    {
                                        model.State = "14-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "MIZORAM")
                                    {
                                        model.State = "15-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "TRIPURA")
                                    {
                                        model.State = "16-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "MEGHLAYA")
                                    {
                                        model.State = "17-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "ASSAM")
                                    {
                                        model.State = "18-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "WEST BENGAL")
                                    {
                                        model.State = "19-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "JHARKHAND")
                                    {
                                        model.State = "20-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "ODISHA")
                                    {
                                        model.State = "21-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "CHATTISGARH")
                                    {
                                        model.State = "22-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "MADHYA PRADESH")
                                    {
                                        model.State = "23-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "GUJARAT")
                                    {
                                        model.State = "24-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "DAMAN AND DIU")
                                    {
                                        model.State = "25-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "DADRA AND NAGAR HAVELI")
                                    {
                                        model.State = "26-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "MAHARASHTRA")
                                    {
                                        model.State = "27-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "ANDHRA PRADESH(BEFORE DIVISION)")
                                    {
                                        model.State = "28-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "KARNATAKA")
                                    {
                                        model.State = "29-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "GOA")
                                    {
                                        model.State = "30-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "LAKSHWADEEP")
                                    {
                                        model.State = "31-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "KERALA")
                                    {
                                        model.State = "32-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "TAMIL NADU")
                                    {
                                        model.State = "33-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "PUDUCHERRY")
                                    {
                                        model.State = "34-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "ANDAMAN AND NICOBAR ISLANDS")
                                    {
                                        model.State = "35-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "TELANGANA")
                                    {
                                        model.State = "36-" + model.State;
                                    }
                                    else if (model.State.ToUpper() == "ANDHRA PRADESH (NEW)")
                                    {
                                        model.State = "37-" + model.State;
                                    }
                                }
                                else
                                {
                                    model.State = "";
                                }
                                if (stateChanged == true)
                                {
                                    model.SupplyType = "Inter State";
                                }
                                if (stateChanged == false)
                                {
                                    model.SupplyType = "Intra State";
                                }

                                double rate;
                                if (item.FirstOrDefault(x => x.PurchaseId == line.Id).Percentage1 < 1)
                                {
                                    rate = (double)(item.FirstOrDefault(x => x.PurchaseId == line.Id).Percentage1);
                                }
                                else
                                {
                                    rate = ((double)@item.FirstOrDefault(x => x.PurchaseId == line.Id).Percentage1) + ((double)@item.FirstOrDefault(x => x.PurchaseId == line.Id).Percentage2);
                                }
                                model.Rate = rate.ToString("###0.00");
                                model.TaxableValue = item.Sum(x => x.GrandTotal).Value.ToString("###0.00");

                                var tax1 = item.FirstOrDefault(x => x.PurchaseId == line.Id).Tax1Amount;

                                var tax2 = item.FirstOrDefault(x => x.PurchaseId == line.Id).Tax2Amount;

                                if (stateChanged == true)
                                {
                                    model.Integrated_Tax_Paid = ((decimal)(tax1 + tax2)).ToString("###0.00");
                                }
                                if (stateChanged == false)
                                {
                                    model.Central_Tax_Paid = ((decimal)(item.FirstOrDefault(x => x.PurchaseId == line.Id).Tax1Amount)).ToString("###0.00");
                                }


                                if (stateChanged == false)
                                {
                                    model.State_UT_Tax_Paid = ((decimal)(item.FirstOrDefault(x => x.PurchaseId == line.Id).Tax2Amount)).ToString("###0.00");
                                }

                                model.Eligibility_For_ITC = line.ITCEligibility.Value.GetEnumDescription(_localizer);

                                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}", model.Supplier_Name, model.InvoiceNo, model.InvoiceDate, model.Total, model.State, model.SupplyType, model.Rate, model.TaxableValue, model.Integrated_Tax_Paid, model.Central_Tax_Paid, model.State_UT_Tax_Paid, model.Cess_Paid, model.Eligibility_For_ITC, model.Availed_ITC_Integrated_Tax, model.Availed_ITC_Central_Tax, model.Availed_ITC_State_UT_Tax, model.Availed_ITC_Cess, Environment.NewLine);
                            }
                        }
                    }
                    //using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/GSTR2B2BUR.CSV"), false))
                    //{
                    //    _testData.WriteLine(sb.ToString()); // Write the file.
                    //}


                    var byteData = ASCIIEncoding.ASCII.GetBytes(sb.ToString());
                    return new FileContentResult(byteData, "text/csv");
                }
            }
            catch (Exception ex)
            {
                //TempData["Errormessage"] = ex.Message;
                return null;
            }
        }

        public static string GetEnumDescription(string value)
        {
            Type type = typeof(Masters.GSTITCEligibilityTypeMaster);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

    }
}