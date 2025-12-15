using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.NotificationModules")]
    public enum NotificationModules
    {
        [Description("Contacts")]
        Contacts = 1,
        [Description("Enquiry")]
        Enquiry,
        [Description("Quotation")]
        Quotation,
        [Description("Proforma")]
        Proforma,
        [Description("Sales")]
        Sales,
        [Description("Challan")]
        Challan,
        [Description("Sales Return")]
        SalesReturn,
        [Description("Purchase")]
        Purchase,
        [Description("Purchase Return")]
        PurchaseReturn,
        [Description("Purchase Order")]
        PurchaseOrder,
        [Description("AMC")]
        AMC,
        [Description("CMS")]
        CMS,
        [Description("TeleCalling")]
        TeleCalling,
        [Description("Tasks")]
        Tasks
    }
}