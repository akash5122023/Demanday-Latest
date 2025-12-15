using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class ContactModel
    {
        public int id { get; set; }
        public int ContactType { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string MailId { get; set; } 
        public string Address { get; set; }

       
    }
}