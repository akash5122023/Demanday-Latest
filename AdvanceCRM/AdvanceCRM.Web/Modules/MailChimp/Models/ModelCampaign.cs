using System;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Modules.MailChimp.Models
{
    public class ModelCampaign
    {
        public string ArchiveUrl { get; set; }
        public string ContentType { get; set; }
        public DateTime CreateTime { get; set; }
        public string DashboardLink { get; }
        //public ModelDeliveryStatus DeliveryStatus { get; set; }           
        public bool CanCancel { get; set; }
        public int EmailsCanceled { get; set; }
        public int EmailsSent { get; set; }
        public bool Enabled { get; set; }
        public string DeliveryStatus { get; set; }
        public int? DeliveryEmailsSent { get; set; }
        public string Id { get; set; }
        //public ModelLink[] Links { get; set; }
        public string Href { get; set; }
        //public Method Method { get; set; }       
        public string Rel { get; set; }
        public string Schema { get; set; }
        public string TargetSchema { get; set; }
        //public ModelRecipient Recipients{ get; set; }
        //public SegmentOptions SegmentOptions { get; set; }       
        public string SegmentText { get; set; }
        public string ListId { get; set; }
        public string ListName { get; set; }
        //public ModelReportSummary ReportSummary { get; set; }  
        public double ClickRate { get; set; }
        public int Clicks { get; set; }
        //public Ecommerce Ecommerce { get; set; }      
        public double OpenRate { get; set; }
        public int EcommerceOpens { get; set; }
        public int SubscriberClicks { get; set; }
        public int UniqueOpens { get; set; }
        // public RssOptions RssOptions { get; set; }            
        public DateTime? SendTime { get; set; }
        //public ModelSetting Settings { get; set; }  
        public bool Authenticate { get; set; }
        public bool AutoFooter { get; set; }
        public bool AutoTweet { get; set; }
        public bool DragAndDrop { get; set; }
        public bool FbComments { get; set; }
        public string FolderId { get; set; }
        public string FromName { get; set; }
        public bool InlineCss { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string ReplyTo { get; set; }
        public string SubjectLine { get; set; }
        public int TemplateId { get; set; }
        public bool Timewarp { get; set; }
        public string Title { get; set; }
        public string ToName { get; set; }
        public bool UseConversation { get; set; }
        //public ModelSocialCard SocialCard { get; set; } 
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string SocialTitle { get; set; }
        public string SocialStatus { get; set; }
        //public ModelTracking Tracking { get; set; }
        //public Capsule Capsule { get; set; }        
        public string Clicktale { get; set; }
        public bool Ecomm360 { get; set; }
        public bool GoalTracking { get; set; }
        public string GoogleAnalytics { get; set; }
        //public HighRise HighRise { get; set; }        
        public bool HtmlClicks { get; set; }
        public bool Opens { get; set; }
        //public SalesForce SalesForce { get; set; }        
        public bool TextClicks { get; set; }
        //[JsonConverter(typeof(StringEnumDescriptionConverter))]           
        //public ModelCampaignType Type { get; set; }
    }
    //[Flags]
    //public enum ModelCampaignType
    //{
    //    Regular = 1,
    //    Plaintext = 2,
    //    Absplit = 4,
    //    Rss = 8,
    //    Variate = 16
    //}


}



