
namespace AdvanceCRM.ThirdParty {
    import fld = AdvanceCRM.ThirdParty.SIndiaMartDetailsRow.Fields;
    @Serenity.Decorators.registerClass()
    export class SIndiaMartDetailsGrid extends GridBase<SIndiaMartDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.SIndiaMartDetails'; }
        protected getDialogType() { return SIndiaMartDetailsDialog; }
        protected getIdProperty() { return SIndiaMartDetailsRow.idProperty; }
        protected getInsertPermission() { return SIndiaMartDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return SIndiaMartDetailsRow.localTextPrefix; }
        protected getService() { return SIndiaMartDetailsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            SIndiaMartDetailsService.Sync({},
                response => { }
            );
        }
        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Enquiries',
                onClick: () => {
                    SIndiaMartDetailsService.Sync({},
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                    this.refresh();
                },
                separator: true
            });

            buttons.push({

                title: 'Sync2',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Enquiries from Version 2 API',
                onClick: () => {
                    SIndiaMartDetailsService.Sync2({},
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
                    //  var action = new Enquiry.EnquiryExcelImportDialog();
                    var action = new ThirdParty.SIndiaMartBulkDialog();
                    action.element.on('dialogclose', () => {
                        action.MIds = this.rowSelection.getSelectedKeys();
                        //action.MIds = this.rowSelection.
                        // this.refresh();
                        //action = null;
                    });
                    action.dialogOpen();
                },
                ////// dialog.dialogOpen();
                ////action.dialogOpen();
                separator: true

            });

            return buttons;
        }

        protected getColumns() {
            var columns = super.getColumns();
            Q.first(columns, x => x.field == fld.CountryFlag).format =
                ctx => `<a href="${Q.htmlEncode(ctx.value)};" class="inline-image"><img src="${Q.htmlEncode(ctx.value)}"/></a>`;

            return columns;
        }
    }
}