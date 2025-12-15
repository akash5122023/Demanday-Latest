using AdvanceCRM.Administration;
using AdvanceCRM.Purchase;
using AdvanceCRM.Quotation;
using AdvanceCRM.Enquiry;
using AdvanceCRM.Sales;
using AdvanceCRM.Services;
using AdvanceCRM.Template;
using AdvanceCRM.Settings;
using AdvanceCRM.Contacts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serenity;
using System.Text;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
//using iTextSharp.text.pdf;
using System.Threading.Tasks;
using AdvanceCRM.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.StaticFiles;
using AdvanceCRM.Common;
namespace AdvanceCRM
{
    [Route("Report/[action]")]
    public class ReportController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IRequestContext _userContext;
        private readonly IReportRegistry _reportRegistry;
        private readonly ICommonService _commonService;
        private readonly IOptions<EnvironmentSettings> _environmentSettings;
        private readonly ILogger<ReportController> _logger;
        private readonly ITextLocalizer _textLocalizer;

        public ReportController(ISqlConnections connections,

            IOptions<EnvironmentSettings> environmentSettings,
            IWebHostEnvironment hostEnvironment, IRequestContext context,
            ILogger<ReportController> logger,
            IReportRegistry reportRegistry,
             ITextLocalizer textLocalizer,
            ICommonService commonService = null)
        {
            _connections = connections;
            _environmentSettings = environmentSettings;
            _hostEnvironment = hostEnvironment;
            _userContext = context;
            _reportRegistry = reportRegistry;
            _commonService = commonService;
            _logger = logger;
            _textLocalizer = textLocalizer ?? NullTextLocalizer.Instance;

        }
        string tst;

        public IActionResult Render(string key, string opt, string ext, int? print = 0)
        {
            return Execute(key, opt, ext, download: false, printing: print != 0);
        }

        public IActionResult Download(string key, string opt, string ext)
        {
            return Execute(key, opt, ext, download: true, printing: true);
        }

        private IActionResult Execute(string key, string opt, string ext, bool download, bool printing)
        {
            if (key.IsEmptyOrNull())
                throw new ArgumentNullException("reportKey");

            var reportInfo = _reportRegistry.GetReport(key);
            if (reportInfo == null)
                throw new ArgumentOutOfRangeException("reportKey");

            if (reportInfo.Permission != null)
                //Authorization.ValidatePermission(reportInfo.Permission);
                _userContext.Permissions.ValidatePermission(reportInfo.Permission, _textLocalizer);
            var report = (IReport)JsonConvert.DeserializeObject(opt.TrimToNull() ?? "{}",
                reportInfo.Type, JsonSettings.Tolerant);

            byte[] renderedBytes = null;

            if (report is IDataOnlyReport)
            {
                ext = "xlsx";
                renderedBytes = new ReportRepository().Render((IDataOnlyReport)report);
            }
            else
            {
                ext = (ext ?? "html").ToLowerInvariant();

                if (ext == "htm" || ext == "html")
                {
                    //Add this result as the content of html body
                    var result = RenderAsHtml(report, download, printing, ref renderedBytes);
                    if (!download)
                        return result;
                }
                else if (ext == "pdf")
                {
                    renderedBytes = RenderAsPdf(report, key, opt);
                }
                else
                    throw new ArgumentOutOfRangeException("ext");
            }

            return PrepareFileResult(report, ext, download, renderedBytes, reportInfo);
        }

        private IActionResult PrepareFileResult(IReport report, string ext, bool download,
            byte[] renderedBytes, ReportRegistry.Report reportInfo)
        {
            string fileDownloadName = "";
            var data1 = report.GetData();

            var customFileName = report as ICustomFileName;
            if (customFileName != null)
                fileDownloadName = customFileName.GetFileName();
            else
            {
                if (data1.GetType().GetProperty("Name").HasValue())
                {
                    fileDownloadName = data1.GetType().GetProperty("Name").GetValue(data1).ToString().Replace(" ", "_") + "_" + (reportInfo.Title ?? reportInfo.Key.Split('.')[0]) + "_" +
                        DateTime.Now.ToString("yyyyMMdd_HHss");
                }
                else
                {
                    fileDownloadName = (reportInfo.Title ?? reportInfo.Key.Split('.')[0]) + "_" +
                        DateTime.Now.ToString("yyyyMMdd_HHss");
                }
            }

            fileDownloadName += "." + ext;

            if (download)
            {
                var ModType = data1.GetType().GetProperty("modtype");

                if (ModType.GetValue(data1).ToString() == "Quotation")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<QuotationTemplateRow>())
                    {
                        var com = CompanyDetailsRow.Fields;// TryById<UserRow>(_userContext.User.GetIdentifier()
 
                         data.Company = connection.TryFirst<CompanyDetailsRow>(q => q
                              .SelectTableFields()
                              .Select(com.MailToSubContacts)
                              .Select(com.MailToOrganisation)
                              .Where(com.Id == (_userContext.User.ToUserDefinition()).CompanyId)
                              );


                        var c = QuotationTemplateRow.Fields;
                        data.Tempalte = connection.TryFirst<QuotationTemplateRow>(q => q
                            .SelectTableFields()
                            .Select(c.Sender)
                            .Select(c.Subject)
                            .Select(c.EmailTemplate)
                            .Select(c.Host)
                            .Select(c.Port)
                            .Select(c.SSL)
                            .Select(c.EmailId)
                            .Select(c.EmailPassword)
                            .Select(c.CCEmails)
                            .Select(c.BCCEmails)
                            .Where(c.CompanyId == (_userContext.User.ToUserDefinition()).CompanyId)
                             );

                        var u = UserRow.Fields;
                        data.User = connection.TryById<UserRow>(_userContext.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));


                        var quo = QuotationRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.Quotation = connection.TryById<QuotationRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(quo.ContactsName)
                           .Select(quo.ContactsEmail)
                           .Select(quo.ContactPersonEmail)
                           .Select(quo.ContactsContactType)
                           .Select(quo.ContactPersonName)
                           .Select(quo.ContactsCcEmails)
                           .Select(quo.ContactsBccEmails)
                           .Select(quo.Attachment)
                           .Select(quo.Total)
                           );
                    }

                    List<string> emails;
                    List<string> bemails;
                    List<string> ccemails;
                    List<string> bccemails;

                    emails = null;
                    bemails = null;
                    ccemails = null;
                    bccemails = null;

                    if (data.Tempalte.CCEmails != null)
                    {
                        emails = data.Tempalte.CCEmails.Split(',').ToList<string>();
                    }

                    if (data.Tempalte.BCCEmails != null)
                    {
                        bemails = data.Tempalte.BCCEmails.Split(',').ToList<string>();
                    }

                    if (data.Quotation.ContactsCcEmails != null)
                    {
                        ccemails = data.Quotation.ContactsCcEmails.Split(',').ToList<string>();
                    }

                    if (data.Quotation.ContactsBccEmails != null)
                    {
                        bccemails = data.Quotation.ContactsBccEmails.Split(',').ToList<string>();
                    }

                    //Configuration
                    if (data.Tempalte.Host != null)
                    {
                        MailMessage mm = new MailMessage();
                        var addr = new MailAddress(data.Tempalte.EmailId, data.Tempalte.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        bool Organisation = Convert.ToBoolean(data.Company.MailToOrganisation);
                        int contacttype = Convert.ToInt32(data.Quotation.ContactsContactType);
                        if (contacttype == 2 && Organisation == true)
                        {
                            mm.To.Add(data.Quotation.ContactsEmail);
                        }
                        else if (contacttype == 1)
                        {
                            mm.To.Add(data.Quotation.ContactsEmail);
                        }
                       
                        if (data.Company.MailToSubContacts == true)
                        {
                            if (data.Quotation.ContactPersonEmail != null)
                            {
                                mm.To.Add(data.Quotation.ContactPersonEmail);
                            }
                        }
                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.Tempalte.Subject;
                        var msg = data.Tempalte.EmailTemplate;

                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.Quotation.ContactsName);
                        msg = msg.Replace("#amount", data.Quotation.Total.Value.ToString("#,##0.00"));

                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Quotation-" + DateTime.Now.ToLongDateString() + ".pdf"));
                        if (data.Tempalte.Attachment != null)
                        {
                            JArray att = JArray.Parse(data.Tempalte.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                }
                            }
                        }

                        if (data.Quotation.Attachment != null)
                        {
                            JArray att = JArray.Parse(data.Quotation.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                }
                            }
                        }


                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.Tempalte.EmailId, data.Tempalte.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.Tempalte.SSL;
                        mail.Host = data.Tempalte.Host;
                        mail.Port = data.Tempalte.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }
                    else
                    {
                        MailMessage mm = new MailMessage();
                        if (data.User.EmailId.IsEmptyOrNull())
                            return Content("User email is not configured");

                        var addr = new MailAddress(data.User.EmailId, data.Tempalte.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        bool Organisation = Convert.ToBoolean(data.Company.MailToOrganisation);
                        int contacttype = Convert.ToInt32(data.Quotation.ContactsContactType);
                        if (contacttype == 2 && Organisation == true)
                        {
                            mm.To.Add(data.Quotation.ContactsEmail);
                        }
                        else if (contacttype == 1)
                        {
                            mm.To.Add(data.Quotation.ContactsEmail);
                        }
                        //  bool abc = Convert.ToBoolean(data.Company.MailToSubContacts);
                        if (data.Company.MailToSubContacts == true)
                        {
                            if (data.Quotation.ContactPersonEmail != null)
                            {
                                mm.To.Add(data.Quotation.ContactPersonEmail);
                            }
                        }
                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.Tempalte.Subject;
                        var msg = data.Tempalte.EmailTemplate;

                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.Quotation.ContactsName);
                        msg = msg.Replace("#amount", data.Quotation.Total.Value.ToString("#,##0.00"));

                       
                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Quotation-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        if (data.Tempalte.Attachment != null)
                        {
                            JArray att = JArray.Parse(data.Tempalte.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                }
                            }
                        }

                        if (data.Quotation.Attachment != null)
                        {
                            JArray att = JArray.Parse(data.Quotation.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                }
                            }
                        }

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;


                        var mail = new SmtpClient();

                        NetworkCredential nc = new NetworkCredential(data.User.EmailId, data.User.EmailPassword);

                        mail.UseDefaultCredentials = false;
                        mail.DeliveryMethod = SmtpDeliveryMethod.Network;
                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.User.SSL;
                        mail.Host = data.User.Host;
                        mail.Port = data.User.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);

                    }

                    return Content("Mail sent successfully<script type='text/javascript'>window.close();</script>");
                    //return JavaScript("Mail sent successfully, you can now close this window safely");
                }

                if (ModType.GetValue(data1).ToString() == "Enquiry")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<EnquiryTemplateRow>())
                    {
                        var com = CompanyDetailsRow.Fields;
                        data.Company = connection.TryFirst<CompanyDetailsRow>(q => q
                              .SelectTableFields()
                              .Select(com.MailToSubContacts)
                              .Select(com.MailToOrganisation)
                              .Where(com.Id == ((UserDefinition)_userContext.User.ToUserDefinition()).CompanyId));

                        var c = EnquiryTemplateRow.Fields;
                        data.EnquiryTempalte = connection.TryFirst<EnquiryTemplateRow>(q => q
                            .SelectTableFields()
                            .Select(c.Sender)
                            .Select(c.Subject)
                            .Select(c.EmailTemplate)
                            .Select(c.Host)
                            .Select(c.Port)
                            .Select(c.SSL)
                            .Select(c.EmailId)
                            .Select(c.EmailPassword)
                           
                            .Where(c.CompanyId == (_userContext.User.ToUserDefinition()).CompanyId)
                             );

                        var u = UserRow.Fields;
                        data.User = connection.TryById<UserRow>(_userContext.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));


                        var quo = EnquiryRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.Enquiry = connection.TryById<EnquiryRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(quo.ContactsName)
                           .Select(quo.ContactsEmail)
                           
                           .Select(quo.ContactPersonEmail)
                           .Select(quo.ContactPersonName)
                           .Select(quo.Total)
                           );
                    }



                    ////Configuration
                    if (data.EnquiryTempalte.Host != null)
                    {
                        MailMessage mm = new MailMessage();
                        var addr = new MailAddress(data.EnquiryTempalte.EmailId, data.EnquiryTempalte.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        mm.To.Add(data.Enquiry.ContactsEmail);
                        if (data.Company.MailToSubContacts == true)
                        {
                            if (data.Enquiry.ContactPersonEmail != null)
                            {
                                mm.To.Add(data.Enquiry.ContactPersonEmail);
                            }
                        }
                        mm.Subject = data.EnquiryTempalte.Subject;
                        var msg = data.EnquiryTempalte.EmailTemplate;

                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.Enquiry.ContactsName);
                        msg = msg.Replace("#amount", data.Enquiry.Total.Value.ToString("#,##0.00"));

                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Enquiry-" + DateTime.Now.ToLongDateString() + ".pdf"));
                       


                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.EnquiryTempalte.EmailId, data.EnquiryTempalte.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.Tempalte.SSL;
                        mail.Host = data.EnquiryTempalte.Host;
                        mail.Port = data.EnquiryTempalte.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }
                    else
                    {
                        MailMessage mm = new MailMessage();
                        if (data.User.EmailId.IsEmptyOrNull())
                            return Content("User email is not configured");

                        var addr = new MailAddress(data.User.EmailId, data.EnquiryTempalte.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        mm.To.Add(data.Enquiry.ContactsEmail);
                        if (data.Company.MailToSubContacts == true)
                        {
                            if (data.Enquiry.ContactPersonEmail != null)
                            {
                                mm.To.Add(data.Enquiry.ContactPersonEmail);
                            }
                        }
                        mm.Subject = data.EnquiryTempalte.Subject;
                        var msg = data.EnquiryTempalte.EmailTemplate;

                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.Enquiry.ContactsName);
                        msg = msg.Replace("#amount", data.Enquiry.Total.Value.ToString("#,##0.00"));


                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Enquiry-" + DateTime.Now.ToLongDateString() + ".pdf"));

                       

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;


                        var mail = new SmtpClient();

                        NetworkCredential nc = new NetworkCredential(data.User.EmailId, data.User.EmailPassword);

                        mail.UseDefaultCredentials = false;
                        mail.DeliveryMethod = SmtpDeliveryMethod.Network;
                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.User.SSL;
                        mail.Host = data.User.Host;
                        mail.Port = data.User.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);

                    }

                    return Content("Mail sent successfully<script type='text/javascript'>window.close();</script>");
                    //return JavaScript("Mail sent successfully, you can now close this window safely");
                }
                //Wa message
                else if (ModType.GetValue(data1).ToString() == "WAQuotationOld")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<QuotationTemplateRow>())
                    {
                        var c = QuotationTemplateRow.Fields;
                        data.Tempalte = connection.TryFirst<QuotationTemplateRow>(q => q
                            .SelectTableFields()
                            .Select(c.Sender)
                            .Select(c.Subject)
                            .Select(c.SMSTemplate)
                            .Select(c.Host)
                            .Select(c.Port)
                            .Select(c.SSL)
                            .Select(c.EmailId)
                            .Select(c.EmailPassword)
                            .Select(c.CCEmails)
                            .Select(c.BCCEmails)
                            .Where(c.CompanyId == (_userContext.User.ToUserDefinition()).CompanyId)
                             );

                        var u = UserRow.Fields;
                        data.User = connection.TryById<UserRow>(_userContext.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));


                        var quo = QuotationRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.Quotation = connection.TryById<QuotationRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(quo.ContactsName)
                           .Select(quo.ContactsWhatsapp)
                           .Select(quo.ContactsCcEmails)
                           .Select(quo.ContactsBccEmails)
                           .Select(quo.Attachment)
                           .Select(quo.Total)
                           );
                    }

                 
                    //Configuration
                    var SMSConfig = new WaConfigrationRow();

                    using (var connection = _connections.NewFor<ContactsRow>())
                    {
                        var s = WaConfigrationRow.Fields;
                        SMSConfig = connection.TryById<WaConfigrationRow>(1, q => q
                            .SelectTableFields()
                            .Select(s.MediaApi)
                            .Select(s.ApiKey)
                            .Select(s.Mobile)
                            //.Select(s.SenderId)
                            //.Select(s.Key)
                            .Select(s.SuccessResponse)
                            );
                    }
                    ////Upload file
                    String apiStr = SMSConfig.MessageApi;
                    String apikey = SMSConfig.ApiKey;
                    String mobile = SMSConfig.Mobile;

//                    FileStream fileStream = new FileStream("/output/directory/Quotation-" + DateTime.Now.ToLongDateString() + ".pdf", FileMode.Create);
//                    { 
//{
//                        client.convertURI("http://example.com", fileStream);
//                    }


                    String uri = apiStr.Trim().Replace("<apikey>", apikey.Trim()).Replace(" < mobile>", mobile.Trim()).Replace("<contacts>", data.Quotation.ContactsWhatsapp).Replace("<msg>", data.Tempalte.SMSTemplate.Trim());

                 
                    try
                    {
                        // Create a new 'HttpWebRequest' Object to the mentioned URL.
                        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

                        // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                        myHttpWebRequest.Timeout = 15000;

                        HttpWebResponse myHttpWebResponse;
                        myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                        StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                        string response = reader.ReadToEnd();
                        if (response.Contains(SMSConfig.SuccessResponse))
                           // return "SMS sent successfully";
                           return Content("Whatsapp sent successfully<script type='text/javascript'>window.close();</script>");
                        else
                            throw new Exception("Invalid Response");
                        //if (data.Tempalte.Host != null)
                        //{
                        //    MailMessage mm = new MailMessage();
                        //    var addr = new MailAddress(data.Tempalte.EmailId, data.Tempalte.Sender);

                        //    mm.From = addr;
                        //    mm.Sender = addr;
                        //    mm.To.Add(data.Quotation.ContactsEmail);                      

                        //    mm.Subject = data.Tempalte.Subject;
                        //    var msg = data.Tempalte.EmailTemplate;

                        //    msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        //    msg = msg.Replace("#customername", data.Quotation.ContactsName);
                        //    msg = msg.Replace("#amount", data.Quotation.Total.Value.ToString("#,##0.00"));

                        //    mm.Body = msg;
                        //    mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Quotation-" + DateTime.Now.ToLongDateString() + ".pdf"));
                        //    if (data.Tempalte.Attachment != null)
                        //    {
                        //        JArray att = JArray.Parse(data.Tempalte.Attachment);
                        //        foreach (var f in att)
                        //        {
                        //            if (f["Filename"].HasValue())
                        //            {
                        //                mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                        //            }
                        //        }
                        //    }

                        //    if (data.Quotation.Attachment != null)
                        //    {
                        //        JArray att = JArray.Parse(data.Quotation.Attachment);
                        //        foreach (var f in att)
                        //        {
                        //            if (f["Filename"].HasValue())
                        //            {
                        //                mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                        //            }
                        //        }
                        //    }


                        //    mm.IsBodyHtml = true;
                        //    mm.Priority = MailPriority.High;

                        //    NetworkCredential nc = new NetworkCredential(data.Tempalte.EmailId, data.Tempalte.EmailPassword);

                        //    var mail = new SmtpClient();

                        //    mail.Credentials = nc;
                        //    mail.EnableSsl = (Boolean)data.Tempalte.SSL;
                        //    mail.Host = data.Tempalte.Host;
                        //    mail.Port = data.Tempalte.Port.Value;

                        //    mail.Timeout = 100000;
                        //    mail.Send(mm);
                        //}
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }

                 
                    //return JavaScript("Mail sent successfully, you can now close this window safely");
                }
                else if (ModType.GetValue(data1).ToString() == "CMS")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<CmsTemplateRow>())
                    {
                        var com = CompanyDetailsRow.Fields;
                        data.Company = connection.TryFirst<CompanyDetailsRow>(q => q
                              .SelectTableFields()
                              .Select(com.MailToSubContacts)
                              .Where(com.Id == ((UserDefinition)_userContext.User.ToUserDefinition()).CompanyId));

                        var c = CmsTemplateRow.Fields;
                        data.CMSTemplate = connection.TryFirst<CmsTemplateRow>(q => q
                            .SelectTableFields()
                            .Select(c.Sender)
                            .Select(c.Subject)
                            .Select(c.EmailTemplate)
                            .Select(c.EmailTemplateReceipt)
                            .Select(c.ClosedEmailTemplate)
                            .Select(c.CCEmails)
                            .Select(c.BCCEmails)
                            .Select(c.Host)
                            .Select(c.Port)
                            .Select(c.SSL)
                            .Select(c.EmailId)
                            .Select(c.EmailPassword)
                            .Where(c.CompanyId == (_userContext.User.ToUserDefinition()).CompanyId)
                            );

                        var u = UserRow.Fields;
                        data.User = connection.TryById<UserRow>(_userContext.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));


                        var cms = CMSRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.CMS = connection.TryById<CMSRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(cms.ContactsName)
                           .Select(cms.ContactsEmail)
                           .Select(cms.ProductsName)                           
                           .Select(cms.Date)
                           .Select(cms.Id)
                           .Select(cms.AssignedToDisplayName)
                           .Select(cms.ExpectedCompletion)
                           .Select(cms.ContactsCCEmails)
                           .Select(cms.ContactsBCCEmails)
                           .Select(cms.ContactsServiceEmail)
                           );
                    }

                    List<string> emails;
                    List<string> bemails;
                    List<string> ccemails;
                    List<string> bccemails;

                    emails = null;
                    bemails = null;
                    ccemails = null;
                    bccemails = null;

                    if (data.CMSTemplate.CCEmails != null)
                    {
                        emails = data.CMSTemplate.CCEmails.Split(',').ToList<string>();
                    }

                    if (data.CMSTemplate.BCCEmails != null)
                    {
                        bemails = data.CMSTemplate.BCCEmails.Split(',').ToList<string>();
                    }

                    if (data.CMS.ContactsCCEmails != null)
                    {
                        ccemails = data.CMS.ContactsCCEmails.Split(',').ToList<string>();
                    }

                    if (data.CMS.ContactsBCCEmails != null)
                    {
                        bccemails = data.CMS.ContactsBCCEmails.Split(',').ToList<string>();
                    }

                    //Configuration
                    if (data.CMSTemplate.Host != null)
                    {
                        MailMessage mm = new MailMessage();
                        var addr = new MailAddress(data.CMSTemplate.EmailId, data.CMSTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.CMS.ContactsServiceEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.CMS.ContactsServiceEmail);
                            mm.CC.Add(data.CMS.ContactsEmail);
                        }
                        else
                        {
                            mm.To.Add(data.CMS.ContactsEmail);
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.CMSTemplate.Subject;
                        var msg = data.CMSTemplate.EmailTemplateReceipt;
                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.CMS.ContactsName);
                        msg = msg.Replace("#product", data.CMS.ProductsName);
                        msg = msg.Replace("#complaintdate", data.CMS.Date.Value.ToShortDateString());
                        msg = msg.Replace("#complaintno", data.CMS.Id.Value.ToString());
                        msg = msg.Replace("#representative", data.CMS.AssignedToDisplayName);
                        msg = msg.Replace("#expecteddate", data.CMS.ExpectedCompletion.Value.ToShortDateString());

                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Receipt-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.CMSTemplate.EmailId, data.Tempalte.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.CMSTemplate.SSL;
                        mail.Host = data.CMSTemplate.Host;
                        mail.Port = data.CMSTemplate.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }
                    else
                    {
                        MailMessage mm = new MailMessage();
                        if (data.User.EmailId.IsEmptyOrNull())
                            return Content("User email is not configured");

                        var addr = new MailAddress(data.User.EmailId, data.CMSTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.CMS.ContactsServiceEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.CMS.ContactsServiceEmail);
                            mm.CC.Add(data.CMS.ContactsEmail);
                        }
                        else
                        {
                            mm.To.Add(data.CMS.ContactsEmail);
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.CMSTemplate.Subject;
                        var msg = data.CMSTemplate.EmailTemplateReceipt;
                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.CMS.ContactsName);
                        msg = msg.Replace("#product", data.CMS.ProductsName);
                        msg = msg.Replace("#complaintdate", data.CMS.Date.Value.ToShortDateString());
                        msg = msg.Replace("#complaintno", data.CMS.Id.Value.ToString());
                        msg = msg.Replace("#representative", data.CMS.AssignedToDisplayName);
                        msg = msg.Replace("#expecteddate", data.CMS.ExpectedCompletion.Value.ToShortDateString());

                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Receipt-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.User.EmailId, data.User.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.User.SSL;
                        mail.Host = data.User.Host;
                        mail.Port = data.User.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }

                    return Content("Mail sent successfully<script type='text/javascript'>window.close();</script>");
                    //return JavaScript("Mail sent successfully, you can now close this window safely");
                }
                else if (ModType.GetValue(data1).ToString() == "PurchaseOrder")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<PurchaseOrderTemplateRow>())
                    {
                        var com = CompanyDetailsRow.Fields;
                        data.Company = connection.TryFirst<CompanyDetailsRow>(q => q
                              .SelectTableFields()
                              .Select(com.MailToSubContacts)
                              .Where(com.Id == ((UserDefinition)_userContext.User.ToUserDefinition()).CompanyId));


                        var c = PurchaseOrderTemplateRow.Fields;
                        data.POTemplate = connection.TryFirst<PurchaseOrderTemplateRow>(q => q
                            .SelectTableFields()
                            .Select(c.Sender)
                            .Select(c.Subject)
                            .Select(c.EmailTemplate)
                            .Select(c.Host)
                            .Select(c.Port)
                            .Select(c.SSL)
                            .Select(c.EmailId)
                            .Select(c.EmailPassword)
                            .Select(c.CCEmails)
                            .Select(c.Bcc)
                             .Where(c.CompanyId == (_userContext.User.ToUserDefinition()).CompanyId)
                             );

                        var u = UserRow.Fields;
                        data.User = connection.TryById<UserRow>(_userContext.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));


                        var quo = PurchaseOrderRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.PO = connection.TryById<PurchaseOrderRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(quo.ContactsName)
                           .Select(quo.ContactsEmail)
                           // .Select(quo.ContactPersonEmail)
                           //.Select(quo.ContactPersonName)
                           .Select(quo.ContactsCCEmails)
                           .Select(quo.ContactsBCCEmails)
                           .Select(quo.ContactsSalesEmail)
                           );
                    }

                    List<string> emails;
                    List<string> bemails;
                    List<string> ccemails;
                    List<string> bccemails;

                    emails = null;
                    bemails = null;
                    ccemails = null;
                    bccemails = null;

                    if (data.POTemplate.CCEmails != null)
                    {
                        emails = data.POTemplate.CCEmails.Split(',').ToList<string>();
                    }

                    if (data.POTemplate.Bcc != null)
                    {
                        bemails = data.POTemplate.Bcc.Split(',').ToList<string>();
                    }

                    if (data.PO.ContactsCCEmails != null)
                    {
                        ccemails = data.PO.ContactsCCEmails.Split(',').ToList<string>();
                    }

                    if (data.PO.ContactsBCCEmails != null)
                    {
                        bccemails = data.PO.ContactsBCCEmails.Split(',').ToList<string>();
                    }

                    //Configuration
                    if (data.POTemplate.Host != null)
                    {
                        MailMessage mm = new MailMessage();
                        var addr = new MailAddress(data.POTemplate.EmailId, data.POTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.PO.ContactsSalesEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.PO.ContactsSalesEmail);
                            mm.CC.Add(data.PO.ContactsEmail);
                        }
                        else
                        {
                            mm.To.Add(data.PO.ContactsEmail);
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.POTemplate.Subject;
                        var msg = data.POTemplate.EmailTemplate;
                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.PO.ContactsName);
                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Purchase Order-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.POTemplate.EmailId, data.POTemplate.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.Tempalte.SSL;
                        mail.Host = data.POTemplate.Host;
                        mail.Port = data.POTemplate.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }
                    else
                    {
                        MailMessage mm = new MailMessage();
                        if (data.User.EmailId.IsEmptyOrNull())
                            return Content("User email is not configured");

                        var addr = new MailAddress(data.User.EmailId, data.POTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.PO.ContactsSalesEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.PO.ContactsSalesEmail);
                            mm.CC.Add(data.PO.ContactsEmail);
                        }
                        else
                        {
                            mm.To.Add(data.PO.ContactsEmail);
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.POTemplate.Subject;
                        var msg = data.POTemplate.EmailTemplate;
                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.PO.ContactsName);
                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Purchase Order-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;


                        var mail = new SmtpClient();

                        NetworkCredential nc = new NetworkCredential(data.User.EmailId, data.User.EmailPassword);

                        mail.UseDefaultCredentials = false;
                        mail.DeliveryMethod = SmtpDeliveryMethod.Network;
                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.User.SSL;
                        mail.Host = data.User.Host;
                        mail.Port = data.User.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);

                    }

                    return Content("Mail sent successfully<script type='text/javascript'>window.close();</script>");
                    //return JavaScript("Mail sent successfully, you can now close this window safely");
                }
                else if (ModType.GetValue(data1).ToString() == "AMC")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<AMCTemplateRow>())
                    {
                        var c = AMCTemplateRow.Fields;
                        data.AMCTemplate = connection.TryFirst<AMCTemplateRow>(q => q
                            .SelectTableFields()
                            .Select(c.Sender)
                            .Select(c.Subject)
                            .Select(c.EmailTemplateReceipt)
                            .Select(c.Host)
                            .Select(c.Port)
                            .Select(c.SSL)
                            .Select(c.CCEmails)
                            .Select(c.BCCEmails)
                            .Select(c.EmailId)
                            .Select(c.EmailPassword)
                             .Where(c.CompanyId == (_userContext.User.ToUserDefinition()).CompanyId));

                        var u = UserRow.Fields;
                        data.User = connection.TryById<UserRow>(_userContext.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));


                        var quo = AMCRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.AMC = connection.TryById<AMCRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(quo.ContactsName)
                           .Select(quo.ContactsEmail)
                           .Select(quo.ContactsCCEmails)
                           .Select(quo.ContactsBCCEmails)
                           .Select(quo.Attachment)
                           .Select(quo.ContactsServiceEmail)
                           );
                    }


                    List<string> emails;
                    List<string> bemails;
                    List<string> ccemails;
                    List<string> bccemails;

                    emails = null;
                    bemails = null;
                    ccemails = null;
                    bccemails = null;

                    if (data.AMCTemplate.CCEmails != null)
                    {
                        emails = data.AMCTemplate.CCEmails.Split(',').ToList<string>();
                    }

                    if (data.AMCTemplate.BCCEmails != null)
                    {
                        bemails = data.AMCTemplate.BCCEmails.Split(',').ToList<string>();
                    }

                    if (data.AMC.ContactsCCEmails != null)
                    {
                        ccemails = data.AMC.ContactsCCEmails.Split(',').ToList<string>();
                    }

                    if (data.AMC.ContactsBCCEmails != null)
                    {
                        bccemails = data.AMC.ContactsBCCEmails.Split(',').ToList<string>();
                    }

                    //Configuration
                    if (data.AMCTemplate.Host != null)
                    {
                        MailMessage mm = new MailMessage();
                        var addr = new MailAddress(data.AMCTemplate.EmailId, data.AMCTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.AMC.ContactsServiceEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.AMC.ContactsServiceEmail);
                            mm.CC.Add(data.AMC.ContactsEmail);
                        }
                        else
                        {
                            mm.To.Add(data.AMC.ContactsEmail);
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.AMCTemplate.Subject;
                        var msg = data.AMCTemplate.EmailTemplateReceipt;
                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.AMC.ContactsName);
                        mm.Body = msg;

                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "AMC-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        if (data.AMC.Attachment != null)
                        {
                            JArray att = JArray.Parse(data.AMC.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                }
                            }
                        }

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.AMCTemplate.EmailId, data.AMCTemplate.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.AMCTemplate.SSL;
                        mail.Host = data.AMCTemplate.Host;
                        mail.Port = data.AMCTemplate.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }
                    else
                    {
                        MailMessage mm = new MailMessage();
                        if (data.User.EmailId.IsEmptyOrNull())
                            return Content("User email is not configured");

                        var addr = new MailAddress(data.User.EmailId, data.AMCTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.AMC.ContactsServiceEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.AMC.ContactsServiceEmail);
                            mm.CC.Add(data.AMC.ContactsEmail);
                        }
                        else
                        {
                            mm.To.Add(data.AMC.ContactsEmail);
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.AMCTemplate.Subject;
                        var msg = data.AMCTemplate.EmailTemplateReceipt;
                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.AMC.ContactsName);
                        mm.Body = msg;

                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "AMC-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        if (data.AMC.Attachment != null)
                        {
                            JArray att = JArray.Parse(data.AMC.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                }
                            }
                        }


                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.User.EmailId, data.User.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.User.SSL;
                        mail.Host = data.User.Host;
                        mail.Port = data.User.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }

                    return Content("Mail sent successfully<script type='text/javascript'>window.close();</script>");
                    //return JavaScript("Mail sent successfully, you can now close this window safely");
                }
                else if (ModType.GetValue(data1).ToString() == "Invoice")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<InvoiceTemplateRow>())
                    {
                        var com = CompanyDetailsRow.Fields;
                        data.Company = connection.TryFirst<CompanyDetailsRow>(q => q
                              .SelectTableFields()
                              .Select(com.MailToSubContacts)
                              .Select(com.MailToOrganisation)
                              .Where(com.Id == ((UserDefinition)_userContext.User.ToUserDefinition()).CompanyId));

                        var c = InvoiceTemplateRow.Fields;
                        data.InvoiceTemplate = connection.TryFirst<InvoiceTemplateRow>(q => q
                            .SelectTableFields()
                            .Select(c.Sender)
                            .Select(c.Subject)
                            .Select(c.EmailTemplate)
                            .Select(c.Attachment)
                            .Select(c.CCEmails)
                            .Select(c.BCCEmails)
                            .Select(c.Host)
                            .Select(c.Port)
                            .Select(c.SSL)
                            .Select(c.EmailId)
                            .Select(c.EmailPassword)
                             .Where(c.CompanyId == (_userContext.User.ToUserDefinition()).CompanyId));

                        var u = UserRow.Fields;
                        data.User = connection.TryById<UserRow>(_userContext.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));


                        var quo = InvoiceRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.Invoice = connection.TryById<InvoiceRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(quo.ContactsName)
                           .Select(quo.ContactsEmail)
                            .Select(quo.ContactPersonEmail)
                           .Select(quo.ContactPersonName)
                           .Select(quo.ContactsCcEmails)
                           .Select(quo.ContactsBccEmails)
                           .Select(quo.Total)
                           .Select(quo.ContactsContactType)
                           .Select(quo.ContactsAccountsEmail)
                           );
                    }


                    List<string> emails;
                    List<string> bemails;
                    List<string> ccemails;
                    List<string> bccemails;

                    emails = null;
                    bemails = null;
                    ccemails = null;
                    bccemails = null;

                    if (data.InvoiceTemplate.CCEmails != null)
                    {
                        emails = data.InvoiceTemplate.CCEmails.Split(',').ToList<string>();
                    }

                    if (data.InvoiceTemplate.BCCEmails != null)
                    {
                        bemails = data.InvoiceTemplate.BCCEmails.Split(',').ToList<string>();
                    }

                    if (data.Invoice.ContactsCcEmails != null)
                    {
                        ccemails = data.Invoice.ContactsCcEmails.Split(',').ToList<string>();
                    }

                    if (data.Invoice.ContactsBccEmails != null)
                    {
                        bccemails = data.Invoice.ContactsBccEmails.Split(',').ToList<string>();
                    }

                    //Configuration
                    if (data.InvoiceTemplate.Host != null)
                    {
                        MailMessage mm = new MailMessage();
                        var addr = new MailAddress(data.InvoiceTemplate.EmailId, data.InvoiceTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.Invoice.ContactsAccountsEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.Invoice.ContactsAccountsEmail);
                            mm.CC.Add(data.Invoice.ContactsEmail);
                            if (data.Company.MailToSubContacts == true)
                            {
                                if (data.Invoice.ContactPersonEmail != null)
                                {
                                    mm.To.Add(data.Invoice.ContactPersonEmail);
                                }
                            }
                        }
                        else
                        {
                            bool Organisation = Convert.ToBoolean(data.Company.MailToOrganisation);
                            int contacttype = Convert.ToInt32(data.Invoice.ContactsContactType);
                            if (contacttype == 2 && Organisation == true)
                            {
                                mm.To.Add(data.Invoice.ContactsEmail);
                            }
                            else if (contacttype == 1)
                            {
                                mm.To.Add(data.Invoice.ContactsEmail);
                            }
                            if (data.Company.MailToSubContacts == true)
                            {
                                if (data.Invoice.ContactPersonEmail != null)
                                {
                                    mm.To.Add(data.Invoice.ContactPersonEmail);
                                }
                            }
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.InvoiceTemplate.Subject;
                        var msg = data.InvoiceTemplate.EmailTemplate;

                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.Invoice.ContactsName);
                        msg = msg.Replace("#amount", data.Invoice.Total.GetValueOrDefault().ToString("#,##0.00"));

                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Invoice-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        if (data.InvoiceTemplate.Attachment != null)
                        {
                            JArray att = JArray.Parse(data.InvoiceTemplate.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                }
                            }
                        }

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.InvoiceTemplate.EmailId, data.InvoiceTemplate.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.InvoiceTemplate.SSL;
                        mail.Host = data.InvoiceTemplate.Host;
                        mail.Port = data.InvoiceTemplate.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }
                    else
                    {
                        MailMessage mm = new MailMessage();
                        if (data.User.EmailId.IsEmptyOrNull())
                            return Content("User email is not configured");

                        var addr = new MailAddress(data.User.EmailId, data.InvoiceTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.Invoice.ContactsAccountsEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.Invoice.ContactsAccountsEmail);
                            mm.CC.Add(data.Invoice.ContactsEmail);
                            if (data.Company.MailToSubContacts == true)
                            {
                                if (data.Invoice.ContactPersonEmail != null)
                                {
                                    mm.To.Add(data.Invoice.ContactPersonEmail);
                                }
                            }
                        }
                        else
                        {
                            mm.To.Add(data.Invoice.ContactsEmail);
                            if (data.Company.MailToSubContacts == true)
                            {
                                if (data.Invoice.ContactPersonEmail != null)
                                {
                                    mm.CC.Add(data.Invoice.ContactPersonEmail);
                                }
                            }
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.InvoiceTemplate.Subject;
                        var msg = data.InvoiceTemplate.EmailTemplate;

                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.Invoice.ContactsName);
                        msg = msg.Replace("#amount", data.Invoice.Total.GetValueOrDefault().ToString("#,##0.00"));

                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Invoice-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        if (data.InvoiceTemplate.Attachment != null)
                        {
                            JArray att = JArray.Parse(data.InvoiceTemplate.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                }
                            }
                        }

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.User.EmailId, data.User.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.User.SSL;
                        mail.Host = data.User.Host;
                        mail.Port = data.User.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }

                    return Content("Mail sent successfully<script type='text/javascript'>window.close();</script>");
                    //return JavaScript("Mail sent successfully, you can now close this window safely");
                }
                //
                else if (ModType.GetValue(data1).ToString() == "WACMS")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<CmsTemplateRow>())
                    {

                        var cms = CMSRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.CMS = connection.TryById<CMSRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(cms.ContactsName)
                          .Select(cms.ContactsWhatsapp)
                           );

                    }


                    // Set up folder path
                    var uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Common", "intractIMg");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    // Generate unique file name
                    var fileName = $"CMS_{Guid.NewGuid()}.pdf"; // Change extension as needed (e.g., .docx, .txt)
                    var savePath = Path.Combine(uploadFolder, fileName);

                    // Write file bytes to disk
                    System.IO.File.WriteAllBytes(savePath, renderedBytes);

                    // Get file URL
                    string fileUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Common/intractIMg/{fileName}";


                    _commonService.StartCleanupTask(uploadFolder);

                    // Prepare the request for sending the message
                    var request = new SendIntractSMSRequest
                    {
                        Phone = data.CMS.ContactsWhatsapp,
                        Template = "cms_template",
                        Variable = data.CMS.ContactsName,
                        ImageUrl = fileUrl
                    };

                    // Call the SendIntractWa method from CommonController
                    var response = _commonService.SendIntractWa(null, request);


                    return Content("Message sent successfully<script type='text/javascript'>window.close();</script>");

                }
                else if (ModType.GetValue(data1).ToString() == "WAQuotation")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<QuotationTemplateRow>())
                    {
                        var quo = QuotationRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.Quotation = connection.TryById<QuotationRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(quo.ContactsName)
                           .Select(quo.ContactsWhatsapp)
                          
                           );

                    }


                    // Set up folder path
                    var uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Common", "intractIMg");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    // Generate unique file name
                    var fileName = $"Quotation{Guid.NewGuid()}.pdf"; // Change extension as needed (e.g., .docx, .txt)
                    var savePath = Path.Combine(uploadFolder, fileName);

                    // Write file bytes to disk
                    System.IO.File.WriteAllBytes(savePath, renderedBytes);

                    // Get file URL
                    string fileUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Common/intractIMg/{fileName}";


                    _commonService.StartCleanupTask(uploadFolder);

                    // Prepare the request for sending the message
                    var request = new SendIntractSMSRequest
                    {
                        Phone = data.Quotation.ContactsWhatsapp,
                        Template = "quotation_template",
                        Variable = data.Quotation.ContactsName,
                        ImageUrl = fileUrl
                    };

                    // Call the SendIntractWa method from CommonController
                    var response = _commonService.SendIntractWa(null, request);


                    return Content("Message sent successfully<script type='text/javascript'>window.close();</script>");

                }

                else if (ModType.GetValue(data1).ToString() == "WAInvoice")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<InvoiceTemplateRow>())
                    {
                        var quo = InvoiceRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.Invoice = connection.TryById<InvoiceRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(quo.ContactsName)
                           .Select(quo.ContactsWhatsapp)
                           
                           );

                        
                    }
                   
                    // Set up folder path
                    var uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Common", "intractIMg");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    // Generate unique file name
                    var fileName = $"Invoice_{Guid.NewGuid()}.pdf"; // Change extension as needed (e.g., .docx, .txt)
                    var savePath = Path.Combine(uploadFolder, fileName);

                    // Write file bytes to disk
                    System.IO.File.WriteAllBytes(savePath, renderedBytes);

                    // Get file URL
                    string fileUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Common/intractIMg/{fileName}";


                    _commonService.StartCleanupTask(uploadFolder);

                    // Prepare the request for sending the message
                    var request = new SendIntractSMSRequest
                    {
                        Phone = data.Invoice.ContactsWhatsapp,
                        Template = "invoice_template",
                        Variable = data.Invoice.ContactsName,
                        ImageUrl = fileUrl
                    };

                    // Call the SendIntractWa method from CommonController
                    var response = _commonService.SendIntractWa(null, request);


                    return Content("Message sent successfully<script type='text/javascript'>window.close();</script>");
                    //return JavaScript("Mail sent successfully, you can now close this window safely");
                }

                else if (ModType.GetValue(data1).ToString() == "WASales")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<InvoiceTemplateRow>())
                    {
                        
                        var sal = SalesRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.Sales = connection.TryById<SalesRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(sal.ContactsName)
                           .Select(sal.ContactsWhatsapp)
                           );
                    }


                    // Set up folder path
                    var uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Common", "intractIMg");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    // Generate unique file name
                    var fileName = $"Sales_{Guid.NewGuid()}.pdf"; // Change extension as needed (e.g., .docx, .txt)
                    var savePath = Path.Combine(uploadFolder, fileName);

                    // Write file bytes to disk
                    System.IO.File.WriteAllBytes(savePath, renderedBytes);

                    // Get file URL
                    string fileUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Common/intractIMg/{fileName}";


                    _commonService.StartCleanupTask(uploadFolder);

                    // Prepare the request for sending the message
                    var request = new SendIntractSMSRequest
                    {
                        Phone = data.Sales.ContactsWhatsapp,
                        Template = "sales_template",
                        Variable = data.Sales.ContactsName,
                        ImageUrl = fileUrl
                    };

                    // Call the SendIntractWa method from CommonController
                    var response = _commonService.SendIntractWa(null, request);


                    return Content("Message sent successfully<script type='text/javascript'>window.close();</script>");
                    
                }

                else if (ModType.GetValue(data1).ToString() == "WAChallan")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<ChallanTemplateRow>())
                    {
                        var quo = ChallanRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.Challan = connection.TryById<ChallanRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(quo.ContactsName)
                           .Select(quo.ContactsWhatsapp)
                          
                           );

                    }


                    // Set up folder path
                    var uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Common", "intractIMg");
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    // Generate unique file name
                    var fileName = $"Invoice_{Guid.NewGuid()}.pdf"; // Change extension as needed (e.g., .docx, .txt)
                    var savePath = Path.Combine(uploadFolder, fileName);

                    // Write file bytes to disk
                    System.IO.File.WriteAllBytes(savePath, renderedBytes);

                    // Get file URL
                    string fileUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Common/intractIMg/{fileName}";


                    _commonService.StartCleanupTask(uploadFolder);

                    // Prepare the request for sending the message
                    var request = new SendIntractSMSRequest
                    {
                        Phone = data.Challan.ContactsWhatsapp,
                        Template = "challan_template",
                        Variable = data.Challan.ContactsName,
                        ImageUrl = fileUrl
                    };

                    // Call the SendIntractWa method from CommonController
                    var response = _commonService.SendIntractWa(null, request);


                    return Content("Message sent successfully<script type='text/javascript'>window.close();</script>");

                }

                //
                else if (ModType.GetValue(data1).ToString() == "Challan")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<ChallanTemplateRow>())
                    {
                        var c = ChallanTemplateRow.Fields;
                        data.ChallanTemplate = connection.TryFirst<ChallanTemplateRow>(q => q
                            .SelectTableFields()
                            .Select(c.Sender)
                            .Select(c.Subject)
                            .Select(c.EmailTemplate)
                            .Select(c.CCEmails)
                            .Select(c.Bcc)
                            .Select(c.Host)
                            .Select(c.Port)
                            .Select(c.SSL)
                            .Select(c.EmailId)
                            .Select(c.EmailPassword)
                             .Where(c.CompanyId == (_userContext.User.ToUserDefinition()).CompanyId));

                        var u = UserRow.Fields;
                        data.User = connection.TryById<UserRow>(_userContext.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));


                        var quo = ChallanRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.Challan = connection.TryById<ChallanRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(quo.ContactsName)
                           .Select(quo.ContactsEmail)
                           .Select(quo.ContactsCCEmails)
                           .Select(quo.ContactsPurchaseEmail)
                           );
                    }


                    List<string> emails;
                    List<string> ccemails;
                    List<string> bccemails;

                    emails = null;
                    ccemails = null;
                    bccemails = null;

                    if (data.ChallanTemplate.CCEmails != null)
                    {
                        emails = data.ChallanTemplate.CCEmails.Split(',').ToList<string>();
                    }

                    if (data.ChallanTemplate.Bcc != null)
                    {
                        bccemails = data.ChallanTemplate.Bcc.Split(',').ToList<string>();
                    }

                    if (data.Challan.ContactsCCEmails != null)
                    {
                        ccemails = data.Challan.ContactsCCEmails.Split(',').ToList<string>();
                    }


                    //Configuration
                    if (data.ChallanTemplate.Host != null)
                    {
                        MailMessage mm = new MailMessage();
                        var addr = new MailAddress(data.ChallanTemplate.EmailId, data.ChallanTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.Challan.ContactsPurchaseEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.Challan.ContactsPurchaseEmail);
                            mm.CC.Add(data.Challan.ContactsEmail);
                        }
                        else
                        {
                            mm.To.Add(data.Challan.ContactsEmail);
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.ChallanTemplate.Subject;
                        var msg = data.ChallanTemplate.EmailTemplate;
                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.Challan.ContactsName);
                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Challan-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.ChallanTemplate.EmailId, data.ChallanTemplate.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.ChallanTemplate.SSL;
                        mail.Host = data.ChallanTemplate.Host;
                        mail.Port = data.ChallanTemplate.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }
                    else
                    {
                        MailMessage mm = new MailMessage();
                        if (data.User.EmailId.IsEmptyOrNull())
                            return Content("User email is not configured");

                        var addr = new MailAddress(data.User.EmailId, data.ChallanTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.Challan.ContactsPurchaseEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.Challan.ContactsPurchaseEmail);
                            mm.CC.Add(data.Challan.ContactsEmail);
                        }
                        else
                        {
                            mm.To.Add(data.Challan.ContactsEmail);
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.ChallanTemplate.Subject;
                        var msg = data.ChallanTemplate.EmailTemplate;
                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.Challan.ContactsName);
                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Challan-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.User.EmailId, data.User.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.User.SSL;
                        mail.Host = data.User.Host;
                        mail.Port = data.User.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }

                    return Content("Mail sent successfully<script type='text/javascript'> alert('Mail Sent successfully');  window.close(); </script>");
                    //return JavaScript("Mail sent successfully, you can now close this window safely");
                }
                else if (ModType.GetValue(data1).ToString() == "Sales")
                {
                    var data = new TemplateData();

                    using (var connection = _connections.NewFor<InvoiceTemplateRow>())
                    {
                        var com = CompanyDetailsRow.Fields;
                        data.Company = connection.TryFirst<CompanyDetailsRow>(q => q
                              .SelectTableFields()
                              .Select(com.MailToSubContacts)
                              .Select(com.MailToOrganisation)
                              .Where(com.Id == ((UserDefinition)_userContext.User.ToUserDefinition()).CompanyId));

                        var c = InvoiceTemplateRow.Fields;
                        data.InvoiceTemplate = connection.TryFirst<InvoiceTemplateRow>(q => q
                            .SelectTableFields()
                            .Select(c.Sender)
                            .Select(c.Subject)
                            .Select(c.EmailTemplate)
                            .Select(c.Attachment)
                            .Select(c.CCEmails)
                            .Select(c.BCCEmails)
                            .Select(c.Host)
                            .Select(c.Port)
                            .Select(c.SSL)
                            .Select(c.EmailId)
                            .Select(c.EmailPassword)
                             .Where(c.CompanyId == (_userContext.User.ToUserDefinition()).CompanyId));

                        var u = UserRow.Fields;
                        data.User = connection.TryById<UserRow>(_userContext.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));


                        var sal = SalesRow.Fields;

                        var Rpt = data1.GetType().GetProperty("id");
                        tst = Rpt.GetValue(data1).ToString();

                        data.Sales = connection.TryById<SalesRow>(Convert.ToInt32(tst), q => q
                           .SelectTableFields()
                           .Select(sal.ContactsName)
                           .Select(sal.ContactsEmail)
                           .Select(sal.ContactsCcEmails)
                           .Select(sal.ContactsContactType)
                           .Select(sal.ContactsBccEmails)
                           .Select(sal.ContactPersonEmail)
                           .Select(sal.ContactPersonName)
                           .Select(sal.Total)
                           .Select(sal.ContactsAccountsEmail)
                           );
                    }


                    List<string> emails;
                    List<string> bemails;
                    List<string> ccemails;
                    List<string> bccemails;

                    emails = null;
                    bemails = null;
                    ccemails = null;
                    bccemails = null;

                    if (data.InvoiceTemplate.CCEmails != null)
                    {
                        emails = data.InvoiceTemplate.CCEmails.Split(',').ToList<string>();
                    }

                    if (data.InvoiceTemplate.BCCEmails != null)
                    {
                        bemails = data.InvoiceTemplate.BCCEmails.Split(',').ToList<string>();
                    }

                    if (data.Sales.ContactsCcEmails != null)
                    {
                        ccemails = data.Sales.ContactsCcEmails.Split(',').ToList<string>();
                    }

                    if (data.Sales.ContactsBccEmails != null)
                    {
                        bccemails = data.Sales.ContactsBccEmails.Split(',').ToList<string>();
                    }

                    //Configuration
                    if (data.InvoiceTemplate.Host != null)
                    {
                        MailMessage mm = new MailMessage();
                        var addr = new MailAddress(data.InvoiceTemplate.EmailId, data.InvoiceTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.Sales.ContactsAccountsEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.Sales.ContactsAccountsEmail);
                            mm.CC.Add(data.Sales.ContactsEmail);
                            if (data.Company.MailToSubContacts == true)
                            {
                                if (data.Sales.ContactPersonEmail != null)
                                {
                                    mm.To.Add(data.Sales.ContactPersonEmail);
                                }
                            }
                        }
                        else
                        {
                            bool Organisation = Convert.ToBoolean(data.Company.MailToOrganisation);
                            int contacttype = Convert.ToInt32(data.Sales.ContactsContactType);
                            if (contacttype == 2 && Organisation == true)
                            {
                                mm.To.Add(data.Sales.ContactsEmail);
                            }
                            else if (contacttype == 1)
                            {
                                mm.To.Add(data.Sales.ContactsEmail);
                            }
                          
                            if (data.Company.MailToSubContacts == true)
                            {
                                if (data.Sales.ContactPersonEmail != null)
                                {
                                    mm.To.Add(data.Sales.ContactPersonEmail);
                                }
                            }
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.InvoiceTemplate.Subject;
                        var msg = data.InvoiceTemplate.EmailTemplate;

                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.Sales.ContactsName);
                        msg = msg.Replace("#amount", data.Sales.Total.Value.ToString("#,##0.00"));

                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Invoice-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        if (data.InvoiceTemplate.Attachment != null)
                        {
                            JArray att = JArray.Parse(data.InvoiceTemplate.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                }
                            }
                        }

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.InvoiceTemplate.EmailId, data.InvoiceTemplate.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.InvoiceTemplate.SSL;
                        mail.Host = data.InvoiceTemplate.Host;
                        mail.Port = data.InvoiceTemplate.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }
                    else
                    {
                        MailMessage mm = new MailMessage();
                        if (data.User.EmailId.IsEmptyOrNull())
                            return Content("User email is not configured");

                        var addr = new MailAddress(data.User.EmailId, data.InvoiceTemplate.Sender);

                        mm.From = addr;
                        mm.Sender = addr;
                        if (data.Sales.ContactsAccountsEmail.IsEmptyOrNull() == false)
                        {
                            mm.To.Add(data.Sales.ContactsAccountsEmail);
                            mm.CC.Add(data.Sales.ContactsEmail);
                            if (data.Company.MailToSubContacts == true)
                            {
                                if (data.Sales.ContactPersonEmail != null)
                                {
                                    mm.To.Add(data.Sales.ContactPersonEmail);
                                }
                            }
                        }
                        else
                        {
                            mm.To.Add(data.Sales.ContactsEmail);
                            if (data.Company.MailToSubContacts == true)
                            {
                                if (data.Sales.ContactPersonEmail != null)
                                {
                                    mm.To.Add(data.Sales.ContactPersonEmail);
                                }
                            }
                        }

                        if (emails != null)
                        {
                            for (int i = 0; i < emails.Count; i++)
                            {
                                mm.CC.Add(emails.ElementAt(i));
                            }
                        }

                        if (bemails != null)
                        {
                            for (int i = 0; i < bemails.Count; i++)
                            {
                                mm.Bcc.Add(bemails.ElementAt(i));
                            }
                        }

                        if (ccemails != null)
                        {
                            for (int i = 0; i < ccemails.Count; i++)
                            {
                                mm.CC.Add(ccemails.ElementAt(i));
                            }
                        }

                        if (bccemails != null)
                        {
                            for (int i = 0; i < bccemails.Count; i++)
                            {
                                mm.Bcc.Add(bccemails.ElementAt(i));
                            }
                        }

                        mm.Subject = data.InvoiceTemplate.Subject;
                        var msg = data.InvoiceTemplate.EmailTemplate;

                        msg = msg.Replace("#username", _userContext.User.ToUserDefinition().DisplayName);
                        msg = msg.Replace("#customername", data.Sales.ContactsName);
                        msg = msg.Replace("#amount", data.Sales.Total.Value.ToString("#,##0.00"));

                        mm.Body = msg;
                        mm.Attachments.Add(new Attachment(new MemoryStream(renderedBytes), "Invoice-" + DateTime.Now.ToLongDateString() + ".pdf"));

                        if (data.InvoiceTemplate.Attachment != null)
                        {
                            JArray att = JArray.Parse(data.InvoiceTemplate.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    mm.Attachments.Add(new Attachment(Path.Combine(_hostEnvironment.WebRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                }
                            }
                        }

                        mm.IsBodyHtml = true;
                        mm.Priority = MailPriority.High;

                        NetworkCredential nc = new NetworkCredential(data.User.EmailId, data.User.EmailPassword);

                        var mail = new SmtpClient();

                        mail.Credentials = nc;
                        mail.EnableSsl = (Boolean)data.User.SSL;
                        mail.Host = data.User.Host;
                        mail.Port = data.User.Port.Value;

                        mail.Timeout = 100000;
                        mail.Send(mm);
                    }

                    return Content("Mail sent successfully<script type='text/javascript'>window.close();</script>");
                    //return JavaScript("Mail sent successfully, you can now close this window safely");
                }
                else
                {
                    return new FileContentResult(renderedBytes, "application/octet-stream")
                    {
                        FileDownloadName = fileDownloadName
                    };
                }
            }

            var cd = new ContentDisposition
            {
                Inline = true,
                FileName = fileDownloadName
            };

            Response.Headers.Add("Content-Disposition", cd.ToString());
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileDownloadName, out var mimeType))
                mimeType = "application/octet-stream";
            return File(renderedBytes, mimeType, fileDownloadName);
        }

        private byte[] RenderAsPdf(IReport report, string key, string opt)
        {
            var externalUrl = _environmentSettings.Value.SiteExternalUrl ??
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{Url.Content("~/")}";
            var renderUrl = UriHelper.Combine(externalUrl, "Report/Render?" +
                "key=" + Uri.EscapeDataString(key));

            if (!string.IsNullOrEmpty(opt))
                renderUrl += "&opt=" + Uri.EscapeDataString(opt);

            renderUrl += "&print=1";

            var converter = new HtmlToPdfConverter();

            string wkhtmlPath = null;

            // Windows: look for exe
            if (OperatingSystem.IsWindows())
            {
                wkhtmlPath = Path.Combine(_hostEnvironment.ContentRootPath, "bin", "wkhtmltopdf.exe");
                if (!System.IO.File.Exists(wkhtmlPath))
                    wkhtmlPath = Path.Combine(_hostEnvironment.WebRootPath, "Reporting", "wkhtmltopdf.exe");
                if (!System.IO.File.Exists(wkhtmlPath))
                    wkhtmlPath = Path.Combine(_hostEnvironment.ContentRootPath, "wkhtmltopdf.exe");
                if (!System.IO.File.Exists(wkhtmlPath))
                    wkhtmlPath = Path.Combine(_hostEnvironment.ContentRootPath, "App_Data", "wkhtmltopdf.exe");
            }
            else
            {
                // Linux / MacOS: use system installed binary
                wkhtmlPath = "/usr/bin/wkhtmltopdf";
            }

            if (System.IO.File.Exists(wkhtmlPath))
                converter.UtilityExePath = wkhtmlPath;
            else
                throw new FileNotFoundException("wkhtmltopdf not found. Please install it.");

            converter.Url = renderUrl;

            var formsCookie = HttpContext.Request.Cookies[CookieAuthenticationDefaults.CookiePrefix + "Cookies"];
            if (formsCookie != null)
                converter.Cookies[CookieAuthenticationDefaults.CookiePrefix + "Cookies"] = formsCookie;

            var icustomize = report as ICustomizeHtmlToPdf;
            if (icustomize != null)
                icustomize.Customize(converter);

            return converter.Execute();
        }


        private IActionResult RenderAsHtml(IReport report, bool download, bool printing,
            ref byte[] renderedBytes)
        {
            var designAttr = report.GetType().GetAttribute<ReportDesignAttribute>();

            if (designAttr == null)
                throw new Exception(String.Format("Report design attribute for type '{0}' is not found!",
                    report.GetType().FullName));

            var data = report.GetData();

            var viewData = download ?
                new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = data } :
                ViewData;

            var iadditional = report as IReportWithAdditionalData;
            if (iadditional == null)
                viewData["AdditionalData"] = new Dictionary<string, object>();
            else
                viewData["AdditionalData"] = iadditional.GetAdditionalData();

            viewData["Printing"] = printing;

            if (!download)
                return View(viewName: designAttr.Design, model: data);

            var html = TemplateHelper.RenderViewToString(HttpContext.RequestServices,
                designAttr.Design, data);
            renderedBytes = Encoding.UTF8.GetBytes(html);
            return null;
        }

        [HttpPost, JsonFilter]
        public IActionResult Retrieve(ReportRetrieveRequest request)
        {
            return this.ExecuteMethod(() => new ReportRepository().Retrieve(request));
        }
    }

    public class TemplateData
    {
        public QuotationTemplateRow Tempalte { get; set; }
        public EnquiryTemplateRow EnquiryTempalte { get; set; }
        public UserRow User { get; set; }
        public QuotationRow Quotation { get; set; }
        public EnquiryRow Enquiry { get; set; }
        public CMSRow CMS { get; set; }
        public CmsTemplateRow CMSTemplate { get; set; }
        public AMCRow AMC { get; set; }
        public AMCTemplateRow AMCTemplate { get; set; }
        public InvoiceRow Invoice { get; set; }
        public SalesRow Sales { get; set; }
        public InvoiceTemplateRow InvoiceTemplate { get; set; }
        public ChallanRow Challan { get; set; }
        public ChallanTemplateRow ChallanTemplate { get; set; }
        public PurchaseOrderRow PO { get; set; }
        public PurchaseOrderTemplateRow POTemplate { get; set; }
        public CompanyDetailsRow Company { get; set; }
    }
}