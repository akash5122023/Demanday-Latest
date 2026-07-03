
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