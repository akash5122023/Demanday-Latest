
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class RawTelecallGrid extends GridBase<RawTelecallRow, any> {
        protected getColumnsKey() { return 'ThirdParty.RawTelecall'; }
        protected getDialogType() { return RawTelecallDialog; }
        protected getIdProperty() { return RawTelecallRow.idProperty; }
        protected getInsertPermission() { return RawTelecallRow.insertPermission; }
        protected getLocalTextPrefix() { return RawTelecallRow.localTextPrefix; }
        protected getService() { return RawTelecallService.baseUrl; }

        constructor(container: JQuery) {
            super(container);


        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.push({
                title: 'Import',
                cssClass: 'export-xlsx-button',
                hint: "Import Enquiries",
                onClick: () => {
                    // open import dialog, let it handle rest
                    var dialog = new RawTelecallImportDialog();
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