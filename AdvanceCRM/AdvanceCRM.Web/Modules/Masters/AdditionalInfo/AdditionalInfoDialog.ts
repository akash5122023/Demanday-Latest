
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class AdditionalInfoDialog extends DialogBase<AdditionalInfoRow, any> {
        protected getFormKey() { return AdditionalInfoForm.formKey; }
        protected getIdProperty() { return AdditionalInfoRow.idProperty; }
        protected getLocalTextPrefix() { return AdditionalInfoRow.localTextPrefix; }
        protected getNameProperty() { return AdditionalInfoRow.nameProperty; }
        protected getService() { return AdditionalInfoService.baseUrl; }
        protected getDeletePermission() { return AdditionalInfoRow.deletePermission; }
        protected getInsertPermission() { return AdditionalInfoRow.insertPermission; }
        protected getUpdatePermission() { return AdditionalInfoRow.updatePermission; }

        protected form = new AdditionalInfoForm(this.idPrefix);

    }
}