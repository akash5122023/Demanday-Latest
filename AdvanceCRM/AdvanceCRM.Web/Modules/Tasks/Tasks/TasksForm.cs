
namespace AdvanceCRM.Tasks.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Tasks.Tasks")]
    [BasedOnRow(typeof(TasksRow), CheckNames = true)]
    public class TasksForm
    {
        [Category("Recurring")]
        [HalfWidth]
      
        public Boolean Recurring { get; set; }
        [HalfWidth]
        public Int32 Period { get; set; }
        [Category("Task Information")]
        [FullWidth]
        public Int32 ProjectId { get; set; }
        public String TaskTitle { get; set; }

        [FullWidth]
        [DefaultValue("now")]
        public Int32 TaskId { get; set; }

        [OneThirdWidth]
        [DefaultValue("1")]
        public Int32 StatusId { get; set; }
        [OneThirdWidth,DefaultValue("1")]
        public Int32 TypeId { get; set; }
        [OneThirdWidth]
        public Masters.PriorityMaster? Priority { get; set; }
        [OneThirdWidth]
        public Int32 ContactsId { get; set; }
        [OneThirdWidth]
        public Int32 ProductId { get; set; }
       
       
        [DefaultValue("now"), ReadOnly(true), FormCssClass("line-break-sm")]
        [OneThirdWidth, DateTimeEditor]
        public DateTime CreationDate { get; set; }
        [OneThirdWidth, DateTimeEditor]
        public DateTime ExpectedCompletion { get; set; }
        [ReadOnly(true)]
        [OneThirdWidth, DateTimeEditor]
        public DateTime CompletionDate { get; set; }
        [Category("Description")]
        public String Details { get; set; }
        public String Attachments { get; set; }
        [Category("Resolution")]
        public String Resolution { get; set; }
        [Category("Representatives")]
        [OneThirdWidth]
        public Int32 AssignedBy { get; set; }
        [OneThirdWidth]
        public Int32 AssignedTo { get; set; }
        [OneThirdWidth]
        public List<Int32> WatcherList { get; set; }
        public List<object> NoteList { get; set; }
        public List<object> Timeline { get; set; }
    }
}