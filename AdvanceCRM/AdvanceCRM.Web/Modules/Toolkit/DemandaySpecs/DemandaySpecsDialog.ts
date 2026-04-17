
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandaySpecsDialog extends DialogBase<DemandaySpecsRow, any> {
        protected getFormKey() { return DemandaySpecsForm.formKey; }
        protected getIdProperty() { return DemandaySpecsRow.idProperty; }
        protected getLocalTextPrefix() { return DemandaySpecsRow.localTextPrefix; }
        protected getNameProperty() { return DemandaySpecsRow.nameProperty; }
        protected getService() { return DemandaySpecsService.baseUrl; }
        protected getDeletePermission() { return DemandaySpecsRow.deletePermission; }
        protected getInsertPermission() { return DemandaySpecsRow.insertPermission; }
        protected getUpdatePermission() { return DemandaySpecsRow.updatePermission; }

        protected form = new DemandaySpecsForm(this.idPrefix);

    }
}