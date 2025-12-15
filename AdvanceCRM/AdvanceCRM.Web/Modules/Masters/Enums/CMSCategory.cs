using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.CMSCategory")]
    public enum CMSCategoryMaster
    {
        [Description("Chargeable")]
        Chargeable = 1,
        [Description("Non Chargeable")]
        NonChargeable = 2,
    }
}