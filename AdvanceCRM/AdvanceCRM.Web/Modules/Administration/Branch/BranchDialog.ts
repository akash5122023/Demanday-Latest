
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class BranchDialog extends DialogBase<BranchRow, any> {
        protected getFormKey() { return BranchForm.formKey; }
        protected getIdProperty() { return BranchRow.idProperty; }
        protected getLocalTextPrefix() { return BranchRow.localTextPrefix; }
        protected getNameProperty() { return BranchRow.nameProperty; }
        protected getService() { return BranchService.baseUrl; }
        protected getDeletePermission() { return BranchRow.deletePermission; }
        protected getInsertPermission() { return BranchRow.insertPermission; }
        protected getUpdatePermission() { return BranchRow.updatePermission; }

        protected form = new BranchForm(this.idPrefix);

        onSaveSuccess(response) {
            super.onSaveSuccess(response);
            Q.reloadLookupAsync('Administration.Branch');
        }
        onDialogOpen() {
            super.onDialogOpen();

            if (this.form.Country.value < "1") {
                this.form.Country.value = Administration.CompanyDetailsRow.getLookup().itemById[1].Country.toString();
            }
        }
    }
}