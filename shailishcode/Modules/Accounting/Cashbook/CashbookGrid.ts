
/// <reference path="../../Common/Helpers/GridBase.ts" />
namespace AdvanceCRM.Accounting {

    export class CashbookGrid extends GridBase<CashbookRow, any> {
        protected getColumnsKey() { return 'Accounting.Cashbook'; }
        protected getDialogType() { return CashbookDialog; }
        protected getIdProperty() { return CashbookRow.idProperty; }
        protected getInsertPermission() { return CashbookRow.insertPermission; }
        protected getLocalTextPrefix() { return CashbookRow.localTextPrefix; }
        protected getService() { return CashbookService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        //Sum at bottom
        protected createSlickGrid() {
            var grid = super.createSlickGrid();

            this.view.setSummaryOptions({
                aggregators: [
                    new Slick.Aggregators.Sum('CashIn'),
                    new Slick.Aggregators.Sum('CashOut')
                ]
            });

            // for goruping need to register this plugin for grouping or you'll have errors
            grid.registerPlugin(new Slick.Data.GroupItemMetadataProvider());

            return grid;
        }
        protected getSlickOptions() {
            var opt = super.getSlickOptions();
            opt.showFooterRow = true;
            return opt;
        }


        //protected getQuickFilters() {
        //    let filters = super.getQuickFilters();

        //    filters.push({
        //        field: 'Date',
        //        type: Serenity.DateEditor,
        //        title: 'From Date',
        //        handler: h => {
        //            if (h.value) {
        //                h.request.Criteria = Serenity.Criteria.and(
        //                    h.request.Criteria,
        //                    [['Date'], '>=', h.value]
        //                );
        //            }
        //        }
        //    });

        //    filters.push({
        //        field: 'Date',
        //        type: Serenity.DateEditor,
        //        title: 'To Date',
        //        handler: h => {
        //            if (h.value) {
        //                h.request.Criteria = Serenity.Criteria.and(
        //                    h.request.Criteria,
        //                    [['Date'], '<=', h.value]
        //                );
        //            }
        //        }
        //    });

        //    return filters;
        //}




    }
}