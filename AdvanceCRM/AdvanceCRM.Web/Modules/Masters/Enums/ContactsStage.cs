using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.ContactsStage")]
    public enum ContactsStage
    {
        [Description("Enquiry")]
        Enquiry = 1,
        [Description("Quotation")]
        Quotation = 2,
        [Description("Sales")]
        Sales = 3,
        [Description("Purchase")]
        Purchase = 4,
        [Description("CMS")]
        CMS = 5
    }
}