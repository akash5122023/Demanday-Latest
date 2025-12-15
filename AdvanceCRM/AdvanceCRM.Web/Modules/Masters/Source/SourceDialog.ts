
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class SourceDialog extends DialogBase<SourceRow, any> {
        protected getFormKey() { return SourceForm.formKey; }
        protected getIdProperty() { return SourceRow.idProperty; }
        protected getLocalTextPrefix() { return SourceRow.localTextPrefix; }
        protected getNameProperty() { return SourceRow.nameProperty; }
        protected getService() { return SourceService.baseUrl; }
        protected getDeletePermission() { return SourceRow.deletePermission; }
        protected getInsertPermission() { return SourceRow.insertPermission; }
        protected getUpdatePermission() { return SourceRow.updatePermission; }

        protected form = new SourceForm(this.idPrefix);

    }
}