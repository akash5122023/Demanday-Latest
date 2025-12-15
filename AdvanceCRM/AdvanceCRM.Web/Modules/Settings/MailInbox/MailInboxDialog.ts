
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class MailInboxDialog extends ConfigurationDialog<MailInboxRow> {
        protected getFormKey() { return MailInboxForm.formKey; }
        protected getIdProperty() { return MailInboxRow.idProperty; }
        protected getLocalTextPrefix() { return MailInboxRow.localTextPrefix; }
        protected getNameProperty() { return MailInboxRow.nameProperty; }
        protected getService() { return MailInboxService.baseUrl; }
        protected getDeletePermission() { return MailInboxRow.deletePermission; }
        protected getInsertPermission() { return MailInboxRow.insertPermission; }
        protected getUpdatePermission() { return MailInboxRow.updatePermission; }

        protected form = new MailInboxForm(this.idPrefix);
        constructor() {
            super();
        }

        loadEntity(entity: MailInboxRow) {
            super.loadEntity(entity);

            // var getUrl = window.location;
            // var baseUrl = getUrl.protocol + "//" + getUrl.host;
            // baseUrl = baseUrl + "/api/Leads/Facebook?";
            //// this.form.API.value = baseUrl;
        }

    }
}