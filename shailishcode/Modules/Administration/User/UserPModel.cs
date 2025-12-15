using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.Administration.User
{
    public class UserPModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string MobileNo { get; set; }
        public String IsActive { get; set; }
        public bool NonOperational { get; set; }
    }
}