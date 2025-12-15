namespace AdvanceCRM.Administration
{
    using Serenity;
    using Serenity.Abstractions;
    using Serenity.Data;
    using AdvanceCRM.Web.Helpers;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using System.Data;
    using MyRow = UserRow;

    public class UserRetrieveService : IUserRetrieveService
    {
        private static MyRow.RowFields fld => MyRow.Fields;

        protected ITwoLevelCache Cache { get; }
        protected ISqlConnections SqlConnections { get; }

        public UserRetrieveService(ITwoLevelCache cache, ISqlConnections sqlConnections)
        {
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
            SqlConnections = sqlConnections ?? throw new ArgumentNullException(nameof(sqlConnections));
        }

        private static UserDefinition Map(MyRow user)
        {
            if (user == null)
                return null;

            return new UserDefinition
            {
                UserId = user.UserId.Value,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                UserImage = user.UserImage,
                DisplayName = user.DisplayName,
                IsActive = (short)(user.IsActive == true ? 1 : 0),
                Source = user.Source,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                UpdateDate = user.UpdateDate,
                LastDirectoryUpdate = user.LastDirectoryUpdate,
                UpperLevel = user.UpperLevel ?? 0,
                BranchId = user.BranchId,
                CompanyId = user.CompanyId ?? 0
            };
        }

        private UserDefinition GetFirst(IDbConnection connection, ICriteria criteria)
        {
            var user = connection.TryFirst<MyRow>(q => q
                .SelectTableFields()
                .Where(criteria));

            return Map(user);
        }

        public IUserDefinition ById(string id)
        {
            if (id == null)
                return null;

            return Cache.GetLocalStoreOnly("UserByID_" + id, TimeSpan.Zero, fld.GenerationKey, () =>
            {
                using var connection = SqlConnections.NewByKey("Default");
                var row = connection.TryById<MyRow>(Int32.Parse(id));
                return Map(row);
            });
        }

        public IUserDefinition ByUsername(string username)
        {
            if (username.IsEmptyOrNull())
                return null;

            return Cache.GetLocalStoreOnly("UserByName_" + username.ToLowerInvariant(),
                TimeSpan.Zero, fld.GenerationKey, () =>
            {
                using var connection = SqlConnections.NewByKey("Default");
                var user = GetFirst(connection, fld.Username == username);
                if (user == null && username != null && username.Contains("@"))
                    user = GetFirst(connection, fld.Email == username);
                return user;

            });
        }
        public static void RemoveCachedUser(ITwoLevelCache cache, int? userId, string username)
        {
            if (cache != null)
            {
                if (userId != null)
                    cache.Remove("UserByID_" + userId);

                if (!string.IsNullOrEmpty(username))
                    cache.Remove("UserByName_" + username.ToLowerInvariant());
            }

            if (userId != null)
                LocalCache.Remove("UserByID_" + userId);

            if (!string.IsNullOrEmpty(username))
                LocalCache.Remove("UserByName_" + username.ToLowerInvariant());

            if (userId != null)
            {
                var provider = Dependency.Provider;
                if (provider != null)
                {
                    var sqlConnections = provider.GetService<ISqlConnections>();
                    if (sqlConnections != null)
                    {
                        using var connection = sqlConnections.NewByKey("Default");
                        var email = connection.TryById<MyRow>(userId.Value)?.Email;
                        if (!string.IsNullOrEmpty(email) && !string.Equals(email, username, StringComparison.OrdinalIgnoreCase))
                        {
                            cache?.Remove("UserByName_" + email.ToLowerInvariant());
                            LocalCache.Remove("UserByName_" + email.ToLowerInvariant());
                        }
                    }
                }
            }
        }
    }
}
