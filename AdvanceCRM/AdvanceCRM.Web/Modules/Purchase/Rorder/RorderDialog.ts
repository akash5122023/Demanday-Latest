
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    export class RorderDialog extends DialogBase<RorderRow, any> {
        protected getFormKey() { return RorderForm.formKey; }
        protected getIdProperty() { return RorderRow.idProperty; }
        protected getLocalTextPrefix() { return RorderRow.localTextPrefix; }
        protected getService() { return RorderService.baseUrl; }
        protected getDeletePermission() { return RorderRow.deletePermission; }
        protected getInsertPermission() { return RorderRow.insertPermission; }
        protected getUpdatePermission() { return RorderRow.updatePermission; }

        protected form = new RorderForm(this.idPrefix);


        protected getToolbarButtons() {
            //let buttons = super.getToolbarButtons();


          
            let buttons = [];
            buttons.push({
                title: 'Move to Purchase Order',
                cssClass: 'add-button',
                onClick: () => {
                    this.moveToPurchaseOrder();
                }
            });

            buttons.push({
                title: 'Move to Purchase ',
                cssClass: 'add-button',
                onClick: () => {
                    this.moveToPurchase();
                }
            });

            return buttons;
        }

        private moveToPurchase() {

            // Step 1: Create an instance of PurchaseOrderDialog
            var purchaseDialog = new AdvanceCRM.Purchase.PurchaseDialog();

            // Step 2: Set the product data to be passed to the PurchaseOrderProducts editor in PurchaseOrderDialog
            //purchaseOrderDialog.loadProductData(productData);

            // Step 3: Open the PurchaseOrderDialog
            purchaseDialog.dialogOpen({});
        }

        private moveToPurchaseOrder() {
            // Example product data to pass
            let productData = [{
                ProductsId: 2,   // Product ID
                Quantity: 10,    // Quantity to add
                Price: 100,
                Discount: 0,
                DiscountAmount:0
                
            }];

            // Step 1: Create an instance of PurchaseOrderDialog
            var purchaseOrderDialog = new AdvanceCRM.Purchase.PurchaseOrderDialog();

            // Step 2: Set the product data to be passed to the PurchaseOrderProducts editor in PurchaseOrderDialog
            //purchaseOrderDialog.loadProductData(productData);

            // Step 3: Open the PurchaseOrderDialog
            purchaseOrderDialog.dialogOpen({});
        }


      
    }
}