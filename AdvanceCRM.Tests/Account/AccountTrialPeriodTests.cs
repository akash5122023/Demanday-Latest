using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using AdvanceCRM.Membership.Pages;
using AdvanceCRM.Tests.Helpers;
using Serenity.Data;
using Microsoft.AspNetCore.Authentication;
using Serenity.Services;
using Serenity;
using Serenity.Abstractions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using AdvanceCRM.MultiTenancy;
using AdvanceCRM.Settings;

namespace AdvanceCRM.Tests.Account
{
    public class AccountTrialPeriodTests
    {
        private AccountController CreateController(IConfiguration config)
        {
            var temp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(temp);
            var env = new FakeWebHostEnvironment { ContentRootPath = temp };

            // Minimal license file so controller Login calls don't throw if accidentally invoked.
            var mac = NetworkInterface.GetAllNetworkInterfaces().First().GetPhysicalAddress().ToString();
            var requestKey = AdvanceCRM.Web.Helpers.LicenseHelper.GenerateRequestHash(mac);
            var modules = "M1"; var users = "1"; var noUsers = "1"; var daterange = "01/01/2024-12/31/2024"; var endDate = DateTime.Now.AddDays(20).ToString("MM/dd/yyyy");
            var activationKey = AdvanceCRM.Web.Helpers.LicenseHelper.GenerateActivationHash(modules, users, noUsers, daterange, requestKey, endDate);
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
                null,
                mockScopeFactory.Object,
                mockTenantAccessor.Object);
        }

        private (DateTime start, DateTime end) InvokeCalculate(AccountController controller, ProductPlanRow plan)
        {
            var method = typeof(AccountController).GetMethod("CalculateLicensePeriod", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.NotNull(method);
            var tuple = method!.Invoke(controller, new object[] { plan });
            // The private method returns a ValueTuple<DateTime, DateTime>
            var startProp = tuple.GetType().GetField("Item1");
            var endProp = tuple.GetType().GetField("Item2");
            var start = (DateTime)startProp!.GetValue(tuple);
            var end = (DateTime)endProp!.GetValue(tuple);
            return (start, end);
        }

        [Fact]
        public void CalculateLicensePeriod_NoPlan_UsesConfiguredDefault()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["TrialSettings:DefaultDays"] = "7"
                })
                .Build();
            var controller = CreateController(config);
            var (start, end) = InvokeCalculate(controller, null);
            Assert.Equal(7, (end - start).Days + 1); // inclusive
        }

        [Fact]
        public void CalculateLicensePeriod_NoPlan_NoConfig_UsesFallback7()
        {
            var config = new ConfigurationBuilder().Build(); // no default provided
            var controller = CreateController(config);
            var (start, end) = InvokeCalculate(controller, null);
            Assert.Equal(7, (end - start).Days + 1);
        }

        [Fact]
        public void CalculateLicensePeriod_PlanOverridesConfig()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["TrialSettings:DefaultDays"] = "7"
                })
                .Build();
            var controller = CreateController(config);
            var plan = new ProductPlanRow { TrialDays = 10 };
            var (start, end) = InvokeCalculate(controller, plan);
            Assert.Equal(10, (end - start).Days + 1);
        }

        [Fact]
        public void CalculateLicensePeriod_SingleDayPlan_AllowsSingleDay()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["TrialSettings:DefaultDays"] = "7"
                })
                .Build();
            var controller = CreateController(config);
            var plan = new ProductPlanRow { TrialDays = 1 };
            var (start, end) = InvokeCalculate(controller, plan);
            Assert.Equal(1, (end - start).Days + 1);
        }
    }
}
