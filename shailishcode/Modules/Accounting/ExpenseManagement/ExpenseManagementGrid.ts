
/// <reference path="../../Common/Helpers/GridBase.ts" />
namespace AdvanceCRM.Accounting {

    export class ExpenseManagementGrid extends GridBase<ExpenseManagementRow, any> {
        protected getColumnsKey() { return 'Accounting.ExpenseManagement'; }
        protected getDialogType() { return ExpenseManagementDialog; }
        protected getIdProperty() { return ExpenseManagementRow.idProperty; }
        protected getInsertPermission() { return ExpenseManagementRow.insertPermission; }
        protected getLocalTextPrefix() { return ExpenseManagementRow.localTextPrefix; }
        protected getService() { return ExpenseManagementService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        validateEntity(row: ExpenseManagementRow, id) {

            row.HeadId = Q.toId(row.HeadId);
            row.Head = Masters.AccountingHeadsRow.getLookup().itemById[row.HeadId].Head;

            row.RepresentativeId = Q.toId(row.RepresentativeId);
            row.RepresentativeDisplayName = Administration.UserRow.getLookup().itemById[row.RepresentativeId].DisplayName;

            row.ApprovedBy = Q.toId(row.ApprovedBy);
            row.ApprovedByDisplayName = Administration.UserRow.getLookup().itemById[row.ApprovedByDisplayName].DisplayName;

            return true;
        }

        //Sum at bottom
        protected createSlickGrid() {
            var grid = super.createSlickGrid();

            this.view.setSummaryOptions({
                aggregators: [
                    new Slick.Aggregators.Sum('Amount')
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

    }
}