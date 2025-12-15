
namespace AdvanceCRM.Reports {

    import fld = EnquiryProductsRow.Fields;
    @Serenity.Decorators.registerClass()
    export class EnquiryProductsGrid extends GridBase<EnquiryProductsRow, any> {
        protected getColumnsKey() { return 'Reports.EnquiryProducts'; }
        protected getDialogType() { return EnquiryProductsDialog; }
        protected getIdProperty() { return EnquiryProductsRow.idProperty; }
        protected getInsertPermission() { return EnquiryProductsRow.insertPermission; }
        protected getLocalTextPrefix() { return EnquiryProductsRow.localTextPrefix; }
        protected getService() { return EnquiryProductsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            this.view.setGrouping(
                [{
                    formatter: x => 'Enquiry: ' + x.value + ' (' + x.count + ' items)',
                    getter: fld.EnquiryId
                }])
        }
    }
}