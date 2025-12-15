
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.TaskStatus")]
    [BasedOnRow(typeof(TaskStatusRow), CheckNames = true)]
    public class TaskStatusForm
    {
        public String Status { get; set; }
    }
}