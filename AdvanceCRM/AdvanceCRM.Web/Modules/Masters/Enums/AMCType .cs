using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.AMCType")]
    public enum AMCTypeMaster
    {
        [Description("Quantity")]
        Quantity = 1,
        [Description("Visit")]
        Visit = 2,
        [Description("Qty & Visit")]
        QtyAndVisit = 3,
    }
}