
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class TeamsDialog extends DialogBase<TeamsRow, any> {
        protected getFormKey() { return TeamsForm.formKey; }
        protected getIdProperty() { return TeamsRow.idProperty; }
        protected getLocalTextPrefix() { return TeamsRow.localTextPrefix; }
        protected getNameProperty() { return TeamsRow.nameProperty; }
        protected getService() { return TeamsService.baseUrl; }
        protected getDeletePermission() { return TeamsRow.deletePermission; }
        protected getInsertPermission() { return TeamsRow.insertPermission; }
        protected getUpdatePermission() { return TeamsRow.updatePermission; }

        protected form = new TeamsForm(this.idPrefix);

    }
}