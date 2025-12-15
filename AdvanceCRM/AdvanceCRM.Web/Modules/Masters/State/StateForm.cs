
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.State")]
    [BasedOnRow(typeof(StateRow), CheckNames = true)]
    public class StateForm
    {
        public String State { get; set; }
    }
}