using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serenity.Data;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Data;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace AdvanceCRM.Administration
{
    [Authorize]
    [Route("Services/Administration/SaaSSettings/[action]")]
    [Produces("application/json")]
    [IgnoreAntiforgeryToken]
    public class SaaSSettingsEndpoint : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IConfiguration _configuration;
        public SaaSSettingsEndpoint(ISqlConnections connections, IConfiguration configuration)
        {
            _connections = connections;
            _configuration = configuration;
        }

        public class RetrieveResponse
        {
            public string KeyId { get; set; }
            public string KeyIdMasked { get; set; }
            public bool HasSecret { get; set; }
        }

        public class SaveRequest
        {
            public string KeyId { get; set; }
            public string KeySecret { get; set; }
        }

        private const string KeyIdName = "Razorpay.KeyId";
        private const string KeySecretName = "Razorpay.KeySecret";

        [HttpGet]
        public async Task<RetrieveResponse> Retrieve()
        {
            string keyId = null;
            string secret = null;

            try
            {
                using var connection = _connections.NewByKey("Default");
                keyId = connection.QueryFirstOrDefault<string>("SELECT Value FROM SassApplicationSetting WHERE [Key] = @k", new { k = KeyIdName });
                secret = connection.QueryFirstOrDefault<string>("SELECT Value FROM SassApplicationSetting WHERE [Key] = @k", new { k = KeySecretName });
            }
            catch
            {
                // table might not exist yet; ignore and fallback to config
            }

            // Fallback to configuration / environment variables if DB empty
            if (string.IsNullOrWhiteSpace(keyId))
            {
                var section = _configuration.GetSection("Razorpay");
                keyId = section?["KeyId"];
                secret ??= section?["KeySecret"]; // only assign if still null/empty
            }

            var hasSecret = !string.IsNullOrWhiteSpace(secret);
            return new RetrieveResponse
            {
                KeyId = keyId,
                KeyIdMasked = Mask(keyId),
                HasSecret = hasSecret
            };
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SaveRequest request)
        {
            try
            {
                if (request == null)
                {
                    // Fallback: try to read raw body (in case content-type mismatch prevented model binding)
                    try
                    {
                        Request.EnableBuffering();
                        using var sr = new StreamReader(Request.Body, leaveOpen: true);
                        var raw = await sr.ReadToEndAsync();
                        Request.Body.Position = 0;
                        if (!string.IsNullOrWhiteSpace(raw))
                        {
                            try
                            {
                                request = JsonSerializer.Deserialize<SaveRequest>(raw, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            }
                            catch (Exception jex)
                            {
                                return BadRequest(new { success = false, error = "Invalid JSON", detail = jex.Message });
                            }
                        }
                    }
                    catch (Exception rbEx)
                    {
                        return BadRequest(new { success = false, error = "Request body read failure", detail = rbEx.Message });
                    }
                    if (request == null)
                        return BadRequest(new { success = false, error = "Request body missing" });
                }

                if (string.IsNullOrWhiteSpace(request.KeyId))
                    return BadRequest(new { success = false, error = "KeyId required (empty)" });

                using var connection = _connections.NewByKey("Default");
                await Upsert(connection, KeyIdName, request.KeyId.Trim());

                if (!string.IsNullOrWhiteSpace(request.KeySecret))
                    await Upsert(connection, KeySecretName, request.KeySecret.Trim());

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                // Return structured error (avoid leaking internals but provide message)
                return StatusCode(500, new { success = false, error = "Save failed", detail = ex.Message });
            }
        }

        private static async Task Upsert(IDbConnection connection, string key, string value)
        {
            // Try update; if 0 rows affected then insert
            var affected = Dapper.SqlMapper.Execute(connection, $"UPDATE SassApplicationSetting SET Value = @v WHERE [Key] = @k", new { v = value, k = key });
            if (affected == 0)
            {
                Dapper.SqlMapper.Execute(connection, "INSERT INTO SassApplicationSetting ([Key], Value) VALUES (@k, @v)", new { k = key, v = value });
            }
        }

        private static string Mask(string key)
        {
            if (string.IsNullOrEmpty(key)) return string.Empty;
            if (key.Length <= 7) return key[0] + new string('*', Math.Max(0, key.Length - 2)) + key[^1];
            return key.Substring(0, 4) + new string('*', key.Length - 7) + key[^3..];
        }
    }
}
