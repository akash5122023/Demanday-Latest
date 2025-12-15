using AdvanceCRM.ThirdParty;
using System.Collections.Generic;

namespace AdvanceCRM.Common
{
    public class IVRDashboardPageModel
    {

        public int MissCall { get; set; }
        public int AnswerCall { get; set; }
        public int TotalCall { get; set; }

        //public int WelcomeSound { get; set; }
        public int CustomerMissed { get; set; }
        public int AgentMissed { get; set; }
        //public int UserDisconnected { get; set; }

        //public int GrandTotalMonthlyCalls { get; set; }
        public List<KnowlarityDetailsRow> AnsweredCalls { get; set; }
        //public List<KnowlarityDetailsRow> Missedcalls { get; set; }
        //public List<KnowlarityDetailsRow> CustomerMissed { get; set; }
        //public List<KnowlarityDetailsRow> AgentMissed { get; set; }
        public List<KnowlarityDetailsRow> TodaysCalls { get; set; }
        public List<KnowlarityDetailsRow> TotalMonthlyCalls { get; set; }
        //public List<KnowlarityDetailsRow> IVRListChart { get; set; }



    }
}