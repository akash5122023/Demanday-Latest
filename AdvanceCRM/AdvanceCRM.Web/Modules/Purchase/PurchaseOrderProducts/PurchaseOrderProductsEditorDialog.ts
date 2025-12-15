
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />

namespace AdvanceCRM.Purchase {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class PurchaseOrderProductsEditorDialog extends Common.GridEditorDialog<PurchaseOrderProductsRow> {
        protected getFormKey() { return PurchaseOrderProductsForm.formKey; }
                protected getLocalTextPrefix() { return PurchaseOrderProductsRow.localTextPrefix; }
        protected form = new PurchaseOrderProductsForm(this.idPrefix);

        constructor() {
            super();

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].Unit != true) {
                this.form.Unit.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].Travels != true) {
                this.form.From.getGridField().toggle(false);
                this.form.To.getGridField().toggle(false);
                this.form.Destination.getGridField().toggle(false);
                this.form.HotelName.getGridField().toggle(false);
                this.form.Nights.getGridField().toggle(false);
                this.form.Adults.getGridField().toggle(false);
                this.form.Childrens.getGridField().toggle(false);
                this.form.MealPlan.getGridField().toggle(false);
                this.element.find(".category-title:contains('Travels')").parent().toggle(false);
                this.element.find(".category-title:contains('Hotels')").parent().toggle(false);
            }

            this.element.find(".category-title:contains('Travels')").parent().toggle(false);
            this.form.From.getGridField().toggle(false);
            this.form.To.getGridField().toggle(false);
            this.form.Date.getGridField().toggle(false);
            this.element.find(".category-title:contains('Hotels')").parent().toggle(false);
            this.form.Destination.getGridField().toggle(false);
            this.form.HotelName.getGridField().toggle(false);
            this.form.MealPlan.getGridField().toggle(false);
            this.form.Nights.getGridField().toggle(false);
            this.form.Adults.getGridField().toggle(false);
            this.form.Childrens.getGridField().toggle(false);

            
            this.form.ProductsId.changeSelect2(e => {
                var PID = Q.toId(this.form.ProductsId.value);
                const product = Products.ProductsRow.getLookup().itemById[PID];
              

                if (PID != null) {

                    //if (product.From != undefined) {
                    //    this.element.find(".category-title:contains('Travels')").parent().toggle(true);
                    //    this.form.From.getGridField().toggle(true);
                    //    this.form.To.getGridField().toggle(true);
                    //    this.form.Date.getGridField().toggle(true);
                    //    this.form.From.value = Products.ProductsRow.getLookup().itemById[PID].From;
                    //    this.form.To.value = Products.ProductsRow.getLookup().itemById[PID].To;
                    //    this.form.Date.value = Products.ProductsRow.getLookup().itemById[PID].Date;
                    //}
                    //else {
                    //    this.element.find(".category-title:contains('Travels')").parent().toggle(false);
                    //    this.form.From.getGridField().toggle(false);
                    //    this.form.To.getGridField().toggle(false);
                    //    this.form.Date.getGridField().toggle(false);
                    //}
                    //if (product.Destination != undefined) {

                    //    this.element.find(".category-title:contains('Hotels')").parent().toggle(true);
                    //    this.form.Destination.getGridField().toggle(true);
                    //    this.form.HotelName.getGridField().toggle(true);
                    //    this.form.MealPlan.getGridField().toggle(true);
                    //    this.form.Nights.getGridField().toggle(true);
                    //    this.form.Adults.getGridField().toggle(true);
                    //    this.form.Childrens.getGridField().toggle(true);

                    //    this.form.Destination.value = Products.ProductsRow.getLookup().itemById[PID].Destination;
                    //    this.form.HotelName.value = Products.ProductsRow.getLookup().itemById[PID].HotelName;
                    //    this.form.MealPlan.value = Products.ProductsRow.getLookup().itemById[PID].MealPlan;
                    //    this.form.Nights.value = Products.ProductsRow.getLookup().itemById[PID].Nights;
                    //    this.form.Adults.value = Products.ProductsRow.getLookup().itemById[PID].Adults;
                    //    this.form.Childrens.value = Products.ProductsRow.getLookup().itemById[PID].Childrens;
                    //}
                    //else {
                    //    this.element.find(".category-title:contains('Hotels')").parent().toggle(false);
                    //    this.form.Destination.getGridField().toggle(false);
                    //    this.form.HotelName.getGridField().toggle(false);
                    //    this.form.MealPlan.getGridField().toggle(false);
                    //    this.form.Nights.getGridField().toggle(false);
                    //    this.form.Adults.getGridField().toggle(false);
                    //    this.form.Childrens.getGridField().toggle(false);
                    //}

                    if (Administration.CompanyDetailsRow.getLookup().itemById[1].Travels != false) {
                        if (product.From != undefined) {
                            this.element.find(".category-title:contains('Travels')").parent().toggle(true);
                            this.form.From.getGridField().toggle(true);
                            this.form.To.getGridField().toggle(true);
                            this.form.Date.getGridField().toggle(true);
                            this.form.From.value = Products.ProductsRow.getLookup().itemById[PID].From;
                            this.form.To.value = Products.ProductsRow.getLookup().itemById[PID].To;
                            this.form.Date.value = Products.ProductsRow.getLookup().itemById[PID].Date;
                        }
                        else {
                            this.element.find(".category-title:contains('Travels')").parent().toggle(false);
                            this.form.From.getGridField().toggle(false);
                            this.form.To.getGridField().toggle(false);
                            this.form.Date.getGridField().toggle(false);
                        }
                        if (product.Destination != undefined) {

                            this.element.find(".category-title:contains('Hotels')").parent().toggle(true);
                            this.form.Destination.getGridField().toggle(true);
                            this.form.HotelName.getGridField().toggle(true);
                            this.form.MealPlan.getGridField().toggle(true);
                            this.form.Nights.getGridField().toggle(true);
                            this.form.Adults.getGridField().toggle(true);
                            this.form.Childrens.getGridField().toggle(true);

                            this.form.Destination.value = Products.ProductsRow.getLookup().itemById[PID].Destination;
                            this.form.HotelName.value = Products.ProductsRow.getLookup().itemById[PID].HotelName;
                            this.form.MealPlan.value = Products.ProductsRow.getLookup().itemById[PID].MealPlan;
                            this.form.Nights.value = Products.ProductsRow.getLookup().itemById[PID].Nights;
                            this.form.Adults.value = Products.ProductsRow.getLookup().itemById[PID].Adults;
                            this.form.Childrens.value = Products.ProductsRow.getLookup().itemById[PID].Childrens;
                        }
                        else {
                            this.element.find(".category-title:contains('Hotels')").parent().toggle(false);
                            this.form.Destination.getGridField().toggle(false);
                            this.form.HotelName.getGridField().toggle(false);
                            this.form.MealPlan.getGridField().toggle(false);
                            this.form.Nights.getGridField().toggle(false);
                            this.form.Adults.getGridField().toggle(false);
                            this.form.Childrens.getGridField().toggle(false);
                        }
                    }


                    this.form.Price.value = Products.ProductsRow.getLookup().itemById[PID].PurchasePrice;
                    var T1 = Products.ProductsRow.getLookup().itemById[PID].TaxId1;
                    this.form.TaxType1.value = Masters.TaxRow.getLookup().itemById[T1].Type
                    this.form.Percentage1.value = Masters.TaxRow.getLookup().itemById[T1].Percentage
                    var T2 = Products.ProductsRow.getLookup().itemById[PID].TaxId2;
                    this.form.TaxType2.value = Masters.TaxRow.getLookup().itemById[T2].Type
                    this.form.Percentage2.value = Masters.TaxRow.getLookup().itemById[T2].Percentage
                    var U = Products.ProductsRow.getLookup().itemById[PID].UnitId;
                    if (U != null && Masters.ProductsUnitRow.getLookup().itemById[U]) {
                        this.form.Unit.value = Masters.ProductsUnitRow.getLookup().itemById[U].ProductsUnit;
                    }
                  
                   
  
                }
              
            });

            this.form.Inclusive.change(e => {
                if (this.form.Inclusive.value == true) {
                    var amount = this.form.Price.value;
                    var tax = this.form.Percentage1.value + this.form.Percentage2.value;
                    if (amount == null) {
                        Q.alert("Price cannot be less than 0");
                        return;
                    }
                    if (tax == null) {
                        Q.alert("Tax cannot be less than 0");
                        return;
                    }
                    this.form.Price.value = amount * 100 / (100 + tax);
                }
                else {
                    var PID = Q.toId(this.form.ProductsId.value);
                    if (PID != null) {
                        this.form.Price.value = Products.ProductsRow.getLookup().itemById[PID].Mrp;
                    }
                }
            });

        }

        protected getDialogOptions() {
            let opt = super.getDialogOptions();
            opt.width = "800px";
            return opt;
        }

    }
}