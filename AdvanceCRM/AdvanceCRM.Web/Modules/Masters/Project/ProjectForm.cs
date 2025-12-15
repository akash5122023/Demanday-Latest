
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Project")]
    [BasedOnRow(typeof(ProjectRow), CheckNames = true)]
    public class ProjectForm
    {
        public String Project { get; set; }
    }
}