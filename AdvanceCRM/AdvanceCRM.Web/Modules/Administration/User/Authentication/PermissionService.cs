using Serenity;
using Serenity.Abstractions;
using Serenity.Data;
using AdvanceCRM.Administration.Entities;
using AdvanceCRM.Administration.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvanceCRM.Administration
{
    public class PermissionService : IPermissionService
    {
        protected ITwoLevelCache Cache { get; }
        protected ISqlConnections SqlConnections { get; }
        public ITypeSource TypeSource { get; }
        protected IUserAccessor UserAccessor { get; }
        protected IHttpContextAccessor HttpContextAccessor { get; }

        public PermissionService(ITwoLevelCache cache, ISqlConnections sqlConnections,
            ITypeSource typeSource, IUserAccessor userAccessor,
            IHttpContextAccessor httpContextAccessor)
        {
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
            SqlConnections = sqlConnections ?? throw new ArgumentNullException(nameof(sqlConnections));
            TypeSource = typeSource ?? throw new ArgumentNullException(nameof(typeSource));
            UserAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public bool HasPermission(string permission)
        {
            if (permission == null)
                return false;

            if (permission == "*")
                return true;

            var isLoggedIn = UserAccessor.IsLoggedIn();

            if (permission == "?")
                return isLoggedIn;

            if (!isLoggedIn)
                return false;

            var username = UserAccessor.User?.Identity?.Name;
            if (username == "admin")
                return true;

            // only admin has impersonation permission
            if (string.Compare(permission, "ImpersonateAs", StringComparison.OrdinalIgnoreCase) == 0)
                return false;

            var userId = Convert.ToInt32(UserAccessor.User.GetIdentifier());

            bool grant;
            if (GetUserPermissions(userId).TryGetValue(permission, out grant))
                return grant;

            foreach (var roleId in GetUserRoles(userId))
            {
                if (GetRolePermissions(roleId).Contains(permission))
                    return true;
            }

            return false;
        }

        // Permissions are read fresh from the DB and memoized only for the
        // lifetime of the current HTTP request. This keeps permission changes
        // real-time across ALL app instances/pods (no process-local cache that
        // can go stale on other replicas when there is no distributed cache),
        // while still avoiding repeated DB hits while a single page/menu renders.
        private T GetRequestScoped<T>(string key, Func<T> factory)
        {
            var ctx = HttpContextAccessor.HttpContext;
            if (ctx == null)
                return factory(); // background/no-request: always fresh

            if (ctx.Items.TryGetValue(key, out var existing) && existing is T cached)
                return cached;

            var value = factory();
            ctx.Items[key] = value;
            return value;
        }

        private Dictionary<string, bool> GetUserPermissions(int userId)
        {
            return GetRequestScoped("PermSvc:UserPermissions:" + userId, () =>
            {
                var fld = UserPermissionRow.Fields;
                using (var connection = SqlConnections.NewByKey("Default"))
                {
                    var result = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

                    connection.List<UserPermissionRow>(q => q
                            .Select(fld.PermissionKey)
                            .Select(fld.Granted)
                            .Where(new Criteria(fld.UserId) == userId))
                        .ForEach(x => result[x.PermissionKey] = x.Granted ?? true);

                    var implicitPermissions = UserPermissionRepository.GetImplicitPermissions(Cache.Memory, TypeSource);
                    foreach (var pair in result.ToArray())
                    {
                        HashSet<string> list;
                        if (pair.Value && implicitPermissions.TryGetValue(pair.Key, out list))
                            foreach (var x in list)
                                result[x] = true;
                    }

                    return result;
                }
            });
        }

        private HashSet<string> GetRolePermissions(int roleId)
        {
            return GetRequestScoped("PermSvc:RolePermissions:" + roleId, () =>
            {
                var fld = RolePermissionRow.Fields;
                using (var connection = SqlConnections.NewByKey("Default"))
                {
                    var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    connection.List<RolePermissionRow>(q => q
                            .Select(fld.PermissionKey)
                            .Where(new Criteria(fld.RoleId) == roleId))
                        .ForEach(x => result.Add(x.PermissionKey));

                    var implicitPermissions = UserPermissionRepository.GetImplicitPermissions(Cache.Memory, TypeSource);
                    foreach (var key in result.ToArray())
                    {
                        HashSet<string> list;
                        if (implicitPermissions.TryGetValue(key, out list))
                            foreach (var x in list)
                                result.Add(x);
                    }

                    return result;
                }
            });
        }

        private HashSet<int> GetUserRoles(int userId)
        {
            return GetRequestScoped("PermSvc:UserRoles:" + userId, () =>
            {
                var fld = UserRoleRow.Fields;
                using (var connection = SqlConnections.NewByKey("Default"))
                {
                    var result = new HashSet<int>();

                    connection.List<UserRoleRow>(q => q
                            .Select(fld.RoleId)
                            .Where(new Criteria(fld.UserId) == userId))
                        .ForEach(x => result.Add(x.RoleId.Value));

                    return result;
                }
            });
        }
    }
}
