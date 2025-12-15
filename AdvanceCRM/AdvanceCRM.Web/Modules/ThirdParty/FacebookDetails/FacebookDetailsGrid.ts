
namespace AdvanceCRM.ThirdParty {

    import fld = AdvanceCRM.ThirdParty.FacebookDetailsRow.Fields;
    @Serenity.Decorators.registerClass()
    export class FacebookDetailsGrid extends GridBase<FacebookDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.FacebookDetails'; }
        protected getDialogType() { return FacebookDetailsDialog; }
        protected getIdProperty() { return FacebookDetailsRow.idProperty; }
        protected getInsertPermission() { return FacebookDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return FacebookDetailsRow.localTextPrefix; }
        protected getService() { return FacebookDetailsService.baseUrl; }


       // public rowSelection = new Serenity.GridRowSelectionMixin(this);

        constructor(container: JQuery) {
            super(container);

            FacebookDetailsService.Sync({},
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
                    FacebookDetailsService.Sync({},
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                    this.refresh();
                },
                separator: true
            });

            //buttons.push({
            //    title: 'Bulk Move',
            //    hint: 'Bulk Move to Enquiry',
            //    icon: " text-blue",
            //    onClick: () => {
            //      //  var action = new Enquiry.EnquiryExcelImportDialog();
            //        var action = new ThirdParty.FacebookBulkDialog();
            //        action.element.on('dialogclose', () => {
            //            action.MIds = this.rowSelection.getSelectedKeys();
            //            //action.MIds = this.rowSelection.
            //           // this.refresh();
            //            //action = null;
            //        });
            //        action.dialogOpen();
            //    },
            //        ////// dialog.dialogOpen();
            //        ////action.dialogOpen();
            //        separator: true
                
            //});

            buttons.push({
                title: 'Bulk Move',
                hint: 'Bulk Move to Enquiry',
                icon: " text-blue",
                onClick: () => {
                    //  var action = new Enquiry.EnquiryExcelImportDialog();
                    var action = new ThirdParty.FacebookBulkDialog();
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

      
    }
}