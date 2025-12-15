namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class IndiaMartBulkDialog extends Serenity.PropertyDialog<any, any> {

       

        private form: IndiaMartBulkForm;
        public MIds: string[];
        

        constructor() {
            super();

            this.form = new IndiaMartBulkForm(this.idPrefix);
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

                        IndiaMartDetailsService.BulkMoveToEnquiry({
                            //EnqIds: this.options.MIds,
                            EnqIds: this.MIds,
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












////namespace AdvanceCRM.ThirdParty {

////    @Serenity.Decorators.registerClass()
////  /*  @Serenity.Decorators.responsive()*/
////    export class FacebookBulkDialog extends Serenity.PropertyDialog<any, any> {

////        private form: FacebookBulkForm;      
       

////        constructor() {
////            super();

////            this.form = new FacebookBulkForm(this.idPrefix);
////        }

////        protected getDialogTitle(): string {
////            return "Bulk Move";
////        }

////        protected getDialogButtons(): Serenity.DialogButton[] {
////            return [
////                {
////                    text: 'Execute',
////                    click: () => {
////                        FacebookDetailsService.BulkMoveToEnquiry(
////                            {                               
////                              //  ToID: Q.toId(this.form.To.value)
////                                 Ids: this.form.UIds.values
////                            },
////                            response => {
////                                Q.notifyInfo(response.Status);
////                            }
////                        );

////                        this.dialogClose();
////                    }

////                },
////                {
////                    text: 'Cancel',
////                    click: () => this.dialogClose()
////                }
////            ];
////        }
////    }
////}