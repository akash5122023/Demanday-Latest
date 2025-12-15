
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />

namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class AMCProductsEditorDialog extends Common.GridEditorDialog<AMCProductsRow> {
        protected getFormKey() { return AMCProductsForm.formKey; }
        protected getLocalTextPrefix() { return AMCProductsRow.localTextPrefix; }
        protected getNameProperty() { return AMCProductsRow.nameProperty; }
        protected form = new AMCProductsForm(this.idPrefix);

        constructor() {
            super();

            this.form.Code.change(e => {
                var code = Q.text(this.form.Code.value);

                if (code.trim() != "") {
                    this.form.ProductsId.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Id.toString();
                    //if (this.form.Rate.value <= 0) {
                    //    this.form.Rate.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Mrp;
                    //}
                    var T1 = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).TaxId1;
                    if (T1 != undefined) {
                        this.form.TaxType1.value = Masters.TaxRow.getLookup().itemById[T1].Type;
                        this.form.Percentage1.value = Masters.TaxRow.getLookup().itemById[T1].Percentage;
                    }
                    var T2 = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).TaxId2;
                    if (T2 != undefined) {
                        this.form.TaxType2.value = Masters.TaxRow.getLookup().itemById[T2].Type;
                        this.form.Percentage2.value = Masters.TaxRow.getLookup().itemById[T2].Percentage;
                    }
                }
            });

            this.form.Quantity.getGridField().toggle(false);
            this.form.Visits.getGridField().toggle(false);

            this.form.Type.changeSelect2(e => {
                if (this.form.Type.value == "1") {
                    this.form.Quantity.getGridField().toggle(true);
                    this.form.Visits.getGridField().toggle(false);
                }
                else if (this.form.Type.value == "2") {
                    this.form.Quantity.getGridField().toggle(false);
                    this.form.Visits.getGridField().toggle(true);
                }
                else if (this.form.Type.value == "3") {
                    this.form.Quantity.getGridField().toggle(true);
                    this.form.Visits.getGridField().toggle(true);
                }
                else {
                    this.form.Quantity.getGridField().toggle(false);
                    this.form.Visits.getGridField().toggle(false);
                }
            });

            this.form.ProductsId.changeSelect2(e => {
                var PID = Serenity.EditorUtils.getValue(this.form.ProductsId);
                if (PID != null) {
                    //if (this.form.Rate.value <= 0) {
                    //    this.form.Rate.value = Products.ProductsRow.getLookup().itemById[PID].Mrp;
                    //}
                    var T1 = Products.ProductsRow.getLookup().itemById[PID].TaxId1;
                    this.form.TaxType1.value = Masters.TaxRow.getLookup().itemById[T1].Type
                    this.form.Percentage1.value = Masters.TaxRow.getLookup().itemById[T1].Percentage
                    var T2 = Products.ProductsRow.getLookup().itemById[PID].TaxId2;
                    this.form.TaxType2.value = Masters.TaxRow.getLookup().itemById[T2].Type
                    this.form.Percentage2.value = Masters.TaxRow.getLookup().itemById[T2].Percentage
                }
            });

            this.form.Inclusive.change(e => {
                if (this.form.Inclusive.value == true) {
                    var amount = this.form.Rate.value;
                    var tax = this.form.Percentage1.value + this.form.Percentage2.value;
                    if (amount == null) {
                        Q.alert("Rate cannot be less than 0");
                        return;
                    }
                    if (tax == null) {
                        Q.alert("Tax cannot be less than 0");
                        return;
                    }
                    this.form.Rate.value = amount * 100 / (100 + tax);
                }
                else {
                    var PID = Q.toId(this.form.ProductsId.value);
                    if (PID != null) {
                        this.form.Rate.value = Products.ProductsRow.getLookup().itemById[PID].Mrp;
                    }
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

            var PID = Q.toId(this.form.ProductsId.value);
            if (PID != null) {
                this.form.Code.value = Products.ProductsRow.getLookup().itemById[PID].Code;
            }
        }
    }
}