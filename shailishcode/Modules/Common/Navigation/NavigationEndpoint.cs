
namespace AdvanceCRM.Common.Endpoints
{
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    
    using System.Collections.Generic;
    using System.Linq;
    using System.Configuration;
    using Serenity.Web.Providers;
    using System.Text.RegularExpressions;
    using System.IO;
    using System;

    [Route("Services/Common/Navigation/[action]")]
    public class NavigationController : ServiceEndpoint
    {

        [HttpPost]
        public StandardResponse MultiCompany()
        {
            var response = new StandardResponse();

            var Modules = ConfigurationManager.AppSettings["Modules"].ToString();

            List<string> ver;
            ver = null;

            if (Modules != null)
            {
                ver = Modules.Split(',').ToList<string>();
            }

            if (ver.Contains("MultiCompany"))
                response.Status = "No";
            else
                response.Status = "Remove";

            return response;
        }

        [HttpPost]
        public StandardResponse MultiLocation()
        {
            var response = new StandardResponse();

            var Modules = ConfigurationManager.AppSettings["Modules"].ToString();

            List<string> ver;
            ver = null;

            if (Modules != null)
            {
                ver = Modules.Split(',').ToList<string>();
            }

            if (ver.Contains("MultiLocation"))
                response.Status = "No";
            else
                response.Status = "Remove";

            return response;
        }

        [HttpPost]
        public StandardResponse ChannelsManagement()
        {
            var response = new StandardResponse();

            var Modules = ConfigurationManager.AppSettings["Modules"].ToString();

            List<string> ver;
            ver = null;

            if (Modules != null)
            {
                ver = Modules.Split(',').ToList<string>();
            }

            if (ver.Contains("ChannelsManagement"))
                response.Status = "No";
            else
                response.Status = "Remove";

            return response;
        }
    }
}
