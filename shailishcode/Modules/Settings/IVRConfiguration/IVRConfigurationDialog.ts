
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class IVRConfigurationDialog extends ConfigurationDialog<IVRConfigurationRow> {
        protected getFormKey() { return IVRConfigurationForm.formKey; }
        protected getIdProperty() { return IVRConfigurationRow.idProperty; }
        protected getLocalTextPrefix() { return IVRConfigurationRow.localTextPrefix; }
        protected getNameProperty() { return IVRConfigurationRow.nameProperty; }
        protected getService() { return IVRConfigurationService.baseUrl; }
        protected getDeletePermission() { return IVRConfigurationRow.deletePermission; }
        protected getInsertPermission() { return IVRConfigurationRow.insertPermission; }
        protected getUpdatePermission() { return IVRConfigurationRow.updatePermission; }

        protected form = new IVRConfigurationForm(this.idPrefix);

        constructor() {
            super();

            this.form.IVRType.changeSelect2(e => {
                if (this.form.IVRType.value == "2") {
                    this.form.Username.getGridField().toggle(false);
                    this.form.Password.getGridField().toggle(false);
                    this.form.PostUrl.getGridField().toggle(false);
                    this.form.ApiKey.getGridField().toggle(false);
                    this.form.Plan.getGridField().toggle(false);
                    this.form.AppId.getGridField().toggle(true);
                    this.form.AppSecret.getGridField().toggle(true);
                    this.form.CliNumber.getGridField().toggle(false);
                    this.form.Token_Id.getGridField().toggle(false);
                    this.form.userType.getGridField().toggle(false);
                    this.form.Number.getGridField().toggle(false);
                }
                else if (this.form.IVRType.value == "3") {
                    this.form.Username.getGridField().toggle(true);
                    this.form.Password.getGridField().toggle(true);
                    this.form.PostUrl.getGridField().toggle(true);
                    this.form.ApiKey.getGridField().toggle(true);
                    this.form.Plan.getGridField().toggle(false);
                    this.form.AppId.getGridField().toggle(true);
                    this.form.AppSecret.getGridField().toggle(false);
                    this.form.CliNumber.getGridField().toggle(false);
                    this.form.Token_Id.getGridField().toggle(false);
                    this.form.userType.getGridField().toggle(false);
                    this.form.Number.getGridField().toggle(false);
                }
                else if (this.form.IVRType.value == "4") {
                    this.form.Username.getGridField().toggle(false);
                    this.form.Password.getGridField().toggle(false);
                    this.form.PostUrl.getGridField().toggle(false);
                    this.form.ApiKey.getGridField().toggle(false);
                    this.form.Plan.getGridField().toggle(false);
                    this.form.AppId.getGridField().toggle(false);
                    this.form.AppSecret.getGridField().toggle(false);
                    this.form.CliNumber.getGridField().toggle(false);
                    this.form.IVRNumber.getGridField().toggle(true);
                    this.form.Token_Id.getGridField().toggle(true);
                    this.form.userType.getGridField().toggle(true);
                    this.form.Number.getGridField().toggle(true);

                }
                else {
                    this.form.Username.getGridField().toggle(false);
                    this.form.Password.getGridField().toggle(false);
                    this.form.PostUrl.getGridField().toggle(false);
                    this.form.ApiKey.getGridField().toggle(true);
                    this.form.Plan.getGridField().toggle(true);
                    this.form.Agents.getGridField().toggle(true);
                    this.form.AppId.getGridField().toggle(false);
                    this.form.AppSecret.getGridField().toggle(false);
                    this.form.CliNumber.getGridField().toggle(true);

                    this.form.Token_Id.getGridField().toggle(false);
                    this.form.userType.getGridField().toggle(false);
                    this.form.Number.getGridField().toggle(false);
                }
            });
        }

        loadEntity(entity: JustDialConfigurationRow) {
            super.loadEntity(entity);

            var getUrl = window.location;
            var baseUrljson = getUrl.protocol + "//" + getUrl.host;
            baseUrljson = "curl--location " + baseUrljson + "'/api/IVR/IVRJSON' \
           --header 'Content-Type: application/json' \
            --data '{\"CallerNo\" : \"9699556052\", \"CallDate\" : \"2023 - 02 - 15\", \"StartTime\" : \"20: 26: 12\", \"EndTime\" : \"25: 28: 15\", \"AgentNo\" : \"9699556052\", \"CallStatus\" : \"Answered\", \"recordingurl\" : \"www.way2voice.in / abc.mp3\", \"CallType\" : \"Inbound”/ ”Outbound\"}'";

            this.form.PostUrl.value = baseUrljson;
        }

        onDialogOpen() {
            super.onDialogOpen();

            if (this.form.IVRType.value == "2") {
                this.form.Username.getGridField().toggle(false);
                this.form.Password.getGridField().toggle(false);
                this.form.PostUrl.getGridField().toggle(false);
                this.form.ApiKey.getGridField().toggle(false);
                this.form.Plan.getGridField().toggle(false);              
                this.form.AppId.getGridField().toggle(true);
                this.form.AppSecret.getGridField().toggle(true);
                this.form.CliNumber.getGridField().toggle(false);
                this.form.Token_Id.getGridField().toggle(false);
                this.form.userType.getGridField().toggle(false);
                this.form.Number.getGridField().toggle(false);
            }
            else if (this.form.IVRType.value == "3") {
                this.form.Username.getGridField().toggle(true);
                this.form.Password.getGridField().toggle(true);
                this.form.PostUrl.getGridField().toggle(true);
                this.form.ApiKey.getGridField().toggle(true);
                this.form.Plan.getGridField().toggle(false);               
                this.form.AppId.getGridField().toggle(true);
                this.form.AppSecret.getGridField().toggle(false);
                this.form.CliNumber.getGridField().toggle(false);
                this.form.Token_Id.getGridField().toggle(false);
                this.form.userType.getGridField().toggle(false);
                this.form.Number.getGridField().toggle(false);
            }
            else if (this.form.IVRType.value == "4") {
                this.form.Username.getGridField().toggle(false);
                this.form.Password.getGridField().toggle(false);
                this.form.PostUrl.getGridField().toggle(false);
                this.form.ApiKey.getGridField().toggle(false);
                this.form.Plan.getGridField().toggle(false);
                this.form.AppId.getGridField().toggle(false);
                this.form.AppSecret.getGridField().toggle(false);
                this.form.CliNumber.getGridField().toggle(false);
                this.form.IVRNumber.getGridField().toggle(true);
                this.form.Token_Id.getGridField().toggle(true);
                this.form.userType.getGridField().toggle(true);
                this.form.Number.getGridField().toggle(true);

            }
            else {
                this.form.Username.getGridField().toggle(false);
                this.form.Password.getGridField().toggle(false);
                this.form.PostUrl.getGridField().toggle(false);
                this.form.ApiKey.getGridField().toggle(true);
                this.form.Plan.getGridField().toggle(true);          
                this.form.Agents.getGridField().toggle(true);
                this.form.AppId.getGridField().toggle(false);
                this.form.AppSecret.getGridField().toggle(false);
                this.form.CliNumber.getGridField().toggle(true);

                this.form.Token_Id.getGridField().toggle(false);
                this.form.userType.getGridField().toggle(false);
                this.form.Number.getGridField().toggle(false);
            }
        }
    }
}