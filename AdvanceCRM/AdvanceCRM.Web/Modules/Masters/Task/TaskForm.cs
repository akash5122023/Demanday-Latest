
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Task")]
    [BasedOnRow(typeof(TaskRow), CheckNames = true)]
    public class TaskForm
    {
        public String Task { get; set; }
    }
}