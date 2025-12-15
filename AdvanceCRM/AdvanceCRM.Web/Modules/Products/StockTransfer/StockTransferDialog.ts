
namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class StockTransferDialog extends DialogBase<StockTransferRow, any> {
        protected getFormKey() { return StockTransferForm.formKey; }
        protected getIdProperty() { return StockTransferRow.idProperty; }
        protected getLocalTextPrefix() { return StockTransferRow.localTextPrefix; }
        protected getNameProperty() { return StockTransferRow.nameProperty; }
        protected getService() { return StockTransferService.baseUrl; }
        protected getDeletePermission() { return StockTransferRow.deletePermission; }
        protected getInsertPermission() { return StockTransferRow.insertPermission; }
        protected getUpdatePermission() { return StockTransferRow.updatePermission; }

        protected form = new StockTransferForm(this.idPrefix);
        constructor() {
            super();
        }

        protected updateInterface() {
            super.updateInterface();

            this.toolbar.findButton('export-pdf-button').toggle(this.isEditMode());
            //this.toolbar.findButton('mail-button').toggle(this.isEditMode());
            //this.toolbar.findButton('send-button').toggle(this.isEditMode());
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'StockTransfer.PrintStockTransfer',
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));

            return buttons;
        }

        onDialogOpen() {
            super.onDialogOpen();

            this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();

            if (this.form.RepresentativeId.value < "1") {
                this.form.RepresentativeId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            Serenity.EditorUtils.setReadonly(this.form.RepresentativeId.element, true);

            //this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();
        }
    }
}