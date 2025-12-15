using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AdvanceCRM.Administration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AdvanceCRM.Tests.Administration
{
    public class SubdomainServiceTests
    {
        [Fact]
        public async Task CreateSubdomainAsync_SkipsCreation_WhenRecordExists()
        {
            var handler = new StubHttpMessageHandler();
            handler.Handler = (request, cancellationToken) =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);

                var json = JsonSerializer.Serialize(new
                {
                    result = new[] { new { id = "existing" } },
                    success = true
                });

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            };

            var service = CreateService(handler, BuildConfiguration());

            await service.CreateSubdomainAsync("demo");

            Assert.Single(handler.Requests);
            Assert.Equal(HttpMethod.Get, handler.Requests[0].Method);
        }

        [Fact]
        public async Task CreateSubdomainAsync_LogsWarning_WhenTokenUnauthorized()
        {
            var handler = new StubHttpMessageHandler();
            var call = 0;
            handler.Handler = (request, cancellationToken) =>
            {
                call++;

                if (call == 1)
                {
                    Assert.Equal(HttpMethod.Get, request.Method);
                    return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent("Unauthorized")
                    });
                }

                if (call == 2)
                {
                    Assert.Equal(HttpMethod.Get, request.Method);
                    return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{\"result\":[],\"success\":true}", Encoding.UTF8, "application/json")
                    });
                }

                Assert.Equal(3, call);
                Assert.Equal(HttpMethod.Post, request.Method);
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"success\":true}", Encoding.UTF8, "application/json")
                });
            };

            var logger = new Mock<ILogger<SubdomainService>>();
            var service = CreateService(handler, BuildConfiguration(), logger);

            await service.CreateSubdomainAsync("demo");

            logger.Verify(
                l => l.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((state, type) => state != null && state.ToString().Contains("returned 401")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);

            Assert.Equal(3, handler.Requests.Count);
            Assert.Equal(HttpMethod.Post, handler.Requests[2].Method);
        }

        [Fact]
        public async Task CreateSubdomainAsync_SendsExpectedPayload_ForNewRecord()
        {
            var handler = new StubHttpMessageHandler();
            string? payload = null;
            handler.Handler = async (request, cancellationToken) =>
            {
                if (request.Method == HttpMethod.Get)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{\"result\":[],\"success\":true}", Encoding.UTF8, "application/json")
                    };
                }

                Assert.Equal(HttpMethod.Post, request.Method);
                payload = await request.Content.ReadAsStringAsync();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"success\":true}", Encoding.UTF8, "application/json")
                };
            };

            var service = CreateService(handler, BuildConfiguration());

            await service.CreateSubdomainAsync("newsub");

            Assert.Equal(2, handler.Requests.Count);
            Assert.NotNull(payload);

            using var document = JsonDocument.Parse(payload!);
            var root = document.RootElement;
            Assert.Equal("A", root.GetProperty("type").GetString());
            Assert.Equal("newsub.example.com", root.GetProperty("name").GetString());
            Assert.Equal("203.0.113.10", root.GetProperty("content").GetString());
            Assert.True(root.GetProperty("proxied").GetBoolean());
            Assert.Equal(1, root.GetProperty("ttl").GetInt32());
        }

        private static IConfiguration BuildConfiguration(IDictionary<string, string>? overrides = null)
        {
            var settings = new Dictionary<string, string>
            {
                ["Cloudflare:ApiToken"] = "token",
                ["Cloudflare:ApiKey"] = "fallback-key",
                ["Cloudflare:Email"] = "user@example.com",
                ["Cloudflare:ZoneId"] = "zone123",
                ["Cloudflare:ServerIp"] = "203.0.113.10",
                ["Cloudflare:RootDomain"] = "example.com",
            };

            if (overrides != null)
            {
                foreach (var pair in overrides)
                    settings[pair.Key] = pair.Value;
            }

            return new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();
        }

        private static SubdomainService CreateService(StubHttpMessageHandler handler, IConfiguration configuration, Mock<ILogger<SubdomainService>>? loggerMock = null)
        {
            var httpClient = new HttpClient(handler);
            var logger = loggerMock?.Object ?? new Mock<ILogger<SubdomainService>>().Object;
            return new SubdomainService(httpClient, configuration, logger);
        }

        private sealed class StubHttpMessageHandler : HttpMessageHandler
        {
            public List<HttpRequestMessage> Requests { get; } = new();

            public Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>? Handler { get; set; }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Requests.Add(request);

                if (Handler != null)
                    return Handler(request, cancellationToken);

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            }
        }
    }
}
