
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class GradeDialog extends DialogBase<GradeRow, any> {
        protected getFormKey() { return GradeForm.formKey; }
        protected getIdProperty() { return GradeRow.idProperty; }
        protected getLocalTextPrefix() { return GradeRow.localTextPrefix; }
        protected getNameProperty() { return GradeRow.nameProperty; }
        protected getService() { return GradeService.baseUrl; }
        protected getDeletePermission() { return GradeRow.deletePermission; }
        protected getInsertPermission() { return GradeRow.insertPermission; }
        protected getUpdatePermission() { return GradeRow.updatePermission; }

        protected form = new GradeForm(this.idPrefix);

    }
}