
namespace AdvanceCRM.Common {
    export class QuickEmailOptions {
        Email: string;
    }

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class QuickEmailDialog extends Serenity.PropertyDialog<QuickEmailOptions, any> {

        private form: QuickEmailForm;

        constructor(opt?: QuickEmailOptions) {
            super(opt);

            this.form = new QuickEmailForm(this.idPrefix);
        }

        protected getDialogTitle(): string {
            return "Email Sender";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Send',
                    click: () => {
                        if (!this.validateBeforeSave()) {
                            return;
                        }

                        CommonService.SendMail({
                            EmailId: this.form.Email.value, Subject: this.form.Subject.value, Message: this.form.Message.value
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                            }
                        );

                        this.dialogClose();   
                    }

                }
            ];
        }
    }
}