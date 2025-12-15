
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Teams")]
    [BasedOnRow(typeof(TeamsRow), CheckNames = true)]
    public class TeamsForm
    {
        public String Team { get; set; }
        public Int32 UserId { get; set; }
    }
}