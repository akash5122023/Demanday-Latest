
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayContactsDialog extends DialogBase<DemandayContactsRow, any> {
        protected getFormKey() { return DemandayContactsForm.formKey; }
        protected getIdProperty() { return DemandayContactsRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayContactsRow.localTextPrefix; }
        protected getNameProperty() { return DemandayContactsRow.nameProperty; }
        protected getService() { return DemandayContactsService.baseUrl; }
        protected getDeletePermission() { return DemandayContactsRow.deletePermission; }
        protected getInsertPermission() { return DemandayContactsRow.insertPermission; }
        protected getUpdatePermission() { return DemandayContactsRow.updatePermission; }

        protected form = new DemandayContactsForm(this.idPrefix);

    }
}