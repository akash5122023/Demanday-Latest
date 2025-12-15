
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class WebsiteEnquiryGrid extends GridBase<WebsiteEnquiryRow, any> {
        protected getColumnsKey() { return 'ThirdParty.WebsiteEnquiry'; }
        protected getDialogType() { return WebsiteEnquiryDialog; }
        protected getIdProperty() { return WebsiteEnquiryRow.idProperty; }
        protected getInsertPermission() { return WebsiteEnquiryRow.insertPermission; }
        protected getLocalTextPrefix() { return WebsiteEnquiryRow.localTextPrefix; }
        protected getService() { return WebsiteEnquiryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            buttons.push({
                title: 'Bulk Move',
                hint: 'Bulk Move to Enquiry',
                icon: " text-blue",
                onClick: () => {

                    var action = new ThirdParty.WebsiteEnquiryBulkDialog();
                    action.element.on('dialogclose', () => {
                        action.MIds = this.rowSelection.getSelectedKeys();
                    });
                    action.dialogOpen();
                },
                separator: true
            });

            return buttons;
        }
    }
}