
namespace AdvanceCRM.EBBCheck {

    @Serenity.Decorators.registerClass()
    export class EBBCheckGrid extends GridBase<EBBCheckRow, any> {
        protected getColumnsKey() { return "EBBCheck.EBBCheck"; }
        protected getDialogType() { return EBBCheckDialog; }
        protected getIdProperty() { return EBBCheckRow.idProperty; }
        protected getInsertPermission() { return EBBCheckRow.insertPermission; }
        protected getLocalTextPrefix() { return EBBCheckRow.localTextPrefix; }
        protected getService() { return EBBCheckService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = EBBCheckRow.Fields;

            // Quality team can quickly filter by Status.
            filters.push({
                type: Serenity.EnumEditor,
                options: { enumKey: 'EBBCheck.EbbStatus' },
                field: fld.Status,
                title: 'Status'
            });

            return this.orderQuickFilters(filters);
        }
    }
}
