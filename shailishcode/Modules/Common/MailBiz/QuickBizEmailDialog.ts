
namespace AdvanceCRM.Common {
    export class QuickBizEmailOptions {
        Ids: string[];
        ServiceURL: string;
    }

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class QuickBizEmailDialog extends Serenity.PropertyDialog<QuickBizEmailOptions, any> {

        private form: QuickBizEmailForm;

        constructor(opt?: QuickBizEmailOptions) {
            super(opt);

            this.form = new QuickBizEmailForm(this.idPrefix);
        }

        protected getDialogTitle(): string {
            return "Bulk Email Sender";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Send',
                    click: () => {
                        if (!this.validateBeforeSave()) {
                            return;
                        }

                        var action = new Common.BulkEmailAction(this.options.ServiceURL, this.form.Message.value, this.form.Subject.value,this.form.Date.value);
                        action.execute(this.options.Ids);

                        this.dialogClose();
                    }
                    //text: 'Send',
                    //click: () => {
                    //    if (!this.validateBeforeSave()) {
                    //        return;
                    //    }

                    //    CommonService.SendBizMail({
                    //        EmailId: this.form.Email.value, Subject: this.form.Subject.value, Message: this.form.Message.value
                    //    },
                    //        response => {
                    //            Q.notifyInfo(response.Status);
                    //        }
                    //    );

                    //    this.dialogClose();   
                    //}

                }
            ];
        }
    }
}