using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using AdvanceCRM.Tests.Helpers;
using AdvanceCRM.Membership.Pages;
using AdvanceCRM.Web.Helpers;
using Serenity.Services;
using Serenity;
using Serenity.Abstractions;
using Serenity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using AdvanceCRM.MultiTenancy;


namespace AdvanceCRM.Tests.Account
{
    public class AccountControllerTests
    {
        private AccountController CreateController(string endDate)
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

            var mockCon = new Mock<ISqlConnections>();
            var mockAuth = new Mock<IAuthenticationService>();
            var mockUser = new Mock<IUserRetrieveService>();
            //var mockLoc = new Mock<ILocalTextLocalizer>();
            var mockLoc = new Mock<ITextLocalizer>();
            var mockData = new Mock<IDataProtectionProvider>();
            var mockCache = new Mock<ITwoLevelCache>();
            var mockScopeFactory = new Mock<IServiceScopeFactory>();
            mockScopeFactory.Setup(x => x.CreateScope()).Returns(new Mock<IServiceScope>().Object);
            var mockTenantAccessor = new Mock<ITenantAccessor>();

            return new AccountController(
                mockCon.Object,
                mockAuth.Object,
                mockUser.Object,
                env,
                config,
                mockLoc.Object,
                mockData.Object,
                mockCache.Object,
                null, // SubdomainService not required for these tests
                mockScopeFactory.Object,
                mockTenantAccessor.Object);
        }

        [Fact]
        public void Login_Redirects_WhenLicenseInvalid()
        {
            var controller = CreateController("01/01/2000");
            var result = controller.Login(null, null);
            var redirect = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/Activation", redirect.Url);
        }

        [Fact]
        public void Login_ReturnsView_WhenLicenseValid()
        {
            var controller = CreateController(DateTime.Now.AddDays(10).ToString("MM/dd/yyyy"));
            var result = controller.Login(null, null);
            Assert.IsType<ViewResult>(result);
        }
    }
}
