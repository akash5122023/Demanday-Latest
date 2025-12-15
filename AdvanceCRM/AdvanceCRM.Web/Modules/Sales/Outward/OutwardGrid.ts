
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class OutwardGrid extends GridBase<OutwardRow, any> {
        protected getColumnsKey() { return 'Sales.Outward'; }
        protected getDialogType() { return OutwardDialog; }
        protected getIdProperty() { return OutwardRow.idProperty; }
        protected getInsertPermission() { return OutwardRow.insertPermission; }
        protected getLocalTextPrefix() { return OutwardRow.localTextPrefix; }
        protected getService() { return OutwardService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            return buttons;
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
                    reportKey: 'Outward.PrintOutward',
                    params: {
                        Id: item.Id
                    }
                });
            }
        }
    }
}