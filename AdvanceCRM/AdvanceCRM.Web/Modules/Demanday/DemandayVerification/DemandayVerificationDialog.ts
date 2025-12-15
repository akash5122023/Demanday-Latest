
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayVerificationDialog extends DialogBase<DemandayVerificationRow, any> {
        protected getFormKey() { return DemandayVerificationForm.formKey; }
        protected getIdProperty() { return DemandayVerificationRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayVerificationRow.localTextPrefix; }
        protected getNameProperty() { return DemandayVerificationRow.nameProperty; }
        protected getService() { return DemandayVerificationService.baseUrl; }
        protected getDeletePermission() { return DemandayVerificationRow.deletePermission; }
        protected getInsertPermission() { return DemandayVerificationRow.insertPermission; }
        protected getUpdatePermission() { return DemandayVerificationRow.updatePermission; }

        protected form = new DemandayVerificationForm(this.idPrefix);

    }
}