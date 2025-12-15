using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.AppointmentType")]
    public enum AppointmentTypeMaster
    {
        [Description("Open")]
        Open = 1,
        [Description("Appointment")]
        Appointment = 2,
        [Description("Not Interested")]
        NotInterested = 3,
        [Description("Interested")]
        Interested = 4,
        [Description("Pending")]
        Pending = 5
    }
}