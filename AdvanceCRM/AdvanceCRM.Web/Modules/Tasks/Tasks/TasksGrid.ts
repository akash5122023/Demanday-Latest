

namespace AdvanceCRM.Tasks {

    import fld = TasksRow.Fields;
    @Serenity.Decorators.registerClass()
    export class TasksGrid extends GridBase<TasksRow, any> {
        protected getColumnsKey() { return 'Tasks.Tasks'; }
        protected getDialogType() { return TasksDialog; }
        protected getIdProperty() { return TasksRow.idProperty; }
        protected getInsertPermission() { return TasksRow.insertPermission; }
        protected getLocalTextPrefix() { return TasksRow.localTextPrefix; }
        protected getService() { return TasksService.baseUrl; }


        protected getColumns() {
            let columns = super.getColumns();

            // Add your column visibility logic here
            if (Administration.CompanyDetailsRow.getLookup().itemById[1].TaskTitleInTask != true) {
                columns.splice(Q.indexOf(columns, x => x.field == "TaskTitle"), 1);
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].TaskMasterInTask != true) {
                columns.splice(Q.indexOf(columns, x => x.field == "TaskId"), 1);
            }

            return columns;
        }

        constructor(container: JQuery) {
            super(container);
           
            var q = Q.parseQueryString();

            if (q["Open"]) {
                this.findQuickFilter(Serenity.LookupEditor, fld.StatusId).value = "1";
                var user = Q.tryFirst(Administration.UserRow.getLookup().items, x => x.Username == Q.Authorization.userDefinition.Username);
                this.findQuickFilter(Administration.UserEditor, fld.AssignedTo).value = Q.toId(user.UserId);
            }
            else if (q["OpenTeam"]) {
                this.findQuickFilter(Serenity.LookupEditor, fld.StatusId).value = "1";
            }

            this.element.find('.quick-filters-bar').toggle(false)
        }

        getButtons() {
            var buttons = super.getButtons();

            var filterButton = buttons.pop();

            //buttons.push({
            //    title: 'QuickMail',
            //    cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
            //    hint: 'Quick Mail Sender',
            //    onClick: () => {
            //        var selectedKeys = this.rowSelection.getSelectedKeys(); // Get selected row IDs
            //        if (selectedKeys.length === 0) {
            //            Q.alert("Please select at least one contact.");
            //            return;
            //        }

            //        var contacts = selectedKeys.map(id => {
            //            var row = this.view.getItemById(id);
            //            return {
            //                id: id,
            //                email: row.ContactsEmail
            //            };
            //        }).filter(c => c.email); // Ensure valid contacts

            //        if (contacts.length === 0) {
            //            Q.alert("No valid contacts found.");
            //            return;
            //        }

            //        var dialog = new Common.QuickMailDialog({ Contacts: contacts });

            //        dialog.element.on('dialogclose', () => {
            //            dialog = null;
            //        });

            //        dialog.dialogOpen();
            //    }
            //    ,
            //    separator: true
            //});


            if (Authorization.hasPermission("SMS:BulkSMS")) {
                buttons.push(
                    {
                        title: 'SMS',
                        icon: 'fa-comments-o text-green',
                        onClick: () => {
                            var dialog = new Common.BulkSMSDialog({
                                Ids: this.rowSelection.getSelectedKeys(),
                                ServiceURL: this.getService()
                            });

                            dialog.element.on('dialogclose', () => {
                                this.rowSelection.resetCheckedAndRefresh();
                                dialog = null;
                            });

                            dialog.dialogOpen();
                        }
                    }
                );
            }

            buttons.push(filterButton);

            return buttons;
        }
    }
}