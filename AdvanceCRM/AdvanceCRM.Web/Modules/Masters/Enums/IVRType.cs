
using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.IVRType")]
    public enum IVRTypeMaster
    {
        [Description("Knowlarity")]
        Knowlarity = 1,
        [Description("Tele CMI")]
        TeleCMI = 2,
        [Description("way2voice")]
        way2voice = 3,
        [Description("Cloud Connect")]
        Cloud_Connect = 4,

    }
}