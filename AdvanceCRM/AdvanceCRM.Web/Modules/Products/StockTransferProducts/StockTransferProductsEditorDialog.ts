
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />

namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class StockTransferProductsEditorDialog extends Common.GridEditorDialog<StockTransferProductsRow> {
        protected getFormKey() { return StockTransferProductsForm.formKey; }
        protected getLocalTextPrefix() { return StockTransferProductsRow.localTextPrefix; }
        protected getNameProperty() { return StockTransferProductsRow.nameProperty; }
        protected form = new StockTransferProductsForm(this.idPrefix);

        constructor() {
            super();

            this.form.ProductsId.changeSelect2(e => {
                var PID = Q.toId(this.form.ProductsId.value);
                if (PID != null) {
                    if (this.form.TransferPrice.value <= 0) {
                        this.form.TransferPrice.value = Products.ProductsRow.getLookup().itemById[PID].Mrp;
                    }
                    var T1 = Products.ProductsRow.getLookup().itemById[PID].TaxId1;
                    this.form.TaxType1.value = Masters.TaxRow.getLookup().itemById[T1].Type
                    this.form.Percentage1.value = Masters.TaxRow.getLookup().itemById[T1].Percentage
                    var T2 = Products.ProductsRow.getLookup().itemById[PID].TaxId2;
                    this.form.TaxType2.value = Masters.TaxRow.getLookup().itemById[T2].Type
                    this.form.Percentage2.value = Masters.TaxRow.getLookup().itemById[T2].Percentage
                }
            });
        }

        protected getDialogOptions() {
            let opt = super.getDialogOptions();
            opt.width = "800px";
            return opt;
        }

        onDialogOpen() {
            super.onDialogOpen();
            this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].TaxInStockTransfer == true) {
                this.form.TaxType1.getGridField().toggle(false);
                this.form.Percentage1.getGridField().toggle(false);
                this.form.TaxType2.getGridField().toggle(false);
                this.form.Percentage2.getGridField().toggle(false);
            }

        }
    }
}