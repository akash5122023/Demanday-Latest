
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class GrnTwoDialog extends DialogBase<GrnTwoRow, any> {
        protected getFormKey() { return GrnTwoForm.formKey; }
        protected getIdProperty() { return GrnTwoRow.idProperty; }
        protected getLocalTextPrefix() { return GrnTwoRow.localTextPrefix; }
        protected getNameProperty() { return GrnTwoRow.nameProperty; }
        protected getService() { return GrnTwoService.baseUrl; }
        protected getDeletePermission() { return GrnTwoRow.deletePermission; }
        protected getInsertPermission() { return GrnTwoRow.insertPermission; }
        protected getUpdatePermission() { return GrnTwoRow.updatePermission; }

        protected form = new GrnTwoForm(this.idPrefix);
        private initialLoad = true;

        protected onDialogOpen() {
            super.onDialogOpen();

            // Set GRN Type to 1 by default on initial load
            this.form.GrnType.value = "1";
            this.initialLoad = true;

            // Initialize PO fields visibility based on default GRN Type
            this.togglePoFieldsVisibility();

            // Set up GRNType change handler
            this.form.GrnType.change(e => {
                if (!this.initialLoad) {
                    this.togglePoFieldsVisibility();
                }
                this.initialLoad = false;
            });

            // Skip initial visibility toggle since we're setting default value
            this.initialLoad = false;

            // Rest of your existing code...
            this.form.ContactsId.changeSelect2(e => {
                var contactId = Q.toId(this.form.ContactsId.value);
                if (contactId) {
                    var contact = Contacts.ContactsRow.getLookup().itemById[contactId];
                    this.form.ContactsPhone.value = contact.Phone;
                }
            });

            if (this.isNew()) {
                this.form.Status.value = "1";
            }

            if (this.form.OwnerId.value < "1") {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            if (this.form.AssignedId.value < "1") {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }
        }

        private hidePoFields(): void {
            this.form.Po.element.closest('.field').hide();
            this.form.PoDate.element.closest('.field').hide();
        }

        private togglePoFieldsVisibility(): void {
            // Only proceed if there's a value selected
            if (!this.form.GrnType.value) return;

            var grnTypeValue = Q.toId(this.form.GrnType.value);
            var showPoFields = grnTypeValue === 1;

            this.form.Po.element.closest('.field').toggle(showPoFields);
            this.form.PoDate.element.closest('.field').toggle(showPoFields);

            this.form.Po.getGridField().toggleClass('required', showPoFields);
            this.form.PoDate.getGridField().toggleClass('required', showPoFields);

            if (!showPoFields) {
                this.form.Po.value = '';
                this.form.PoDate.value = null;
            }
        }
    }
}