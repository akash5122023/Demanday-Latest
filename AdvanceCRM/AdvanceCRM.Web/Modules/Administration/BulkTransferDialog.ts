namespace AdvanceCRM.Common {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class BulkTransferDialog extends Serenity.PropertyDialog<any, any> {

        private form: BulkTransferForm;

        public Ids: string[];
        public Type: string;

        constructor() {
            super();

            this.form = new BulkTransferForm(this.idPrefix);
        }

        protected getDialogTitle(): string {
            return "Bulk Transfer";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Execute',
                    click: () => {
                        AdvanceCRM.Administration.CompanyDetailsService.BulkTransfer(
                            {
                                Type: Q.text(this.form.Module.value),
                                FromID: Q.toId(this.form.From.value),
                                ToID: Q.toId(this.form.To.value)
                            },
                            response => {
                                Q.notifyInfo(response.Status);
                            }
                        );

                        this.dialogClose();
                    }

                },
                {
                    text: 'Cancel',
                    click: () => this.dialogClose()
                }
            ];
        }
    }
}