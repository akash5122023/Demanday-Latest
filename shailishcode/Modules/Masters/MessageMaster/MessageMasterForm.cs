
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.MessageMaster")]
    [BasedOnRow(typeof(MessageMasterRow), CheckNames = true)]
    public class MessageMasterForm
    {
        public String Message { get; set; }
    }
}