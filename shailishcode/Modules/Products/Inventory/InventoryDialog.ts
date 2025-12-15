
namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    export class InventoryDialog extends Serenity.EntityDialog<InventoryRow, any> {
        private static readonly idProperty = 'Id';
        private static readonly nameProperty = 'Name';
        private static readonly localTextPrefix = 'Inventory.Inventory';
        private static readonly deletePermission = 'Inventory:Delete';
        private static readonly insertPermission = 'Inventory:Insert';
        private static readonly updatePermission = 'Inventory:Update';

        protected getFormKey() { return InventoryForm.formKey; }
        protected getIdProperty() { return InventoryDialog.idProperty; }
        protected getLocalTextPrefix() { return InventoryDialog.localTextPrefix; }
        protected getNameProperty() { return InventoryDialog.nameProperty; }
        protected getService() { return InventoryService.baseUrl; }
        protected getDeletePermission() { return InventoryDialog.deletePermission; }
        protected getInsertPermission() { return InventoryDialog.insertPermission; }
        protected getUpdatePermission() { return InventoryDialog.updatePermission; }

        protected form = new InventoryForm(this.idPrefix);
        constructor() {
            super();

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
    }
}