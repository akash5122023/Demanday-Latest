
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class MasterSupressionGrid extends GridBase<MasterSupressionRow, any> {
        protected getColumnsKey() { return "Toolkit.MasterSupression" }
        protected getDialogType() { return MasterSupressionDialog; }
        protected getIdProperty() { return MasterSupressionRow.idProperty; }
        protected getInsertPermission() { return MasterSupressionRow.insertPermission; }
        protected getLocalTextPrefix() { return MasterSupressionRow.localTextPrefix; }
        protected getService() { return MasterSupressionService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = MasterSupressionRow.Fields;

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
            //buttons.shift();

            if (Q.Authorization.hasPermission("MasterSupression:Import")) {
                buttons.push({
                    title: 'Import',
                    cssClass: 'export-xlsx-button',
                    hint: "Import Master Suppression from Excel",
                    onClick: () => {
                        var dialog = new MasterSupressionExcelImportDialog();
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