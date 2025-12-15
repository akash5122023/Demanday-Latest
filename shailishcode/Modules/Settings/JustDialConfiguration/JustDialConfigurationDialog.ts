
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class JustDialConfigurationDialog extends ConfigurationDialog<JustDialConfigurationRow> {
        protected getFormKey() { return JustDialConfigurationForm.formKey; }
        protected getIdProperty() { return JustDialConfigurationRow.idProperty; }
        protected getLocalTextPrefix() { return JustDialConfigurationRow.localTextPrefix; }
        protected getNameProperty() { return JustDialConfigurationRow.nameProperty; }
        protected getService() { return JustDialConfigurationService.baseUrl; }
        protected getDeletePermission() { return JustDialConfigurationRow.deletePermission; }
        protected getInsertPermission() { return JustDialConfigurationRow.insertPermission; }
        protected getUpdatePermission() { return JustDialConfigurationRow.updatePermission; }

        protected form = new JustDialConfigurationForm(this.idPrefix);

        constructor() {
            super();
        }

        loadEntity(entity: JustDialConfigurationRow) {
            super.loadEntity(entity);

            var getUrl = window.location;
            var baseUrl = getUrl.protocol + "//" + getUrl.host;
            baseUrl = baseUrl + "/api/Leads/JustDial?user=" + this.form.Username.value + "&pass=" + this.form.Password.value;
            this.form.API.value = baseUrl;

            var baseUrljson = getUrl.protocol + "//" + getUrl.host;
            baseUrljson = "curl--location " + baseUrljson + "'/api/leads/JustdialJSON' \
            --header 'Content-Type: application/json' \
            --data '{\"leadid\": \"JD191\",\"leadtype\": \"category\",\"prefix\": \"Mr\",\"name\": \"Sunny\",\"mobile\": \"8879179588\",\"phone\": \"022 - 22355342\",\"email\": \"\",\"date\": \"2023 - 02 - 28\",\"category\": \"Malwa Enclave\",\"area\": \"Connaught Place\",\"city\": \"Delhi\",\"brancharea\": \"Gurgaon Sector 24\",\"dncmobile\": 0,\"dncphone\": 0,\"company\": \"unimont Imperia\",\"pincode\": \"110001\",\"time\": \"12: 03: 03\",\"branchpin\": \"122002\",\"parentid\": \"PXX119\"}'";

            this.form.PostUrl.value = baseUrljson;
        }
    }
}