
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class CRMDialog extends ConfigurationDialog<Administration.CompanyDetailsRow> {
        protected getFormKey() { return CRMForm.formKey; }
        protected getIdProperty() { return Administration.CompanyDetailsRow.idProperty; }
        protected getLocalTextPrefix() { return Administration.CompanyDetailsRow.localTextPrefix; }
        protected getNameProperty() { return Administration.CompanyDetailsRow.nameProperty; }
        protected getService() { return Administration.CompanyDetailsService.baseUrl; }

        protected form = new CRMForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}