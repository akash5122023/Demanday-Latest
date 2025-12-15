
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayTeleMarketingQualiltyDialog extends DialogBase<DemandayTeleMarketingQualiltyRow, any> {
        protected getFormKey() { return DemandayTeleMarketingQualiltyForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingQualiltyRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingQualiltyRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingQualiltyRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingQualiltyService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingQualiltyRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingQualiltyRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingQualiltyRow.updatePermission; }

        protected form = new DemandayTeleMarketingQualiltyForm(this.idPrefix);

    }
}