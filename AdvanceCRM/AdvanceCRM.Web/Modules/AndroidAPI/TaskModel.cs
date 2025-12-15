using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class TaskModel
    {
        public int id { get; set; }
       
       
        public string Task { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpectedCompletion { get; set; }
        public DateTime Completion { get; set; }

        public string assign { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public int Priority { get; set; }
        public string Contacts { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }

        public int ProjectId { get; set; }
        public string Project { get; set; }
    }
}