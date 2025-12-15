/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />

namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class InwardProductsEditorDialog extends Common.GridEditorDialog<InwardProductsRow> {
        protected getFormKey() { return InwardProductsForm.formKey; }
        protected getLocalTextPrefix() { return InwardProductsRow.localTextPrefix; }
        protected getNameProperty() { return InwardProductsRow.nameProperty; }

        protected form = new InwardProductsForm(this.idPrefix);
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
                    this.form.Mrp.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Mrp;
                    this.form.SellingPrice.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).SellingPrice;
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
                    this.form.Description.value = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).Description;
                    var U = Q.tryFirst(Products.ProductsRow.getLookup().items, x => x.Code == code).UnitId;
                    if (U != undefined) {
                        this.form.Unit.value = Masters.ProductsUnitRow.getLookup().itemById[U].ProductsUnit;
                    }
                }
            });

            this.form.ProductsId.changeSelect2(e => {
                var PID = Q.toId(this.form.ProductsId.value);
                if (PID != null) {

                    var minstock = Products.ProductsRow.getLookup().itemById[PID].Quantity;
                    if ((minstock != 0) || (minstock != null)) {
                        Products.StockTransferService.CheckReorder({ Id: PID },
                            response => {
                                Q.notifyInfo("Total stock: " + response.Status);
                            });
                    }

                    this.form.Mrp.value = Products.ProductsRow.getLookup().itemById[PID].Mrp;
                    this.form.SellingPrice.value = Products.ProductsRow.getLookup().itemById[PID].SellingPrice;
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
                    this.form.Description.value = Products.ProductsRow.getLookup().itemById[PID].Description;
                    var U = Products.ProductsRow.getLookup().itemById[PID].UnitId;
                    if (U != undefined) {
                        this.form.Unit.value = Masters.ProductsUnitRow.getLookup().itemById[U].ProductsUnit;
                    }
                }
            });
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