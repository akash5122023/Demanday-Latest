using AdvanceCRM.Administration;
using Serenity;
using Serenity.Abstractions;
using Serenity.Data;
using Serenity.Services;
using Serenity.Localization;

namespace AdvanceCRM
{
    public class MultiCompanyBehavior : IImplicitBehavior,
        ISaveBehavior, IDeleteBehavior,
        IListBehavior, IRetrieveBehavior
    {
        private readonly IRequestContext _context;
        private Int32Field fldCompanyId;

        public MultiCompanyBehavior(IRequestContext context)
        {
            _context = context;
        }

        public bool ActivateFor(IRow row)
        {
            var mt = row as IMultiCompanyRow;
            if (mt == null)
                return false;

            fldCompanyId = mt.CompanyIdField;
            return true;
        }

        public void OnPrepareQuery(IRetrieveRequestHandler handler,
            SqlQuery query)
        {
            var perms = handler.Context?.Permissions ?? _context.Permissions;
            var user = (handler.Context?.User ?? _context.User)
                ?.ToUserDefinition();

            if (user != null && !perms.HasPermission(PermissionKeys.Company))
                query.Where(fldCompanyId == user.CompanyId);
        }

        public void OnPrepareQuery(IListRequestHandler handler,
            SqlQuery query)
        {
            var perms = handler.Context?.Permissions ?? _context.Permissions;
            var user = (handler.Context?.User ?? _context.User)
                ?.ToUserDefinition();

            if (user != null && !perms.HasPermission(PermissionKeys.Company))
                query.Where(fldCompanyId == user.CompanyId);
        }

        public void OnSetInternalFields(ISaveRequestHandler handler)
        {
            if (handler.IsCreate)
            {
                var user = (handler.Context?.User ?? _context.User)
                    ?.ToUserDefinition();

                if (user != null)
                {
                    var perms = handler.Context?.Permissions ?? _context.Permissions;
                    if (!perms.HasPermission(PermissionKeys.Company))
                        fldCompanyId[handler.Row] = user.CompanyId;
                }
            }
        }

        public void OnValidateRequest(ISaveRequestHandler handler)
        {
            if (handler.IsUpdate)
            {
                if (fldCompanyId[handler.Old] != fldCompanyId[handler.Row])
                {
                    var perms = handler.Context?.Permissions ?? _context.Permissions;
                    var localizer = handler.Context?.Localizer ?? _context.Localizer ?? NullTextLocalizer.Instance;
                    perms.ValidatePermission(PermissionKeys.Company, localizer);
                }
            }
        }

        public void OnValidateRequest(IDeleteRequestHandler handler)
        {
            var user = (handler.Context?.User ?? _context.User)
                ?.ToUserDefinition();
            if (user != null && fldCompanyId[handler.Row] != user.CompanyId)
            {
                var perms = handler.Context?.Permissions ?? _context.Permissions;
                var localizer = handler.Context?.Localizer ?? _context.Localizer ?? NullTextLocalizer.Instance;
                perms.ValidatePermission(PermissionKeys.Company, localizer);
            }
        }

        public void OnAfterDelete(IDeleteRequestHandler handler) { }
        public void OnAfterExecuteQuery(IRetrieveRequestHandler handler) { }
        public void OnAfterExecuteQuery(IListRequestHandler handler) { }
        public void OnAfterSave(ISaveRequestHandler handler) { }
        public void OnApplyFilters(IListRequestHandler handler, SqlQuery query) { }
        public void OnAudit(IDeleteRequestHandler handler) { }
        public void OnAudit(ISaveRequestHandler handler) { }
        public void OnBeforeDelete(IDeleteRequestHandler handler) { }
        public void OnBeforeExecuteQuery(IRetrieveRequestHandler handler) { }
        public void OnBeforeExecuteQuery(IListRequestHandler handler) { }
        public void OnBeforeSave(ISaveRequestHandler handler) { }
        public void OnPrepareQuery(IDeleteRequestHandler handler, SqlQuery query) { }
        public void OnPrepareQuery(ISaveRequestHandler handler, SqlQuery query) { }
        public void OnReturn(IDeleteRequestHandler handler) { }
        public void OnReturn(IRetrieveRequestHandler handler) { }
        public void OnReturn(IListRequestHandler handler) { }
        public void OnReturn(ISaveRequestHandler handler) { }
        public void OnValidateRequest(IRetrieveRequestHandler handler) { }
        public void OnValidateRequest(IListRequestHandler handler) { }
    }
}