namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class MasterSupressionExcelImportDialog extends Serenity.PropertyDialog<any, any> {
        protected getFormKey() { return MasterSupressionExcelImportForm.formKey; }
        protected getIdProperty() { return "__id"; }
        protected getLocalTextPrefix() { return MasterSupressionRow.localTextPrefix; }
        protected getService() { return MasterSupressionService.baseUrl; }

        private form: MasterSupressionExcelImportForm;

        constructor() {
            super();

            this.form = new MasterSupressionExcelImportForm(this.idPrefix);
        }

        protected getDialogTitle(): string {
            return "Excel Import";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Template',
                    click: () => {
                        Q.postToService({
                            service: 'Toolkit/MasterSupression/DownloadTemplate',
                            request: { EntityId: null },
                            target: '_blank'
                        });
                    }
                },
                {
                    text: 'Import',
                    click: () => {
                        if (!this.validateBeforeSave())
                            return;

                        if (this.form.MasterAccountId.value == null || Q.isEmptyOrNull(this.form.MasterAccountId.value)) {
                            Q.notifyError("Please select a Master Account!");
                            return;
                        }

                        if (this.form.FileName.value == null ||
                            Q.isEmptyOrNull(this.form.FileName.value.Filename)) {
                            Q.notifyError("Please select a file!");
                            return;
                        }

                        Q.serviceCall<ExcelImportResponse>({
                            url: Q.resolveUrl('~/Services/Toolkit/MasterSupression/ExcelImport'),
                            request: {
                                FileName: this.form.FileName.value.Filename,
                                MasterAccountId: Q.toId(this.form.MasterAccountId.value)
                            },
                            onSuccess: response => {

                                if (response.Inserted < 1) {
                                    Q.notifyError('No records found in selected Excel sheet');
                                }
                                else {
                                    Q.notifyInfo((response.Inserted || 0) + ' records added successfully');
                                }

                                if (response.ErrorList != null && response.ErrorList.length > 0) {
                                    Q.notifyError(response.ErrorList.join(',\r\n '));
                                }

                                this.dialogClose();
                            }
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
