
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class PurchaseOrderDialog extends DialogBase<PurchaseOrderRow, any> {
        protected getFormKey() { return PurchaseOrderForm.formKey; }
        protected getIdProperty() { return PurchaseOrderRow.idProperty; }
        protected getLocalTextPrefix() { return PurchaseOrderRow.localTextPrefix; }
        protected getNameProperty() { return PurchaseOrderRow.nameProperty; }
        protected getService() { return PurchaseOrderService.baseUrl; }
        protected getDeletePermission() { return PurchaseOrderRow.deletePermission; }
        protected getInsertPermission() { return PurchaseOrderRow.insertPermission; }
        protected getUpdatePermission() { return PurchaseOrderRow.updatePermission; }

        protected form = new PurchaseOrderForm(this.idPrefix);
        constructor() {
            super();

            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));



            this.form.CurrencyConversion.change(e => {
                if (this.form.CurrencyConversion.value == true) {
                    this.form.Conversion.getGridField().toggle(true);
                    this.form.FromCurrency.getGridField().toggle(true);
                    this.form.ToCurrency.getGridField().toggle(true);
                }
                else {
                    this.form.Conversion.getGridField().toggle(false);
                    this.form.FromCurrency.getGridField().toggle(false);
                    this.form.ToCurrency.getGridField().toggle(false);
                }
            });



        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'PurchaseOrder.PrintPurchaseOrder',
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                reportKey: 'PurchaseOrder.PrintPurchaseOrder',
                getParams: () => ({ Id: this.get_entityId(), ModType: "PurchaseOrder" }), //ModType Quotation, Invoice
                download: true //Set true if have to send mail
            }));

            return buttons;
        }

        protected updateInterface() {
            super.updateInterface();

            this.toolbar.findButton('export-pdf-button').toggle(this.isEditMode());
            this.toolbar.findButton('mail-button').toggle(this.isEditMode());
            this.toolbar.findButton('send-button').toggle(this.isEditMode());
        }

        onDialogOpen() {
            super.onDialogOpen();
            this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();
            if (this.form.OwnerId.value < "1") {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < "1") {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].RoundupInPurchase != true) {
                this.form.Roundup.getGridField().toggle(false);
            }

            if (this.form.PurchaseOrderNo.value < 1) {
                // call our service, see CustomerEndpoint.cs and CustomerRepository.cs
                PurchaseOrderService.GetNextNumber({
                    Prefix: '',
                    Length: 5
                }, response => {
                    this.form.PurchaseOrderNo.value = Q.toId(response.Serial);
                });
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].MultiCurrency != true) {
                this.form.CurrencyConversion.getGridField().toggle(false);
                this.form.Conversion.getGridField().toggle(false);
                this.form.FromCurrency.getGridField().toggle(false);
                this.form.ToCurrency.getGridField().toggle(false);
            }

            if (this.form.CurrencyConversion.value == true) {
                this.form.Conversion.getGridField().toggle(true);
                this.form.FromCurrency.getGridField().toggle(true);
                this.form.ToCurrency.getGridField().toggle(true);
            }
            else {
                this.form.Conversion.getGridField().toggle(false);
                this.form.FromCurrency.getGridField().toggle(false);
                this.form.ToCurrency.getGridField().toggle(false);
            }

        }
    }
}