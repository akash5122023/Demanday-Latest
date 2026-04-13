
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayMasterAccountDialog extends Serenity.EntityDialog<DemandayMasterAccountRow, any> {
        protected getFormKey() { return DemandayMasterAccountForm.formKey; }
        protected getIdProperty() { return DemandayMasterAccountRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayMasterAccountRow.localTextPrefix; }
        protected getNameProperty() { return DemandayMasterAccountRow.nameProperty; }
        protected getService() { return DemandayMasterAccountService.baseUrl; }
        protected getDeletePermission() { return DemandayMasterAccountRow.deletePermission; }
        protected getInsertPermission() { return DemandayMasterAccountRow.insertPermission; }
        protected getUpdatePermission() { return DemandayMasterAccountRow.updatePermission; }

        protected form = new DemandayMasterAccountForm(this.idPrefix);

    }
}