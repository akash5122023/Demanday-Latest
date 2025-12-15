
using AdvanceCRM.Masters;
using AdvanceCRM.Sales;
using AdvanceCRM.Administration;
using Serenity;
using Serenity.Data;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Serenity.Services;

namespace AdvanceCRM.Modules.Reports.GST
{
    [Route("GSTR1")]
    [ReadPermission("Reports:GSTR1")]
    public class GSTR1Controller : Controller
    {
        private readonly ISqlConnections _connections;
        protected IRequestContext Context { get; }

        public GSTR1Controller(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context;
        }
        // GET: GSTR1 B2B
        [HttpGet, Route("~/Reports/GSTR1Report")]
        public ActionResult Index()
        {

            return View(MVC.Views.Reports.GST.GSTR1ReportView);
        }


        public ActionResult LoadData(string StartDate, string EndDate)
        {
            try
            {

                var model = new GSTR1PageModel();

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
                var user = (UserDefinition)Context.User.ToUserDefinition();
                var ucom = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
                var Sal = SalesRow.Fields;
                var SalP = SalesProductsRow.Fields;
                var Stat = StateRow.Fields;
                var com = CompanyDetailsRow.Fields;

                using (var connection = _connections.NewFor<SalesRow>())
                {

                    //Sales  List
                    model.SalesList = connection.List<SalesRow>(us => us
                            .SelectTableFields()
                            .Select(Sal.Id)
                            .Select(Sal.Total)
                            .Select(Sal.Date)
                            .Select(Sal.ContactsGstin)
                            .Select(Sal.ContactsName)
                            .Select(Sal.InvoiceNo) //If this is empty then print Id
                            .Select(Sal.ContactsStateId)
                            .Select(Sal.ContactsEComGstin)
                            .Select(Sal.ReverseCharge)
                            .Select(Sal.InvoiceType)
                            .Select(Sal.CompanyId)
                            .Where(Sal.ContactsGstin.IsNotNull())
                            .Where(Sal.CompanyId == ucom)
                            .Where(Sal.Date >= model.StartDate)
                            .Where(Sal.Date <= model.EndDate)
                            );

                    //Sales Product  List
                    model.SalesProductList = connection.List<SalesProductsRow>(us => us
                            .SelectTableFields()
                            .Select(SalP.Id)
                            .Select(SalP.GrandTotal)
                            .Select(SalP.SalesId)
                            .Select(SalP.Percentage1)
                            .Select(SalP.Percentage2)
                            .Select(SalP.ProductsName)
                            .Select(SalP.ProductsHsn)
                            .Select(SalP.Quantity)
                            .Select(SalP.Price)
                            .Select(SalP.Discount)
                            .Select(SalP.DiscountAmount)
                            .Where(SalP.SalesDate >= model.StartDate)
                            .Where(SalP.SalesDate <= model.EndDate)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );

                    model.CompanyList = connection.List<CompanyDetailsRow>(us => us
                      .SelectTableFields()
                      .Select(com.Id)
                      .Select(com.StateId)
                      //.Where(com.Id==Sal.CompanyId)
                      );
                }
                return PartialView(MVC.Views.Reports.GST.GSTR1B2BPartialView, model);
            }
            catch (Exception ex)
            {
                //return PartialView(MVC.Views.Reports.GST.GSTR1B2BPartialView,ex.Message);
                return Json(ex.Message);
            }

        }


        [HttpGet]
        public FileContentResult ExportCSV(string StartDate, string EndDate)
        {
            try
            {

                var model = new GSTR1PageModel();

                if (StartDate == null && EndDate == null)
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = DateTime.Now.AddMonths(-1);
                    model.EndDate = DateTime.Now;
                }
                else
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = Convert.ToDateTime(StartDate, culture);
                    model.EndDate = Convert.ToDateTime(EndDate, culture);
                }
                var user = (UserDefinition)Context.User.ToUserDefinition();
                var Sal = SalesRow.Fields;
                var SalP = SalesProductsRow.Fields;
                var Stat = StateRow.Fields;
                //var msg = "";

                using (var connection = _connections.NewFor<SalesRow>())
                {
                    //Sales  List
                    model.SalesList = connection.List<SalesRow>(us => us
                            .SelectTableFields()
                            .Select(Sal.Id)
                            .Select(Sal.Total)
                            .Select(Sal.Date)
                            .Select(Sal.ContactsGstin)
                            .Select(Sal.ContactsName)
                            .Select(Sal.InvoiceNo) //If this is empty then print Id
                            .Select(Sal.InvoiceType)
                            .Select(Sal.ReverseCharge)
                            .Select(Sal.ContactsStateId)
                            .Select(Sal.ContactsEComGstin)
                            .Where(Sal.Date >= model.StartDate)
                            .Where(Sal.Date <= model.EndDate)
                            );

                    //Sales Product  List
                    model.SalesProductList = connection.List<SalesProductsRow>(us => us
                            .SelectTableFields()
                            .Select(SalP.Id)
                            .Select(SalP.GrandTotal)
                            .Select(SalP.SalesId)
                            .Select(SalP.Percentage1)
                            .Select(SalP.Percentage2)
                            .Where(SalP.SalesDate >= model.StartDate)
                            .Where(SalP.SalesDate <= model.EndDate)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );


                    var sb = new StringBuilder();
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", "ContactsGstin/UIN of Recipient", "Receiver Name", "Invoice No", "Invoice Date", "Invoice Value", "Place Of Supply", "Reverse Charge", "Invoice Type", "E-Commerce ContactsGstin", "Rate", "Applicable % of Tax Rate", "Taxable Value", "Cess Amount", Environment.NewLine);
                    foreach (var line in model.SalesList)
                    {
                        foreach (var item in model.SalesProductList.Where(x => x.SalesId == line.Id).GroupBy(y => y.Percentage1))
                        {
                            if (!(line.ContactsGstin.IsNullOrEmpty()))
                            {
                                model.GSTIN = line.ContactsGstin;
                                model.NAME = line.ContactsName;
                                string invno;
                                if (line.InvoiceNo.HasValue)
                                {
                                    invno = line.Date.Value.Year.ToString() + "/" + line.InvoiceNo.ToString();
                                }
                                else
                                {
                                    invno = line.Date.Value.Year.ToString() + "/" + line.Id.ToString();
                                }

                                model.InvoiceNo = invno;
                                model.InvoiceDate = line.Date.Value.ToString("dd-MMM-yyyy");
                                model.Total = line.Total.Value.ToString("###0.00");

                                if (line.ContactsStateId.HasValue)
                                {
                                    model.State = model.StateList.FirstOrDefault(x => x.Id == line.ContactsStateId).State;

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

                                if (!line.ContactsEComGstin.IsNullOrEmpty())
                                {
                                    model.ECommGSTIN = line.ContactsEComGstin;
                                }
                                else
                                {
                                    model.ECommGSTIN = "";
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
                                    model.InvoiceType = line.InvoiceType.Value.GetText(Context.Localizer);
                                }

                                double rate;
                                if (item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1 < 1)
                                {
                                    rate = (double)(item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1);
                                }
                                else
                                {
                                    rate = ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1) + ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage2);
                                }
                                model.Rate = rate.ToString("###0.00");
                                double appamt = ((double)(line.Total.Value) * rate) / 100;
                                model.ApplicablePer = appamt;
                                model.TaxableValue = item.Sum(x => x.GrandTotal).Value.ToString("###0.00");


                                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", model.GSTIN, model.NAME, model.InvoiceNo, model.InvoiceDate, model.Total, model.State, model.ReverseCharge, model.InvoiceType, model.ECommGSTIN, model.Rate, model.ApplicablePer, model.TaxableValue, null, Environment.NewLine);
                            }


                        }
                    }

                    //using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/GSTR1B2B.CSV"), false))
                    //{
                    //	_testData.WriteLine(sb.ToString()); // Write the file.
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

        [HttpGet]
        public FileContentResult ExportCSV1(string StartDate, string EndDate)
        {
            try
            {
                var model = new GSTR1PageModel();
                if (StartDate == null && EndDate == null)
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = DateTime.Now.AddMonths(-1);
                    model.EndDate = DateTime.Now;
                }
                else
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = Convert.ToDateTime(StartDate, culture);
                    model.EndDate = Convert.ToDateTime(EndDate, culture);
                }
                var user = (UserDefinition)Context.User.ToUserDefinition();
                var ucom = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
                var Sal = SalesRow.Fields;
                var SalP = SalesProductsRow.Fields;
                var Stat = StateRow.Fields;
                var com = CompanyDetailsRow.Fields;
                //var msg = "";
                var sb = new StringBuilder();
                using (var connection = _connections.NewFor<SalesRow>())
                {
                    //Sales  List
                    model.SalesList = connection.List<SalesRow>(us => us
                            .SelectTableFields()
                            .Select(Sal.Id)
                            .Select(Sal.Total)
                            .Select(Sal.Date)
                            .Select(Sal.ContactsGstin)
                            .Select(Sal.ContactsName)
                            .Select(Sal.InvoiceNo) //If this is empty then print Id
                            .Select(Sal.InvoiceType)
                            .Select(Sal.ReverseCharge)
                            .Select(Sal.ContactsStateId)
                            .Select(Sal.ContactsEComGstin)
                            .Where(Sal.Date >= model.StartDate)
                            .Where(Sal.Date <= model.EndDate)
                            );

                    //Sales Product  List
                    model.SalesProductList = connection.List<SalesProductsRow>(us => us
                            .SelectTableFields()
                            .Select(SalP.Id)
                            .Select(SalP.GrandTotal)
                            .Select(SalP.SalesId)
                            .Select(SalP.TaxType1)
                            .Select(SalP.TaxType2)
                            .Select(SalP.Tax1Amount)
                            .Select(SalP.Tax2Amount)
                            .Select(SalP.Percentage1)
                            .Select(SalP.Percentage2)
                           
                            .Where(SalP.SalesDate >= model.StartDate)
                            .Where(SalP.SalesDate <= model.EndDate)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );


                    model.CompanyList = connection.List<CompanyDetailsRow>(us => us
                      .SelectTableFields()
                      .Select(com.Id)
                      .Select(com.StateId)
                      //.Where(com.Id==Sal.CompanyId)
                      );


                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38}", "ContactsGstin/UIN of Recipient", "Receiver Name", "Invoice No", "Invoice Date", "Invoice Value", "Place Of Supply", "Reverse Charge", "Invoice Type", "E-Commerce ContactsGstin", "Rate", "Applicable % of Tax Rate", "Taxable Value", "Cess Amount", "0% taxable Amount", "5% taxable Amount","2.5% Type1","2.5% Type1 TaxAmount", "2.5% Type2", "2.5% Type2 TaxAmount","5% IGST", "12% taxable Amount", "6% Type1", "6% Type1 TaxAmount", "6% Type2", "6% Type2 TaxAmount", "12% IGST", "18% taxable Amount", "9% Type1", "9% Type1 TaxAmount", "9% Type2", "9% Type2 TaxAmount", "18% IGST", "28% taxable Amount", "14% Type1", "14% Type1 TaxAmount", "14% Type2", "14% Type2 TaxAmount", "28% IGST", Environment.NewLine);
                    foreach (var line in model.SalesList)
                    {
                        foreach (var com1 in model.CompanyList.Where(x => x.Id == line.CompanyId))
                        {
                            int comstate = (int)com1.StateId;
                            // foreach (var item in model.SalesProductList.Where(x => x.SalesId == line.Id).GroupBy(y => y.Percentage1))
                            foreach (var item in model.SalesProductList.Where(x => x.SalesId == line.Id))
                            {
                                if (!(line.ContactsGstin.IsNullOrEmpty()))
                                {
                                    model.GSTIN = line.ContactsGstin;
                                    model.NAME = line.ContactsName;
                                    string invno;
                                    if (line.InvoiceNo.HasValue)
                                    {
                                        invno = line.Date.Value.Year.ToString() + "/" + line.InvoiceNo.ToString();
                                    }
                                    else
                                    {
                                        invno = line.Date.Value.Year.ToString() + "/" + line.Id.ToString();
                                    }

                                    model.InvoiceNo = invno;
                                    model.InvoiceDate = line.Date.Value.ToString("dd-MMM-yyyy");
                                    model.Total = line.Total.Value.ToString("###0.00");

                                    if (line.ContactsStateId.HasValue)
                                    {
                                        model.State = model.StateList.FirstOrDefault(x => x.Id == line.ContactsStateId).State;

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

                                    if (!line.ContactsEComGstin.IsNullOrEmpty())
                                    {
                                        model.ECommGSTIN = line.ContactsEComGstin;
                                    }
                                    else
                                    {
                                        model.ECommGSTIN = "";
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
                                        model.InvoiceType = line.InvoiceType.Value.GetText(Context.Localizer);
                                    }
                                    dynamic tax0=0,tt5 = 0, tt12 = 0, tt18 = 0, tt28 = 0, tt25 = 0, tt212 = 0, tt218 = 0, tt228 = 0, ta5 = 0, ta12 = 0, ta18 = 0, ta28 = 0, ta25 = 0, ta212 = 0, ta218 = 0, ta228 = 0, ti5 = 0, ti12 = 0, ti18 = 0, ti28 = 0, tia5 = 0, tia12 = 0, tia18 = 0, tia28 = 0;
                                    dynamic tax5=0, tax12=0, tax18=0, tax28=0;
                                    foreach (var item1 in model.SalesProductList.Where(x => x.SalesId == line.Id))
                                    {
                                        double ptot = 0, txamt1 = 0, txamt2 = 0, txtotamt = 0, igstamt = 0, igstper = 0;
                                        
                                        if (item.Discount != 0)
                                        {
                                            ptot = Convert.ToDouble(item1.Price * item1.Quantity);
                                            double dis = ((double)item1.Price * (double)item1.Quantity * (double)item1.Discount) / 100;
                                            ptot = (ptot - dis);
                                            ptot.ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            ptot = Convert.ToDouble(item1.Price * item1.Quantity);
                                            ptot.ToString("#,##0.00");
                                        }

                                        dynamic tt = Convert.ToDouble(item1.Percentage1);

                                        if (comstate == line.ContactsStateId)
                                        {
                                            if (tt == 2.5)
                                            {
                                                tt5 = Convert.ToString(item1.TaxType1) + Convert.ToString(item1.Percentage1);
                                                tt25 = Convert.ToString(item1.TaxType2) + Convert.ToString(item1.Percentage2);

                                                ta5 += ptot * ((double)item1.Percentage1 / 100);
                                                ta25 += ptot * ((double)item1.Percentage2 / 100);
                                                tax5 += ptot;

                                            }
                                            else if (tt == 6)
                                            {
                                                tt12 = Convert.ToString(item1.TaxType1) + Convert.ToString(item1.Percentage1);
                                                tt212 = Convert.ToString(item1.TaxType2) + Convert.ToString(item1.Percentage2);

                                                ta12 += ptot * ((double)item1.Percentage1 / 100);
                                                ta212 += ptot * ((double)item1.Percentage2 / 100);
                                                tax12 += ptot;
                                            }
                                            else if (tt == 9)
                                            {
                                                tt18 = Convert.ToString(item1.TaxType1) + Convert.ToString(item1.Percentage1);
                                                tt218 = Convert.ToString(item1.TaxType2) + Convert.ToString(item1.Percentage2);

                                                ta18 += ptot * ((double)item1.Percentage1 / 100);
                                                ta218 += ptot * ((double)item1.Percentage2 / 100);
                                                tax18 += ptot;
                                            }
                                            else if (tt == 14)
                                            {
                                                tt28 = Convert.ToString(item1.TaxType1) + Convert.ToString(item1.Percentage1);
                                                tt228 = Convert.ToString(item1.TaxType2) + Convert.ToString(item1.Percentage2);

                                                ta28 += ptot * ((double)item1.Percentage1 / 100);
                                                ta228 += ptot * ((double)item1.Percentage2 / 100);
                                                tax28 += ptot;
                                            }
                                            else
                                            {
                                                tax0 += ptot;
                                            }
                                        }
                                        else
                                        {
                                            if (tt == 2.5)
                                            {
                                                ti5 = (double)item.Percentage1 + (double)item.Percentage2;
                                                tia5 += ptot * (((double)item.Percentage1 + (double)item.Percentage2) / 100);
                                                tax5 += ptot;
                                            }
                                            else if (tt == 6)
                                            {
                                                ti12 = (double)item.Percentage1 + (double)item.Percentage2;
                                                tia12 += ptot * (((double)item.Percentage1 + (double)item.Percentage2) / 100);
                                                tax12 += ptot;
                                            }
                                            else if (tt == 9)
                                            {
                                                ti18 = (double)item.Percentage1 + (double)item.Percentage2;
                                                tia18 += ptot * (((double)item.Percentage1 + (double)item.Percentage2) / 100);
                                                tax18 += ptot;
                                            }
                                            else if (tt == 1)
                                            {
                                                ti28 = (double)item.Percentage1 + (double)item.Percentage2;
                                                tia28 += ptot * (((double)item.Percentage1 + (double)item.Percentage2) / 100);
                                                tax28 += ptot;
                                            }
                                            else
                                            {
                                                tax0 += ptot;
                                            }
                                        }

                                        model.taxper15 = Convert.ToString(tt5); model.taxper25 = Convert.ToString(tt25);
                                        model.taxamt15 = Convert.ToString(ta5); model.taxamt25 = Convert.ToString(ta25);

                                        model.taxper112 = Convert.ToString(tt12); model.taxper212 = Convert.ToString(tt212);
                                        model.taxamt112 = Convert.ToString(ta12); model.taxamt212 = Convert.ToString(ta212);

                                        model.taxper118 = Convert.ToString(tt18); model.taxper218 = Convert.ToString(tt218);
                                        model.taxamt118 = Convert.ToString(ta18); model.taxamt218 = Convert.ToString(ta218);

                                        model.taxper128 = Convert.ToString(tt28); model.taxper228 = Convert.ToString(tt228);
                                        model.taxamt128 = Convert.ToString(ta28); model.taxamt228 = Convert.ToString(ta228);

                                        model.igst5 = Convert.ToString(ti5); model.igstamt5 = Convert.ToString(tia5);
                                        model.igst12 = Convert.ToString(ti12); model.igstamt12 = Convert.ToString(tia12);
                                        model.igst18 = Convert.ToString(ti18); model.igstamt18 = Convert.ToString(tia18);
                                        model.igst28 = Convert.ToString(ti28); model.igstamt28 = Convert.ToString(tia28);
                                       // model.igst5 = Convert.ToString(ti5); model.igstamt5 = Convert.ToString(tia5);

                                        model.taxable0= Convert.ToString(tax0);
                                        model.taxable5 = Convert.ToString(tax5);
                                        model.taxable12 = Convert.ToString(tax12);
                                        model.taxable18 = Convert.ToString(tax18);
                                        model.taxable28 = Convert.ToString(tax28);

                                    }
                                    


                                }

                                //double rate = 0;
                                //if (item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1 < 1)
                                //{
                                //    rate = (double)(item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1);
                                //}
                                //else
                                //{
                                //    rate = ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1) + ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage2);
                                //}
                                //model.Rate = rate.ToString("###0.00");
                                //model.ApplicablePer = ((double)(line.Total.Value) * rate) / 100;
                                //model.TaxableValue = item.Sum(x => x.GrandTotal).Value.ToString("###0.00");


                             
                            }
                        }

                        sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38}", model.GSTIN, model.NAME, model.InvoiceNo, model.InvoiceDate, model.Total, model.State, model.ReverseCharge, model.InvoiceType, model.ECommGSTIN, model.Rate, null, model.TaxableValue, null,model.taxable0,model.taxable5, model.taxper15,model.taxamt15, model.taxper25, model.taxamt25,model.igstamt5, model.taxable12, model.taxper112, model.taxamt112, model.taxper212, model.taxamt212, model.igstamt12, model.taxable18, model.taxper118, model.taxamt118, model.taxper218, model.taxamt218, model.igstamt18, model.taxable28, model.taxper128, model.taxper128, model.taxper228, model.taxper228, model.igstamt28, Environment.NewLine);

                    }

                    //using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/GSTR1B2B.CSV"), false))
                    //{
                    //	_testData.WriteLine(sb.ToString()); // Write the file.
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
            //try
            //{
            //    var model = new GSTR1PageModel();

            //    if (StartDate == null && EndDate == null)
            //    {
            //        CultureInfo culture = new CultureInfo("en-US");
            //        model.StartDate = DateTime.Now.AddMonths(-1);
            //        model.EndDate = DateTime.Now;
            //    }
            //    else
            //    {
            //        CultureInfo culture = new CultureInfo("en-US");
            //        model.StartDate = Convert.ToDateTime(StartDate, culture);
            //        model.EndDate = Convert.ToDateTime(EndDate, culture);
            //    }
            //    var user = (UserDefinition)Context.User.ToUserDefinition();
            //    var ucom = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
            //    var Sal = SalesRow.Fields;
            //    var SalP = SalesProductsRow.Fields;
            //    var Stat = StateRow.Fields;
            //    var com = CompanyDetailsRow.Fields;
            //    //var msg = "";

            //    using (var connection = _connections.NewFor<SalesRow>())
            //    {
            //        //Sales  List
            //        model.SalesList = connection.List<SalesRow>(us => us
            //                .SelectTableFields()
            //                .Select(Sal.Id)
            //                .Select(Sal.Total)
            //                .Select(Sal.Date)
            //                .Select(Sal.ContactsGstin)
            //                .Select(Sal.ContactsName)
            //                .Select(Sal.InvoiceNo) //If this is empty then print Id
            //                .Select(Sal.ContactsStateId)
            //                .Select(Sal.ContactsEComGstin)
            //                .Select(Sal.ReverseCharge)
            //                .Select(Sal.InvoiceType)
            //                .Select(Sal.CompanyId)
            //                .Where(Sal.ContactsGstin.IsNotNull())
            //                .Where(Sal.CompanyId == ucom)
            //                .Where(Sal.Date >= model.StartDate)
            //                .Where(Sal.Date <= model.EndDate)
            //                );

            //        //Sales Product  List
            //        model.SalesProductList = connection.List<SalesProductsRow>(us => us
            //                .SelectTableFields()
            //                .Select(SalP.Id)
            //                .Select(SalP.GrandTotal)
            //                .Select(SalP.SalesId)
            //                .Select(SalP.Percentage1)
            //                .Select(SalP.Percentage2)
            //                .Select(SalP.ProductsName)
            //                .Select(SalP.ProductsHsn)
            //                .Select(SalP.Quantity)
            //                .Select(SalP.Price)
            //                .Select(SalP.Discount)
            //                .Select(SalP.DiscountAmount)
            //                .Where(SalP.SalesDate >= model.StartDate)
            //                .Where(SalP.SalesDate <= model.EndDate)
            //                );

            //        //State List
            //        model.StateList = connection.List<StateRow>(us => us
            //            .SelectTableFields()
            //            .Select(Stat.Id)
            //            .Select(Stat.State)
            //            );

            //        model.CompanyList = connection.List<CompanyDetailsRow>(us => us
            //          .SelectTableFields()
            //          .Select(com.Id)
            //          .Select(com.StateId)
            //          //.Where(com.Id==Sal.CompanyId)
            //          );


            //        var sb = new StringBuilder();
            //        sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", "ContactsGstin/UIN of Recipient", "Receiver Name", "Invoice No", "Invoice Date", "Invoice Value", "Place Of Supply", "Reverse Charge", "Invoice Type", "E-Commerce ContactsGstin", "Rate", "Applicable % of Tax Rate", "Taxable Value", "Cess Amount", Environment.NewLine);
            //        foreach (var line in model.SalesList)
            //        {
            //            foreach (var com1 in model.CompanyList.Where(x => x.Id == line.CompanyId))
            //            {
            //                int comstate = (int)com1.StateId;
            //                foreach (var item in model.SalesProductList.Where(x => x.SalesId == line.Id).GroupBy(y => y.Percentage1))
            //                {
            //                    if (!(line.ContactsGstin.IsNullOrEmpty()))
            //                    {
            //                        model.GSTIN = line.ContactsGstin;
            //                        model.NAME = line.ContactsName;
            //                        string invno;
            //                        if (line.InvoiceNo.HasValue)
            //                        {
            //                            invno = line.Date.Value.Year.ToString() + "/" + line.InvoiceNo.ToString();
            //                        }
            //                        else
            //                        {
            //                            invno = line.Date.Value.Year.ToString() + "/" + line.Id.ToString();
            //                        }

            //                        model.InvoiceNo = invno;
            //                        model.InvoiceDate = line.Date.Value.ToString("dd-MMM-yyyy");
            //                        model.Total = line.Total.Value.ToString("###0.00");

            //                        if (line.ContactsStateId.HasValue)
            //                        {
            //                            model.State = model.StateList.FirstOrDefault(x => x.Id == line.ContactsStateId).State;

            //                            if (model.State.ToUpper() == ("JAMMU AND KASHMIR"))
            //                            {
            //                                model.State = "01-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "HIMACHAL PRADESH")
            //                            {
            //                                model.State = "02-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "PUNJAB")
            //                            {
            //                                model.State = "03-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "CHANDIGARH")
            //                            {
            //                                model.State = "04-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "UTTARAKHAND")
            //                            {
            //                                model.State = "05-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "HARYANA")
            //                            {
            //                                model.State = "06-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "DELHI")
            //                            {
            //                                model.State = "07-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "RAJASTHAN")
            //                            {
            //                                model.State = "08-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "UTTAR  PRADESH")
            //                            {
            //                                model.State = "09-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "BIHAR")
            //                            {
            //                                model.State = "10-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "SIKKIM")
            //                            {
            //                                model.State = "11-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "ARUNACHAL PRADESH")
            //                            {
            //                                model.State = "12-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "NAGALAND")
            //                            {
            //                                model.State = "13-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "MANIPUR")
            //                            {
            //                                model.State = "14-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "MIZORAM")
            //                            {
            //                                model.State = "15-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "TRIPURA")
            //                            {
            //                                model.State = "16-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "MEGHLAYA")
            //                            {
            //                                model.State = "17-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "ASSAM")
            //                            {
            //                                model.State = "18-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "WEST BENGAL")
            //                            {
            //                                model.State = "19-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "JHARKHAND")
            //                            {
            //                                model.State = "20-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "ODISHA")
            //                            {
            //                                model.State = "21-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "CHATTISGARH")
            //                            {
            //                                model.State = "22-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "MADHYA PRADESH")
            //                            {
            //                                model.State = "23-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "GUJARAT")
            //                            {
            //                                model.State = "24-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "DAMAN AND DIU")
            //                            {
            //                                model.State = "25-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "DADRA AND NAGAR HAVELI")
            //                            {
            //                                model.State = "26-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "MAHARASHTRA")
            //                            {
            //                                model.State = "27-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "ANDHRA PRADESH(BEFORE DIVISION)")
            //                            {
            //                                model.State = "28-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "KARNATAKA")
            //                            {
            //                                model.State = "29-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "GOA")
            //                            {
            //                                model.State = "30-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "LAKSHWADEEP")
            //                            {
            //                                model.State = "31-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "KERALA")
            //                            {
            //                                model.State = "32-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "TAMIL NADU")
            //                            {
            //                                model.State = "33-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "PUDUCHERRY")
            //                            {
            //                                model.State = "34-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "ANDAMAN AND NICOBAR ISLANDS")
            //                            {
            //                                model.State = "35-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "TELANGANA")
            //                            {
            //                                model.State = "36-" + model.State;
            //                            }
            //                            else if (model.State.ToUpper() == "ANDHRA PRADESH (NEW)")
            //                            {
            //                                model.State = "37-" + model.State;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            model.State = "";
            //                        }

            //                        if (!line.ContactsEComGstin.IsNullOrEmpty())
            //                        {
            //                            model.ECommGSTIN = line.ContactsEComGstin;
            //                        }
            //                        else
            //                        {
            //                            model.ECommGSTIN = "";
            //                        }
            //                        if (line.ReverseCharge.HasValue)
            //                        {
            //                            if (line.ReverseCharge == true)
            //                            {
            //                                model.ReverseCharge = "Y";
            //                            }
            //                            else
            //                            {
            //                                model.ReverseCharge = "N";
            //                            }
            //                        }

            //                        if (line.InvoiceType.HasValue)
            //                        {
            //                            //TODO: have to get enum text
            //                            model.InvoiceType = line.InvoiceType.Value.GetText();
            //                        }

            //                        double rate;
            //                        if (item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1 < 1)
            //                        {
            //                            rate = (double)(item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1);
            //                        }
            //                        else
            //                        {
            //                            rate = ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1) + ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage2);
            //                        }
            //                        model.Rate = rate.ToString("###0.00");
            //                        model.TaxableValue = item.Sum(x => x.GrandTotal).Value.ToString("###0.00");


            //                        sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", model.GSTIN, model.NAME, model.InvoiceNo, model.InvoiceDate, model.Total, model.State, model.ReverseCharge, model.InvoiceType, model.ECommGSTIN, model.Rate, null, model.TaxableValue, null, Environment.NewLine);
            //                    }


            //                }

            //                ///Product List
            //                foreach (var item in model.SalesProductList.Where(x => x.SalesId == line.Id))
            //                {
            //                    model.ProductName = item.ProductsName;
            //                    model.Price = Convert.ToString(item.Price);
            //                    model.discount = Convert.ToString(item.Discount);
            //                    model.qty = Convert.ToString(item.Quantity);

            //                    double ptot = 0, txamt1 = 0, txamt2 = 0, txtotamt = 0, igstamt = 0, igstper = 0;
            //                    if (item.Discount != 0)
            //                    {
            //                        ptot = Convert.ToDouble(item.Price * item.Quantity);
            //                        double dis = ((double)item.Price * (double)item.Quantity * (double)item.Discount) / 100;
            //                        ptot = (ptot - dis);
            //                        ptot.ToString("#,##0.00");
            //                    }
            //                    else
            //                    {
            //                        ptot = Convert.ToDouble(item.Price * item.Quantity);
            //                        ptot.ToString("#,##0.00");
            //                    }


            //                    if (comstate == line.ContactsStateId)
            //                    {
            //                        model.taxtype1 = Convert.ToString(item.TaxType1) + Convert.ToString(item.Percentage1);
            //                    }

            //                    if (comstate == line.ContactsStateId)
            //                    {
            //                        txamt1 = ptot * ((double)item.Percentage1 / 100);
            //                        model.taxamt1 = txamt1;
            //                        model.taxamt1.ToString("#,##0.00");
            //                    }

            //                    if (comstate == line.ContactsStateId)
            //                    {
            //                        model.taxtype2 = Convert.ToString(item.TaxType2) + Convert.ToString(item.Percentage2);
            //                    }

            //                    if (comstate == line.ContactsStateId)
            //                    {
            //                        txamt2 = ptot * ((double)item.Percentage2 / 100);
            //                        model.taxamt2 = txamt2;
            //                        model.taxamt2.ToString("#,##0.00");
            //                    }

            //                    //igst
            //                    if (comstate != line.ContactsStateId)
            //                    {
            //                        model.igst = (double)item.Percentage1 + (double)item.Percentage2;
            //                        model.igst.ToString("#,##0.00");
            //                    }
            //                    else
            //                    {
            //                        model.igst = 0;
            //                    }

            //                    if (comstate != line.ContactsStateId)
            //                    {
            //                        model.igstamt = ptot * (((double)item.Percentage1 + (double)item.Percentage2) / 100);
            //                        model.igstamt.ToString("#,##0.00");
            //                    }
            //                    else
            //                    {
            //                        model.igstamt = 0;
            //                    }

            //                    model.totamt = ptot + model.taxamt1 + model.taxamt2 + model.igstamt;
            //                    model.totamt.ToString("#,##0.00");




            //                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", model.ProductName, model.Price, model.discount, model.qty, model.Ptotal, model.taxtype1, model.taxamt1, model.taxtype2, model.taxamt2, model.igst, model.igstamt, model.totamt, null, Environment.NewLine, ConsoleColor.Green);



            //                }
            //            }
            //        }

            //        //using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/GSTR1B2B.CSV"), false))
            //        //{
            //        //	_testData.WriteLine(sb.ToString()); // Write the file.
            //        //}

            //        //msg = "Exported to CSV Successfully";			

            //        var byteData = ASCIIEncoding.ASCII.GetBytes(sb.ToString());
            //        return new FileContentResult(byteData, "text/csv");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    //TempData["Errormessage"] = ex.Message;	
            //    return null;
            //}

        }

        [HttpGet]
        public FileContentResult ExportCSV2(string StartDate, string EndDate)
        {
            try
            {

                var model = new GSTR1PageModel();

                if (StartDate == null && EndDate == null)
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = DateTime.Now.AddMonths(-1);
                    model.EndDate = DateTime.Now;
                }
                else
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = Convert.ToDateTime(StartDate, culture);
                    model.EndDate = Convert.ToDateTime(EndDate, culture);
                }
                var user = (UserDefinition)Context.User.ToUserDefinition();
                var ucom = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
                var Sal = SalesRow.Fields;
                var SalP = SalesProductsRow.Fields;
                var Stat = StateRow.Fields;
                var com = CompanyDetailsRow.Fields;
                //var msg = "";
                var sb = new StringBuilder();
                using (var connection = _connections.NewFor<SalesRow>())
                {
                    //Sales  List
                    model.SalesList = connection.List<SalesRow>(us => us
                            .SelectTableFields()
                            .Select(Sal.Id)
                            .Select(Sal.Total)
                            .Select(Sal.Date)
                            .Select(Sal.ContactsGstin)
                            .Select(Sal.ContactsName)
                            .Select(Sal.InvoiceNo) //If this is empty then print Id
                            .Select(Sal.InvoiceType)
                            .Select(Sal.ReverseCharge)
                            .Select(Sal.ContactsStateId)
                            .Select(Sal.ContactsEComGstin)
                            .Where(Sal.Date >= model.StartDate)
                            .Where(Sal.Date <= model.EndDate)
                            );

                    //Sales Product  List
                    model.SalesProductList = connection.List<SalesProductsRow>(us => us
                            .SelectTableFields()
                            .Select(SalP.Id)
                            .Select(SalP.GrandTotal)
                            .Select(SalP.SalesId)
                            .Select(SalP.TaxType1)
                            .Select(SalP.TaxType2)
                            .Select(SalP.Tax1Amount)
                            .Select(SalP.Tax2Amount)
                            .Select(SalP.Percentage1)
                            .Select(SalP.Percentage2)
                             .Where(SalP.Id == Sal.Id)
                            //.Where(SalP.SalesDate >= model.StartDate)
                            //.Where(SalP.SalesDate <= model.EndDate)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );


                    model.CompanyList = connection.List<CompanyDetailsRow>(us => us
                      .SelectTableFields()
                      .Select(com.Id)
                      .Select(com.StateId)
                      //.Where(com.Id==Sal.CompanyId)
                      );


                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", "ContactsGstin/UIN of Recipient", "Receiver Name", "Invoice No", "Invoice Date", "Invoice Value", "Place Of Supply", "Reverse Charge", "Invoice Type", "E-Commerce ContactsGstin", "Rate", "Applicable % of Tax Rate", "Taxable Value", "Cess Amount", Environment.NewLine);
                    foreach (var line in model.SalesList)
                    {
                        foreach (var com1 in model.CompanyList.Where(x => x.Id == line.CompanyId))
                        {
                            int comstate = (int)com1.StateId;
                            // foreach (var item in model.SalesProductList.Where(x => x.SalesId == line.Id).GroupBy(y => y.Percentage1))
                            foreach (var item in model.SalesProductList.Where(x => x.SalesId == line.Id))
                            {
                                if (!(line.ContactsGstin.IsNullOrEmpty()))
                                {
                                    model.GSTIN = line.ContactsGstin;
                                    model.NAME = line.ContactsName;
                                    string invno;
                                    if (line.InvoiceNo.HasValue)
                                    {
                                        invno = line.Date.Value.Year.ToString() + "/" + line.InvoiceNo.ToString();
                                    }
                                    else
                                    {
                                        invno = line.Date.Value.Year.ToString() + "/" + line.Id.ToString();
                                    }

                                    model.InvoiceNo = invno;
                                    model.InvoiceDate = line.Date.Value.ToString("dd-MMM-yyyy");
                                    model.Total = line.Total.Value.ToString("###0.00");

                                    if (line.ContactsStateId.HasValue)
                                    {
                                        model.State = model.StateList.FirstOrDefault(x => x.Id == line.ContactsStateId).State;

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

                                    if (!line.ContactsEComGstin.IsNullOrEmpty())
                                    {
                                        model.ECommGSTIN = line.ContactsEComGstin;
                                    }
                                    else
                                    {
                                        model.ECommGSTIN = "";
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
                                        model.InvoiceType = line.InvoiceType.Value.GetText(Context.Localizer);
                                    }

                                    double ptot = 0, txamt1 = 0, txamt2 = 0, txtotamt = 0, igstamt = 0, igstper = 0;
                                    dynamic tt5, tt12, tt18, tt28, tt25, tt212, tt218, tt228, ta5, ta12, ta18, ta28, ta25, ta212, ta218, ta228, ti5, ti12, ti18, ti28, tia5, tia12, tia18, tia28;
                                    foreach (var test in model.SalesProductList.Where(x => x.SalesId == line.Id).GroupBy(y => y.Percentage1))
                                    {
                                        if (item.Discount != 0)
                                        {
                                            ptot = Convert.ToDouble(item.Price * item.Quantity);
                                            double dis = ((double)item.Price * (double)item.Quantity * (double)item.Discount) / 100;
                                            ptot = (ptot - dis);
                                            ptot.ToString("#,##0.00");
                                        }
                                        else
                                        {
                                            ptot = Convert.ToDouble(item.Price * item.Quantity);
                                            ptot.ToString("#,##0.00");
                                        }

                                        dynamic tt = test;
                                        if (comstate == line.ContactsStateId)
                                        {
                                            if (tt == 2.5)
                                            {
                                                tt5 = Convert.ToString(item.TaxType1) + Convert.ToString(item.Percentage1);
                                                tt5 = Convert.ToString(item.TaxType2) + Convert.ToString(item.Percentage2);

                                                ta5 = ptot * ((double)item.Percentage1 / 100);
                                                //model.taxamt1 = txamt1;
                                                //model.taxamt1.ToString("#,##0.00");

                                                ta25 = ptot * ((double)item.Percentage2 / 100);
                                                //model.taxamt2 = txamt2;
                                                //model.taxamt2.ToString("#,##0.00");
                                            }
                                        }
                                        else
                                        {
                                            ti5= (double)item.Percentage1 + (double)item.Percentage2;
                                         //   model.igst.ToString("#,##0.00");

                                           tia5 = ptot * (((double)item.Percentage1 + (double)item.Percentage2) / 100);
                                           // model.igstamt.ToString("#,##0.00");
                                        }
                                    }
                                }

                                double rate = 0;
                                //if (item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1 < 1)
                                //{
                                //    rate = (double)(item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1);
                                //}
                                //else
                                //{
                                //    rate = ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1) + ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage2);
                                //}
                                //model.Rate = rate.ToString("###0.00");
                                //model.ApplicablePer = ((double)(line.Total.Value) * rate) / 100;
                                //model.TaxableValue = item.Sum(x => x.GrandTotal).Value.ToString("###0.00");


                                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", model.GSTIN, model.NAME, model.InvoiceNo, model.InvoiceDate, model.Total, model.State, model.ReverseCharge, model.InvoiceType, model.ECommGSTIN, model.Rate, null, model.TaxableValue, null, Environment.NewLine);

                            }
                        }
                    }

                        //using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/GSTR1B2B.CSV"), false))
                        //{
                        //	_testData.WriteLine(sb.ToString()); // Write the file.
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

        [HttpGet, Route("~/Reports/GSTR1B2CSReport")]
        public ActionResult GSTR1B2CS()
        {
            return View(MVC.Views.Reports.GST.GSTR1B2CSReportView);
        }


        //Partial View for GST-R1 B2CS 
        public ActionResult LoadDataB2CS(string StartDate, string EndDate)
        {
            try
            {

                var model = new GSTR1B2CSPageModel();

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

                var user = (UserDefinition)Context.User.ToUserDefinition();
                var Sal = SalesRow.Fields;
                var SalP = SalesProductsRow.Fields;
                var Stat = StateRow.Fields;

                using (var connection = _connections.NewFor<SalesRow>())
                {
                    //Sales  List
                    model.SalesList = connection.List<SalesRow>(us => us
                          .SelectTableFields()
                          .Select(Sal.Id)
                          .Select(Sal.Total)
                          .Select(Sal.Date)
                          .Select(Sal.ContactsGstin)
                          .Select(Sal.ContactsName)
                          .Select(Sal.InvoiceNo) //If this is empty then print Id
                          .Select(Sal.InvoiceType)
                          .Select(Sal.ReverseCharge)
                          .Select(Sal.ContactsStateId)
                          .Select(Sal.ContactsEComGstin)
                          .Select(Sal.EcomType)
                          .Where(Sal.Total < 250000)
                          .Where(Sal.ContactsGstin.IsNull())
                          );

                    //Sales Product  List
                    model.SalesProductList = connection.List<SalesProductsRow>(us => us
                            .SelectTableFields()
                            .Select(SalP.Id)
                            .Select(SalP.GrandTotal)
                            .Select(SalP.SalesId)
                            .Select(SalP.Percentage1)
                            .Select(SalP.Percentage2)
                            .Where(SalP.GrandTotal < 250000)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );
                }
                return PartialView(MVC.Views.Reports.GST.GSTR1B2CSPartialView, model);
            }
            catch (Exception ex)
            {
                //return PartialView(MVC.Views.Reports.GST.GSTR1B2BPartialView,ex.Message);
                return Json(ex.Message);
            }

        }


        //Export to CSV GST-R1 B2CS 
        [HttpPost]
        public FileContentResult ExportCSVB2CS(string StartDate, string EndDate)
        {
            try
            {

                var model = new GSTR1B2CSPageModel();

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
                var user = (UserDefinition)Context.User.ToUserDefinition();
                var Sal = SalesRow.Fields;
                var SalP = SalesProductsRow.Fields;
                var Stat = StateRow.Fields;
                //var msg = "";

                using (var connection = _connections.NewFor<SalesRow>())
                {
                    //Sales  List
                    model.SalesList = connection.List<SalesRow>(us => us
                          .SelectTableFields()
                          .Select(Sal.Id)
                          .Select(Sal.Total)
                          .Select(Sal.Date)
                          .Select(Sal.ContactsGstin)
                          .Select(Sal.ContactsName)
                          .Select(Sal.InvoiceNo) //If this is empty then print Id
                          .Select(Sal.ContactsStateId)
                          .Select(Sal.ContactsEComGstin)
                          .Select(Sal.EcomType)
                          .Where(Sal.Total < 250000)
                          .Where(Sal.ContactsGstin.IsNull())
                          );

                    //Sales Product  List
                    model.SalesProductList = connection.List<SalesProductsRow>(us => us
                            .SelectTableFields()
                            .Select(SalP.Id)
                            .Select(SalP.GrandTotal)
                            .Select(SalP.SalesId)
                            .Select(SalP.Percentage1)
                            .Select(SalP.Percentage2)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );

                    var sb = new StringBuilder();
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7}", "Type", "Place Of Supply", "Rate", "Applicable % of Tax Rate", "Taxable Value", "Cess Amount", "E - Commerce ContactsGstin", Environment.NewLine);
                    foreach (var line in model.SalesList)
                    {
                        foreach (var item in model.SalesProductList.Where(x => x.SalesId == line.Id).GroupBy(y => y.Percentage1))
                        {
                            if ((line.ContactsGstin.IsNullOrEmpty()))
                            {
                                {
                                    //model.Type = (Int32)line.Id;

                                    if (line.EcomType.HasValue)
                                    {
                                        model.Type = line.EcomType.Value.GetText(Context.Localizer);

                                    }
                                    if (line.ContactsStateId.HasValue)
                                    {
                                        model.State = model.StateList.FirstOrDefault(x => x.Id == line.ContactsStateId).State;

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

                                    double rate;
                                    if (item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1 < 1)
                                    {
                                        rate = (double)(item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1);
                                    }
                                    else
                                    {
                                        rate = ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1) + ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage2);
                                    }
                                    model.Rate = rate.ToString("###0.00");
                                    model.TaxableValue = item.Sum(x => x.GrandTotal).Value.ToString("###0.00");

                                    if (!line.ContactsEComGstin.IsNullOrEmpty())
                                    {
                                        model.ECommGSTIN = line.ContactsEComGstin;
                                    }
                                    else
                                    {
                                        model.ECommGSTIN = "";
                                    }

                                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7}", model.Type, model.State, model.Rate, null, model.TaxableValue, null, model.ECommGSTIN, Environment.NewLine);
                                }


                            }
                        }
                    }
                    //using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/GSTR1B2CS.CSV"), false))
                    //{
                    //	_testData.WriteLine(sb.ToString()); // Write the file.
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



        [HttpGet, Route("~/Reports/GSTR1B2CLReport")]
        public ActionResult GSTR1B2CL()
        {

            return View(MVC.Views.Reports.GST.GSTR1B2CLReportView);
        }


        //Partial View for GST-R1 B2CL
        public ActionResult LoadDataB2CL(string StartDate, string EndDate)
        {
            try
            {

                var model = new GSTR1B2CLPageModel();

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

                var user = (UserDefinition)Context.User.ToUserDefinition();
                var Sal = SalesRow.Fields;
                var SalP = SalesProductsRow.Fields;
                var Stat = StateRow.Fields;

                using (var connection = _connections.NewFor<SalesRow>())
                {
                    //Sales  List
                    model.SalesList = connection.List<SalesRow>(us => us
                          .SelectTableFields()
                          .Select(Sal.Id)
                          .Select(Sal.Total)
                          .Select(Sal.Date)
                          .Select(Sal.ContactsGstin)
                          .Select(Sal.ContactsName)
                          .Select(Sal.InvoiceNo) //If this is empty then print Id
                          .Select(Sal.ContactsStateId)
                          .Select(Sal.ContactsEComGstin)
                          .Where(Sal.Total > 250000)
                          .Where(Sal.ContactsGstin.IsNull())
                          );

                    //Sales Product  List
                    model.SalesProductList = connection.List<SalesProductsRow>(us => us
                            .SelectTableFields()
                            .Select(SalP.Id)
                            .Select(SalP.GrandTotal)
                            .Select(SalP.SalesId)
                            .Select(SalP.Percentage1)
                            .Select(SalP.Percentage2)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );
                }
                return PartialView(MVC.Views.Reports.GST.GSTR1CLPartialView, model);
            }
            catch (Exception ex)
            {
                //return PartialView(MVC.Views.Reports.GST.GSTR1B2BPartialView,ex.Message);
                return Json(ex.Message);
            }

        }

        //Export to CSV GST-R1 B2CL 
        [HttpPost]
        public FileContentResult ExportCSVB2CL(string StartDate, string EndDate)
        {
            try
            {

                var model = new GSTR1B2CLPageModel();

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
                var user = (UserDefinition)Context.User.ToUserDefinition();
                var Sal = SalesRow.Fields;
                var SalP = SalesProductsRow.Fields;
                var Stat = StateRow.Fields;
                //var msg = "";

                using (var connection = _connections.NewFor<SalesRow>())
                {
                    //Sales  List
                    model.SalesList = connection.List<SalesRow>(us => us
                          .SelectTableFields()
                          .Select(Sal.Id)
                          .Select(Sal.Total)
                          .Select(Sal.Date)
                          .Select(Sal.ContactsGstin)
                          .Select(Sal.ContactsName)
                          .Select(Sal.InvoiceNo) //If this is empty then print Id
                          .Select(Sal.ContactsStateId)
                          .Select(Sal.ContactsEComGstin)
                          .Where(Sal.Total > 250000)
                          .Where(Sal.ContactsGstin.IsNull())
                          );

                    //Sales Product  List
                    model.SalesProductList = connection.List<SalesProductsRow>(us => us
                            .SelectTableFields()
                            .Select(SalP.Id)
                            .Select(SalP.GrandTotal)
                            .Select(SalP.SalesId)
                            .Select(SalP.Percentage1)
                            .Select(SalP.Percentage2)
                            );

                    //State List
                    model.StateList = connection.List<StateRow>(us => us
                        .SelectTableFields()
                        .Select(Stat.Id)
                        .Select(Stat.State)
                        );


                    var sb = new StringBuilder();
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", "Invoice Number", "Invoice date", "Invoice Value", "Place Of Supply", "Rate", "Applicable % of Tax Rate", "Taxable Value", "Cess Amount", "E-Commerce ContactsGstin", Environment.NewLine);
                    foreach (var line in model.SalesList)
                    {
                        foreach (var item in model.SalesProductList.Where(x => x.SalesId == line.Id).GroupBy(y => y.Percentage1))
                        {
                            if (line.ContactsGstin.IsNullOrEmpty())
                            {

                                string invno;
                                if (line.InvoiceNo.HasValue)
                                {
                                    invno = line.Date.Value.Year.ToString() + "/" + line.InvoiceNo.ToString();
                                }
                                else
                                {
                                    invno = line.Date.Value.Year.ToString() + "/" + line.Id.ToString();
                                }

                                model.InvoiceNo = invno;
                                model.InvoiceDate = line.Date.Value.ToString("dd-MMM-yyyy");
                                model.Total = line.Total.Value.ToString("###0.00");

                                if (line.ContactsStateId.HasValue)
                                {
                                    model.State = model.StateList.FirstOrDefault(x => x.Id == line.ContactsStateId).State;

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

                                if (!line.ContactsEComGstin.IsNullOrEmpty())
                                {
                                    model.ECommGSTIN = line.ContactsEComGstin;
                                }
                                else
                                {
                                    model.ECommGSTIN = "";
                                }

                                double rate;
                                if (item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1 < 1)
                                {
                                    rate = (double)(item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1);
                                }
                                else
                                {
                                    rate = ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage1) + ((double)@item.FirstOrDefault(x => x.SalesId == line.Id).Percentage2);
                                }
                                model.Rate = rate.ToString("###0.00");
                                model.TaxableValue = item.Sum(x => x.GrandTotal).Value.ToString("###0.00");

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
                                    var type = line.InvoiceType.Value.GetText(Context.Localizer);



                                }


                                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", model.InvoiceNo, model.InvoiceDate, model.Total, model.State, model.Rate, null, model.TaxableValue, null, model.ECommGSTIN, Environment.NewLine);

                            }

                        }
                    }

                    //using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/GSTR1B2CL.CSV"), false))
                    //{
                    //	_testData.WriteLine(sb.ToString()); // Write the file.
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




    }
}