using AdvanceCRM.Administration.Repositories;
using AdvanceCRM.MultiTenancy;
using AdvanceCRM.Web.Helpers;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
using AdvanceCRM.Settings;

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
            this.tenantAccessor = tenantAccessor;
            _connections = connections;
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            EnforceTenantUserLimit(uow, request);
            return new MyRepository(Context).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            EnforceTenantUserLimitOnActivation(uow, request);
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

        private void EnforceTenantUserLimit(IUnitOfWork uow, SaveRequest<MyRow> request = null)
        {
            if (uow == null)
                throw new ArgumentNullException(nameof(uow));

            if (request?.Entity?.IsActive == false)
                return;

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
                    "Your current plan allows creating up to {0} active users. Please upgrade your plan or deactivate an existing user before adding a new one.",
                    limit.Value);
                throw new ValidationError("TrialUserLimit", "UserId", message);
            }
        }

        private void EnforceTenantUserLimitOnActivation(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            if (uow == null || request == null)
                return;

            var entity = request.Entity;
            if (entity == null || entity.IsActive != true)
                return;

            var connection = uow.Connection;
            if (connection == null)
                return;

            int? userId = entity?.UserId;

            if (userId == null && request?.EntityId != null)
            {
                if (request.EntityId is int i)
                    userId = i;
                else if (request.EntityId is long l)
                    userId = (int)l;
                else if (request.EntityId is short s)
                    userId = s;
                else if (request.EntityId is string s2 && int.TryParse(s2, out var parsed))
                    userId = parsed;
            }

            if (!userId.HasValue)
                return;

            var existingIsActive = connection.QueryFirstOrDefault<int?>(
                "SELECT ISNULL(IsActive, 0) FROM [dbo].[Users] WHERE UserId = @UserId",
                new { UserId = userId.Value });

            if (existingIsActive.HasValue && existingIsActive.Value > 0)
                return;

            EnforceTenantUserLimit(uow);
        }

        private int? GetTenantUserLimit()
        {
            if (tenantAccessor == null)
                return null;

            var tenant = tenantAccessor.CurrentTenant;
            if (tenant == null || tenant.TenantId <= 0)
                return null;

            string plan = null;
            int? purchasedUsers = null;
            var originalTenant = tenantAccessor.CurrentTenant;

            try
            {
                tenantAccessor.CurrentTenant = null;

                using (var connection = _connections.NewByKey("Default"))
                {
                    var tenantRow = connection.TryById<TenantRow>(tenant.TenantId);
                    plan = tenantRow?.Plan?.Trim();
                    if (tenantRow?.PurchasedUsers > 0)
                        return tenantRow.PurchasedUsers;
                    purchasedUsers = tenantRow?.PurchasedUsers;
                }
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }

            if (purchasedUsers.HasValue && purchasedUsers.Value > 0)
                return purchasedUsers;

            return ResolveUserLimit(plan);
        }

        private int? ResolveUserLimit(string plan)
        {
            try
            {
                var planFields = ProductPlanRow.Fields;
                using var connection = _connections.NewFor<ProductPlanRow>();
                var plans = connection.List<ProductPlanRow>(q => q
                    .SelectTableFields()
                    .Where(planFields.IsActive == 1));

                if (!string.IsNullOrWhiteSpace(plan))
                {
                    var matchedPlan = plans.FirstOrDefault(p =>
                        string.Equals(p?.Name?.Trim(), plan.Trim(), StringComparison.OrdinalIgnoreCase));

                    if (matchedPlan?.UserLimit > 0)
                        return matchedPlan.UserLimit;
                }

                var fallbackPlan = plans
                    .Where(p => p?.UserLimit > 0)
                    .OrderBy(p => p.SortOrder ?? int.MaxValue)
                    .ThenBy(p => p.Id ?? int.MaxValue)
                    .FirstOrDefault();

                if (fallbackPlan?.UserLimit > 0)
                    return fallbackPlan.UserLimit;
            }
            catch
            {
                return null;
            }

            return null;
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
