
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

        // Show up to 100000 rows on a single page (effectively "all" for this module).
        protected getViewOptions() {
            let opt = super.getViewOptions();
            opt.rowsPerPage = 100000;
            return opt;
        }

        // Make 100000 an actual, selected option in the pager's page-size dropdown
        // (otherwise the dropdown shows blank since 100000 isn't a default option).
        protected getPagerOptions() {
            let opt = super.getPagerOptions();
            opt.rowsPerPage = 100000;
            opt.rowsPerPageOptions = [2500, 5000, 10000, 50000, 100000];
            return opt;
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
