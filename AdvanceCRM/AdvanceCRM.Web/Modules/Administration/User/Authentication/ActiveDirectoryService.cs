using System;
using Microsoft.Extensions.Configuration;
using System.DirectoryServices.AccountManagement;
using Serenity;

namespace AdvanceCRM.Administration
{
    public class ActiveDirectoryService : IDirectoryService
    {
        private readonly string _domain;

        public ActiveDirectoryService(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _domain = configuration["ActiveDirectory:Domain"];
        }

        public DirectoryEntry Validate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(_domain))
                return null;

            using (var context = new PrincipalContext(ContextType.Domain, _domain))
            {
                bool isValid;
                try
                {
                    isValid = context.ValidateCredentials(username, password, ContextOptions.Negotiate);
                }
                catch
                {
                    return null;
                }

                if (!isValid)
                    return null;

                var identity = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username);
                if (identity == null)
                    return null;

                return new DirectoryEntry
                {
                    Username = identity.SamAccountName,
                    Email = identity.EmailAddress.TrimToNull(),
                    FirstName = identity.GivenName,
                    LastName = identity.Surname
                };
            }
        }
    }
}
