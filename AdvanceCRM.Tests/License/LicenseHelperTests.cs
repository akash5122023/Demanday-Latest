using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Xunit;
using AdvanceCRM.Web.Helpers;
using AdvanceCRM.Tests.Helpers;

namespace AdvanceCRM.Tests.License
{
    public class LicenseHelperTests
    {
        private (FakeWebHostEnvironment env, IConfiguration config, string mac, string modules, string users, string noUsers, string daterange) CreateEnv(string endDate)
        {
            var temp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(temp);
            var env = new FakeWebHostEnvironment { ContentRootPath = temp };
            var config = new ConfigurationBuilder().Build();

            var mac = NetworkInterface.GetAllNetworkInterfaces().First().GetPhysicalAddress().ToString();
            var modules = "M1";
            var users = "1";
            var noUsers = "1";
            var daterange = "01/01/2024-12/31/2024";

            var requestKey = LicenseHelper.GenerateRequestHash(mac);
            var activationKey = LicenseHelper.GenerateActivationHash(modules, users, noUsers, daterange, requestKey, endDate);

            var xml = $"<?xml version=\"1.0\"?>\n<appSettings>\n" +
                      $"<add key=\"Modules\" value=\"{modules}\" />\n" +
                      $"<add key=\"Users\" value=\"{users}\" />\n" +
                      $"<add key=\"NOUsers\" value=\"{noUsers}\" />\n" +
                      $"<add key=\"Daterange\" value=\"{daterange}\" />\n" +
                      $"<add key=\"Requestkey\" value=\"{requestKey}\" />\n" +
                      $"<add key=\"Activationkey\" value=\"{activationKey}\" />\n" +
                      $"<add key=\"EndDate\" value=\"{endDate}\" />\n" +
                      "</appSettings>";
            File.WriteAllText(Path.Combine(temp, "ExternalAppSetting.config"), xml);

            return (env, config, mac, modules, users, noUsers, daterange);
        }

        [Fact]
        public void IsLicenseValid_ReturnsTrue_ForValidConfig()
        {
            var endDate = DateTime.Now.AddDays(10).ToString("MM/dd/yyyy");
            var (env, config, _, _, _, _, _) = CreateEnv(endDate);
            Assert.True(LicenseHelper.IsLicenseValid(env, config));
        }

        [Fact]
        public void IsLicenseValid_ReturnsFalse_WhenExpired()
        {
            var endDate = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy");
            var (env, config, _, _, _, _, _) = CreateEnv(endDate);
            Assert.False(LicenseHelper.IsLicenseValid(env, config));
        }

        [Fact]
        public void Initialize_SetsActivatedFlag()
        {
            var endDate = DateTime.Now.AddDays(5).ToString("MM/dd/yyyy");
            var (env, config, _, _, _, _, _) = CreateEnv(endDate);
            LicenseHelper.Initialize(env, config);
            Assert.True(LicenseHelper.Activated);
        }

        [Fact]
        public void IsLicenseValid_AcceptsLegacyRequestKey()
        {
            var temp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(temp);
            var env = new FakeWebHostEnvironment { ContentRootPath = temp };
            var config = new ConfigurationBuilder().Build();

            var mac = NetworkInterface.GetAllNetworkInterfaces().First().GetPhysicalAddress().ToString();
            var legacy = typeof(LicenseHelper)
                .GetMethod("GenerateLegacyRequestHash", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.Invoke(null, new object[] { mac }) as string;
            Assert.False(string.IsNullOrEmpty(legacy));

            var modules = "M1";
            var users = "1";
            var noUsers = "1";
            var daterange = "01/01/2024-12/31/2024";
            var activationKey = LicenseHelper.GenerateActivationHash(modules, users, noUsers, daterange, legacy, DateTime.Now.AddDays(10).ToString("MM/dd/yyyy"));

            var xml = $"<?xml version=\"1.0\"?>\n<appSettings>\n" +
                      $"<add key=\"Modules\" value=\"{modules}\" />\n" +
                      $"<add key=\"Users\" value=\"{users}\" />\n" +
                      $"<add key=\"NOUsers\" value=\"{noUsers}\" />\n" +
                      $"<add key=\"Daterange\" value=\"{daterange}\" />\n" +
                      $"<add key=\"Requestkey\" value=\"{legacy}\" />\n" +
                      $"<add key=\"Activationkey\" value=\"{activationKey}\" />\n" +
                      $"<add key=\"EndDate\" value=\"{DateTime.Now.AddDays(10).ToString("MM/dd/yyyy")}\" />\n" +
                      "</appSettings>";
            File.WriteAllText(Path.Combine(temp, "ExternalAppSetting.config"), xml);

            Assert.True(LicenseHelper.IsLicenseValid(env, config));
        }
    }
}
