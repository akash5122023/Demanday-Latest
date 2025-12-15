
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayTeleMarketingTeamLeaderDialog extends DialogBase<DemandayTeleMarketingTeamLeaderRow, any> {
        protected getFormKey() { return DemandayTeleMarketingTeamLeaderForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingTeamLeaderRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingTeamLeaderRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingTeamLeaderRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingTeamLeaderService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingTeamLeaderRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingTeamLeaderRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingTeamLeaderRow.updatePermission; }

        protected form = new DemandayTeleMarketingTeamLeaderForm(this.idPrefix);

    }
}