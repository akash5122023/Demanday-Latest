
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    export class ChallanGrid extends GridBase<ChallanRow, any> {
        protected getColumnsKey() { return 'Sales.Challan'; }
        protected getDialogType() { return ChallanDialog; }
        protected getIdProperty() { return ChallanRow.idProperty; }
        protected getInsertPermission() { return ChallanRow.insertPermission; }
        protected getLocalTextPrefix() { return ChallanRow.localTextPrefix; }
        protected getService() { return ChallanService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
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

        getButtons() {
            var buttons = super.getButtons();

            buttons.push({
                title: 'BizWhatsApp',
                cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
                hint: 'Bizplus Whatsapp Sender',
                onClick: () => {
                    var selectedKeys = this.rowSelection.getSelectedKeys(); // Get selected row IDs
                    if (selectedKeys.length === 0) {
                        Q.alert("Please select at least one contact.");
                        return;
                    }

                    var contacts = selectedKeys.map(id => {
                        var row = this.view.getItemById(id);
                        return {
                            id: id,
                            number: row.ContactsWhatsapp || row.ContactsPhone,
                            name: row.ContactsName
                        };
                    }).filter(c => c.number); // Ensure valid contacts

                    if (contacts.length === 0) {
                        Q.alert("No valid contacts found.");
                        return;
                    }

                    var dialog = new Common.IntractWaDialog({ Contacts: contacts });

                    dialog.element.on('dialogclose', () => {
                        dialog = null;
                    });

                    dialog.dialogOpen();
                }
                ,
                separator: true
            });
            return buttons;
        }
    
        protected onInlineActionClick(target, recordId, item): void {
            super.onInlineActionClick(target, recordId, item);

            if (target.hasClass('print-row')) {
                AdvanceCRM.Common.ReportHelper.execute({
                    reportKey: 'Challan.PrintChallan',
                    params: {
                        Id: item.Id
                    }
                });
            }
        }
    }
}