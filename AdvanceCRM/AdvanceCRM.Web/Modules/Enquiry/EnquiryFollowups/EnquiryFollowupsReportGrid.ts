
namespace AdvanceCRM.Enquiry {

    @Serenity.Decorators.registerClass()
    export class EnquiryFollowupsReportGrid extends GridBase<EnquiryFollowupsRow, any> {
        protected getColumnsKey() { return 'Enquiry.EnquiryFollowups'; }
        protected getIdProperty() { return EnquiryFollowupsRow.idProperty; }
        protected getInsertPermission() { return EnquiryFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return EnquiryFollowupsRow.localTextPrefix; }
        protected getService() { return EnquiryFollowupsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }


        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();
            return buttons;
        }
    }
}