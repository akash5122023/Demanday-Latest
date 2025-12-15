
namespace AdvanceCRM.Common {
    export class BulkMailOptions {
        Ids: string[];
        ServiceURL: string;
        
    }

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class BulkMailDialog extends Serenity.PropertyDialog<BulkMailOptions, any> {

        private form: BulkMailForm;

        constructor(opt?: BulkMailOptions) {
            super(opt);

            this.form = new BulkMailForm(this.idPrefix);
        }

        protected getDialogTitle(): string {
            return "Bulk Mail Sender";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Send',
                    click: () => {
                        if (!this.validateBeforeSave()) {
                            return;
                        }

                        var action = new Common.BulkMailAction(this.form.Message.value);
                        action.execute(this.options.Ids);

                        this.dialogClose();   
                    }

                }
            ];
        }
    }
}