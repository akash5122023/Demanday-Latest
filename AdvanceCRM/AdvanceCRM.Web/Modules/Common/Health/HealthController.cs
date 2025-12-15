using Microsoft.AspNetCore.Mvc;
using Serenity.Data;
using System;

namespace AdvanceCRM.Common
{
    [Route("healthz")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ISqlConnections _connections;
        public HealthController(ISqlConnections connections)
        {
            _connections = connections;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new HealthStatus { UptimeSeconds = (long)(DateTime.UtcNow - System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime()).TotalSeconds };
            try
            {
                using var conn = _connections.NewByKey("Default");
                // cheap metadata query
                conn.Execute("SELECT 1");
                result.Database = "OK";
            }
            catch (Exception ex)
            {
                result.Database = "ERROR";
                result.DatabaseError = ex.GetType().Name;
            }
            return Ok(result);
        }

        public sealed class HealthStatus
        {
            public string Status => "OK";
            public string Database { get; set; } = "UNKNOWN";
            public string DatabaseError { get; set; }
            public long UptimeSeconds { get; set; }
        }
    }
}
