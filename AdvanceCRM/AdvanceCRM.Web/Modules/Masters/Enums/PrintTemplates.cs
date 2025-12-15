using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.PrintTemplates")]
    public enum PrintTemplates
    {
        [Description("Default Template")]
        Default = 1,
        [Description("Template with Images")]
        Template1 = 2,
        [Description("Template with Column Images")]
        Template2 = 3
    }
}