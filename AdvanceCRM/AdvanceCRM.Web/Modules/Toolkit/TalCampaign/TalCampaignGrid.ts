
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class TalCampaignGrid extends GridBase<TalCampaignRow, any> {
        protected getColumnsKey() { return "Toolkit.TalCampaign" }
        protected getDialogType() { return TalCampaignDialog; }
        protected getIdProperty() { return TalCampaignRow.idProperty; }
        protected getInsertPermission() { return TalCampaignRow.insertPermission; }
        protected getLocalTextPrefix() { return TalCampaignRow.localTextPrefix; }
        protected getService() { return TalCampaignService.baseUrl; }

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
            var fld = TalCampaignRow.Fields;

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

        protected getButtons() {
            var buttons = super.getButtons();

            if (Q.Authorization.hasPermission("TalCampaign:Import")) {
                buttons.push({
                    title: 'Import',
                    cssClass: 'export-xlsx-button',
                    hint: "Import Tal Campaign from Excel",
                    onClick: () => {
                        var dialog = new TalCampaignExcelImportDialog();
                        dialog.element.on('dialogclose', () => {
                            this.refresh();
                            dialog = null;
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