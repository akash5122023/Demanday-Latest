using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.CompanyType")]
    public enum CompanyTypeMaster
    {
        [Description("Pneumatics")]
        Pneumatics = 1,
        [Description("Finance")]
        Finance = 2,
    }
}