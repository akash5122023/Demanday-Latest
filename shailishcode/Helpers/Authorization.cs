namespace Serenity
{
    using Serenity.Abstractions;
    using Serenity.Extensions.DependencyInjection;
    using System;

    public static class Authorization
    {
        private static T Resolve<T>() where T : class
        {
            return Dependency.Resolve<T>();
        }

        public static bool HasPermission(string permission)
        {
            var svc = Resolve<IPermissionService>();
            return svc != null && svc.HasPermission(permission);
        }

        public static void ValidatePermission(string permission)
        {
            if (!HasPermission(permission))
                throw new UnauthorizedAccessException("Access denied: " + permission);
        }

        public static bool IsLoggedIn
        {
            get
            {
                var accessor = Resolve<IUserAccessor>();
                return accessor?.User?.Identity?.IsAuthenticated == true;
            }
        }
        public static string UserId => UserDefinition?.Id;
        public static string Username
        {
            get
            {
                var accessor = Resolve<IUserAccessor>();
                return accessor?.User?.Identity?.Name ?? string.Empty;
            }
        }

        public static IUserDefinition UserDefinition
        {
            get
            {
                var accessor = Resolve<IUserAccessor>();
                var userId = accessor?.User?.GetIdentifier();
                if (userId == null)
                    return null;
                var retriever = Resolve<IUserRetrieveService>();
                return retriever?.ById(userId);
            }
        }
    }
}
