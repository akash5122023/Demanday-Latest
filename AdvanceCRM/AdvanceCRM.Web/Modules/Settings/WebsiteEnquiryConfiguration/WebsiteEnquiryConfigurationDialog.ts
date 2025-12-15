
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class WebsiteEnquiryConfigurationDialog extends ConfigurationDialog<WebsiteEnquiryConfigurationRow> {
        protected getFormKey() { return WebsiteEnquiryConfigurationForm.formKey; }
        protected getIdProperty() { return WebsiteEnquiryConfigurationRow.idProperty; }
        protected getLocalTextPrefix() { return WebsiteEnquiryConfigurationRow.localTextPrefix; }
        protected getNameProperty() { return WebsiteEnquiryConfigurationRow.nameProperty; }
        protected getService() { return WebsiteEnquiryConfigurationService.baseUrl; }
        protected getDeletePermission() { return WebsiteEnquiryConfigurationRow.deletePermission; }
        protected getInsertPermission() { return WebsiteEnquiryConfigurationRow.insertPermission; }
        protected getUpdatePermission() { return WebsiteEnquiryConfigurationRow.updatePermission; }

        protected form = new WebsiteEnquiryConfigurationForm(this.idPrefix);

        constructor() {
            super();
        }

        loadEntity(entity: JustDialConfigurationRow) {
            super.loadEntity(entity);

            var getUrl = window.location;
            var baseUrl = getUrl.protocol + "//" + getUrl.host;
            baseUrl = baseUrl + "/api/Leads/Website?user=" + this.form.Username.value + "&pass=" + this.form.Password.value;
            this.form.API.value = baseUrl;
        }

    }
}