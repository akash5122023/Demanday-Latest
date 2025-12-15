
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class TabGenderDialog extends Serenity.EntityDialog<TabGenderRow, any> {
        protected getFormKey() { return TabGenderForm.formKey; }
        protected getIdProperty() { return TabGenderRow.idProperty; }
        protected getLocalTextPrefix() { return TabGenderRow.localTextPrefix; }
        protected getNameProperty() { return TabGenderRow.nameProperty; }
        protected getService() { return TabGenderService.baseUrl; }
        protected getDeletePermission() { return TabGenderRow.deletePermission; }
        protected getInsertPermission() { return TabGenderRow.insertPermission; }
        protected getUpdatePermission() { return TabGenderRow.updatePermission; }

        protected form = new TabGenderForm(this.idPrefix);

    }
}