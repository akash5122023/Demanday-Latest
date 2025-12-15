
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class VillageDialog extends DialogBase<VillageRow, any> {
        protected getFormKey() { return VillageForm.formKey; }
        protected getIdProperty() { return VillageRow.idProperty; }
        protected getLocalTextPrefix() { return VillageRow.localTextPrefix; }
        protected getNameProperty() { return VillageRow.nameProperty; }
        protected getService() { return VillageService.baseUrl; }
        protected getDeletePermission() { return VillageRow.deletePermission; }
        protected getInsertPermission() { return VillageRow.insertPermission; }
        protected getUpdatePermission() { return VillageRow.updatePermission; }

        protected form = new VillageForm(this.idPrefix);

    }
}