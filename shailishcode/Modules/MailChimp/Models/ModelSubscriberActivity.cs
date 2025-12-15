using System.Collections.Generic;

namespace AdvanceCRM.Modules.MailChimp.Models
{
    public class ModelSubscriberActivity
    {
        public List<Activity1> Activities { get; set; }


    }
    public class Activity1
    {
        public int Action { get; set; }
        public int Timestamp { get; set; }
    }
}