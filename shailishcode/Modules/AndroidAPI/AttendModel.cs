using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class AttendModel
    {
        public int Id { get; set; }

        public int Name { get; set; }
        public DateTime DateNTime { get; set; }
        public int Type { get; set; }
        public string Location { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime PunchIn { get; set; }
        public DateTime PunchOut { get; set; }
        public double Distance { get; set; }
    }
}