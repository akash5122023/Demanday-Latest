using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class IndiaMartModel
    {
        public Int32 Id { get; set; }
        public Int32 Rn { get; set; }
        public String SenderName { get; set; }
        public String SenderEmail { get; set; }
        public Int32 Source { get; set; }
        public String Subject { get; set; }      
        public Boolean IsMoved { get; set; }  
        public DateTime DateTimeRe { get; set; }
        public String GlUserCompanyName { get; set; }
        public String Mob { get; set; }
        public String CountryFlag { get; set; }
        public String EnqMessage { get; set; }
        public String EnqAddress { get; set; }
        public String EnqCallDuration { get; set; }
        public String EnqReceiverMob { get; set; }
        public String EnqCity { get; set; }
        public String EnqState { get; set; }
        public String EmailAlt { get; set; }
        public String MobileAlt { get; set; }
        public String Phone { get; set; }
        public String PhoneAlt { get; set; }
        public String Feedback { get; set; }

       
        public String QueryId { get; set; }
        public String QueryType { get; set; }
        public int ReadStatus { get; set; }
        public String SenderGlUserId { get; set; }
        public String QueryModId { get; set; }
        public String LogTime { get; set; }
        public String QueryModRefId { get; set; }
        public int DirQueryModrefType { get; set; }
        public String OrgSenderGlUserId { get; set; }

        public String ProductName { get; set; }
        public String CountryIso { get; set; }

        public String ImmemberSince { get; set; }
        public int TotalCnt { get; set; }

    }
}