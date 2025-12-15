
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class IndentDialog extends DialogBase<IndentRow, any> {
        protected getFormKey() { return IndentForm.formKey; }
        protected getIdProperty() { return IndentRow.idProperty; }
        protected getLocalTextPrefix() { return IndentRow.localTextPrefix; }
        protected getNameProperty() { return IndentRow.nameProperty; }
        protected getService() { return IndentService.baseUrl; }
        protected getDeletePermission() { return IndentRow.deletePermission; }
        protected getInsertPermission() { return IndentRow.insertPermission; }
        protected getUpdatePermission() { return IndentRow.updatePermission; }

        protected form = new IndentForm(this.idPrefix);
        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'Indent.PrintIndent',
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));

            return buttons;
        }


        protected onDialogOpen() {
            super.onDialogOpen();

            if (this.isNew()) {
                this.form.Date.value = Q.formatDate(new Date(), 'yyyy-MM-dd');
            }


            this.form.ContactsId.changeSelect2(e => {
                var contactId = Q.toId(this.form.ContactsId.value);
                if (contactId) {
                    var contact = Contacts.ContactsRow.getLookup().itemById[contactId];
                    this.form.ContactsPhone.value = contact.Phone;
                }
            });

            if (this.form.OwnerId.value < 1) {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < 1) {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }


            // Set default status to "Open" for new records
            if (this.isNew()) {
                this.form.Status.value = 1; // Assuming "1" is the value for "Open"
            }

        }
    }
}