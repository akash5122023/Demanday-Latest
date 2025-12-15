
namespace AdvanceCRM.ThirdParty {
    import fld = AdvanceCRM.ThirdParty.KnowlarityDetailsRow.Fields;
    @Serenity.Decorators.registerClass()
    export class KnowlarityDetailsGrid extends GridBase<KnowlarityDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.KnowlarityDetails'; }
        protected getDialogType() { return KnowlarityDetailsDialog; }
        protected getIdProperty() { return KnowlarityDetailsRow.idProperty; }
        protected getInsertPermission() { return KnowlarityDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return KnowlarityDetailsRow.localTextPrefix; }
        protected getService() { return KnowlarityDetailsService.baseUrl; }

        protected callDurationFilter: Serenity.EnumEditor;

        constructor(container: JQuery) {
            super(container);
        }

        ////Conditional formatiing
        //protected getColumns(): Slick.Column[] {
        //    var columns = super.getColumns();

        //    let inlineActionsColumnWidth = 22;
        //    let inlineActionsColumnContent = `<audio controls autoplay><source src = '" + url + "' /></audio>`;

        //    columns.splice(1, 0, {
        //        field: 'inline-actions',
        //        name: '',
        //        cssClass: 'inline-actions-column',
        //        width: inlineActionsColumnWidth,
        //        minWidth: inlineActionsColumnWidth,
        //        maxWidth: inlineActionsColumnWidth,
        //        format: ctx => inlineActionsColumnContent
        //    });

        //    // adding a specific css class to UnitPrice column, 
        //    // to be able to format cell with a different background
        //    Q.first(columns, x => x.field == fld.Type).cssClass += " col-unit-Quotcolor";

        //    if (Administration.CompanyDetailsRow.getLookup().itemById[1].ProjectWithContacts != true) {
        //        columns.splice(Q.indexOf(columns, x => x.field == "ContactPersonProject"), 1);
        //    }

        //    return columns;
        //}

        protected createQuickFilters() {
            super.createQuickFilters();

            this.callDurationFilter = this.findQuickFilter(Serenity.EnumEditor, fld.CallDurationState);
        }
        public set_CallDurationState(value: number): void {
            //this.calldurationfilter.value = value == null ? '' : value.tostring();
            this.callDurationFilter.value = value == null ? '' : value.toString();
            //this.calldurationfilter.value = value == 0 ? '' : 0;
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Enquiries',
                onClick: () => {
                    KnowlarityDetailsService.Sync({},
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                    this.refresh();
                },
                separator: true
            });

            buttons.push({
                title: 'Bulk Move',
                hint: 'Bulk Move to Enquiry',
                icon: " text-blue",
                onClick: () => {
                  
                    var action = new ThirdParty.KnowlarityBulkDialog();
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