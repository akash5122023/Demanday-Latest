using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.CType")]
    public enum CTypeMaster
    {
        [Description("Individual")]
        Individual = 1,
        [Description("Organization")]
        Organization = 2
    }
}