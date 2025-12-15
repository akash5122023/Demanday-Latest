using AdvanceCRM.Administration.Repositories;
using AdvanceCRM.MultiTenancy;
using AdvanceCRM.Web.Helpers;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Services;
using Serenity.Extensions;
using Serenity.Web;
using Serenity.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AdvanceCRM.Administration.Entities;

using MyRepository = AdvanceCRM.Administration.Repositories.UserRepository;
using MyRow = AdvanceCRM.Administration.UserRow;
using TenantRow = AdvanceCRM.Administration.TenantRow;

namespace AdvanceCRM.Administration.Endpoints
{
    [Route("Services/Administration/User/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class UserController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IConfiguration configuration;
        private readonly ITenantAccessor tenantAccessor;

        private readonly IUserAccessor userAccessor;
        private readonly IPermissionService permissionService;
        private readonly IRequestContext requestContext;
        private readonly IMemoryCache memoryCache;
        private readonly ITypeSource typeSource;
        private readonly IUserRetrieveService userRetriever;

        public UserController(
            IUserAccessor userAccessor,
            ISqlConnections connections,
            IConfiguration configuration,
            ITenantAccessor tenantAccessor,
            IPermissionService permissionService,
            IRequestContext requestContext,
            IMemoryCache memoryCache,
            ITypeSource typeSource,
            IUserRetrieveService userRetriever)
        {
            this.userAccessor = userAccessor;
            this.permissionService = permissionService;
            this.requestContext = requestContext;
            this.memoryCache = memoryCache;
            this.typeSource = typeSource;
            this.userRetriever = userRetriever;
            this.configuration = configuration;
            this.tenantAccessor = tenantAccessor;
            _connections = connections;
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            EnforceTenantUserLimit(uow);
            return new MyRepository(Context).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MyRepository(Context).Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyRepository(Context).Delete(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public UndeleteResponse Undelete(IUnitOfWork uow, UndeleteRequest request)
        {
            return new MyRepository(Context).Undelete(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRepository(Context).Retrieve(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context).List(connection, request);
        }

        private void EnforceTenantUserLimit(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException(nameof(uow));

            var limit = GetTenantUserLimit();
            if (!limit.HasValue || limit.Value <= 0)
                return;

            var connection = uow.Connection;
            if (connection == null)
                return;

            var activeUserCount = connection.QuerySingle<int>(
                "SELECT COUNT(*) FROM [dbo].[Users] WHERE ISNULL(IsActive, 0) = 1");

            if (activeUserCount >= limit.Value)
            {
                var message = string.Format(
                    "Your current plan allows creating up to {0} active users during the trial period.",
                    limit.Value);
                throw new ValidationError("TrialUserLimit", "UserId", message);
            }
        }

        private int? GetTenantUserLimit()
        {
            if (tenantAccessor == null)
                return null;

            var tenant = tenantAccessor.CurrentTenant;
            if (tenant == null || tenant.TenantId <= 0)
                return null;

            string plan = null;
            var originalTenant = tenantAccessor.CurrentTenant;

            try
            {
                tenantAccessor.CurrentTenant = null;

                using (var connection = _connections.NewByKey("Default"))
                {
                    var tenantRow = connection.TryById<TenantRow>(tenant.TenantId);
                    plan = tenantRow?.Plan?.Trim();
                }
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }

            return ResolveUserLimit(plan);
        }

        private int? ResolveUserLimit(string plan)
        {
            if (configuration == null)
                return null;

            var trialSection = configuration.GetSection("TrialSettings");
            if (!trialSection.Exists())
                return null;

            var userLimitsSection = trialSection.GetSection("UserLimits");
            if (!string.IsNullOrWhiteSpace(plan) && userLimitsSection.Exists())
            {
                foreach (var child in userLimitsSection.GetChildren())
                {
                    var key = child.Key;
                    if (string.IsNullOrWhiteSpace(key))
                        continue;

                    if (string.Equals(key, plan, StringComparison.OrdinalIgnoreCase) ||
                        plan.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (int.TryParse(child.Value, out var planLimit) && planLimit > 0)
                            return planLimit;
                    }
                }
            }

            var defaultLimit = trialSection.GetValue<int?>("DefaultUserLimit");
            return defaultLimit.HasValue && defaultLimit.Value > 0 ? defaultLimit : null;
        }

        private static string[] permissionsUsedFromScript;

        [NonAction, ServiceAuthorize]
        public ScriptUserDefinition GetUserData()
        {
            var result = new ScriptUserDefinition();
            var user = userAccessor?.User?.GetUserDefinition(userRetriever);

            if (user == null)
            {
                result.Permissions = new Dictionary<string, bool>();
                return result;
            }

            result.UserId = Convert.ToInt32(user.Id);
            result.Username = user.Username;
            result.DisplayName = user.DisplayName;
            result.IsAdmin = user.Username == "admin";
           
            using (var connection = _connections.NewByKey("Default"))
            {
                var row = new UserRepository(Context)
                    .Retrieve(connection, new RetrieveRequest { EntityId = user.Id }).Entity;

                result.UpperLevel = (row.UpperLevel ?? 0).ToString();
                result.BranchId = row.BranchId;
                result.CompanyId = row.CompanyId;
            }

            result.Permissions = LocalCache.GetLocalStoreOnly("ScriptUserPermissions:" + user.Id, TimeSpan.Zero,
                UserPermissionRow.Fields.GenerationKey, () =>
                {
                    var permissions = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

                    if (permissionsUsedFromScript == null)
                    {
                        permissionsUsedFromScript = UserPermissionRepository.ListPermissionKeys(memoryCache, typeSource)
                            .Where(permissionKey =>
                            {
                                // Optional: filter permissions if needed
                                return true;
                            }).ToArray();
                    }

                    foreach (var permissionKey in permissionsUsedFromScript)
                    {
                        if (permissionService.HasPermission(permissionKey))
                            permissions[permissionKey] = true;
                    }

                    return permissions;
                });

            return result;
        }
    }
}
