using MailKit.Net.Imap;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;



namespace AdvanceCRM.Common.Mailbox.Classes
{
    public class EmailDetails
    {
        public MimeKit.InternetAddressList To { get; set; }
        //public string Receipent { get; set; }
        public string Subject { get; set; }
        public string ToName { get; set; }
        [Required(ErrorMessage = "Please Enter Receipent")]
        public string ToAddress { get; set; }
        public string FromName { get; set; }
        public string CCAddress { get; set; }
        public string BCCAddress { get; set; }
        //public string FromAddress { get; set; }
        public MimeKit.MimeEntity Body { get; set; }
        public System.Collections.Generic.List<MimeKit.MimeEntity> Bodyparts { get; set; }
        public MimeKit.MailboxAddress FromAddress { get; set; }
        public MimeKit.MailboxAddress ToAddress1 { get; set; }
        public MimeKit.InternetAddressList From { get; set; }
        public MimeKit.InternetAddressList CC { get; set; }
        public MimeKit.InternetAddressList BCC { get; set; }
        public IEnumerable<MimeEntity> Attachments { get; set; }
        public IEnumerable<MimePart> mimeParts { get; set; }
        public DateTimeOffset Date { get; set; }
        [AllowHtml]
        public string HtmlBody { get; set; }
        public string TextBody { get; set; }
        public int InboxCount { get; set; }
        public int UnseenCount { get; set; }
        public string Filename { get; set; }
        public long FileSize { get; set; }
        public Uri FileLocation { get; set; }
        public MimeMessage Message { get; set; }
        public int MessageIndex { get; set; }
        public string File { get; set; }
        public int UniqueId { get; set; }
        public string FilesToBeUploaded { get; set; }
        public string AttachedFiles { get; set; }
        public IList<MailKit.UniqueId> UId { get; set; }
        public string FolderName { get; set; }


        static string[] CommonSentFolderNames = { "Sent Items", "Sent Mail", /* maybe add some translated names */ };

        static IFolder GetSentFolder(ImapClient client, CancellationToken cancellationToken)
        {
            var personal = client.GetFolder(client.PersonalNamespaces[0]);

            foreach (var folder in personal.GetSubfolders(false, cancellationToken))
            {
                foreach (var name in CommonSentFolderNames)
                {
                    if (folder.Name == "Sent")
                        return (AdvanceCRM.Common.Mailbox.Classes.IFolder)folder;
                }
            }

            return null;
        }


    }

    internal interface IFolder
    {
    }
}