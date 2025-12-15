
namespace AdvanceCRM.Common {
    export class BulkSMSOptions {
        Ids: string[];
        ServiceURL: string;
    }

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class BulkSMSDialog extends Serenity.PropertyDialog<BulkSMSOptions, any> {

        private form: BulkSMSForm;

        constructor(opt?: BulkSMSOptions) {
            super(opt);

            this.form = new BulkSMSForm(this.idPrefix);
        }

        protected getDialogTitle(): string {
            return "Bulk SMS Sender";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Send',
                    click: () => {
                        if (!this.validateBeforeSave()) {
                            return;
                        }

                        var action = new Common.BulkSMSAction(this.options.ServiceURL, this.form.Message.value);
                        action.execute(this.options.Ids);

                        this.dialogClose();
                    }

                }
            ];
        }
    }
}