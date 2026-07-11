
namespace AdvanceCRM.TeleMarketingEmailTeam {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class TeleMarketingEmailTeamDialog extends DialogBase<TeleMarketingEmailTeamRow, any> {
        protected getFormKey() { return TeleMarketingEmailTeamForm.formKey; }
        protected getIdProperty() { return TeleMarketingEmailTeamRow.idProperty; }
        protected getLocalTextPrefix() { return TeleMarketingEmailTeamRow.localTextPrefix; }
        protected getNameProperty() { return TeleMarketingEmailTeamRow.nameProperty; }
        protected getService() { return TeleMarketingEmailTeamService.baseUrl; }
        protected getDeletePermission() { return TeleMarketingEmailTeamRow.deletePermission; }
        protected getInsertPermission() { return TeleMarketingEmailTeamRow.insertPermission; }
        protected getUpdatePermission() { return TeleMarketingEmailTeamRow.updatePermission; }

        protected form = new TeleMarketingEmailTeamForm(this.idPrefix);
    }
}
