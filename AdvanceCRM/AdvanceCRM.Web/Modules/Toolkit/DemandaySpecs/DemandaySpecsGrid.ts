
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class DemandaySpecsGrid extends GridBase<DemandaySpecsRow, any> {
        protected getColumnsKey() { return "Toolkit.DemandaySpecs"; }
        protected getDialogType() { return DemandaySpecsDialog; }
        protected getIdProperty() { return DemandaySpecsRow.idProperty; }
        protected getInsertPermission() { return DemandaySpecsRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandaySpecsRow.localTextPrefix; }
        protected getService() { return DemandaySpecsService.baseUrl; }

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
            var fld = DemandaySpecsRow.Fields;

            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: "Masters.DemandayMasterAccount"
                },
                field: fld.MasterAccountId,
                title: 'Master Account'
            });

            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: "Masters.DemandayCampaignId",
                    cascadeFrom: fld.MasterAccountId,
                    cascadeField: "DemandayMasterAccountId"
                },
                field: fld.CampaignId,
                title: 'Campaign'
            });

            return this.orderQuickFilters(filters);
        }

        protected getButtons(): Serenity.ToolButton[] {
            var buttons = super.getButtons();

            if (Q.Authorization.hasPermission("DemandaySpecs:Import")) {
                buttons.push({
                    title: 'Import from Excel',
                    cssClass: 'export-xlsx-button',
                    onClick: () => {
                        var dialog = new DemandaySpecsExcelImportDialog();
                        dialog.element.on('dialogclose', () => {
                            this.refresh();
                        });
                        dialog.dialogOpen();
                    },
                    separator: true
                });
            }

            return buttons;
        }
    }
}