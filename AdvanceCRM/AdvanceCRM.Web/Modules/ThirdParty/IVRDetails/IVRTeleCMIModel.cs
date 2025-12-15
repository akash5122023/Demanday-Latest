using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.ThirdParty.IVRDetails
{
    public class IVRTeleCMIModel
    {
        public int Page { get; set; }
        public int Type { get; set; }
        public IPagedList<IVRTeleCMIList> list;
        public DateTime StartDate;
        public DateTime EndDate;
        public List<string> IVRNumbers;
    }

    [Serializable]
    public class IVRTeleCMIList
    {
        public String CustomerName { get; set; }
        public String CustomerNumber { get; set; }
        public String EmployeeNumber { get; set; }
        public String Duration { get; set; }
        public String Recording { get; set; }
        public String DateTime { get; set; }
    }
}