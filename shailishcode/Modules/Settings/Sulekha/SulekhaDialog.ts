/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class SulekhaDialog extends ConfigurationDialog<SulekhaRow> {
        protected getFormKey() { return SulekhaForm.formKey; }
        protected getIdProperty() { return SulekhaRow.idProperty; }
        protected getLocalTextPrefix() { return SulekhaRow.localTextPrefix; }
        protected getNameProperty() { return SulekhaRow.nameProperty; }
        protected getService() { return SulekhaService.baseUrl; }
        protected getDeletePermission() { return SulekhaRow.deletePermission; }
        protected getInsertPermission() { return SulekhaRow.insertPermission; }
        protected getUpdatePermission() { return SulekhaRow.updatePermission; }

        protected form = new SulekhaForm(this.idPrefix);

    }
}