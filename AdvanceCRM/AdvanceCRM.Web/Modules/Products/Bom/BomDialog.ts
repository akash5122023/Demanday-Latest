
namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class BomDialog extends DialogBase<BomRow, any> {
        protected getFormKey() { return BomForm.formKey; }
        protected getIdProperty() { return BomRow.idProperty; }
        protected getLocalTextPrefix() { return BomRow.localTextPrefix; }
        protected getNameProperty() { return BomRow.nameProperty; }
        protected getService() { return BomService.baseUrl; }
        protected getDeletePermission() { return BomRow.deletePermission; }
        protected getInsertPermission() { return BomRow.insertPermission; }
        protected getUpdatePermission() { return BomRow.updatePermission; }

        protected form = new BomForm(this.idPrefix);


        onDialogOpen() {
            super.onDialogOpen();
            //this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();
            if (this.form.OwnerId.value < '1') {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < '1') {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            //if (this.form.BranchId.value < "1" && Authorization.userDefinition.BranchId) {
            //    this.form.BranchId.value = Authorization.userDefinition.BranchId.toString().toString();
            //}

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].RoundupInPurchase != true) {
                this.form.Roundup.getGridField().toggle(false);
            }

            //this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();
        }

    }
}