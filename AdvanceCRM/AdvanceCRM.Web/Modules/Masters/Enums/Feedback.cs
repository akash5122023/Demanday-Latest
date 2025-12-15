using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.Feedback")]
    public enum FeedbackMaster
    {
        //TODO: For bank to bank deposit add another module and add one deopsit and expense entry in cashbook module
        [Description("Best")]
        Best = 1,
        [Description("Better")]
        Better = 2,
        [Description("Good")]
        Good = 3,
        [Description("Bad")]
        Bad = 4,
        [Description("VeryBad")]
        VeryBad = 5
             
    }
}