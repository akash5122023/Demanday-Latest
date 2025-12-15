
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.TaskType")]
    [BasedOnRow(typeof(TaskTypeRow), CheckNames = true)]
    public class TaskTypeForm
    {
        public String Type { get; set; }
    }
}