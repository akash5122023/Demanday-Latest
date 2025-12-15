namespace AdvanceCRM.MailChimpList {

    @Serenity.Decorators.registerClass()
    export class MailChimpListDialog extends Serenity.PropertyDialog<any, any> {

        private form: MailChimpListForm;
        public MIds: string[];
        public MModule: string;

        constructor() {
            super();

            this.form = new MailChimpListForm(this.idPrefix);
        }

        protected getDialogTitle(): string {
            return "Create and Add";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Create and Add',
                    click: () => {
                        if (!this.validateBeforeSave())
                            return;

                        if (this.form.ListName.value == null ||
                            Q.isEmptyOrNull(this.form.ListName.value)) {
                            Q.notifyError("Please specify list name!");
                            return;
                        }

                        if (this.MModule == "Contacts") {
                            Contacts.ContactsService.AddToMailChimp({
                                MailChimpIds: this.MIds,
                                ListName: this.form.ListName.value
                            },
                                response => {
                                    if (response.MailChimpReturnResponse == "Success") {
                                        Q.information('Selected contact added successfully to MailChimp ' + this.form.ListName.value + ' list', () => { Q.resolveUrl('#'); });
                                    }
                                    else {
                                        if (response.MailChimpReturnResponse == "In selected list no valid email Id where found") {
                                            Q.alert(response.MailChimpReturnResponse);
                                        }
                                        else {
                                            Q.alert('Error occurred while adding selected contacts to MailChimp list\n' + response.MailChimpReturnResponse);
                                        }
                                    }
                                },
                                { async: true }
                            )
                        }
                        else if (this.MModule == "Enquiry") {
                            Enquiry.EnquiryService.AddToMailChimp({
                                MailChimpIds: this.MIds,
                                ListName: this.form.ListName.value
                            },
                                response => {
                                    if (response.MailChimpReturnResponse == "Success") {
                                        Q.information('Selected contact added successfully to MailChimp ' + this.form.ListName.value + ' list', () => { Q.resolveUrl('#'); });
                                    }
                                    else {
                                        if (response.MailChimpReturnResponse == "In selected list no valid email Id where found") {
                                            Q.alert(response.MailChimpReturnResponse);
                                        }
                                        else {
                                            Q.alert('Error occurred while adding selected contacts to MailChimp list\n' + response.MailChimpReturnResponse);
                                        }
                                    }
                                },
                                { async: true }
                            )
                        }
                        else if (this.MModule == "Quotation") {
                            Quotation.QuotationService.AddToMailChimp({
                                MailChimpIds: this.MIds,
                                ListName: this.form.ListName.value
                            },
                                response => {
                                    if (response.MailChimpReturnResponse == "Success") {
                                        Q.information('Selected contact added successfully to MailChimp ' + this.form.ListName.value + ' list', () => { Q.resolveUrl('#'); });
                                    }
                                    else {
                                        if (response.MailChimpReturnResponse == "In selected list no valid email Id where found") {
                                            Q.alert(response.MailChimpReturnResponse);
                                        }
                                        else {
                                            Q.alert('Error occurred while adding selected contacts to MailChimp list\n' + response.MailChimpReturnResponse);
                                        }
                                    }
                                },
                                { async: true }
                            )
                        }
                        else if (this.MModule == "Sales") {
                            Sales.SalesService.AddToMailChimp({
                                MailChimpIds: this.MIds,
                                ListName: this.form.ListName.value
                            },
                                response => {
                                    if (response.MailChimpReturnResponse == "Success") {
                                        Q.information('Selected contact added successfully to MailChimp ' + this.form.ListName.value + ' list', () => { Q.resolveUrl('#'); });
                                    }
                                    else {
                                        if (response.MailChimpReturnResponse == "In selected list no valid email Id where found") {
                                            Q.alert(response.MailChimpReturnResponse);
                                        }
                                        else {
                                            Q.alert('Error occurred while adding selected contacts to MailChimp list\n' + response.MailChimpReturnResponse);
                                        }
                                    }
                                },
                                { async: true }
                            )
                        }

                    },
                },
                {
                    text: 'Cancel',
                    click: () => this.dialogClose()
                }
            ];
        }
    }
}