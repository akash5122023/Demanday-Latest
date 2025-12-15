
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class StageDialog extends DialogBase<StageRow, any> {
        protected getFormKey() { return StageForm.formKey; }
        protected getIdProperty() { return StageRow.idProperty; }
        protected getLocalTextPrefix() { return StageRow.localTextPrefix; }
        protected getNameProperty() { return StageRow.nameProperty; }
        protected getService() { return StageService.baseUrl; }
        protected getDeletePermission() { return StageRow.deletePermission; }
        protected getInsertPermission() { return StageRow.insertPermission; }
        protected getUpdatePermission() { return StageRow.updatePermission; }

        protected form = new StageForm(this.idPrefix);

    }
}