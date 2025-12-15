
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class CampaignBmDialog extends DialogBase<CampaignBmRow, any> {
        protected getFormKey() { return CampaignBmForm.formKey; }
        protected getIdProperty() { return CampaignBmRow.idProperty; }
        protected getLocalTextPrefix() { return CampaignBmRow.localTextPrefix; }
        protected getNameProperty() { return CampaignBmRow.nameProperty; }
        protected getService() { return CampaignBmService.baseUrl; }
        protected getDeletePermission() { return CampaignBmRow.deletePermission; }
        protected getInsertPermission() { return CampaignBmRow.insertPermission; }
        protected getUpdatePermission() { return CampaignBmRow.updatePermission; }

        protected form = new CampaignBmForm(this.idPrefix);


        constructor() {
            super();

            this.form.BmListId.changeSelect2(e => {
                var SourceName = BizMail.BmListRow.getLookup().itemById[this.form.BmListId.value].ListId;
                this.form.ListId.value = SourceName;

                
            });
        }

    }
}