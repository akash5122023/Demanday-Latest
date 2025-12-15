using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.BizMailRules")]
    public enum BizMailRulesMaster
    {
        [Description("Contact")]
        Contact = 1,
        [Description("CMS")]
        CMS = 2,
        [Description("Facebook")]
        Facebook = 3,
        [Description("IndiaMart")]
        IndiaMart = 4,
        [Description("InstaMojo")]
        InstaMojo = 5,
        [Description("IVR")]
        IVR = 6,
        [Description("JustDial")]
        JustDial = 7,
        [Description("TradeIndia")]
        TradeIndia = 8,
        [Description("Visit")]
        Visit = 9,
        [Description("WebEnquiry")]
        WebEnquiry = 10,
        [Description("WooCommerce")]
        WooCommerce = 11


    }
}