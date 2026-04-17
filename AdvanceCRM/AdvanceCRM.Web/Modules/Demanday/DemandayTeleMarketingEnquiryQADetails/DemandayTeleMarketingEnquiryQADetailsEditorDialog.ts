/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />

namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class DemandayTeleMarketingEnquiryQADetailsEditorDialog extends Common.GridEditorDialog<DemandayTeleMarketingEnquiryQADetailsRow> {
        protected getFormKey() { return DemandayTeleMarketingEnquiryQADetailsForm.formKey; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryQADetailsRow.localTextPrefix; }
        protected form = new DemandayTeleMarketingEnquiryQADetailsForm(this.idPrefix);

        protected afterLoadEntity() {
            super.afterLoadEntity();

            // Trigger cascade chain: CampaignId → QuestionId → AnswerId
            // loadEntity sets values but does not fire change events,
            // so the cascade LookupEditors never update their filtered items.
            this.form.CampaignId.element.triggerHandler('change');
            this.form.QuestionId.element.triggerHandler('change');
        }
    }
}
