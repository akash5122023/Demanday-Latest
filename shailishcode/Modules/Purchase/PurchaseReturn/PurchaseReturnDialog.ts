
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class PurchaseReturnDialog extends DialogBase<PurchaseReturnRow, any> {
        protected getFormKey() { return PurchaseReturnForm.formKey; }
        protected getIdProperty() { return PurchaseReturnRow.idProperty; }
        protected getLocalTextPrefix() { return PurchaseReturnRow.localTextPrefix; }
        protected getNameProperty() { return PurchaseReturnRow.nameProperty; }
        protected getService() { return PurchaseReturnService.baseUrl; }
        protected getDeletePermission() { return PurchaseReturnRow.deletePermission; }
        protected getInsertPermission() { return PurchaseReturnRow.insertPermission; }
        protected getUpdatePermission() { return PurchaseReturnRow.updatePermission; }

        protected form = new PurchaseReturnForm(this.idPrefix);

       
        constructor() {
            super();

            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));


            Common.NavigationService.MultiLocation({},
                response => {
                    if (response.Status != "Remove") {
                        this.form.BranchId.element.addClass(" required");

                        if (Authorization.hasPermission("Purchase:Change Branch")) {
                            Serenity.EditorUtils.setReadonly(this.form.BranchId.element, false);
                        }
                        else {
                            Serenity.EditorUtils.setReadonly(this.form.BranchId.element, true);
                        }
                    }
                }
            );

            this.form.InvoiceNo.change(e => {
                var invNo = Q.toId(this.form.InvoiceNo.value.toString());
                Q.notifyInfo(invNo);

               Q.notifyInfo(Q.tryFirst(PurchaseRow.getLookup().items, x => x.InvoiceNo == invNo).PurchaseFromId.toString());

                if (Q.tryFirst(PurchaseRow.getLookup().items, x => x.InvoiceNo == invNo).PurchaseFromId.toString() != "") {
                    this.form.ContactsId.value = Q.tryFirst(PurchaseRow.getLookup().items, x => x.InvoiceNo == invNo).PurchaseFromId.toString();
                    this.form.InvoiceDate.value = Q.tryFirst(PurchaseRow.getLookup().items, x => x.InvoiceNo == invNo).InvoiceDate;
                }
                else {
                    this.form.ContactsId.value = Q.tryFirst(PurchaseRow.getLookup().items, x => x.Id == invNo).PurchaseFromId.toString();
                    this.form.InvoiceDate.value = Q.tryFirst(PurchaseRow.getLookup().items, x => x.Id == invNo).InvoiceDate;
                }

              


                //var purchase = Q.tryFirst(PurchaseRow.getLookup().items, x => x.InvoiceNo == invNo);

                //if (purchase?.Id) {
                //    this.form.ContactsId.value = purchase.Id.toString();
                //}


                //if (this.form.ContactsId && purchase.PurchaseFromId != null) {
                //    this.form.ContactsId.value = purchase.PurchaseFromId.toString();
                //}

                //if (this.form.InvoiceDate && purchase.InvoiceDate != null) {
                //    this.form.InvoiceDate.value = purchase.InvoiceDate;
                //}



                //const s = Q.tryFirst(PurchaseRow.getLookup().items, x => x.InvoiceNo == invNo);

                //if (s && s.PurchaseFromId != null) {
                //    this.form.PurchaseFromId.value = s.Id.toString();                  
                //    this.form.PurchaseFromId.value = s.PurchaseFromId.toString();  
                //    this.form.InvoiceDate.value = s.InvoiceDate;
                //} else {
                //    let fallbackSale = Q.tryFirst(PurchaseRow.getLookup().items, x => x.Id == invNo);
                //    if (fallbackSale && fallbackSale.PurchaseFromId != null) {
                //        this.form.PurchaseFromId.value = fallbackSale.Id.toString();                 
                //        this.form.PurchaseFromId.value = fallbackSale.PurchaseFromId.toString();  
                //        this.form.InvoiceDate.value = fallbackSale.InvoiceDate;
                //    }
                //}


            });
        }


        protected validateBeforeSave() {
            if (!super.validateBeforeSave())
                return false;

            if (this.form.Products.getItems().length === 0) {
                Q.notifyError("Please add at least one product.");
                return false;
            }

            return true;
        }


        onDialogOpen() {
            super.onDialogOpen();

            if (this.form.OwnerId.value < "1") {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < "1") {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            if (this.form.BranchId.value < "1" && Authorization.userDefinition.BranchId) {
                this.form.BranchId.value = Authorization.userDefinition.BranchId.toString().toString();
            }


        }
       
      
    }
}