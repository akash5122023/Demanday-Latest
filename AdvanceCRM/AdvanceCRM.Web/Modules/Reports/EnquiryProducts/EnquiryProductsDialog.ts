
namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class EnquiryProductsDialog extends DialogBase<EnquiryProductsRow, any> {
        protected getFormKey() { return EnquiryProductsForm.formKey; }
        protected getIdProperty() { return EnquiryProductsRow.idProperty; }
        protected getLocalTextPrefix() { return EnquiryProductsRow.localTextPrefix; }
        protected getNameProperty() { return EnquiryProductsRow.nameProperty; }
        protected getService() { return EnquiryProductsService.baseUrl; }
        protected getDeletePermission() { return EnquiryProductsRow.deletePermission; }
        protected getInsertPermission() { return EnquiryProductsRow.insertPermission; }
        protected getUpdatePermission() { return EnquiryProductsRow.updatePermission; }

        protected form = new EnquiryProductsForm(this.idPrefix);

    }
}