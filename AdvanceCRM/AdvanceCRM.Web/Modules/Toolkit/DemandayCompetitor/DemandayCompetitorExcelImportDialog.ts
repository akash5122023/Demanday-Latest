namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class DemandayCompetitorExcelImportDialog extends Serenity.PropertyDialog<any, any> {
        protected getFormKey() { return DemandayCompetitorExcelImportForm.formKey; }
        protected getIdProperty() { return "__id"; }
        protected getLocalTextPrefix() { return DemandayCompetitorRow.localTextPrefix; }
        protected getService() { return DemandayCompetitorService.baseUrl; }

        constructor() {
            super();
            this.form = new DemandayCompetitorExcelImportForm(this.idPrefix);
        }

        protected form: DemandayCompetitorExcelImportForm;

        protected getDialogTitle(): string {
            return "Excel Import - Demanday Competitor";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Template',
                    click: () => {
                        Q.postToService({
                            service: 'Toolkit/DemandayCompetitor/DownloadTemplate',
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

                        // Campaign is optional here - a row without one can instead name its own
                        // Master Account Id / Campaign Id column in the sheet.
                        if (this.form.FileName.value == null || Q.isEmptyOrNull(this.form.FileName.value.Filename)) {
                            Q.notifyError("Please select a file!");
                            return;
                        }

                        Q.serviceCall<ExcelImportResponse>({
                            url: Q.resolveUrl('~/Services/Toolkit/DemandayCompetitor/ExcelImport'),
                            request: {
                                FileName: this.form.FileName.value.Filename,
                                CampaignId: Q.toId(this.form.CampaignId.value)
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
