/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />

namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()

    export class GrnProductsTwoEditorDialog extends Common.GridEditorDialog<GrnProductsTwoRow> {
        protected getFormKey() { return GrnProductsTwoForm.formKey; }
        protected getLocalTextPrefix() { return GrnProductsTwoRow.localTextPrefix; }
        protected getNameProperty() { return GrnProductsTwoRow.nameProperty; }
        protected form = new GrnProductsTwoForm(this.idPrefix);

        public ContactsIdD: number;
        public ChannelType: number;

        protected beforeLoadEntity(entity) {
            super.beforeLoadEntity(entity);

            if (this.ContactsIdD != null) {
                this.ChannelType = Contacts.ContactsRow.getLookup().itemById[this.ContactsIdD].ChannelCategory;
            }
        }


        constructor() {
            super();


            this.form.Code.change(e => {

                var code = Q.text(this.form.Code.value);

                if (code.trim() != "") {
                    this.form.ProductsId.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Id.toString();
                    //this.form.Mrp.value = Q.tryFirst(Products.InventoryRow.getLookup().items, x => x.Code == code).Mrp;
                    //this.form.SellingPrice.value = Q.tryFirst(Products.InventoryRow.getLookup().items, x => x.Code == code).SellingPrice;
                    //this.form.ProductsDivision.value = Q.tryFirst(Products.InventoryRow.getLookup().items, x => x.Code == code).DivisionProductsDivision;
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
                    //var T1 = Q.tryFirst(Products.InventoryRow.getLookup().items, x => x.Code == code).TaxId1;
                    //if (T1 != undefined) {
                    //    this.form.TaxType1.value = Masters.TaxRow.getLookup().itemById[T1].Type;
                    //    this.form.Percentage1.value = Masters.TaxRow.getLookup().itemById[T1].Percentage;
                    //}
                    //var T2 = Q.tryFirst(Products.InventoryRow.getLookup().items, x => x.Code == code).TaxId2;
                    //if (T2 != undefined) {
                    //    this.form.TaxType2.value = Masters.TaxRow.getLookup().itemById[T2].Type;
                    //    this.form.Percentage2.value = Masters.TaxRow.getLookup().itemById[T2].Percentage;
                    //}
                    //this.form.Description.value = Q.tryFirst(Products.InventoryRow.getLookup().items, x => x.Code == code).Description;
                    //var U = Q.tryFirst(Products.InventoryRow.getLookup().items, x => x.Code == code).UnitId;
                    //if (U != undefined) {
                    //    this.form.Unit.value = Masters.ProductsUnitRow.getLookup().itemById[U].ProductsUnit;
                    //}
                }
            });

            this.form.ProductsId.changeSelect2(e => {
                var PID = Q.toId(this.form.ProductsId.value);
                if (PID != null) {
                    this.form.Code.value = Products.ProductsRow.getLookup().itemById[PID].Code;
                    //this.form.Mrp.value = Products.InventoryRow.getLookup().itemById[PID].Mrp;

                    //StockDetails

                    Products.StockTransferService.CheckReorder({ Id: PID },
                        response => {
                            Q.notifyInfo("Total stock: " + response.Status.toString());

                            //  Q.alert("Consider reordering this product as it has reached minimum reorder stock\nCurrent stock = " + response.Status + ", Minimum stock required = " + Q.text(minstock.toString()));

                        });

                    ////

                    //this.form.SellingPrice.value = Products.InventoryRow.getLookup().itemById[PID].SellingPrice;
                    //this.form.ProductsDivision.value = Products.InventoryRow.getLookup().itemById[PID].DivisionProductsDivision;
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
                    //var T1 = Products.InventoryRow.getLookup().itemById[PID].TaxId1;
                    //if (T1 != undefined) {
                    //    this.form.TaxType1.value = Masters.TaxRow.getLookup().itemById[T1].Type;
                    //    this.form.Percentage1.value = Masters.TaxRow.getLookup().itemById[T1].Percentage;
                    //}
                    //var T2 = Products.InventoryRow.getLookup().itemById[PID].TaxId2;
                    //if (T2 != undefined) {
                    //    this.form.TaxType2.value = Masters.TaxRow.getLookup().itemById[T2].Type;
                    //    this.form.Percentage2.value = Masters.TaxRow.getLookup().itemById[T2].Percentage;
                    //}
                    //this.form.Description.value = Products.InventoryRow.getLookup().itemById[PID].Description;
                    //var U = Products.InventoryRow.getLookup().itemById[PID].UnitId;
                    //if (U != undefined) {
                    //    this.form.Unit.value = Masters.ProductsUnitRow.getLookup().itemById[U].ProductsUnit;

                    //}
                }
            });

            //this.form.Inclusive.change(e => {
            //    if (this.form.Inclusive.value == true) {
            //        var amount = this.form.Price.value;
            //        var tax = this.form.Percentage1.value + this.form.Percentage2.value;
            //        if (amount == null) {
            //            Q.alert("Price cannot be less than 0");
            //            return;
            //        }
            //        if (tax == null) {
            //            Q.alert("Tax cannot be less than 0");
            //            return;
            //        }
            //        this.form.Price.value = amount * 100 / (100 + tax);
            //    }
            //    else {
            //        var PID = Q.toId(this.form.ProductsId.value);
            //        if (PID != null) {
            //            this.form.Price.value = Products.InventoryRow.getLookup().itemById[PID].Mrp;
            //        }
            //    }
            //});

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
