using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.SendMail")]
    public enum SendMailMaster
    {
        [Description("Don't send Email")]
        DontSendMail = 1,
        [Description("Common Email")]
        CommonMail = 2
    }
}