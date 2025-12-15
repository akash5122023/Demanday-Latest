
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayTeamLeaderDialog extends DialogBase<DemandayTeamLeaderRow, any> {
        protected getFormKey() { return DemandayTeamLeaderForm.formKey; }
        protected getIdProperty() { return DemandayTeamLeaderRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeamLeaderRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeamLeaderRow.nameProperty; }
        protected getService() { return DemandayTeamLeaderService.baseUrl; }
        protected getDeletePermission() { return DemandayTeamLeaderRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeamLeaderRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeamLeaderRow.updatePermission; }

        protected form = new DemandayTeamLeaderForm(this.idPrefix);

    }
}