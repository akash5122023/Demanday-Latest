using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.ModulesType")]
    public enum ModulesTypeMaster
    {
        [Description("Enquiry")]
        Enquiry = 1,
        [Description("Quotation")]
        Quotation = 2,
        [Description("Proforma")]
        Proforma = 3,
        [Description("Sales")]
        Sales = 4,
        [Description("Contacts")]
        Contacts = 5,
        [Description("CMS")]
        CMS = 6,
        [Description("AMC")]
        AMC = 7
    }
}