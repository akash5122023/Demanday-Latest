
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class CityDialog extends DialogBase<CityRow, any> {
        protected getFormKey() { return CityForm.formKey; }
        protected getIdProperty() { return CityRow.idProperty; }
        protected getLocalTextPrefix() { return CityRow.localTextPrefix; }
        protected getNameProperty() { return CityRow.nameProperty; }
        protected getService() { return CityService.baseUrl; }
        protected getDeletePermission() { return CityRow.deletePermission; }
        protected getInsertPermission() { return CityRow.insertPermission; }
        protected getUpdatePermission() { return CityRow.updatePermission; }

        protected form = new CityForm(this.idPrefix);

    }
}