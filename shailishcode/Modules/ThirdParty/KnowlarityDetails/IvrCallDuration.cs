//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace AdvanceCRM.Modules.ThirdParty.KnowlarityDetails
//{
//    public class IvrCallDuration
//    {
//    }
//}

namespace AdvanceCRM.Modules.ThirdParty.KnowlarityDetails
{
    using Serenity.ComponentModel;
    using System.ComponentModel;

  
//    [EnumKey("Default.CallDurationState")]
    //[EnumKey("ThirdParty.CallDurationState")]
    [EnumKey("ThirdParty.CallDurationState")]
    public enum IvrCallDuration
    {
        [Description("Missed Calls")]
        MissedCalls = 0,
        [Description("Answer  Calls")]
        AnsweredCalls = 1,
        //[Description("Welcome Sound")]
        //WelcomeSound = 2,
        //[Description("User Disconnected")]
        //UserDisconnected = 3,
        [Description("Customer Missed")]
        CustomerMissed = 2,
        [Description("Agent Missed")]
        AgentMissed = 3,
        [Description("Todays Calls")]
        TodaysCalls = 4,
        //[Description("Total Monthly Calls")]
        //TotalMonthlyCalls = 5

    }
}