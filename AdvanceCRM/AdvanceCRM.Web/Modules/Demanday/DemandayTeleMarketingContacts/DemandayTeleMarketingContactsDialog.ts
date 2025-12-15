
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayTeleMarketingContactsDialog extends DialogBase<DemandayTeleMarketingContactsRow, any> {
        protected getFormKey() { return DemandayTeleMarketingContactsForm.formKey; }
        protected getIdProperty() { return DemandayTeleMarketingContactsRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingContactsRow.localTextPrefix; }
        protected getNameProperty() { return DemandayTeleMarketingContactsRow.nameProperty; }
        protected getService() { return DemandayTeleMarketingContactsService.baseUrl; }
        protected getDeletePermission() { return DemandayTeleMarketingContactsRow.deletePermission; }
        protected getInsertPermission() { return DemandayTeleMarketingContactsRow.insertPermission; }
        protected getUpdatePermission() { return DemandayTeleMarketingContactsRow.updatePermission; }

        protected form = new DemandayTeleMarketingContactsForm(this.idPrefix);

    }
}