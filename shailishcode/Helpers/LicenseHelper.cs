using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serenity.Web.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AdvanceCRM.Web.Helpers
{
    public static class LicenseHelper
    {
        public static bool Activated { get; private set; }

        public static void Initialize(IWebHostEnvironment env, IConfiguration configuration)
        {
            Activated = IsLicenseValid(env, configuration);
        }

        public static bool IsLicenseValid(IWebHostEnvironment env, IConfiguration configuration)
        {
            var configFile = Path.Combine(env.ContentRootPath, "ExternalAppSetting.config");
            if (!File.Exists(configFile))
                return false;

            var xml = XDocument.Load(configFile);
            var licenseConfig = xml.Descendants("add")
                .Where(x => x.Attribute("key") != null)
                .ToDictionary(
                    x => (string)x.Attribute("key"),
                    x => (string?)x.Attribute("value"),
                    StringComparer.OrdinalIgnoreCase);

            // read and normalize values
            string users = licenseConfig.TryGetValue("Users", out var u) ? u ?? "" : "";
            string noUsers = licenseConfig.TryGetValue("NOUsers", out var n) ? n ?? "" : "";
            string dateRange = licenseConfig.TryGetValue("Daterange", out var d) ? d ?? "" : "";
            string requestKeyRaw = licenseConfig.TryGetValue("Requestkey", out var r) ? r ?? "" : "";
            string activationKey = licenseConfig.TryGetValue("Activationkey", out var a) ? a ?? "" : "";
            string endDate = licenseConfig.TryGetValue("EndDate", out var e) ? e ?? "" : "";
            string modules = licenseConfig.TryGetValue("Modules", out var m) ? m ?? "" : "";

            // normalize stored requestKey (uppercase, no spaces)
            var requestKey = Regex.Replace(requestKeyRaw, @"\s+", "").ToUpperInvariant();

            var macs = NetworkInterface.GetAllNetworkInterfaces()
                        .Select(nic => nic.GetPhysicalAddress().ToString());

            foreach (var mac in macs)
            {
                // generate the canonical request token for this machine
                var req = GenerateRequestHash(mac);

                // instead of strict equality, allow stored key to include extra suffix
                if (!requestKey.StartsWith(req, StringComparison.OrdinalIgnoreCase))
                    continue;

                // compute activation hash and validate expiry
                var act = GenerateActivationHash(modules, users, noUsers, dateRange, requestKey, endDate);
                if (string.Equals(activationKey, act, StringComparison.OrdinalIgnoreCase)
                    && DateTime.TryParseExact(endDate, "MM/dd/yyyy",
                                               System.Globalization.CultureInfo.InvariantCulture,
                                               System.Globalization.DateTimeStyles.None,
                                               out var parsed)
                    && parsed >= DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }

        private static string InsertDashes(string hash, int startRemove)
        {
            hash = Regex.Replace(hash, @"[^0-9a-zA-Z]+", "");

            hash = hash.ToUpper();

            hash = hash.Remove(0, 58);

            int keycount = hash.Length;

            string dash = "-";

            for (int i = 4; i <= keycount; i = i + 6)
            {
                hash = hash.Insert(i, dash);
            }
            return hash;
            //// strip non-alphanumeric, uppercase
            //hash = Regex.Replace(hash, @"[^0-9A-Za-z]", "");
            //hash = hash.ToUpperInvariant();

            //// drop the prefix
            //hash = hash.Remove(0, startRemove);

            //// insert dash every 6 characters, starting after 4 chars
            //for (int i = 4; i <= hash.Length; i += 6)
            //    hash = hash.Insert(i, "-");

            //return hash;
        }
        private static string InsertDashes1(string hash, int startRemove)
        {
            hash = Regex.Replace(hash, @"[^0-9a-zA-Z]+", "");

            hash = hash.ToUpper();

            hash = hash.Remove(0, 39);

            int keycount = hash.Length;

            string dash = "-";

            for (int i = 4; i <= keycount; i = i + 6)
            {
                hash = hash.Insert(i, dash);
            }
            return hash;
            //// strip non-alphanumeric, uppercase
            //hash = Regex.Replace(hash, @"[^0-9A-Za-z]", "");
            //hash = hash.ToUpperInvariant();

            //// drop the prefix
            //hash = hash.Remove(0, startRemove);

            //// insert dash every 6 characters, starting after 4 chars
            //for (int i = 4; i <= hash.Length; i += 6)
            //    hash = hash.Insert(i, "-");

            //return hash;
        }

        public static string GenerateRequestHash(string requestKey)
        {
            var raw = SiteMembershipProvider.ComputeSHA512(requestKey + "Developer@2020#$");
            return InsertDashes(raw, 58);
        }

        public static string GenerateActivationHash(
            string modules,
            string users,
            string noUsers,
            string dateRange,
            string requestKey,
            string endDate)
        {
            var combined = modules + users + noUsers + dateRange + requestKey + endDate;
            var raw = SiteMembershipProvider.ComputeSHA512(combined + "Developer@2020#$");
            return InsertDashes1(raw, 39);
        }
    }
}
