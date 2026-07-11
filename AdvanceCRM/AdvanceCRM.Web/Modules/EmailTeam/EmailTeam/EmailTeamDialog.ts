
namespace AdvanceCRM.EmailTeam {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class EmailTeamDialog extends DialogBase<EmailTeamRow, any> {
        protected getFormKey() { return EmailTeamForm.formKey; }
        protected getIdProperty() { return EmailTeamRow.idProperty; }
        protected getLocalTextPrefix() { return EmailTeamRow.localTextPrefix; }
        protected getNameProperty() { return EmailTeamRow.nameProperty; }
        protected getService() { return EmailTeamService.baseUrl; }
        protected getDeletePermission() { return EmailTeamRow.deletePermission; }
        protected getInsertPermission() { return EmailTeamRow.insertPermission; }
        protected getUpdatePermission() { return EmailTeamRow.updatePermission; }

        protected form = new EmailTeamForm(this.idPrefix);
    }
}
