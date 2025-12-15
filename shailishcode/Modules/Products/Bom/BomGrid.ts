
namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    export class BomGrid extends GridBase<BomRow, any> {
        protected getColumnsKey() { return 'Products.Bom'; }
        protected getDialogType() { return BomDialog; }
        protected getIdProperty() { return BomRow.idProperty; }
        protected getInsertPermission() { return BomRow.insertPermission; }
        protected getLocalTextPrefix() { return BomRow.localTextPrefix; }
        protected getService() { return BomService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected createSlickGrid() {
            var grid = super.createSlickGrid();

            this.view.setSummaryOptions({
                aggregators: [
                    new Slick.Aggregators.Sum('GrandTotal')
                ]
            });

            grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());

            return grid;
        }

        protected getSlickOptions() {
            var opt = super.getSlickOptions();
            opt.showFooterRow = true;
            return opt;
        }

        getButtons() {
            var buttons = super.getButtons();

            var filterButton = buttons.pop();

            buttons.push(filterButton);

            return buttons;
        }

        //Adding Print Preview for Bom
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

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts != true) {
                columns.splice(Q.indexOf(columns, x => x.field == "ContactPersonProject"), 1);
            }
            return columns;
        }

        //Adding Bom preview button
        protected onInlineActionClick(target, recordId, item): void {
            super.onInlineActionClick(target, recordId, item);

            if (target.hasClass('print-row')) {
                AdvanceCRM.Common.ReportHelper.execute({
                    reportKey: 'Bom.PrintBom',
                    params: {
                        Id: item.Id
                    }
                });
            }
        }

    }
}