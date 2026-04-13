
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class ClientSupressionGrid extends GridBase<ClientSupressionRow, any> {
        protected getColumnsKey() { return "Toolkit.ClientSupression" }
        protected getDialogType() { return ClientSupressionDialog; }
        protected getIdProperty() { return ClientSupressionRow.idProperty; }
        protected getInsertPermission() { return ClientSupressionRow.insertPermission; }
        protected getLocalTextPrefix() { return ClientSupressionRow.localTextPrefix; }
        protected getService() { return ClientSupressionService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = ClientSupressionRow.Fields;

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

            return filters;
        }

        protected getButtons(): Serenity.ToolButton[] {
            var buttons = super.getButtons();

            buttons.push({
                title: 'Import from Excel',
                cssClass: 'export-xlsx-button',
                onClick: () => {
                    var dialog = new ClientSupressionExcelImportDialog();
                    dialog.element.on('dialogclose', () => {
                        this.refresh();
                    });
                    dialog.dialogOpen();
                },
                separator: true
            });

            return buttons;
        }
    }
}