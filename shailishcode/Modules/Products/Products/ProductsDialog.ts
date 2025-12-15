
namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class ProductsDialog extends DialogBase<ProductsRow, any> {
        protected getFormKey() { return ProductsForm.formKey; }
        protected getIdProperty() { return ProductsRow.idProperty; }
        protected getLocalTextPrefix() { return ProductsRow.localTextPrefix; }
        protected getNameProperty() { return ProductsRow.nameProperty; }
        protected getService() { return ProductsService.baseUrl; }
        protected getDeletePermission() { return ProductsRow.deletePermission; }
        protected getInsertPermission() { return ProductsRow.insertPermission; }
        protected getUpdatePermission() { return ProductsRow.updatePermission; }

        protected form = new ProductsForm(this.idPrefix);

        constructor() {
            super();

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].Code != true) {
                this.form.Code.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].HSN != true) {
                this.form.HSN.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].Unit != true) {
                this.form.UnitId.getGridField().toggle(false);
            }
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].OpeningStock != true) {
                this.form.OpeningStock.getGridField().toggle(false);
            }
            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].RawMaterial != true) {
            //    this.form.RawMaterial.getGridField().toggle(false);
            //}
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].Group != true) {
                this.form.GroupId.getGridField().toggle(false);
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

            this.form.Name.change(e => {
                this.form.Name.value = this.form.Name.value.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1); });
            });

            Common.NavigationService.ChannelsManagement({},
                response => {
                    if (response.Status == "Remove") {
                        this.form.ChannelCustomerPrice.getGridField().hide();
                        this.form.ResellerPrice.getGridField().hide();
                        this.form.WholesalerPrice.getGridField().hide();
                        this.form.DealerPrice.getGridField().hide();
                        this.form.DistributorPrice.getGridField().hide();
                        this.form.StockiestPrice.getGridField().hide();
                        this.form.NationalDistributorPrice.getGridField().hide();
                        this.form.ChannelCustomerPrice.element.closest('.category').toggle(false);
                    }
                }
            );
        }

        onSaveSuccess(response) {
            super.onSaveSuccess(response);
            Q.reloadLookupAsync('Products.Products');
        }
    }
}