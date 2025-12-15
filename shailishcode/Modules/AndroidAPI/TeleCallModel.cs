using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class TeleCallModel
    {
        public int id { get; set; }


        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Details { get; set; }        
        public int CreatedBy { get; set; }
        public int AssignedTo { get; set; }
        public string Feedback { get; set; }
        public string created { get; set; }
        public string asssign { get; set; }
        public bool IsMoved { get; set; }
    }
}