using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class IVRConfigModel
    {
        public int Id { get; set; }

        public int IVRType { get; set; }
        public string IVRNumber { get; set; }
        public string ApiKey { get; set; }
        public string Plan { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }
}