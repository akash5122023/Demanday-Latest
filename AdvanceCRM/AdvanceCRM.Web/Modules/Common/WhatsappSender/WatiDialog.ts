
namespace AdvanceCRM.Common {
    export class WatiOptions {
        Number: string;
    }

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class WatiDialog extends Serenity.PropertyDialog<WatiOptions, any> {

        private form: WatiForm;

        constructor(opt?: WatiOptions) {
            super(opt);

            this.form = new WatiForm(this.idPrefix);
            this.form.Number.value = opt.Number;
        }

        protected getDialogTitle(): string {
            return "Wati Sender";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {

                    text: 'Send',
                    click: () => {
                        if (!this.validateBeforeSave()) {
                            return;
                        }

                        CommonService.SendWati({
                            Phone: this.form.Number.value,
                            SMSType: this.form.Message.value
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