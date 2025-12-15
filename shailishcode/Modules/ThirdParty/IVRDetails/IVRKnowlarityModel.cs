using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.ThirdParty.IVRDetails
{
    public class IVRKnowlarityModel
    {
        public int Page { get; set; }
        public int Type { get; set; }
        public IPagedList<dynamic>  list;
        public DateTime StartDate;
        public DateTime EndDate;
        public List<string> IVRNumbers;
    }

    [Serializable]
    public class IVRKnowlarityList
    {
        public String CustomerNumber { get; set; }
        public String EmployeeNumber { get; set; }
        public String Duration { get; set; }
        public String Recording { get; set; }
        public String IVRNumber { get; set; }
        public String DateTime { get; set; }
    }
}