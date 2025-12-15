
namespace AdvanceCRM.Services {

    import fld = CMSRow.Fields;
    export class CMSGrid extends GridBase<CMSRow, any> {
        protected getColumnsKey() { return 'Services.CMS'; }
        protected getDialogType() { return CMSDialog; }
        protected getIdProperty() { return CMSRow.idProperty; }
        protected getInsertPermission() { return CMSRow.insertPermission; }
        protected getLocalTextPrefix() { return CMSRow.localTextPrefix; }
        protected getService() { return CMSService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            var q = Q.parseQueryString();
            if (q["Open"]) {
                this.findQuickFilter(Serenity.EnumEditor, fld.Status).value = "1";
                var user = Q.tryFirst(Administration.UserRow.getLookup().items, x => x.Username == Q.Authorization.userDefinition.Username);
                this.findQuickFilter(Administration.UserEditor, fld.AssignedTo).value = Q.toId(user.UserId);
            }
            else if (q["OpenTeam"]) {
                this.findQuickFilter(Serenity.EnumEditor, fld.Status).value = "1";
            }

            this.element.find('.quick-filters-bar').toggle(false)
        }

        getButtons() {
            var buttons = super.getButtons();
            var filterButton = buttons.pop();

            //Buttons for groupping
            buttons.push(
                {
                    title: 'Category',
                    icon: 'fa-object-group text-yellow',
                    onClick: () => {
                        //Q.information('Grouping:\n1=Open, 2=Closed, 3=Pending', () => { Q.resolveUrl('#'); });
                        this.view.setGrouping(
                            [{
                                formatter: x => Serenity.EnumFormatter.format(Serenity.EnumTypeRegistry.get('Masters.CMSCategory'), Q.toId(x.value)),
                                getter: 'Category'
                            }])
                    },
                    separator: true
                }
            );

            buttons.push(
                {
                    title: 'Priority',
                    icon: 'fa-object-group text-yellow',
                    onClick: () => {
                        this.view.setGrouping(
                            [{
                                formatter: x => Serenity.EnumFormatter.format(Serenity.EnumTypeRegistry.get('Masters.Priority'), Q.toId(x.value)),
                                getter: 'Priority'
                            }])
                    }
                }
            );
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

            buttons.push(
                {
                    title: 'Grouping',
                    cssClass: 'delete-button',
                    onClick: () => {
                        this.view.setGrouping([])
                    }
                }
            );

            buttons.push(filterButton);

            return buttons;
        }
    }
}