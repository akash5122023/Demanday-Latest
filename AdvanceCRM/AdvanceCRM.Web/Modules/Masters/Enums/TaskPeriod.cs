using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.TaskPeriod")]
    public enum TaskPeriodMaster
    {
        [Description("Monthly")]
        Monthly = 1,
        [Description("Quaterly")]
        Quaterly = 2,
        [Description("Half-Year")]
        HalfYear = 3,
        [Description("Yearly")]
        Yearly = 4
       

    }
}