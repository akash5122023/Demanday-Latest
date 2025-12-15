namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class WebsiteEnquiryBulkDialog extends Serenity.PropertyDialog<any, any> {

       

        private form: WebsiteEnquiryBulkForm;
        public MIds: string[];
        

        constructor() {
            super();

            this.form = new WebsiteEnquiryBulkForm(this.idPrefix);
        }

        protected getDialogTitle(): string {
            return "Bulk Move";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                
                {
                    text: 'Assign',
                    click: () => {
                        if (!this.validateBeforeSave())
                            return;

                        if (this.form.UIds.value == null ||
                            Q.isEmptyOrNull(this.form.UIds.value)){
                            Q.notifyError("Please select a User!");
                            return;
                        }

                        WebsiteEnquiryService.BulkMoveToEnquiry({
                            EnqIds: this.options.MIds,
                            Ids: this.form.UIds.values
                        }, response => {

                            if (response.Inserted < 1) {
                                Q.notifyError('No enquiries found in selected Gird');
                            }
                            else {
                                Q.notifyInfo((response.Inserted || 0) + ' Enquiries added successfully');
                            }

                            if (response.ErrorList != null && response.ErrorList.length > 0) {
                                Q.notifyError(response.ErrorList.join(',\r\n '));
                            }
                                                      
                            this.dialogClose();
                        });
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












