
namespace AdvanceCRM.Common {
    export class QuickSMSOptions {
        Number: string;
    }

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class QuickSMSDialog extends Serenity.PropertyDialog<QuickSMSOptions, any> {

        private form: QuickSMSForm;

        constructor(opt?: QuickSMSOptions) {
            super(opt);

            this.form = new QuickSMSForm(this.idPrefix);
        }

        protected getDialogTitle(): string {
            return "SMS Sender";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Send',
                    click: () => {
                        if (!this.validateBeforeSave()) {
                            return;
                        }

                        CommonService.SendSMS({
                            Phone: this.form.Number.value, SMSType: this.form.Message.value
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