
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

            return filters;
        }

        protected getButtons() {
            var buttons = super.getButtons();

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

            return buttons;
        }
    }
}