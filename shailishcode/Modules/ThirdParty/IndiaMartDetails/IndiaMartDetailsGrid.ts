
namespace AdvanceCRM.ThirdParty {

    import fld = AdvanceCRM.ThirdParty.IndiaMartDetailsRow.Fields;
    @Serenity.Decorators.registerClass()
    export class IndiaMartDetailsGrid extends GridBase<IndiaMartDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.IndiaMartDetails'; }
        protected getDialogType() { return IndiaMartDetailsDialog; }
        protected getIdProperty() { return IndiaMartDetailsRow.idProperty; }
        protected getInsertPermission() { return IndiaMartDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return IndiaMartDetailsRow.localTextPrefix; }
        protected getService() { return IndiaMartDetailsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            IndiaMartDetailsService.Sync({},
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
                    IndiaMartDetailsService.Sync({},
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
                    IndiaMartDetailsService.Sync2({},
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
                    var action = new ThirdParty.IndiaMartBulkDialog();
                    //action.element.on('dialogclose', () => {
                        action.MIds = this.rowSelection.getSelectedKeys();
                        //action.MIds = this.rowSelection.
                        // this.refresh();
                        //action = null;
                    //});
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