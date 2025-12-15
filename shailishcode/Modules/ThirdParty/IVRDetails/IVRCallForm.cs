using AdvanceCRM.Settings;
using AdvanceCRM.Settings;
using AdvanceCRM.ThirdParty;
using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvanceCRM.ThirdParty.Forms
{
    [FormScript("ThirdParty.IVRCall")]
    public class IVRCallForm
    {
        [DisplayName("IVR Number"), Required]
        [IVRNumberEditor]
        public Int32 IVRNumber { get; set; }
        [DisplayName("Agent Number"), Required]
        [LookupEditor(typeof(KnowlarityAgentsRow))]
        public Int32 AgentNumber { get; set; }
        [DisplayName("Customer Number (+91)"), Required]
        public String CustomerNumber { get; set; }

        //[DisplayName("Call ID (+91)")]
        //public String CallID { get; set; }
    }
}