using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.CampaignType")]
    public enum CampaignTypeMaster
    {
        [Description("Regular")]
        Regular = 1,
        [Description("Autoresponder")]
        Autoresponder = 2
    }
}