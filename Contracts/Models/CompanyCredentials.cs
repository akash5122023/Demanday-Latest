using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public  class CompanyCredentials
    {
        //sap b1 company credentials
        public string Server { get; set; }
        public string ODBCServer { get; set; }
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LicenseServer { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        public string DbServerType { get; set; }
        public string Language { get; set; }
        
    }
}
