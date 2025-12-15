
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class JustDialDetailsGrid extends GridBase<JustDialDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.JustDialDetails'; }
        protected getDialogType() { return JustDialDetailsDialog; }
        protected getIdProperty() { return JustDialDetailsRow.idProperty; }
        protected getInsertPermission() { return JustDialDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return JustDialDetailsRow.localTextPrefix; }
        protected getService() { return JustDialDetailsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            //buttons.push({
            //    title: 'Bulk Move',
            //    hint: 'Bulk Move to Enquiry',
            //    icon: " text-blue",
            //    onClick: () => {
            //        //  var action = new Enquiry.EnquiryExcelImportDialog();
            //        var action = new ThirdParty.JustDialBulkDialog();
            //        action.element.on('dialogclose', () => {
            //            action.MIds = this.rowSelection.getSelectedKeys();
            //            //action.MIds = this.rowSelection.
            //            // this.refresh();
            //            //action = null;
            //        });
            //        action.dialogOpen();
            //    },
            //    ////// dialog.dialogOpen();
            //    ////action.dialogOpen();
            //    separator: true

            //});

            buttons.push({
                title: 'Bulk Move',
                hint: 'Bulk Move to Enquiry',
                icon: "text-blue",
                onClick: () => {
                    const selectedKeys = this.rowSelection.getSelectedKeys(); // Get selected rows

                    if (!selectedKeys || selectedKeys.length === 0) {
                        Q.notifyError("Please select at least one row!");
                        return;
                    }

                    // Create dialog instance
                    const action = new ThirdParty.JustDialBulkDialog();

                    // Assign selected rows to the dialog's MIds
                    action.MIds = selectedKeys;

                    // Open the dialog
                    action.dialogOpen();
                },
                separator: true
            });


            return buttons;
        }
    }
}