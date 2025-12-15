
namespace AdvanceCRM.Common.Mailbox
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.IO;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using MailKit;
    using MailKit.Net.Imap;
    using MailKit.Net.Smtp;
    using MailKit.Search;
    using MailKit.Security;
    using MimeKit;
    using MimeKit.IO;
    using MimeKit.Tnef;
    using MimeKit.Text;
    using MimeKit.Utils;
    using PagedList;
    using PagedList.Mvc;
    using AdvanceCRM.Common.Mailbox.Classes;
    using Serenity.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    [Authorize]
    [Route("Mailbox")]
    public class MailboxController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public MailboxController(IWebHostEnvironment env)
        {
            _env = env;
        }

        //Inbox(Displaying all mails,Filtering mails according to respective folder,Searching and Paging)  
        //Outlook,Hotmail,Live
        EmailSettings objSettings = new EmailSettings();
        //Gmail
        //EmailSettings objSettings = new EmailSettings("imap.gmail.com", 993, "smtp.gmail.com", 587, "landmarktechedge@gmail.com", "L@ndmark@2020");
        //EmailSettings objSettings = new EmailSettings("imap.gmail.com", 993, "smtp.gmail.com", 587, "heenaiattar@gmail.com", "L@ndmark@2020");
        //BizplusCRM(Another Credientials "passwordreset@bizpluscrm.com", "L@ndmark@2020")
        //EmailSettings objSettings = new EmailSettings("mail.bizpluscrm.com", 993, "mail.bizpluscrm.com", 587, "support@bizpluscrm.com", "L@ndmark@2020");
        /*Rediffmail Not working 
        EmailSettings objSettings = new EmailSettings("pop.rediffmail.com",110,"smtp.rediffmail.com",25, "heenaattar89@rediffmail.com","heenarediff8");*/
        //Yahoo 
        //EmailSettings objSettings = new EmailSettings("imap.mail.yahoo.com", 993, "smtp.mail.yahoo.com", 587, "heenaattar89@yahoo.com", "landmark@2020");
        //EmailSettings objSettings = new EmailSettings("imap.mail.yahoo.com", 993, "smtp.mail.yahoo.com", 587, "heenaiattar@yahoo.com", "mclaren2018");
        [HttpGet("Inbox")]
        public IActionResult Inbox(string flag, string Search, int? page, string min, string max, string prepageNo)
        //public async Task<ActionResult> Inbox(string flag, string Search, int? page, string min, string max, string prepageNo)             
        {
            ReceiveMail model = new ReceiveMail();
            try
            {
                int pageSize = 20;
                int pageIndex = 1;
                model.Page = page;
                model.PrePageNo = Convert.ToInt32(prepageNo);
                model.Flag = flag;
                model.Search = Search;
                model.TitleName = "Inbox";
                pageIndex = model.Page.HasValue ? model.Page.Value : pageIndex;
                //Request to server will not go 
                if (string.IsNullOrEmpty(model.Flag))
                {
                    model.Flag = "Inbox";
                }
                //var m = new List<dynamic>();
                using (var client = new ImapClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    client.Connect(objSettings.IMAPHost, objSettings.IMAPPort, true);
                    // If you want to disable an authentication mechanism,
                    // you can do so by removing the mechanism like this:
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.Authenticate(objSettings.UserName, objSettings.Password);
                    //The Inbox folder is always available...
                    var mail = client.Inbox;
                    client.Inbox.Open(FolderAccess.ReadOnly);
                    model.UnseenCount = client.Inbox.Search(SearchQuery.NotSeen).Count;
                    model.InboxCount = mail.Count;
                    model.LastPage = (model.InboxCount / 20);
                    if (model.LastPage == 0)
                    {
                        model.LastPage = 1;
                    }
                    if (model.Flag == "Inbox")
                    {
                        if (model.InboxCount <= pageSize)
                        {
                            model.Min = 0;
                            model.Max = model.InboxCount;
                        }
                        else if (model.Page > model.PrePageNo)
                        {
                            model.Min = Convert.ToInt32(min) - 19;
                            model.Max = Convert.ToInt32(max) - 19;
                        }
                        else if (model.Page < model.PrePageNo)
                        {
                            model.Min = Convert.ToInt32(min) + 19;
                            model.Max = Convert.ToInt32(max) + 19;
                        }
                        else
                        {
                            model.Min = model.InboxCount - (pageSize);
                            model.Max = model.InboxCount;
                        }
                    }
                    //model.DraftCount=client.Inbox.Search(SearchQuery.Draft).Count;
                    //model.RecentCount = client.Inbox.Search(SearchQuery.New).Count;
                    if ((client.Capabilities & (ImapCapabilities.SpecialUse | ImapCapabilities.XList)) != 0)
                    {
                        model.ServerName = "Gmail";
                        ViewBag.ServerName = "Gmail";
                        if (model.Flag == "Drafts")
                        {
                            model.TitleName = "Draft";
                            mail = client.GetFolder(SpecialFolder.Drafts);
                        }
                        else if (model.Flag == "Trash")
                        {
                            model.TitleName = "Trash";
                            mail = client.GetFolder(SpecialFolder.Trash);
                        }
                        else if (model.Flag == "Sent")
                        {
                            model.TitleName = "Sent";
                            mail = client.GetFolder(SpecialFolder.Sent);
                        }
                        else if (model.Flag == "Junk")
                        {
                            model.TitleName = "Junk";
                            mail = client.GetFolder(SpecialFolder.Junk);
                        }
                        else if (model.Flag == "All")
                        {
                            model.TitleName = "All Mail";
                            mail = client.GetFolder(SpecialFolder.All);
                        }
                        else if (model.Flag == "Starred")
                        {
                            model.TitleName = "Starred";
                            mail = client.GetFolder(SpecialFolder.Flagged);
                        }
                    }
                    else
                    {
                        var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
                        if (model.Flag == null || model.Flag == "Inbox")
                        {
                            model.TitleName = "Inbox";
                        }
                        else if (model.Flag == "Drafts")
                        {
                            model.TitleName = "Draft";
                            if (objSettings.IMAPHost == "imap.mail.yahoo.com")
                            {
                                mail = FindFolder(toplevel, "Draft");
                            }
                            else
                            {
                                mail = FindFolder(toplevel, model.Flag);
                            }
                        }
                        else if (model.Flag == "Sent")
                        {
                            model.TitleName = "Sent";
                            mail = FindFolder(toplevel, model.Flag);
                        }
                        else if (model.Flag == "Starred")
                        {
                            model.TitleName = "Starred";
                            mail = FindFolder(toplevel, model.Flag);
                            //var foldermodel = client.GetFolder(foldername == "Starred" ? "INBOX" : foldername); foldermodel.Open(FolderAccess.ReadOnly); var uids = foldermodel.Search(foldername == "Starred" ? SearchQuery.Flagged : SearchQuery.All).OrderByDescending(m => m.Id).Take(10);
                        }
                        else if (model.Flag == "Junk")
                        {
                            model.TitleName = "Junk";
                            if (objSettings.IMAPHost == "imap.mail.yahoo.com")
                            {
                                mail = FindFolder(toplevel, "Bulk Mail");
                            }
                            else
                            {
                                mail = FindFolder(toplevel, model.Flag);
                            }
                        }
                        else if (model.Flag == "Trash")
                        {
                            model.TitleName = "Trash";
                            if (objSettings.IMAPHost == "imap-mail.outlook.com")
                            {
                                mail = FindFolder(toplevel, "Deleted");
                            }
                            else
                            {
                                mail = FindFolder(toplevel, model.Flag);
                            }
                        }
                        else if (model.Flag == "spam")
                        {
                            model.TitleName = "Spam";
                            mail = FindFolder(toplevel, model.Flag);
                        }
                        else if (model.Flag == "Archive")
                        {
                            model.TitleName = "Archive";
                            mail = FindFolder(toplevel, model.Flag);
                        }
                    }
                    mail.Open(FolderAccess.ReadOnly);
                    if (mail.Count > 0)
                    {
                        //fetch the UIDs of the newest 100 messages
                        //int index = Math.Max(mail.Count - 50, 0);                       
                        var orderBy = new[] { OrderBy.ReverseArrival, OrderBy.Subject };
                        //var messages = mail.Fetch(index, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.Envelope | MessageSummaryItems.All);
                        if (model.Search != "" && model.Search != null)
                        {
                            //var query = SearchQuery.Not(SearchQuery.Uids(dateRange.BeginDate)
                            //.Or(SearchQuery.DeliveredAfter(dateRange.EndDate)));
                            var query = SearchQuery.FromContains(model.Search.ToString()).Or((SearchQuery.SubjectContains(model.Search.ToString()))).Or(SearchQuery.SubjectContains(model.Search.ToString())).Or(SearchQuery.BodyContains(model.Search.ToString()));
                            //query.And(SearchQuery.SubjectContains(model.Search.ToString()));
                            //query.And(SearchQuery.BodyContains(model.Search.ToString()));
                            //query.Or(SearchQuery.All);
                            var uids = mail.Search(query);
                            if (uids.Count() != 0)
                            {
                                var messages = mail.Fetch(uids, MessageSummaryItems.Envelope | MessageSummaryItems.UniqueId | MessageSummaryItems.All | MailKit.MessageSummaryItems.Flags).ToList();
                                MessageSorter.Sort(messages, orderBy);
                                model.MailList = messages;
                            }
                            else
                            {
                                ViewBag.Message = "No records to display";
                                model.MailList = null;
                                client.Disconnect(true);
                                return View("~/Modules/Common/Mailbox/Inbox.cshtml", model);
                            }
                        }
                        else
                        {
                            if (model.Flag == "Inbox")
                            {
                                if ((objSettings.IMAPHost == "imap.gmail.com"))
                                {
                                    var messages = mail.Fetch(model.Min, model.Max, MessageSummaryItems.Envelope | MessageSummaryItems.UniqueId | MessageSummaryItems.All | MailKit.MessageSummaryItems.Flags).ToList();
                                    MessageSorter.Sort(messages, orderBy);
                                    model.MailList = messages;
                                }
                                else
                                {
                                    var messages = mail.Fetch(model.Min, model.Max - 1, MessageSummaryItems.Envelope | MessageSummaryItems.UniqueId | MessageSummaryItems.All | MailKit.MessageSummaryItems.Flags).ToList();
                                    MessageSorter.Sort(messages, orderBy);
                                    model.MailList = messages;
                                }

                            }
                            else if (!(model.Flag == "Inbox"))
                            {
                                var messages = mail.Fetch(0, -1, MessageSummaryItems.Envelope | MessageSummaryItems.UniqueId | MessageSummaryItems.All | MailKit.MessageSummaryItems.Flags).ToList();
                                MessageSorter.Sort(messages, orderBy);
                                model.MailList = messages;
                            }
                        }
                    }
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }
            return View("~/Modules/Common/Mailbox/Inbox.cshtml", model);
        }

        //Star and Unstar Mail
        public IActionResult StarUnstarAction(int Index, bool flag, string FolderName)
        {
            ReceiveMail model = new ReceiveMail();
            try
            {
                using (var client = new ImapClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    client.Connect(objSettings.IMAPHost, objSettings.IMAPPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.Authenticate(objSettings.UserName, objSettings.Password);
                    model.Flag = FolderName;
                    var mail = client.Inbox;
                    mail.Open(FolderAccess.ReadWrite);
                    if ((client.Capabilities & (ImapCapabilities.SpecialUse | ImapCapabilities.XList)) != 0)
                    {
                        model.ServerName = "Gmail";
                        ViewBag.ServerName = "Gmail";
                        if (model.Flag == "Drafts")
                        {
                            model.TitleName = "Draft";
                            mail = client.GetFolder(SpecialFolder.Drafts);
                        }
                        else if (model.Flag == "Trash")
                        {
                            model.TitleName = "Trash";
                            mail = client.GetFolder(SpecialFolder.Trash);
                        }
                        else if (model.Flag == "Sent")
                        {
                            model.TitleName = "Sent";
                            mail = client.GetFolder(SpecialFolder.Sent);
                        }
                        else if (model.Flag == "Junk")
                        {
                            model.TitleName = "Junk";
                            mail = client.GetFolder(SpecialFolder.Junk);
                        }
                        else if (model.Flag == "All")
                        {
                            model.TitleName = "All Mail";
                            mail = client.GetFolder(SpecialFolder.All);
                        }
                        else if (model.Flag == "Starred")
                        {
                            model.TitleName = "Starred";
                            mail = client.GetFolder(SpecialFolder.Flagged);
                        }
                        mail.Open(FolderAccess.ReadWrite);
                        if (flag == true)
                        {
                            mail.AddFlags(Index, MessageFlags.Flagged, true);
                            mail.AddLabels(Index, new string[] { "\\Starred" }, true);
                        }
                        else if (flag == false)
                        {
                            mail.RemoveFlags(Index, MessageFlags.Flagged, true);
                            mail.RemoveLabels(Index, new string[] { "\\Starred" }, true);
                        }
                    }
                    else
                    {
                        var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
                        if (model.Flag == null || model.Flag == "Inbox")
                        {
                            model.TitleName = "Inbox";
                        }
                        else if (model.Flag == "Drafts")
                        {
                            model.TitleName = "Draft";
                            mail = FindFolder(toplevel, model.Flag);
                        }
                        else if (model.Flag == "Sent")
                        {
                            model.TitleName = "Sent";
                            mail = FindFolder(toplevel, model.Flag);
                        }
                        else if (model.Flag == "Starred")
                        {
                            model.TitleName = "Starred";
                            mail = FindFolder(toplevel, model.Flag);
                            //var foldermodel = client.GetFolder(foldername == "Starred" ? "INBOX" : foldername); foldermodel.Open(FolderAccess.ReadOnly); var uids = foldermodel.Search(foldername == "Starred" ? SearchQuery.Flagged : SearchQuery.All).OrderByDescending(m => m.Id).Take(10);
                        }
                        else if (model.Flag == "Junk")
                        {
                            model.TitleName = "Junk";
                            mail = FindFolder(toplevel, model.Flag);
                        }
                        else if (model.Flag == "Trash")
                        {
                            model.TitleName = "Trash";
                            if (objSettings.IMAPHost == "imap-mail.outlook.com")
                            {
                                mail = FindFolder(toplevel, "Deleted");
                            }
                            else
                            {
                                mail = FindFolder(toplevel, model.Flag);
                            }
                        }
                        else if (model.Flag == "spam")
                        {
                            model.TitleName = "Spam";
                            mail = FindFolder(toplevel, model.Flag);
                        }
                        else if (model.Flag == "Archive")
                        {
                            model.TitleName = "Archive";
                            mail = FindFolder(toplevel, model.Flag);
                        }
                        mail.Open(FolderAccess.ReadWrite);
                        if (flag == true)
                        {
                            //var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
                            //mail = FindFolder(toplevel, "Inbox");                    
                            var message = mail.Fetch(Index, -1, MessageSummaryItems.Envelope | MessageSummaryItems.UniqueId | MessageSummaryItems.All | MailKit.MessageSummaryItems.Flags);
                            var MessageFlag = message.FirstOrDefault().Flags;
                            if (MessageFlag.ToString() == "Seen")
                            {
                                mail.SetFlags(Index, MessageFlags.Seen | MessageFlags.Flagged, true);
                                //mail.SetFlags(Index, MessageFlags.Flagged, true);
                            }
                            else if (MessageFlag.ToString() == "Seen, Answered")
                            {
                                mail.SetFlags(Index, MessageFlags.Seen | MessageFlags.Answered | MessageFlags.Flagged, true);
                            }
                            else if ((MessageFlag.ToString() == "Seen, Draft") || (MessageFlag.ToString() == "Draft"))
                            {
                                mail.SetFlags(Index, MessageFlags.Seen | MessageFlags.Draft | MessageFlags.Flagged, true);
                            }
                            //else if (MessageFlag.ToString() == "Draft")
                            //{
                            //    mail.SetFlags(Index,MessageFlags.Draft | MessageFlags.Flagged, true);
                            //}
                            else
                            {
                                mail.SetFlags(Index, MessageFlags.Flagged, true);
                            }
                        }
                        else
                        {
                            mail.SetFlags(Index, MessageFlags.Seen, true);
                        }

                    }
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }
            return Json(new { status = true, message = "Successfull!!" });
        }

        //Compose Mail
        [HttpGet("Compose")]
        public IActionResult Compose()
        {
            EmailDetails model = new EmailDetails();

            // gracefully handle missing mail configuration instead of throwing
            if (string.IsNullOrWhiteSpace(objSettings.IMAPHost) ||
                string.IsNullOrWhiteSpace(objSettings.UserName))
            {
                TempData["Errormessage"] = "Mail server settings are not configured.";
                return View("~/Modules/Common/Mailbox/Compose.cshtml", model);
            }

            using (var client = new ImapClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                client.Connect(objSettings.IMAPHost, objSettings.IMAPPort, true);
                // If you want to disable an authentication mechanism,
                // you can do so by removing the mechanism like this:
                client.AuthenticationMechanisms.Remove("XOAUTH");
                client.Authenticate(objSettings.UserName, objSettings.Password);
                //The Inbox folder is always available...
                var mail = client.Inbox;
                client.Inbox.Open(FolderAccess.ReadOnly);
                model.UnseenCount = client.Inbox.Search(SearchQuery.NotSeen).Count;
                client.Disconnect(true);
            }
            return View("~/Modules/Common/Mailbox/Compose.cshtml", model);
        }

        //Upload Files
        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file selected.");

                string uploadDir = Path.Combine(_env.ContentRootPath, "App_Data", "Tmp", "Files");
                Directory.CreateDirectory(uploadDir);
                string savedFileName = Path.Combine(uploadDir, Path.GetFileName(file.FileName));
                using (var stream = new FileStream(savedFileName, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var msg = new { msg = "File Uploaded", filename = file.FileName, url = savedFileName };
                return Json(msg);
            }
            catch (Exception e)
            {
                //If you want this working with a custom error you need to change in file jquery.uploadfile.js, the name of 
                //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
                var msg = new { jquery_upload_file_error = e.Message };
                return Json(msg);
            }
        }

        [HttpPost("DeleteFile")]
        public IActionResult DeleteFile(string filepath)
        {
            try
            {
                System.IO.File.Delete(filepath);
                var msg = new { msg = "File Deleted!" };
                return Json(msg);
            }
            catch (Exception e)
            {
                //If you want this working with a custom error you need to change the name of 
                //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
                var msg = new { jquery_upload_file_error = e.Message };
                return Json(msg);
            }
        }

        [HttpPost("DeleteFile1")]
        public IActionResult DeleteFile1(string op, string name)
        {
            try
            {
                string uploadDir = Path.Combine(_env.ContentRootPath, "App_Data", "Tmp", "Files");
                string savedFileName = Path.Combine(uploadDir, name);
                System.IO.File.Delete(savedFileName);
                //var msg = new { msg = "File Uploaded", filename = hpf.FileName, url = savedFileName };
                //return Json(msg);
                //if (model.AttachedFiles != null)
                //{
                //    foreach (var filePath in model.AttachedFiles.Split(','))
                //    {
                //        if (System.IO.File.Exists(filePath))
                //        {
                //            System.IO.File.Delete(filePath);
                //        }
                //    }
                //}
                //var msg = new { msg = "File Uploaded", filename = hpf.FileName, url = savedFileName };
                var msg = "Deleted Successfully";
                return Json(msg);
            }
            catch (Exception e)
            {
                //If you want this working with a custom error you need to change in file jquery.uploadfile.js, the name of 
                //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
                var msg = new { jquery_upload_file_error = e.Message };
                return Json(msg);
            }
        }

        //Compose Mail
        [HttpPost("Compose")]
        [ValidateAntiForgeryToken]
        public IActionResult Compose(EmailDetails model)
        {
            //var message = new MimeMessage();
            try
            {
                if (ModelState.IsValid)
                {
                    var message = new MailMessage();
                    var m = new MailAddress(objSettings.UserName, objSettings.UserName);
                    message.From = m;
                    List<string> Receipent = model.ToAddress.Split(',').ToList();
                    for (int i = 0; i < Receipent.Count; i++)
                    {
                        message.To.Add(Receipent.ElementAt(i));
                    }
                    if (!string.IsNullOrEmpty(model.CCAddress))
                    {
                        List<string> CCReceipent = model.CCAddress.Split(',').ToList();
                        for (int i = 0; i < CCReceipent.Count; i++)
                        {
                            message.CC.Add(CCReceipent.ElementAt(i));
                        }
                    }
                    if (!string.IsNullOrEmpty(model.BCCAddress))
                    {
                        List<string> BCCReceipent = model.BCCAddress.Split(',').ToList();
                        for (int i = 0; i < BCCReceipent.Count; i++)
                        {
                            message.Bcc.Add(BCCReceipent.ElementAt(i));
                        }
                    }
                    message.Subject = model.Subject;
                    // create our message text, just like before (except don't set it as the message.Body)
                    var builder = new BodyBuilder();
                    // Set the plain-text version of the message text
                    builder.TextBody = model.TextBody;
                    // now you can get and validate the file type:
                    //var isFileSupported = IsFileSupported(postedFile);
                    // Now we just need to set the message body and we're done   
                    if (ModelState.IsValid)
                    {   //iterating through multiple file collection   
                        //Checking file is available to save.
                        if (model.AttachedFiles != null)
                        {
                            foreach (var filePath in model.AttachedFiles.Split(','))
                            {
                                if (System.IO.File.Exists(filePath))
                                {
                                    message.Attachments.Add(new Attachment(filePath));
                                    //builder.Attachments.Add(filePath);
                                }
                            }
                        }
                    }
                    //message.Attachments = model.Attachments;
                    //message.Attachments = model.Attachments;
                    message.Body = model.TextBody;
                    message.IsBodyHtml = true;
                    NetworkCredential nc = new NetworkCredential(objSettings.UserName, objSettings.Password);
                    using (var client = new System.Net.Mail.SmtpClient())
                    {
                        client.Credentials = nc;
                        client.EnableSsl = true;
                        client.Host = objSettings.SMTPHost;
                        client.Port = objSettings.SMTPPort;
                        client.Timeout = 100000;
                        client.Send(message);
                        client.Dispose();
                        message.Dispose();
                    }
                    if (model.AttachedFiles != null)
                    {
                        foreach (var filePath in model.AttachedFiles.Split(','))
                        {
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                    }
                    message.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
                    TempData["message"] = "Message sent successfully";
                    return RedirectToAction("Inbox", "Mailbox", new { flag = "Inbox", Search = "", page = 1, min = 0, max = 0, prepageNo = 1 });
                }
            }
            catch (SmtpFailedRecipientException ex)
            {
                //ex.FailedRecipient;
                TempData["Errormessage"] = ex.GetBaseException().Message.ToString();
                return Json(TempData);
                //ex.FailedRecipient;
                //and ex.GetBaseException() should give you enough info.
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                return Json(TempData);
                //return View(SModel);
            }
            return RedirectToAction("Compose");
        }

        class HtmlPreviewVisitor : MimeVisitor
        {
            List<MultipartRelated> stack = new List<MultipartRelated>();
            List<MimeEntity> attachments = new List<MimeEntity>();
            readonly string tempDir;
            string body;

            /// <summary>
            /// Creates a new HtmlPreviewVisitor.
            /// </summary>
            /// <param name="tempDirectory">A temporary directory used for storing image files.</param>
            public HtmlPreviewVisitor(string tempDirectory)
            {
                tempDir = tempDirectory;
            }

            /// <summary>
            /// The list of attachments that were in the MimeMessage.
            /// </summary>
            public IList<MimeEntity> Attachments
            {
                get { return attachments; }
            }

            /// <summary>
            /// The HTML string that can be set on the BrowserControl.
            /// </summary>
            public string HtmlBody
            {
                get { return body ?? string.Empty; }
            }

            protected override void VisitMultipartAlternative(MultipartAlternative alternative)
            {
                // walk the multipart/alternative children backwards from greatest level of faithfulness to the least faithful
                for (int i = alternative.Count - 1; i >= 0 && body == null; i--)
                    alternative[i].Accept(this);
            }

            protected override void VisitMultipartRelated(MultipartRelated related)
            {
                var root = related.Root;

                // push this multipart/related onto our stack
                stack.Add(related);

                // visit the root document
                root.Accept(this);

                // pop this multipart/related off our stack
                stack.RemoveAt(stack.Count - 1);
            }

            // look up the image based on the img src url within our multipart/related stack
            bool TryGetImage(string url, out MimePart image)
            {
                UriKind kind;
                int index;
                Uri uri;

                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    kind = UriKind.Absolute;
                else if (Uri.IsWellFormedUriString(url, UriKind.Relative))
                    kind = UriKind.Relative;
                else
                    kind = UriKind.RelativeOrAbsolute;

                try
                {
                    uri = new Uri(url, kind);
                }
                catch
                {
                    image = null;
                    return false;
                }

                for (int i = stack.Count - 1; i >= 0; i--)
                {
                    if ((index = stack[i].IndexOf(uri)) == -1)
                        continue;

                    image = stack[i][index] as MimePart;
                    return image != null;
                }

                image = null;

                return false;
            }

            // Save the image to our temp directory and return a "file://" url suitable for
            // the browser control to load.
            // Note: if you'd rather embed the image data into the HTML, you can construct a
            // "data:" url instead.
            string SaveImage(MimePart image, string url)
            {
                string fileName = url.Replace(':', '_').Replace('\\', '_').Replace('/', '_');

                string path = Path.Combine(tempDir, fileName);

                if (!System.IO.File.Exists(path))
                {
                    using (var output = System.IO.File.Create(path))
                        image.Content.DecodeTo(output);
                }

                return "file://" + path.Replace('\\', '/');
            }

            // Replaces <img src=...> urls that refer to images embedded within the message with
            // "file://" urls that the browser control will actually be able to load.
            void HtmlTagCallback(HtmlTagContext ctx, HtmlWriter htmlWriter)
            {
                if (ctx.TagId == HtmlTagId.Image && !ctx.IsEndTag && stack.Count > 0)
                {
                    ctx.WriteTag(htmlWriter, false);

                    // replace the src attribute with a file:// URL
                    foreach (var attribute in ctx.Attributes)
                    {
                        if (attribute.Id == HtmlAttributeId.Src)
                        {
                            MimePart image;
                            string url;

                            if (!TryGetImage(attribute.Value, out image))
                            {
                                htmlWriter.WriteAttribute(attribute);
                                continue;
                            }

                            url = SaveImage(image, attribute.Value);

                            htmlWriter.WriteAttributeName(attribute.Name);
                            htmlWriter.WriteAttributeValue(url);
                        }
                        else
                        {
                            htmlWriter.WriteAttribute(attribute);
                        }
                    }
                }
                else if (ctx.TagId == HtmlTagId.Body && !ctx.IsEndTag)
                {
                    ctx.WriteTag(htmlWriter, false);

                    // add and/or replace oncontextmenu="return false;"
                    foreach (var attribute in ctx.Attributes)
                    {
                        if (attribute.Name.ToLowerInvariant() == "oncontextmenu")
                            continue;

                        htmlWriter.WriteAttribute(attribute);
                    }

                    htmlWriter.WriteAttribute("oncontextmenu", "return false;");
                }
                else
                {
                    // pass the tag through to the output
                    ctx.WriteTag(htmlWriter, true);
                }
            }

            protected override void VisitTextPart(TextPart entity)
            {
                TextConverter converter;

                if (body != null)
                {
                    // since we've already found the body, treat this as an attachment
                    attachments.Add(entity);
                    return;
                }

                if (entity.IsHtml)
                {
                    converter = new HtmlToHtml
                    {
                        HtmlTagCallback = HtmlTagCallback
                    };
                }
                else if (entity.IsFlowed)
                {
                    var flowed = new FlowedToHtml();
                    string delsp;

                    if (entity.ContentType.Parameters.TryGetValue("delsp", out delsp))
                        flowed.DeleteSpace = delsp.ToLowerInvariant() == "yes";

                    converter = flowed;
                }
                else
                {
                    converter = new TextToHtml();
                }

                body = converter.Convert(entity.Text);
            }

            protected override void VisitTnefPart(TnefPart entity)
            {
                // extract any attachments in the MS-TNEF part
                attachments.AddRange(entity.ExtractAttachments());
            }

            protected override void VisitMessagePart(MessagePart entity)
            {
                // treat message/rfc822 parts as attachments
                attachments.Add(entity);

            }

            protected override void VisitMimePart(MimePart entity)
            {
                // realistically, if we've gotten this far, then we can treat this as an attachment
                // even if the IsAttachment property is false.
                attachments.Add(entity);
            }

        }
        HtmlPreviewVisitor Render(MimeMessage message)
        {
            var tmpDir = Path.Combine(Path.GetTempPath(), message.MessageId);
            var visitor = new HtmlPreviewVisitor(tmpDir);
            Directory.CreateDirectory(tmpDir);
            message.Accept(visitor);
            return visitor;
            //DisplayHtml(visitor.HtmlBody);
            //DisplayAttachments(visitor.Attachments);
        }


        //Read single mail FolderName
        public IActionResult Read(int IndexId, string Flag, string FolderName)
        {
            EmailDetails model = new EmailDetails();
            try
            {
                model.MessageIndex = IndexId;
                using (var client = new ImapClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    client.Connect(objSettings.IMAPHost, objSettings.IMAPPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.Authenticate(objSettings.UserName, objSettings.Password);
                    model.FolderName = FolderName;
                    var mail = client.Inbox;
                    mail.Open(FolderAccess.ReadWrite);
                    model.UnseenCount = client.Inbox.Search(SearchQuery.NotSeen).Count;
                    if ((client.Capabilities & (ImapCapabilities.SpecialUse | ImapCapabilities.XList)) != 0)
                    {

                        if (model.FolderName == "Drafts")
                        {
                            mail = client.GetFolder(SpecialFolder.Drafts);
                        }
                        else if (model.FolderName == "Trash")
                        {
                            mail = client.GetFolder(SpecialFolder.Trash);
                        }
                        else if (model.FolderName == "Sent")
                        {
                            mail = client.GetFolder(SpecialFolder.Sent);
                        }
                        else if (model.FolderName == "Junk")
                        {
                            mail = client.GetFolder(SpecialFolder.Junk);
                        }
                        else if (model.FolderName == "All")
                        {
                            model.FolderName = "All Mail";
                            mail = client.GetFolder(SpecialFolder.All);
                        }
                        else if (model.FolderName == "Starred")
                        {
                            mail = client.GetFolder(SpecialFolder.Flagged);
                        }
                        mail.Open(FolderAccess.ReadWrite);
                    }
                    else
                    {
                        var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
                        if (model.FolderName == null || model.FolderName == "Inbox")
                        {
                        }
                        else if (model.FolderName == "Drafts")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Sent")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Starred")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                            //var foldermodel = client.GetFolder(foldername == "Starred" ? "INBOX" : foldername); foldermodel.Open(FolderAccess.ReadOnly); var uids = foldermodel.Search(foldername == "Starred" ? SearchQuery.Flagged : SearchQuery.All).OrderByDescending(m => m.Id).Take(10);
                        }
                        else if (model.FolderName == "Junk")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Trash")
                        {
                            if (objSettings.IMAPHost == "imap-mail.outlook.com")
                            {
                                mail = FindFolder(toplevel, "Deleted");
                            }
                            else
                            {
                                mail = FindFolder(toplevel, model.FolderName);
                            }
                        }
                        else if (model.FolderName == "spam")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Archive")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        mail.Open(FolderAccess.ReadWrite);
                    }
                    model.InboxCount = client.Inbox.Count;
                    model.Message = mail.GetMessage(model.MessageIndex);
                    //Render(model.Message);
                    //Render(model.Message);
                    if (Flag == "None")
                    {
                        mail.AddFlags(model.MessageIndex, MessageFlags.Seen, true);
                    }
                    else if (Flag == "Flagged")
                    {
                        mail.SetFlags(model.MessageIndex, MessageFlags.Seen | MessageFlags.Flagged, true);
                        //mail.AddFlags(model.MessageIndex, MessageFlags.Seen, true);
                    }
                    //string body, bodyparts;                    
                    model.Body = mail.GetMessage(model.MessageIndex).Body;
                    model.HtmlBody = mail.GetMessage(model.MessageIndex).HtmlBody;
                    model.TextBody = mail.GetMessage(model.MessageIndex).TextBody;
                    model.Subject = mail.GetMessage(model.MessageIndex).Subject;
                    model.To = mail.GetMessage(model.MessageIndex).To;
                    model.ToName = mail.GetMessage(model.MessageIndex).To.Mailboxes.FirstOrDefault().Name;
                    model.ToAddress = mail.GetMessage(model.MessageIndex).To.Mailboxes.FirstOrDefault().Address;
                    model.From = mail.GetMessage(model.MessageIndex).From;
                    model.FromName = mail.GetMessage(model.MessageIndex).From.Mailboxes.FirstOrDefault().Name;
                    model.FromAddress = mail.GetMessage(model.MessageIndex).From.Mailboxes.FirstOrDefault();
                    model.CC = mail.GetMessage(model.MessageIndex).Cc;
                    model.BCC = mail.GetMessage(model.MessageIndex).Bcc;
                    model.Date = mail.GetMessage(model.MessageIndex).Date;
                    model.Attachments = mail.GetMessage(model.MessageIndex).Attachments;
                    model.mimeParts = mail.GetMessage(model.MessageIndex).BodyParts.OfType<MimePart>().Where(x => x.IsAttachment);
                    if ((string.IsNullOrEmpty(model.TextBody)) && (string.IsNullOrEmpty(model.HtmlBody)))
                    {
                        model.Bodyparts = mail.GetMessage(model.MessageIndex).BodyParts.ToList();
                        foreach (var bodyPart in model.Bodyparts)
                        {
                            //if (!bodyPart.IsAttachment)
                            //    continue;

                            if (bodyPart is MessagePart)
                            {

                                var rfc822 = (MessagePart)bodyPart;
                                model.HtmlBody = rfc822.Message.HtmlBody;
                                model.TextBody = rfc822.Message.TextBody;
                                model.Attachments = rfc822.Message.Attachments;
                                // rfc822.Message.WriteTo(stream);
                            }

                        }
                        //model.Bodyparts.SingleOrDefault().

                    }
                    //foreach (MimePart part in inbox.GetMessage(model.MessageIndex).BodyParts.OfType<MimePart>().Where(x => x.IsAttachment))
                    //{
                    //    Console.WriteLine("\t* " + part.FileName);
                    //    //model.FileSize = Convert.ToInt64(part.ContentDisposition.Size);                   
                    //    model.Filename = part.FileName;
                    //    model.FileLocation = part.ContentLocation;
                    //}                
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }
            return View("~/Modules/Common/Mailbox/Read.cshtml", model);
        }

        //Delete Single Mail
        public IActionResult Delete(int Index)
        {
            try
            {
                using (var client = new ImapClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    //client.Connect("imap.gmail.com", 993, true);
                    client.Connect("mail.bizpluscrm.com", 993, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.Authenticate("support@bizpluscrm.com", "L@ndmark@2020");
                    //client.Authenticate("landmarktechedge@gmail.com", "L@ndmark@2020");
                    // The Inbox folder is always available...               
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadWrite);
                    //inbox.AddFlagsAsync(Index, MessageFlags.Deleted,true,CancellationToken.None);               
                    inbox.SetFlags(Index, MessageFlags.Deleted, false);
                    //client.Inbox.AddFlagsAsync(Index, MessageFlags.Deleted, false, CancellationToken.None);
                    //client.Inbox.AddFlags(new int[] {  }, MessageFlags.Deleted);
                    //client.Inbox.Expunge();
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }
            return RedirectToAction("Inbox");
        }

        //Delete Multiple Mails
        [HttpPost]
        public IActionResult DeleteMail(int[] id, string flag)
        {
            try
            {
                using (var client = new ImapClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    client.Connect(objSettings.IMAPHost, objSettings.IMAPPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.Authenticate(objSettings.UserName, objSettings.Password);
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadWrite);
                    //client.Inbox.AddFlags(new int[] { index }, MessageFlags.Deleted);
                    foreach (var item in id)
                    {

                        if ((client.Capabilities & (ImapCapabilities.SpecialUse | ImapCapabilities.XList)) != 0)
                        {
                            if (flag == "Inbox")
                            {
                                inbox.AddLabels(item, new string[] { "\\Trash" }, true);
                                //client.Inbox.Expunge();
                            }
                            else if (flag == "Sent")
                            {
                                client.GetFolder(SpecialFolder.Sent).Open(FolderAccess.ReadWrite);
                                client.GetFolder(SpecialFolder.Sent).AddLabels(item, new string[] { "\\Trash" }, true);
                            }
                            else if (flag == "Drafts")
                            {
                                client.GetFolder(SpecialFolder.Drafts).Open(FolderAccess.ReadWrite);
                                client.GetFolder(SpecialFolder.Drafts).AddLabels(item, new string[] { "\\Trash" }, true);
                            }
                            else if (flag == "Starred")
                            {
                                client.GetFolder(SpecialFolder.Flagged).Open(FolderAccess.ReadWrite);
                                client.GetFolder(SpecialFolder.Flagged).AddLabels(item, new string[] { "\\Trash" }, true);
                            }
                            else if (flag == "Junk")
                            {
                                client.GetFolder(SpecialFolder.Junk).Open(FolderAccess.ReadWrite);
                                client.GetFolder(SpecialFolder.Junk).AddLabels(item, new string[] { "\\Trash" }, true);
                            }
                            else if (flag == "Trash")
                            {
                                client.GetFolder(SpecialFolder.Trash).Open(FolderAccess.ReadWrite);
                                client.GetFolder(SpecialFolder.Trash).AddFlags(item, MessageFlags.Deleted, true);
                                client.GetFolder(SpecialFolder.Trash).Expunge();
                            }
                            //var trash = client.GetFolder(SpecialFolder.Trash); var moved = client.Inbox.MoveTo(uid, trash); if (moved.HasValue) { trash.Open(FolderAccess.ReadWrite); trash.AddFlags(moved.Value, MessageFlags.Deleted, true); trash.Expunge(new[] { moved.Value }); }
                        }
                        else
                        {
                            var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
                            IMailFolder trash;
                            if (objSettings.IMAPHost == "imap-mail.outlook.com")
                            {
                                trash = FindFolder(toplevel, "Deleted");
                            }
                            else
                            {
                                trash = FindFolder(toplevel, "Trash");
                            }
                            if (flag != "Trash")
                            {
                                if (flag == "Inbox")
                                {
                                    inbox.MoveTo(item, trash);
                                }
                                else if (flag == "Sent")
                                {
                                    FindFolder(toplevel, "Sent").Open(FolderAccess.ReadWrite);
                                    FindFolder(toplevel, "Sent").MoveTo(item, trash);
                                }
                                else if (flag == "Drafts")
                                {
                                    FindFolder(toplevel, "Drafts").Open(FolderAccess.ReadWrite);
                                    FindFolder(toplevel, "Drafts").MoveTo(item, trash);
                                }
                                else if (flag == "Junk")
                                {
                                    FindFolder(toplevel, "Junk").Open(FolderAccess.ReadWrite);
                                    FindFolder(toplevel, "Junk").MoveTo(item, trash);
                                }
                                else if (flag == "Archive")
                                {
                                    FindFolder(toplevel, "Archive").Open(FolderAccess.ReadWrite);
                                    FindFolder(toplevel, "Archive").MoveTo(item, trash);
                                }
                            }
                            else
                            {
                                trash.Open(FolderAccess.ReadWrite);
                                trash.AddFlags(item, MessageFlags.Deleted, true);
                                trash.Expunge();
                            }
                        }
                    }
                    client.Disconnect(true);
                }
            }

            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }

            //return RedirectToAction("Inbox");
            return Json(new { status = true });
        }

        //Reply Function
        public class ReplyVisitor : MimeVisitor
        {
            readonly Stack<Multipart> stack = new Stack<Multipart>();
            MimeMessage original, reply;
            MailboxAddress from;
            bool replyToAll;

            /// <summary>
            /// Creates a new ReplyVisitor.
            /// </summary>
            public ReplyVisitor(MailboxAddress from, bool replyToAll)
            {
                this.replyToAll = replyToAll;
                this.from = from;
            }
            /// <summary>
            /// Gets the reply.
            /// </summary>
            /// <value>The reply.</value>
            public MimeMessage Reply
            {
                get { return reply; }
            }

            void Push(MimeEntity entity)
            {
                var multipart = entity as Multipart;

                if (reply.Body == null)
                {
                    reply.Body = entity;
                }
                else
                {
                    var parent = stack.Peek();
                    parent.Add(entity);
                }

                if (multipart != null)
                    stack.Push(multipart);
            }

            void Pop()
            {
                stack.Pop();
            }

            //static string GetOnDateSenderWrote(MimeMessage message)
            //{
            //    var sender = message.Sender != null ? message.Sender : message.From.Mailboxes.FirstOrDefault();
            //    var name = sender != null ? (!string.IsNullOrEmpty(sender.Name) ? sender.Name : sender.Address) : "an unknown sender";

            //    return string.Format("On {0}, {1} wrote:", message.Date.ToString("f"), name);
            //}

            /// <summary>
            /// Visit the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            public override void Visit(MimeMessage message)
            {
                reply = new MimeMessage();
                original = message;

                stack.Clear();

                reply.From.Add(from.Clone());

                // reply to the sender of the message
                if (message.ReplyTo.Count > 0)
                {
                    reply.To.AddRange(message.ReplyTo);
                }
                else if (message.From.Count > 0)
                {
                    reply.To.AddRange(message.From);
                }
                else if (message.Sender != null)
                {
                    reply.To.Add(message.Sender);
                }

                if (replyToAll)
                {
                    // include all of the other original recipients - TODO: remove ourselves from these lists
                    reply.To.AddRange(message.To);
                    reply.Cc.AddRange(message.Cc);
                }

                // set the reply subject
                if (!message.Subject.StartsWith("Re:", StringComparison.OrdinalIgnoreCase))
                    reply.Subject = "Re: " + message.Subject;
                else
                    reply.Subject = message.Subject;

                // construct the In-Reply-To and References headers
                if (!string.IsNullOrEmpty(message.MessageId))
                {
                    reply.InReplyTo = message.MessageId;
                    foreach (var id in message.References)
                        reply.References.Add(id);
                    reply.References.Add(message.MessageId);
                }

                base.Visit(message);
            }

            /// <summary>
            /// Visit the specified entity.
            /// </summary>
            /// <param name="entity">The MIME entity.</param>
            /// <exception cref="System.NotSupportedException">
            /// Only Visit(MimeMessage) is supported.
            /// </exception>
            public override void Visit(MimeEntity entity)
            {
                throw new NotSupportedException();
            }

            protected override void VisitMultipartAlternative(MultipartAlternative alternative)
            {
                var multipart = new MultipartAlternative();

                Push(multipart);

                for (int i = 0; i < alternative.Count; i++)
                    alternative[i].Accept(this);

                Pop();
            }

            protected override void VisitMultipartRelated(MultipartRelated related)
            {
                var multipart = new MultipartRelated();
                var root = related.Root;

                Push(multipart);

                root.Accept(this);

                for (int i = 0; i < related.Count; i++)
                {
                    if (related[i] != root)
                        related[i].Accept(this);
                }

                Pop();
            }

            protected override void VisitMultipart(Multipart multipart)
            {
                foreach (var part in multipart)
                {
                    if (part is MultipartAlternative)
                        part.Accept(this);
                    else if (part is MultipartRelated)
                        part.Accept(this);
                    else if (part is TextPart)
                        part.Accept(this);
                }
            }

            void HtmlTagCallback(HtmlTagContext ctx, HtmlWriter htmlWriter)
            {
                if (ctx.TagId == HtmlTagId.Body && !ctx.IsEmptyElementTag)
                {
                    if (ctx.IsEndTag)
                    {
                        // end our opening <blockquote>
                        htmlWriter.WriteEndTag(HtmlTagId.BlockQuote);

                        // pass the </body> tag through to the output
                        ctx.WriteTag(htmlWriter, true);
                    }
                    else
                    {
                        // pass the <body> tag through to the output
                        ctx.WriteTag(htmlWriter, true);

                        // prepend the HTML reply with "On {DATE}, {SENDER} wrote:"
                        htmlWriter.WriteStartTag(HtmlTagId.P);
                        //htmlWriter.WriteText(GetOnDateSenderWrote(original));
                        htmlWriter.WriteEndTag(HtmlTagId.P);

                        // Wrap the original content in a <blockquote>
                        htmlWriter.WriteStartTag(HtmlTagId.BlockQuote);
                        htmlWriter.WriteAttribute(HtmlAttributeId.Style, "border-left: 1px #ccc solid; margin: 0 0 0 .8ex; padding-left: 1ex;");

                        ctx.InvokeCallbackForEndTag = true;
                    }
                }
                else
                {
                    // pass the tag through to the output
                    ctx.WriteTag(htmlWriter, true);
                }
            }

            string QuoteText(string text)
            {
                using (var quoted = new StringWriter())
                {
                    //quoted.WriteLine(GetOnDateSenderWrote(original));

                    using (var reader = new StringReader(text))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            quoted.Write("> ");
                            quoted.WriteLine(line);
                        }
                    }

                    return quoted.ToString();
                }
            }

            protected override void VisitTextPart(TextPart entity)
            {
                string text;

                if (entity.IsHtml)
                {
                    var converter = new HtmlToHtml
                    {
                        HtmlTagCallback = HtmlTagCallback
                    };

                    text = converter.Convert(entity.Text);
                }
                else if (entity.IsFlowed)
                {
                    var converter = new FlowedToText();

                    text = converter.Convert(entity.Text);
                    text = QuoteText(text);
                }
                else
                {
                    // quote the original message text
                    text = QuoteText(entity.Text);
                }

                var part = new TextPart(entity.ContentType.MediaSubtype.ToLowerInvariant())
                {
                    Text = text
                };

                Push(part);
            }

            protected override void VisitMessagePart(MessagePart entity)
            {
                // don't descend into message/rfc822 parts
            }
        }

        //ReplyFunction
        public static MimeMessage ReplyFunction(MimeMessage message, MailboxAddress from, bool replyToAll)
        {
            var visitor = new ReplyVisitor(from, replyToAll);
            visitor.Visit(message);
            return visitor.Reply;
        }

        public static MimeMessage ReplyFunction(MimeMessage message, bool replyToAll)
        {
            var reply = new MimeMessage();
            // reply to the sender of the message
            if (message.ReplyTo.Count > 0)
            {
                reply.To.AddRange(message.ReplyTo);
            }
            else if (message.From.Count > 0)
            {
                reply.To.AddRange(message.From);
            }
            else if (message.Sender != null)
            {
                reply.To.Add(message.Sender);
            }
            if (replyToAll)
            {
                // include all of the other original recipients - TODO: remove ourselves from these lists
                reply.To.AddRange(message.To);
                reply.Cc.AddRange(message.Cc);
            }
            // set the reply subject
            if (!message.Subject.StartsWith("Re:", StringComparison.OrdinalIgnoreCase))
                reply.Subject = "Re:" + message.Subject;
            else
                reply.Subject = message.Subject;
            // construct the In-Reply-To and References headers
            if (!string.IsNullOrEmpty(message.MessageId))
            {
                reply.InReplyTo = message.MessageId;
                foreach (var id in message.References)
                    reply.References.Add(id);
                reply.References.Add(message.MessageId);
            }
            // quote the original message text
            using (var quoted = new StringWriter())
            {
                var sender = message.Sender ?? message.From.Mailboxes.FirstOrDefault();

                //quoted.WriteLine("On {0}, {1} wrote:", message.Date.ToString("f"), !string.IsNullOrEmpty(sender.Name) ? sender.Name : sender.Address);
                using (var reader = new StringReader(message.TextBody))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        quoted.Write(">");
                        quoted.WriteLine(line);
                    }
                }

                reply.Body = new TextPart("plain")
                {
                    Text = quoted.ToString()
                };
            }

            return reply;
        }

        //Reply Mail
        [HttpGet("Reply")]
        public IActionResult Reply(int Index, bool ReplyAllFlag, string Foldername)
        {
            EmailDetails model = new EmailDetails();
            try
            {
                using (var client = new ImapClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    client.Connect(objSettings.IMAPHost, objSettings.IMAPPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.Authenticate(objSettings.UserName, objSettings.Password);
                    model.FolderName = Foldername;
                    var mail = client.Inbox;
                    mail.Open(FolderAccess.ReadWrite);
                    model.InboxCount = client.Inbox.Count;
                    if ((client.Capabilities & (ImapCapabilities.SpecialUse | ImapCapabilities.XList)) != 0)
                    {

                        if (model.FolderName == "Drafts")
                        {

                            mail = client.GetFolder(SpecialFolder.Drafts);
                        }
                        else if (model.FolderName == "Trash")
                        {
                            mail = client.GetFolder(SpecialFolder.Trash);
                        }
                        else if (model.FolderName == "Sent")
                        {
                            mail = client.GetFolder(SpecialFolder.Sent);
                        }
                        else if (model.FolderName == "Junk")
                        {
                            mail = client.GetFolder(SpecialFolder.Junk);
                        }
                        else if (model.FolderName == "All")
                        {
                            model.FolderName = "All Mail";
                            mail = client.GetFolder(SpecialFolder.All);
                        }
                        else if (model.FolderName == "Starred")
                        {
                            mail = client.GetFolder(SpecialFolder.Flagged);
                        }
                        mail.Open(FolderAccess.ReadWrite);
                    }
                    else
                    {
                        var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
                        if (model.FolderName == null || model.FolderName == "Inbox")
                        {
                        }
                        else if (model.FolderName == "Drafts")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Sent")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Starred")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Junk")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Trash")
                        {
                            if (objSettings.IMAPHost == "imap-mail.outlook.com")
                            {
                                mail = FindFolder(toplevel, "Deleted");
                            }
                            else
                            {
                                mail = FindFolder(toplevel, model.FolderName);
                            }
                        }
                        else if (model.FolderName == "spam")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Archive")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        mail.Open(FolderAccess.ReadWrite);
                    }
                    model.Message = mail.GetMessage(Index);
                    model.FromAddress = mail.GetMessage(Index).From.Mailboxes.FirstOrDefault();
                    var ReplyValues = ReplyFunction(model.Message, model.FromAddress, ReplyAllFlag);
                    model.Attachments = ReplyValues.Attachments;
                    model.Subject = ReplyValues.Subject;
                    model.To = ReplyValues.To;
                    model.TextBody = ReplyValues.TextBody;
                    model.HtmlBody = ReplyValues.HtmlBody;
                    model.Date = ReplyValues.Date;
                    model.CC = ReplyValues.Cc;
                    model.BCC = ReplyValues.Bcc;
                    if (model.To.Count == 0)
                    {
                        model.ToAddress = model.To.Mailboxes.FirstOrDefault().Address;
                        model.ToName = model.To.Mailboxes.FirstOrDefault().Name;
                    }
                    else
                    {
                        foreach (var mailbox in model.To.Mailboxes)
                        {
                            model.ToAddress = string.IsNullOrEmpty(model.ToAddress) ?
                                                !string.IsNullOrEmpty(mailbox.Name) ? "\"" + mailbox.Name + "\"" + "<" + mailbox.Address + ">" : mailbox.Address
                                                : !string.IsNullOrEmpty(mailbox.Name) ? model.ToAddress + "," + "\"" + mailbox.Name + "\"" + "<" + mailbox.Address + ">" : model.ToAddress + "," + mailbox.Address;

                        }
                    }
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }
            return View("~/Modules/Common/Mailbox/Reply.cshtml", model);
        }

        [HttpPost("Reply")]
        public IActionResult Reply(EmailDetails model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(objSettings.UserName, objSettings.UserName));
                    List<string> Receipent = model.ToAddress.Split(',').ToList();
                    InternetAddressList toMails = new InternetAddressList();
                    InternetAddressList CCMails = new InternetAddressList();
                    InternetAddressList BCCMails = new InternetAddressList();
                    for (int i = 0; i < Receipent.Count; i++)
                    {
                        MailboxAddress to = new MailboxAddress(Receipent.ElementAt(i), Receipent.ElementAt(i));
                        toMails.Add(to);
                    }
                    message.To.AddRange(toMails);
                    if (!string.IsNullOrEmpty(model.CCAddress))
                    {
                        List<string> CCReceipent = model.CCAddress.Split(',').ToList();

                        for (int i = 0; i < CCReceipent.Count; i++)
                        {
                            MailboxAddress CC = new MailboxAddress(CCReceipent.ElementAt(i), CCReceipent.ElementAt(i));
                            CCMails.Add(CC);
                        }
                    }
                    message.Cc.AddRange(CCMails);
                    if (!string.IsNullOrEmpty(model.BCCAddress))
                    {
                        List<string> BCCReceipent = model.BCCAddress.Split(',').ToList();
                        for (int i = 0; i < BCCReceipent.Count; i++)
                        {
                            MailboxAddress BCC = new MailboxAddress(BCCReceipent.ElementAt(i), BCCReceipent.ElementAt(i));
                            BCCMails.Add(BCC);
                        }
                    }
                    message.Bcc.AddRange(BCCMails);
                    message.Subject = model.Subject;
                    var builder = new BodyBuilder();
                    builder.TextBody = model.TextBody;
                    builder.HtmlBody = model.HtmlBody;
                    if (model.AttachedFiles != null)
                    {
                        foreach (var filePath in model.AttachedFiles.Split(','))
                        {
                            if (System.IO.File.Exists(filePath))
                            {
                                builder.Attachments.Add(filePath);
                            }
                        }
                    }
                    message.Body = builder.ToMessageBody();
                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.Connect(objSettings.SMTPHost, objSettings.SMTPPort, false);
                        client.Authenticate(objSettings.UserName, objSettings.Password);
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    if (model.AttachedFiles != null)
                    {
                        foreach (var filePath in model.AttachedFiles.Split(','))
                        {
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                    }
                    TempData["message"] = "Message sent successfully";
                }
            }
            catch (SmtpFailedRecipientException ex)
            {
                ex.GetBaseException();
                TempData["Errormessage"] = ex.GetBaseException().Message.ToString();
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }
            return RedirectToAction("Inbox", "Mailbox", new { flag = "Inbox", Search = "", page = 1, min = 0, max = 0, prepageNo = 1 });

        }

        //Reply Mail
        [HttpPost]
        public IActionResult ReplyOldmethod(EmailDetails model)
        {
            try
            {
                var message = new MailMessage();
                var m = new MailAddress(objSettings.UserName, objSettings.UserName);
                message.From = m;
                List<string> Receipent = model.ToAddress.Split(',').ToList();
                for (int i = 0; i < Receipent.Count; i++)
                {
                    message.To.Add(Receipent.ElementAt(i));
                }
                List<string> CCReceipent = model.CCAddress.Split(',').ToList();
                for (int i = 0; i < CCReceipent.Count; i++)
                {
                    message.CC.Add(CCReceipent.ElementAt(i));
                }
                List<string> BCCReceipent = model.BCCAddress.Split(',').ToList();
                for (int i = 0; i < BCCReceipent.Count; i++)
                {
                    message.Bcc.Add(BCCReceipent.ElementAt(i));
                }
                message.Subject = model.Subject;
                var builder = new BodyBuilder();
                // Set the plain-text version of the message text
                builder.TextBody = model.TextBody;
                if (ModelState.IsValid)
                {   //iterating through multiple file collection   
                    //Checking file is available to save.
                    if (model.AttachedFiles != null)
                    {
                        foreach (var filePath in model.AttachedFiles.Split(','))
                        {
                            if (System.IO.File.Exists(filePath))
                            {
                                message.Attachments.Add(new Attachment(filePath));
                            }
                        }
                    }
                }
                message.Body = model.TextBody;
                message.IsBodyHtml = true;
                NetworkCredential nc = new NetworkCredential(objSettings.UserName, objSettings.Password);
                using (var client = new System.Net.Mail.SmtpClient())
                {
                    client.Credentials = nc;
                    client.EnableSsl = true;
                    client.Host = objSettings.SMTPHost;
                    client.Port = objSettings.SMTPPort;
                    client.Timeout = 100000;
                    client.Send(message);
                    client.Dispose();
                    message.Dispose();
                }
                if (model.AttachedFiles != null)
                {
                    foreach (var filePath in model.AttachedFiles.Split(','))
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }
                TempData["message"] = "Message sent successfully";
            }
            catch (SmtpFailedRecipientException ex)
            {
                ex.GetBaseException();
                TempData["Errormessage"] = ex.GetBaseException().Message.ToString();
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }
            return RedirectToAction("Inbox", "Mailbox", new { flag = "Inbox", Search = "", page = 1, min = 0, max = 0, prepageNo = 1 });

        }

        //Forward Function      
        public static (MimeMessage, string) ForwardFunction(MimeMessage original, MailboxAddress from)
        {
            var message = new MimeMessage();
            message.From.Add(from);
            // message.To.AddRange(to);
            // set the forwarded subject
            if (!original.Subject.StartsWith("FW:", StringComparison.OrdinalIgnoreCase))
                message.Subject = "FW: " + original.Subject;
            else
                message.Subject = original.Subject;
            // quote the original message text
            dynamic tmp;
            using (var text = new StringWriter())
            {

                //text.WriteLine();
                text.WriteLine("-------- Forwarded Message --------");
                text.WriteLine("<br>");
                text.WriteLine("From: {0}", original.From);
                text.WriteLine("<br>");
                //String format = "dd MMM yyyy hh:mm tt \"pst\"";
                //var dat = new DateTime(2016, 8, 18, 16, 50, 0);
                text.WriteLine("Date: {0}", DateUtils.FormatDate(original.Date.UtcDateTime));
                text.WriteLine("<br>");
                text.WriteLine("Subject: {0}", original.Subject);
                text.WriteLine("<br>");
                text.WriteLine("To: {0}", original.To);
                text.WriteLine("<br>");
                text.Write(original.HtmlBody);
                //text.Write(original.Attachments);
                //model.mimeParts = mail.GetMessage(model.MessageIndex).BodyParts.OfType<MimePart>().Where(x => x.IsAttachment);
                //text.Write(original.BodyParts.OfType<MimePart>().Where(x => x.IsAttachment));
                tmp = text.ToString();
                //text.WriteLine();
                //text.Write(original.HtmlBody);
                //tmp = text.ToString();
            }
            return (message, tmp);
        }

        //Forward Mail
        [HttpGet("Forward")]
        public IActionResult Forward(int Index, string Foldername)
        {
            EmailDetails model = new EmailDetails();
            try
            {
                using (var client = new ImapClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    client.Connect(objSettings.IMAPHost, objSettings.IMAPPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.Authenticate(objSettings.UserName, objSettings.Password);
                    model.FolderName = Foldername;
                    var mail = client.Inbox;
                    mail.Open(FolderAccess.ReadWrite);
                    model.InboxCount = client.Inbox.Count;
                    if ((client.Capabilities & (ImapCapabilities.SpecialUse | ImapCapabilities.XList)) != 0)
                    {

                        if (model.FolderName == "Drafts")
                        {

                            mail = client.GetFolder(SpecialFolder.Drafts);
                        }
                        else if (model.FolderName == "Trash")
                        {
                            mail = client.GetFolder(SpecialFolder.Trash);
                        }
                        else if (model.FolderName == "Sent")
                        {
                            mail = client.GetFolder(SpecialFolder.Sent);
                        }
                        else if (model.FolderName == "Junk")
                        {
                            mail = client.GetFolder(SpecialFolder.Junk);
                        }
                        else if (model.FolderName == "All")
                        {
                            model.FolderName = "All Mail";
                            mail = client.GetFolder(SpecialFolder.All);
                        }
                        else if (model.FolderName == "Starred")
                        {
                            mail = client.GetFolder(SpecialFolder.Flagged);
                        }
                        mail.Open(FolderAccess.ReadWrite);
                    }
                    else
                    {
                        var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
                        if (model.FolderName == null || model.FolderName == "Inbox")
                        {
                        }
                        else if (model.FolderName == "Drafts")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Sent")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Starred")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                            //var foldermodel = client.GetFolder(foldername == "Starred" ? "INBOX" : foldername); foldermodel.Open(FolderAccess.ReadOnly); var uids = foldermodel.Search(foldername == "Starred" ? SearchQuery.Flagged : SearchQuery.All).OrderByDescending(m => m.Id).Take(10);
                        }
                        else if (model.FolderName == "Junk")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Trash")
                        {
                            if (objSettings.IMAPHost == "imap-mail.outlook.com")
                            {
                                mail = FindFolder(toplevel, "Deleted");
                            }
                            else
                            {
                                mail = FindFolder(toplevel, model.FolderName);
                            }
                        }
                        else if (model.FolderName == "spam")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Archive")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        mail.Open(FolderAccess.ReadWrite);
                    }
                    model.MessageIndex = Index;
                    model.Message = mail.GetMessage(model.MessageIndex);
                    model.FromAddress = mail.GetMessage(model.MessageIndex).From.Mailboxes.FirstOrDefault();
                    var ForwardValues = ForwardFunction(model.Message, model.FromAddress);
                    model.Subject = ForwardValues.Item1.Subject;
                    //model.HtmlBody = ForwardValues.Item2;
                    model.HtmlBody = mail.GetMessage(model.MessageIndex).HtmlBody;
                    model.CC = ForwardValues.Item1.Cc;
                    model.BCC = ForwardValues.Item1.Bcc;
                    model.Attachments = mail.GetMessage(model.MessageIndex).Attachments;
                    model.mimeParts = mail.GetMessage(model.MessageIndex).BodyParts.OfType<MimePart>().Where(x => x.IsAttachment);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }
            return View("~/Modules/Common/Mailbox/Forward.cshtml", model);
        }

        [HttpPost("Forward")]
        public IActionResult Forward(EmailDetails model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                //var message = new MailMessage();

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(objSettings.UserName, objSettings.UserName));
                List<string> Receipent = model.ToAddress.Split(',').ToList();
                InternetAddressList toMails = new InternetAddressList();
                InternetAddressList CCMails = new InternetAddressList();
                InternetAddressList BCCMails = new InternetAddressList();
                for (int i = 0; i < Receipent.Count; i++)
                {

                    MailboxAddress to = new MailboxAddress(Receipent.ElementAt(i), Receipent.ElementAt(i));
                    toMails.Add(to);
                    //message.To.Add(Receipent.ElementAt(i));
                    // message.To.AddRange(Receipent.ElementAt(i));
                }
                message.To.AddRange(toMails);
                if (!string.IsNullOrEmpty(model.CCAddress))
                {
                    List<string> CCReceipent = model.CCAddress.Split(',').ToList();

                    for (int i = 0; i < CCReceipent.Count; i++)
                    {
                        MailboxAddress CC = new MailboxAddress(CCReceipent.ElementAt(i), CCReceipent.ElementAt(i));
                        CCMails.Add(CC);
                    }
                }
                message.Cc.AddRange(CCMails);
                if (!string.IsNullOrEmpty(model.BCCAddress))
                {
                    List<string> BCCReceipent = model.BCCAddress.Split(',').ToList();
                    for (int i = 0; i < BCCReceipent.Count; i++)
                    {
                        MailboxAddress BCC = new MailboxAddress(BCCReceipent.ElementAt(i), BCCReceipent.ElementAt(i));
                        BCCMails.Add(BCC);
                    }
                }
                message.Bcc.AddRange(BCCMails);
                if (!model.Subject.StartsWith("FW:", StringComparison.OrdinalIgnoreCase))
                    message.Subject = "FW: " + model.Subject;
                else
                    message.Subject = model.Subject;


                //if (model.AttachedFiles != null)
                //{
                //    foreach (var filePath in model.AttachedFiles.Split(','))
                //    {
                //        if (System.IO.File.Exists(filePath))
                //        {
                //            message.Attachments.Add(new Attachment(filePath));
                //            //builder.Attachments.Add(new MessagePart { Message = messageToForward });
                //        }
                //    }
                //}
                //message.Attachments.Add(new Attachment(model.Attachments.FirstOrDefault().ContentDisposition.FileName));
                //builder.Attachments.Add(new MessagePart { Message = message.Attachments });
                //message.Body = builder.ToMessageBody();
                //message.Body = model.HtmlBody;                    
                //message.IsBodyHtml = true;
                using (var client = new ImapClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    client.Connect(objSettings.IMAPHost, objSettings.IMAPPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.Authenticate(objSettings.UserName, objSettings.Password);
                    var mail = client.Inbox;
                    mail.Open(FolderAccess.ReadWrite);
                    model.InboxCount = client.Inbox.Count;
                    if ((client.Capabilities & (ImapCapabilities.SpecialUse | ImapCapabilities.XList)) != 0)
                    {

                        if (model.FolderName == "Drafts")
                        {

                            mail = client.GetFolder(SpecialFolder.Drafts);
                        }
                        else if (model.FolderName == "Trash")
                        {
                            mail = client.GetFolder(SpecialFolder.Trash);
                        }
                        else if (model.FolderName == "Sent")
                        {
                            mail = client.GetFolder(SpecialFolder.Sent);
                        }
                        else if (model.FolderName == "Junk")
                        {
                            mail = client.GetFolder(SpecialFolder.Junk);
                        }
                        else if (model.FolderName == "All")
                        {
                            model.FolderName = "All Mail";
                            mail = client.GetFolder(SpecialFolder.All);
                        }
                        else if (model.FolderName == "Starred")
                        {
                            mail = client.GetFolder(SpecialFolder.Flagged);
                        }
                        mail.Open(FolderAccess.ReadWrite);
                    }
                    else
                    {
                        var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
                        if (model.FolderName == null || model.FolderName == "Inbox")
                        {
                        }
                        else if (model.FolderName == "Drafts")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Sent")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Starred")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                            //var foldermodel = client.GetFolder(foldername == "Starred" ? "INBOX" : foldername); foldermodel.Open(FolderAccess.ReadOnly); var uids = foldermodel.Search(foldername == "Starred" ? SearchQuery.Flagged : SearchQuery.All).OrderByDescending(m => m.Id).Take(10);
                        }
                        else if (model.FolderName == "Junk")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Trash")
                        {
                            if (objSettings.IMAPHost == "imap-mail.outlook.com")
                            {
                                mail = FindFolder(toplevel, "Deleted");
                            }
                            else
                            {
                                mail = FindFolder(toplevel, model.FolderName);
                            }
                        }
                        else if (model.FolderName == "spam")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        else if (model.FolderName == "Archive")
                        {
                            mail = FindFolder(toplevel, model.FolderName);
                        }
                        mail.Open(FolderAccess.ReadWrite);
                    }
                    model.Message = mail.GetMessage(model.MessageIndex);
                    //model.Attachments = client.Inbox.GetMessage(model.MessageIndex).Attachments;
                    //model.mimeParts = client.Inbox.GetMessage(model.MessageIndex).BodyParts.OfType<MimePart>().Where(x => x.IsAttachment);
                    client.Disconnect(true);
                }
                //var text = new TextPart("plain") { Text = model.TextBody };

                // create the message/rfc822 attachment for the original message
                var rfc822 = new MessagePart { Message = model.Message };

                // create a multipart/mixed container for the text body and the forwarded message
                var multipart = new Multipart("mixed");
                //multipart.Add(text);
                multipart.Add(rfc822);

                // set the multipart as the body of the message
                message.Body = multipart;
                Render(message);
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(objSettings.SMTPHost, objSettings.SMTPPort, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(objSettings.UserName, objSettings.Password);

                    client.Send(message);
                    client.Disconnect(true);
                }
                if (model.AttachedFiles != null)
                {
                    foreach (var filePath in model.AttachedFiles.Split(','))
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }
                //}
                TempData["message"] = "Message sent successfully";
            }
            catch (SmtpFailedRecipientException ex)
            {
                //ex.FailedRecipient;
                ex.GetBaseException();
                TempData["Errormessage"] = ex.GetBaseException().Message.ToString();
                //ex.FailedRecipient;
                //and ex.GetBaseException() should give you enough info.
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(SModel);
            }
            return RedirectToAction("Inbox", "Mailbox", new { flag = "Inbox", Search = "", page = 1, min = 0, max = 0, prepageNo = 1 });
        }

        //Forward Mail
        [HttpPost]
        public IActionResult Forward1(EmailDetails model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var message = new MailMessage();
                    var m = new MailAddress(objSettings.UserName, objSettings.UserName);
                    message.From = m;
                    List<string> Receipent = model.ToAddress.Split(',').ToList();
                    for (int i = 0; i < Receipent.Count; i++)
                    {
                        message.To.Add(Receipent.ElementAt(i));
                    }
                    if (!string.IsNullOrEmpty(model.CCAddress))
                    {
                        List<string> CCReceipent = model.CCAddress.Split(',').ToList();
                        for (int i = 0; i < CCReceipent.Count; i++)
                        {
                            message.CC.Add(CCReceipent.ElementAt(i));
                        }
                    }
                    if (!string.IsNullOrEmpty(model.BCCAddress))
                    {
                        List<string> BCCReceipent = model.BCCAddress.Split(',').ToList();
                        for (int i = 0; i < BCCReceipent.Count; i++)
                        {
                            message.Bcc.Add(BCCReceipent.ElementAt(i));
                        }
                    }
                    message.Subject = model.Subject;
                    // create our message text, just like before (except don't set it as the message.Body)
                    var builder = new BodyBuilder();
                    // Set the plain-text version of the message text
                    builder.TextBody = model.TextBody;
                    // now you can get and validate the file type:
                    //var isFileSupported = IsFileSupported(postedFile);
                    // Now we just need to set the message body and we're done   
                    //iterating through multiple file collection   
                    //Checking file is available to save.
                    if (model.AttachedFiles != null)
                    {
                        foreach (var filePath in model.AttachedFiles.Split(','))
                        {
                            if (System.IO.File.Exists(filePath))
                            {
                                message.Attachments.Add(new Attachment(filePath));
                            }
                        }
                    }
                    message.Body = model.TextBody;
                    message.IsBodyHtml = true;
                    NetworkCredential nc = new NetworkCredential(objSettings.UserName, objSettings.Password);
                    using (var client = new System.Net.Mail.SmtpClient())
                    {
                        client.Credentials = nc;
                        client.EnableSsl = true;
                        client.Host = objSettings.SMTPHost;
                        client.Port = objSettings.SMTPPort;
                        client.Timeout = 100000;
                        client.Send(message);
                        client.Dispose();
                        message.Dispose();
                    }
                    if (model.AttachedFiles != null)
                    {
                        foreach (var filePath in model.AttachedFiles.Split(','))
                        {
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                    }
                }
                TempData["message"] = "Message sent successfully";
            }
            catch (SmtpFailedRecipientException ex)
            {
                //ex.FailedRecipient;
                ex.GetBaseException();
                TempData["Errormessage"] = ex.GetBaseException().Message.ToString();
                //ex.FailedRecipient;
                //and ex.GetBaseException() should give you enough info.
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
                //return View(SModel);
            }
            return RedirectToAction("Inbox");
        }

        //static string[] CommonSentFolderNames = { "Sent Items", "Sent Mail", /* maybe add some translated names */ };
        static string[] CommonSentFolderNames = { "Sent Mail", "Junk E-Mail", "Drafts", "Sent Items", "Starred", "Trash", "All Mail" };

        static IFolder GetSentFolder(ImapClient client, CancellationToken cancellationToken)
        {
            var personal = client.GetFolder(client.PersonalNamespaces[0]);

            foreach (var folder in personal.GetSubfolders(false, cancellationToken))
            {
                foreach (var name in CommonSentFolderNames)
                {
                    if (folder.Name == "Sent Mail")
                        return (AdvanceCRM.Common.Mailbox.Classes.IFolder)folder;
                }
            }
            return null;
        }

        static IMailFolder FindFolder(IMailFolder toplevel, string name)
        {
            var subfolders = toplevel.GetSubfolders().ToList();

            foreach (var subfolder in subfolders)
            {
                if (subfolder.Name == name)
                    return subfolder;
            }

            foreach (var subfolder in subfolders)
            {
                var folder = FindFolder(subfolder, name);

                if (folder != null)
                    return folder;
            }

            return null;
        }

        //Save Mail as Draft
        [HttpPost]
        public IActionResult Draft(EmailDetails model)
        {
            try
            {
                var message = new MailMessage();
                var m = new MailAddress(objSettings.UserName, objSettings.UserName);
                message.From = m;
                List<string> Receipent = model.ToAddress.Split(',').ToList();
                for (int i = 0; i < Receipent.Count; i++)
                {
                    message.To.Add(Receipent.ElementAt(i));
                }
                message.Subject = model.Subject;
                // create our message text, just like before (except don't set it as the message.Body)
                var builder = new BodyBuilder();
                // Set the plain-text version of the message text
                builder.TextBody = model.TextBody;
                if (ModelState.IsValid)
                {
                    if (model.AttachedFiles != null)
                    {
                        foreach (var filePath in model.AttachedFiles.Split(','))
                        {
                            if (System.IO.File.Exists(filePath))
                            {
                                message.Attachments.Add(new Attachment(filePath));
                                //builder.Attachments.Add(filePath);
                            }
                        }
                    }
                }
                message.Body = model.TextBody;
                message.IsBodyHtml = true;
                var mailmessage = new MimeMessage();
                mailmessage = (MimeMessage)message;
                using (var client = new ImapClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    client.Connect(objSettings.IMAPHost, objSettings.IMAPPort, true);
                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    // MailKit uses by default ntlm authentication
                    client.Authenticate(objSettings.UserName, objSettings.Password);
                    var inbox = client.Inbox;
                    var draftFolder = client.GetFolder(SpecialFolder.Drafts);
                    if (draftFolder != null)
                    {
                        draftFolder.Open(FolderAccess.ReadWrite);
                        draftFolder.Append(mailmessage, MessageFlags.Draft);
                        draftFolder.Expunge();
                    }
                    else
                    {
                        var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
                        var DraftFolder = toplevel.Create(SpecialFolder.Drafts.ToString(), true);
                        DraftFolder.Open(FolderAccess.ReadWrite);
                        DraftFolder.Append(mailmessage, MessageFlags.Draft);
                        DraftFolder.Expunge();
                    }
                    client.Disconnect(true);
                    client.Dispose();
                    message.Dispose();
                }
                if (model.AttachedFiles != null)
                {
                    foreach (var filePath in model.AttachedFiles.Split(','))
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message;
            }
            return RedirectToAction("Inbox", "Mailbox", new { flag = "Inbox", Search = "", page = 1, min = 0, max = 0, prepageNo = 1 });
        }

        private string GetVirtualPath(string physicalPath)
        {
            string rootpath = _env.ContentRootPath + Path.DirectorySeparatorChar;
            physicalPath = physicalPath.Replace(rootpath, string.Empty);
            physicalPath = physicalPath.Replace("\\", "/");
            return "~/" + physicalPath;
        }

        //Download and Save Attachment
        public IActionResult SaveAttachment(int Index, string file)
        {
            try
            {
                using (var client = new ImapClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                    client.Connect(objSettings.IMAPHost, objSettings.IMAPPort, true);
                    //client.Connect("mail.bizpluscrm.com", 993, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    //client.Authenticate("support@bizpluscrm.com", "L@ndmark@2020");
                    client.Authenticate(objSettings.UserName, objSettings.Password);
                    // The Inbox folder is always available...               
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadWrite);
                    //var message = inbox.Fetch(Index, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure);
                    var message = inbox.GetMessage(Index);
                    foreach (var attachment in message.Attachments)
                    {
                        var part = attachment as MimePart;
                        if (part.FileName == file)
                        {
                            var stream = new MemoryStream();
                            if (attachment is MessagePart)
                            {
                                var rfc822 = (MessagePart)attachment;
                                rfc822.Message.WriteTo(stream);
                                rfc822.Message.WriteTo(Path.Combine(_env.ContentRootPath, file));
                            }
                            else
                            {
                                part = (MimePart)attachment;
                                part.Content.WriteTo(stream);
                            }

                            FileStream fs = new FileStream("temp", FileMode.CreateNew, FileAccess.ReadWrite);

                            stream.Position = 0;


                            return File(stream.ToArray(), "application/octet-stream", file);
                        }

                    }
                    client.Disconnect(true);
                }
                return null;
            }


            catch (Exception ex)
            {
                //TempData["Errormessage"] = ex.Message;
                return null;
            }
            //return Json(new { status = true, message = "Downloaded Successfully" }, JsonRequestBehavior.AllowGet);
        }


    }
}