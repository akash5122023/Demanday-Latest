using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.Religion")]
    public enum ReligionMaster
    {
        [Description("Hindu")]
        Hindu = 1,
        [Description("Muslim")]
        Muslim = 2,
        [Description("Christian")]
        Christian = 3,
        [Description("Sikh")]
        Sikh = 4,
        [Description("Buddha")]
        Buddha = 5,
        [Description("Other")]
        Other = 6
    }
}