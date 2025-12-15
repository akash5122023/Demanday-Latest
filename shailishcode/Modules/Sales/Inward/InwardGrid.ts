
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class InwardGrid extends GridBase <InwardRow, any> {
        protected getColumnsKey() { return 'Sales.Inward'; }
        protected getDialogType() { return InwardDialog; }
        protected getIdProperty() { return InwardRow.idProperty; }
        protected getInsertPermission() { return InwardRow.insertPermission; }
        protected getLocalTextPrefix() { return InwardRow.localTextPrefix; }
        protected getService() { return InwardService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        protected getColumns(): Slick.Column[] {
            var columns = super.getColumns();

            let inlineActionsColumnWidth = 22;
            let inlineActionsColumnContent = `<a class="inline-actions print-row" title="${q.text('Controls.Print', 'Print')}" style="padding-left: 5px;"><i class="print-row fa fa-print"></i></a>`;

            columns.splice(1, 0, {
                field: 'inline-actions',
                name: '',
                cssClass: 'inline-actions-column',
                width: inlineActionsColumnWidth,
                minWidth: inlineActionsColumnWidth,
                maxWidth: inlineActionsColumnWidth,
                format: ctx => inlineActionsColumnContent
            });

            return columns;
        }

        protected onInlineActionClick(target, recordId, item): void {
            super.onInlineActionClick(target, recordId, item);

            if (target.hasClass('print-row')) {
                AdvanceCRM.Common.ReportHelper.execute({
                    reportKey: 'Inward.PrintInward',
                    params: {
                        Id: item.Id
                    }
                });
            }
        }
    }
}