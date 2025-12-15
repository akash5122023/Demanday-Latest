
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class ComplaintTypeDialog extends DialogBase<ComplaintTypeRow, any> {
        protected getFormKey() { return ComplaintTypeForm.formKey; }
        protected getIdProperty() { return ComplaintTypeRow.idProperty; }
        protected getLocalTextPrefix() { return ComplaintTypeRow.localTextPrefix; }
        protected getNameProperty() { return ComplaintTypeRow.nameProperty; }
        protected getService() { return ComplaintTypeService.baseUrl; }
        protected getDeletePermission() { return ComplaintTypeRow.deletePermission; }
        protected getInsertPermission() { return ComplaintTypeRow.insertPermission; }
        protected getUpdatePermission() { return ComplaintTypeRow.updatePermission; }

        protected form = new ComplaintTypeForm(this.idPrefix);

    }
}