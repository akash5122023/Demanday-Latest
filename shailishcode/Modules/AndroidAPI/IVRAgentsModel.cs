using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class IVRAgentsModel
    {
        public int Id { get; set; }

        public int IVRId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }
}