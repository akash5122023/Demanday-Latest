namespace AdvanceCRM.Common.Pages
{
    using AdvanceCRM.Common.Activation;
    using Serenity;
    using Serenity.Web;
    using Serenity.Web.Providers;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    using System.Net.NetworkInformation;
    using Microsoft.AspNetCore.Mvc;
    using AdvanceCRM.Web.Helpers;

    // Map the controller to the /Activation prefix. Individual actions
    // are routed explicitly so that /Activation resolves to Index while
    // other actions like GenrateCrypto are mapped by name.
    [Route("Activation")]
    public class ActivationController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public ActivationController(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        [HttpGet("")]
        public ActionResult Index()
        {
            //if (LicenseHelper.Activated)
            //    return RedirectToAction("Login", "Account");

            return ActivationView();
        }

        [HttpGet("Edit")]
        public ActionResult Edit()
        {
            return ActivationView();
        }

        private ActionResult ActivationView()
        {
            var model = new ActivationModel();

            // Getting MAC addresses
            model.nics = NetworkInterface.GetAllNetworkInterfaces();

            return View(MVC.Views.Common.Activation.ActivationIndex, model);
        }

        [HttpGet("GenrateCryptoActivation")]
        [HttpPost("GenrateCryptoActivation")]
        public ActionResult GenrateCryptoActivation(string modules, string users, string NOusers, string daterange, string requestkey, string enddate)
        {
            var hash = LicenseHelper.GenerateActivationHash(modules, users, NOusers, daterange, requestkey, enddate);
            return new JsonResult(hash);
        }

        [HttpGet("GenrateCryptoActivationRazor")]
        [HttpPost("GenrateCryptoActivationRazor")]
        public ActionResult GenrateCryptoActivationRazor(string modules, string users, string NOusers, string daterange, string requestkey, string enddate, string activationfield)
        {
            var hash = LicenseHelper.GenerateActivationHash(modules, users, NOusers, daterange, requestkey, enddate);

            string status = "";

            if (hash == activationfield)
            {
                status = "Success";
            }
            else
            {
                status = "Failed";
            }

            return new JsonResult(status);
        }

        [HttpGet("GenrateCrypto")]
        [HttpPost("GenrateCrypto")]
        public ActionResult GenrateCrypto(string requestKey)
        {
            var hash = LicenseHelper.GenerateRequestHash(requestKey);
            return new JsonResult(hash);
        }

        [HttpGet("GenerateExternalSettings")]
        [HttpPost("GenerateExternalSettings")]
    public ActionResult GenerateExternalSettings(string modules, string users, string NOusers, string daterange, string requestkey, string endDate, string activationkey)
        {
        
            string status = "";

            string file_contents = "";

            file_contents = "<?xml version=\"1.0\"?>\n" +
                            "<appSettings>\n" +
                            "<add key = \"Modules\" value = \"" + modules + "\" />\n" +
                            "<add key = \"Users\" value = \"" + users + "\" />\n" +
                            "<add key = \"NOUsers\" value = \"" + NOusers + "\" />\n" +
                            "<add key = \"Daterange\" value = \"" + daterange + "\" />\n" +
                            "<add key = \"Requestkey\" value = \"" + requestkey + "\" />\n" +
                            "<add key = \"Activationkey\" value = \"" + activationkey + "\" />\n" +
                            "<add key = \"EndDate\" value = \"" + endDate + "\" />\n" +
                            "</appSettings>";

            try
            {
                var filePath = Path.Combine(_env.ContentRootPath, "ExternalAppSetting.config");
                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(file_contents); // Write the file.
                }

                // Re-evaluate license status immediately so activation takes effect without app restart
                try
                {
                    LicenseHelper.Initialize(_env, _configuration);
                }
                catch { /* swallow - activation file already written */ }

                status = "Success";
            }
            catch (Exception ex)
            {
                status = "Error\n" + ex.Message.ToString();
            }


            return new JsonResult(status);
        }


    }
}