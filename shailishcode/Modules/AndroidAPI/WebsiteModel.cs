using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class WebsiteModel
    {
        public int Id { get; set; }        
        public String Name { get; set; }        
        public DateTime DateTime { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }     
        public String Address { get; set; }     
        public String Requirement { get; set; }        
        public String Feedback { get; set; }   
        public Boolean IsMoved { get; set; }
    }
}