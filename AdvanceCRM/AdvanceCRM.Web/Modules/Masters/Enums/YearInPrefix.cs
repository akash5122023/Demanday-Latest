using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.YearInPrefix")]
    public enum YearInPrefix
    {
        [Description("None")]
        None = 1,
        [Description("Year")]
        Year = 2,
        [Description("Finacial Year")]
        FinacialYear = 3
    }
}