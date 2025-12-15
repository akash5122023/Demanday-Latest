using System;

namespace AdvanceCRM.Modules.MailChimp.Models
{
    public class ModelReport
    {
        public ModelReport()
        { }
        public int AbuseReports { get; set; }
        //public Bounce1 Bounces { get; set; } 
        public int HardBounces { get; set; }
        public int SoftBounces { get; set; }
        public int SyntaxErrors { get; set; }

        public string CampaignTitle { get; set; }
        // public Click1 Clicks { get; set; } 
        public double ClickRate { get; set; }
        public int ClicksTotal { get; set; }
        public DateTime? LastClick { get; set; }
        public int UniqueClicks { get; set; }
        public int UniqueSubscriberClicks { get; set; }

        // public DeliveryStatus1 DeliveryStatus { get; set; }
        public bool CanCancel { get; set; }
        public int EmailsCanceled { get; set; }
        public int DeliveryEmailsSent { get; set; }
        public bool Enabled { get; set; }
        public string Status { get; set; }

        public string ViewEmail { get; set; }
        public int EmailsSent { get; set; }
        //public FacebookLikes1 FacebookLikes { get; set; }
        public int FacebookLikeCount { get; set; }
        public int RecipientLikes { get; set; }
        public int UniqueLikes { get; set; }

        //public Forwards1 Forwards { get; set; }  
        public int ForwardsCount { get; set; }
        public int ForwardsOpens { get; set; }

        public string Id { get; set; }
        //public IndustryStats1 IndustryStats { get; set; }   

        // public IEnumerable<Link1> Links { get; set; } 
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Schema { get; set; }
        public string TargetSchema { get; set; }

        // public ListStats1 ListStats { get; set; } 
        public double ListStatsClickRate { get; set; }
        public double ListStatsOpenRate { get; set; }
        public double ListStatsSubRate { get; set; }
        public double ListStatsUnsubRate { get; set; }

        //public Opens1 Opens { get; set; }  
        public DateTime? LastOpen { get; set; }
        public double OpenRate { get; set; }
        public int OpensTotal { get; set; }
        public int UniqueOpens { get; set; }

        public string SendTime { get; set; }
        //public ShareReport1 ShareReport { get; set; }  
        public string SharePassword { get; set; }
        public string ShareUrl { get; set; }

        public string SubjectLine { get; set; }
        // public IEnumerable<Timesery1> Timeseries { get; set; }    

        public int Timesery1EmailsSent { get; set; }
        public int RecipientsClicks { get; set; }
        public string Timestamp { get; set; }
        public int Timesery1UniqueOpens { get; set; }

        public CampaignType1 Type { get; set; }
        public int Unsubscribed { get; set; }
        public string ListName { get; set; }
        public int MemberCount { get; set; }
    }


    public class IndustryStats1
    {
        public IndustryStats1() { }
        public double AbuseRate { get; set; }
        public double BounceRate { get; set; }
        public double ClickRate { get; set; }
        public double OpenRate { get; set; }
        public string Result { get; set; }
        public double UnopenRate { get; set; }
        public double UnsubRate { get; set; }
    }
    [Flags]
    public enum CampaignType1
    {
        Regular = 1,
        Plaintext = 2,
        Absplit = 4,
        Rss = 8,
        Variate = 16
    }
}