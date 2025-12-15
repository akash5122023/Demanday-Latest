
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayTeleMarketingMISDialog extends DialogBase<DemandayTeleMarketingMISRow, any> {
        protected getFormKey() { return DemandayTeleMarketingMISForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingMISRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingMISRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingMISRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingMISService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingMISRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingMISRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingMISRow.updatePermission; }

        protected form = new DemandayTeleMarketingMISForm(this.idPrefix);

    }
}