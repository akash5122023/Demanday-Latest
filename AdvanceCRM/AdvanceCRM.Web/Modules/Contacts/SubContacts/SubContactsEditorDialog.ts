
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />

namespace AdvanceCRM.Contacts {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class SubContactsEditorDialog extends Common.GridEditorDialog<SubContactsRow> {
        protected getFormKey() { return SubContactsForm.formKey; }
        protected getLocalTextPrefix() { return SubContactsRow.localTextPrefix; }
        protected getNameProperty() { return SubContactsRow.nameProperty; }
        protected form = new SubContactsForm(this.idPrefix);

        constructor(container: JQuery) {
            super(container);

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].PassportDetails != true) {

                this.form.PassportNumber.getGridField().toggle(false);
                this.form.FirstName.getGridField().toggle(false);
                this.form.LastName.getGridField().toggle(false);
                this.form.ExpiryDate.getGridField().toggle(false);
                this.element.find(".category-title:contains('PassportDetails')").parent().toggle(false);

            }
            else {

                this.form.PassportNumber.getGridField().toggle(true);
                this.form.FirstName.getGridField().toggle(true);
                this.form.LastName.getGridField().toggle(true);
                this.form.ExpiryDate.getGridField().toggle(true);
                this.element.find(".category-title:contains('PassportDetails')").parent().toggle(true);


            }

            this.form.MarriageAnniversary.getGridField().toggle(false);

            this.form.MaritalStatus.change(e => {
                if (this.form.MaritalStatus.value == "2") {
                    this.form.MarriageAnniversary.getGridField().toggle(true);
                }
                else {
                    this.form.MarriageAnniversary.getGridField().toggle(false);
                }
            });
        }

    }
}