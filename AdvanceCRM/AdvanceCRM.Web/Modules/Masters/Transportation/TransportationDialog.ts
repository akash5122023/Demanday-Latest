
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.maximizable()
    export class TransportationDialog extends DialogBase<TransportationRow, any> {
        protected getFormKey() { return TransportationForm.formKey; }
        protected getIdProperty() { return TransportationRow.idProperty; }
        protected getLocalTextPrefix() { return TransportationRow.localTextPrefix; }
        protected getNameProperty() { return TransportationRow.nameProperty; }
        protected getService() { return TransportationService.baseUrl; }
        protected getDeletePermission() { return TransportationRow.deletePermission; }
        protected getInsertPermission() { return TransportationRow.insertPermission; }
        protected getUpdatePermission() { return TransportationRow.updatePermission; }

        protected form = new TransportationForm(this.idPrefix);

    }
}