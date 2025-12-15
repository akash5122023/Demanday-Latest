
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayMisDialog extends DialogBase<DemandayMisRow, any> {
        protected getFormKey() { return DemandayMisForm.formKey; }
        protected getIdProperty() { return DemandayMisRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayMisRow.localTextPrefix; }
        protected getNameProperty() { return DemandayMisRow.nameProperty; }
        protected getService() { return DemandayMisService.baseUrl; }
        protected getDeletePermission() { return DemandayMisRow.deletePermission; }
        protected getInsertPermission() { return DemandayMisRow.insertPermission; }
        protected getUpdatePermission() { return DemandayMisRow.updatePermission; }

        protected form = new DemandayMisForm(this.idPrefix);

    }
}