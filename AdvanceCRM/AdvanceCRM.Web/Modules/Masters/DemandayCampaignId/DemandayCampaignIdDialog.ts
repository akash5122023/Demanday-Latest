
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayCampaignIdDialog extends Serenity.EntityDialog<DemandayCampaignIdRow, any> {
        protected getFormKey() { return DemandayCampaignIdForm.formKey; }
        protected getIdProperty() { return DemandayCampaignIdRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayCampaignIdRow.localTextPrefix; }
        protected getNameProperty() { return DemandayCampaignIdRow.nameProperty; }
        protected getService() { return DemandayCampaignIdService.baseUrl; }
        protected getDeletePermission() { return DemandayCampaignIdRow.deletePermission; }
        protected getInsertPermission() { return DemandayCampaignIdRow.insertPermission; }
        protected getUpdatePermission() { return DemandayCampaignIdRow.updatePermission; }

        protected form = new DemandayCampaignIdForm(this.idPrefix);

    }
}