using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;


namespace AdvanceCRM.Common.Mailbox.Classes
{
    public class ReceiveMail
    {

        public int InboxCount { get; set; }
        public int UnseenCount { get; set; }
        //public List<dynamic> MailList { get; set; }  
        public List<MailKit.IMessageSummary> MailList { get; set; }
        //public IPagedList<MailKit.MessageSummary> MailList { get; set; }
        public bool IsAttachment { get; set; }
        public string Search { get; set; }
        public string Flag { get; set; }
        public int? Page { get; set; }
        public string ServerName { get; set; }
        public int RecentCount { get; set; }
        public int DraftCount { get; set; }
        public int FlaggedCount { get; set; }
        public string TitleName { get; set; }
        public int? PageNo { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public int LastPage { get; set; }
        public int PrePageNo { get; set; }
        //public List<ReceiveMail> ListMail{get; set;}
    }
}