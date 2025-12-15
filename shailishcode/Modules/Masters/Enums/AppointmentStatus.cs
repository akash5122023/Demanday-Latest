using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.AppointmentStatus")] //This is only used in Appointment Module and not in Telecalling
    public enum AppointmentStatusMaster
    {
        [Description("Open")]
        Open = 1,
        [Description("Done")]
        Done = 2,
        [Description("Cancelled")]
        Cancelled = 3
    }
}