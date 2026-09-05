namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class ClientSupressionExcelImportDialog extends Serenity.PropertyDialog<any, any> {
        protected getFormKey() { return ClientSupressionExcelImportForm.formKey; }
        protected getIdProperty() { return "__id"; }
        protected getLocalTextPrefix() { return ClientSupressionRow.localTextPrefix; }
        protected getService() { return ClientSupressionService.baseUrl; }

        constructor() {
            super();
            this.form = new ClientSupressionExcelImportForm(this.idPrefix);
        }

        protected form: ClientSupressionExcelImportForm;

        protected getDialogTitle(): string {
            return "Excel Import - Client Supression";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Import',
                    click: () => {
                        if (!this.validateBeforeSave())
                            return;

                        // Campaign is optional here - a row without one can instead name its own
                        // Master Account Id / Campaign Id column in the sheet.
                        if (this.form.FileName.value == null || Q.isEmptyOrNull(this.form.FileName.value.Filename)) {
                            Q.notifyError("Please select a file!");
                            return;
                        }

                        Q.serviceCall<ExcelImportResponse>({
                            url: Q.resolveUrl('~/Services/Toolkit/ClientSupression/ExcelImport'),
                            request: {
                                FileName: this.form.FileName.value.Filename,
                                CampaignId: Q.toId(this.form.CampaignId.value)
                            },
                            onSuccess: response => {
                                Q.notifyInfo('Inserted: ' + (response.Inserted || 0) + ', Updated: ' + (response.Updated || 0));
                                if (response.ErrorList != null && response.ErrorList.length > 0) {
                                    Q.notifyError(response.ErrorList.join(',\r\n'));
                                }
                                this.dialogClose();
                            }
                        });
                    }
                },
                {
                    text: 'Download Template',
                    click: () => {
                        var url = Q.resolveUrl('~/Services/Toolkit/ClientSupression/DownloadTemplate');
                        Q.postToService({ url: url, request: {}, target: '_blank' });
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
