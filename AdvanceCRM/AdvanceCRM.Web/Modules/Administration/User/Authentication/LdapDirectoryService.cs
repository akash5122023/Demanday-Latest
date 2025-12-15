using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serenity;
using System;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security;

namespace AdvanceCRM.Administration
{
    public class LdapDirectoryService : IDirectoryService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LdapDirectoryService> _logger;

        public LdapDirectoryService(IConfiguration configuration, ILogger<LdapDirectoryService> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public DirectoryEntry Validate(string username, string password)
        {
            var config = _configuration.GetSection("LDAP").Get<Settings>();
            var directory = new LdapDirectoryIdentifier(
                config.Host,
                config.Port,
                fullyQualifiedDnsHostName: true,
                connectionless: false);

            var secureConfigPassword = new System.Security.SecureString();
            foreach (var ch in config.Password ?? string.Empty)
                secureConfigPassword.AppendChar(ch);
            secureConfigPassword.MakeReadOnly();
            var credential = new NetworkCredential(
                config.Username,
                secureConfigPassword);

            var ldapConnection = new LdapConnection(directory, credential)
            {
                AuthType = AuthType.Basic
            };
            try
            {
                ldapConnection.SessionOptions.ProtocolVersion = 3;

                var request = new SearchRequest(
                        config.DistinguishedName,
                        "(&(objectClass=*)(uid=" + username + "))",
                        SearchScope.Subtree,
                        new string[] { "uid", "givenName", "sn", "mail" });

                var result = (SearchResponse)ldapConnection.SendRequest(request);

                if (result.Entries.Count == 0)
                    return null;

                var item = result.Entries[0];
                try
                {
                    var securePwd = new System.Security.SecureString();
                    foreach (var ch in password ?? string.Empty)
                        securePwd.AppendChar(ch);
                    securePwd.MakeReadOnly();
                    ldapConnection.Bind(new NetworkCredential(item.DistinguishedName, securePwd));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error authenticating user");
                    return null;
                }

                // make sure to check these attribute names match with your LDAP attributes
                var uid = item.Attributes["uid"];
                var firstName = item.Attributes["givenName"];
                var lastName = item.Attributes["sn"];
                var email = item.Attributes["mail"];

                var entry = new DirectoryEntry
                {
                    Username = uid[0] as string,
                    FirstName = uid.Count > 0 ? firstName[0] as string : null,
                    LastName = lastName.Count > 0 ? lastName[0] as string : null,
                    Email = email.Count > 0 ? email[0] as string : null
                };

                return entry;
            }
            finally
            {
                try
                {
                    ldapConnection.Dispose();
                }
                catch
                {
                }
            }
        }

        private class Settings
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string DistinguishedName { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
