
namespace AdvanceCRM.Services {

    import fld = AMCRow.Fields;
    
    @Serenity.Decorators.registerClass()
    export class AMCGrid extends GridBase<AMCRow, any> {
        protected getColumnsKey() { return 'Services.AMC'; }
        protected getDialogType() { return AMCDialog; }
        protected getIdProperty() { return AMCRow.idProperty; }
        protected getInsertPermission() { return AMCRow.insertPermission; }
        protected getLocalTextPrefix() { return AMCRow.localTextPrefix; }
        protected getService() { return AMCService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            var q = Q.parseQueryString();
            if (q["Open"]) {
                this.findQuickFilter(Serenity.EnumEditor, fld.Status).value = "1";
                var user = Q.tryFirst(Administration.UserRow.getLookup().items, x => x.Username == Q.Authorization.userDefinition.Username);
                this.findQuickFilter(Administration.UserEditor, fld.AssignedId).value = Q.toId(user.UserId);
            }
            else if (q["OpenTeam"]) {
                this.findQuickFilter(Serenity.EnumEditor, fld.Status).value = "1";
            }

            this.element.find('.quick-filters-bar').toggle(false)
        }
    }
}