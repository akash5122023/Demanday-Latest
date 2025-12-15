
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />

namespace AdvanceCRM.Quotation {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class QuotationProductsEditorDialog extends Common.GridEditorDialog<QuotationProductsRow> {
        protected getFormKey() { return QuotationProductsForm.formKey; }
        protected getLocalTextPrefix() { return QuotationProductsRow.localTextPrefix; }
        protected getNameProperty() { return QuotationProductsRow.nameProperty; }
        protected form = new QuotationProductsForm(this.idPrefix);

        public ContactsIdD: number;
        public ChannelType: number;

        protected beforeLoadEntity(entity) {
            super.beforeLoadEntity(entity);

            if (this.ContactsIdD != null) {
                this.ChannelType = Contacts.ContactsRow.getLookup().itemById[this.ContactsIdD].ChannelCategory;
            }
            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].CapacityInProducts == true) {
            //    this.form.Capacity.getGridField().toggle(true);
            //} else { this.form.Capacity.getGridField().toggle(false); }

           
        }

        constructor() {
            super();

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].Capacity != true) {
                this.form.Capacity.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].MRP != true) {
                this.form.Mrp.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].SellingPrice != true) {
                this.form.SellingPrice.getGridField().toggle(false);
            }
            
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
                this.form.FileAttachment.getGridField().toggle(false);
            
          
           

            //this.element.find(".category-title:contains('Travels')").parent().toggle(false);
            //this.form.From.getGridField().toggle(false);
            //this.form.To.getGridField().toggle(false);
            //this.form.Date.getGridField().toggle(false);
            //this.element.find(".category-title:contains('Hotels')").parent().toggle(false);
            //this.form.Destination.getGridField().toggle(false);
            //this.form.HotelName.getGridField().toggle(false);
            //this.form.MealPlan.getGridField().toggle(false);
            //this.form.Nights.getGridField().toggle(false);
            //this.form.Adults.getGridField().toggle(false);
            //this.form.Childrens.getGridField().toggle(false);
            
            this.form.Code.change(e => {

                var code = Q.text(this.form.Code.value);

                if (code.trim() != "") {
                    this.form.ProductsId.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Id.toString();

                    this.form.From.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).From;
                    this.form.To.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).To;
                    this.form.Date.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Date;
                    this.form.Destination.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Destination;
                    this.form.HotelName.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).HotelName;
                    this.form.MealPlan.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).MealPlan;
                    this.form.Nights.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Nights;
                    this.form.Adults.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Adults;
                    this.form.Childrens.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Childrens;



                    this.form.Mrp.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Mrp;
                    this.form.SellingPrice.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).SellingPrice;
                    this.form.ProductsDivision.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).DivisionProductsDivision;
                    if (this.form.Price.value <= 0) {
                        //Set if channel customer
                        if (this.ChannelType != null) {
                            if (this.ChannelType == 1) {
                                this.form.Price.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Mrp;
                            }
                            else if (this.ChannelType == 2) {
                                this.form.Price.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).ChannelCustomerPrice;
                            }
                            else if (this.ChannelType == 3) {
                                this.form.Price.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).ResellerPrice;
                            }
                            else if (this.ChannelType == 4) {
                                this.form.Price.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).WholesalerPrice;
                            }
                            else if (this.ChannelType == 5) {
                                this.form.Price.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).DealerPrice;
                            }
                            else if (this.ChannelType == 6) {
                                this.form.Price.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).DistributorPrice;
                            }
                            else if (this.ChannelType == 7) {
                                this.form.Price.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).StockiestPrice;
                            }
                            else if (this.ChannelType == 8) {
                                this.form.Price.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).NationalDistributorPrice;
                            }
                        }
                        else {
                            this.form.Price.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Mrp;
                        }
                    }
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
                    this.form.Description.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Description;
                   

                    var U = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).UnitId;
                    if (U != undefined) {
                        this.form.Unit.value = Masters.ProductsUnitRow.getLookup().itemById[U].ProductsUnit;
                    }
                }
            });

            this.form.ProductsId.changeSelect2(e => {
                var PID = Q.toId(this.form.ProductsId.value);
                const product = Products.ProductsRow.getLookup().itemById[PID];

                if (PID != null) {
                    this.form.Code.value = Products.ProductsRow.getLookup().itemById[PID].Code;
                    this.form.Mrp.value = Products.ProductsRow.getLookup().itemById[PID].Mrp;
                    var minstock = Products.ProductsRow.getLookup().itemById[PID].MinimumStock;
                    //StockDetails

                    Products.StockTransferService.CheckReorder({ Id: PID },
                        response => {
                            Q.notifyInfo("Total stock: " + response.Status.toString());
                            if (response.Status < minstock.toString()) {
                                Q.alert("Consider reordering this product as it has reached minimum reorder stock\nCurrent stock = " + response.Status + ", Minimum stock required = " + Q.text(minstock.toString()));
                            }


                        });

                    ////

                    this.form.SellingPrice.value = Products.ProductsRow.getLookup().itemById[PID].SellingPrice;
                    this.form.ProductsDivision.value = Products.ProductsRow.getLookup().itemById[PID].DivisionProductsDivision;
                    if (this.form.Price.value <= 0) {
                        //Set if channel customer
                        if (this.ChannelType != null) {
                            if (this.ChannelType == 1) {
                                this.form.Price.value = Products.ProductsRow.getLookup().itemById[this.form.ProductsId.value].Mrp;
                            }
                            else if (this.ChannelType == 2) {
                                this.form.Price.value = Products.ProductsRow.getLookup().itemById[this.form.ProductsId.value].ChannelCustomerPrice;
                            }
                            else if (this.ChannelType == 3) {
                                this.form.Price.value = Products.ProductsRow.getLookup().itemById[this.form.ProductsId.value].ResellerPrice;
                            }
                            else if (this.ChannelType == 4) {
                                this.form.Price.value = Products.ProductsRow.getLookup().itemById[this.form.ProductsId.value].WholesalerPrice;
                            }
                            else if (this.ChannelType == 5) {
                                this.form.Price.value = Products.ProductsRow.getLookup().itemById[this.form.ProductsId.value].DealerPrice;
                            }
                            else if (this.ChannelType == 6) {
                                this.form.Price.value = Products.ProductsRow.getLookup().itemById[this.form.ProductsId.value].DistributorPrice;
                            }
                            else if (this.ChannelType == 7) {
                                this.form.Price.value = Products.ProductsRow.getLookup().itemById[this.form.ProductsId.value].StockiestPrice;
                            }
                            else if (this.ChannelType == 8) {
                                this.form.Price.value = Products.ProductsRow.getLookup().itemById[this.form.ProductsId.value].NationalDistributorPrice;
                               
                            }
                        }
                        else {
                            this.form.Price.value = Products.ProductsRow.getLookup().itemById[this.form.ProductsId.value].Mrp;
                        }
                    }
                    var T1 = Products.ProductsRow.getLookup().itemById[PID].TaxId1;
                    if (T1 != undefined) {
                        this.form.TaxType1.value = Masters.TaxRow.getLookup().itemById[T1].Type;
                        this.form.Percentage1.value = Masters.TaxRow.getLookup().itemById[T1].Percentage;
                    }
                    var T2 = Products.ProductsRow.getLookup().itemById[PID].TaxId2;
                    if (T2 != undefined) {
                        this.form.TaxType2.value = Masters.TaxRow.getLookup().itemById[T2].Type;
                        this.form.Percentage2.value = Masters.TaxRow.getLookup().itemById[T2].Percentage;
                    }
                    this.form.Description.value = Products.ProductsRow.getLookup().itemById[PID].Description;
                    //if (product.From != null) {
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
                   
                    //if (product.Destination != null ) {

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
                   
                    

                    var U = Products.ProductsRow.getLookup().itemById[PID].UnitId;
                    if (U != undefined) {
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

        onDialogOpen() {
            super.onDialogOpen();

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].Code != true) {
                this.form.Code.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].Unit != true) {
                this.form.Unit.getGridField().toggle(false);
            }
            //var PID = Q.toId(this.form.ProductsId.value);
            //if (PID != null) {
            //    this.form.Code.value = Products.ProductsRow.getLookup().itemById[PID].Code;
            //}
        }

    }
}