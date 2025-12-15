using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.Type")]
    public enum TypeMaster
    {
        [Description("Domestic")]
        Domestic = 1,
        [Description("International")]
        International = 2
    }
}