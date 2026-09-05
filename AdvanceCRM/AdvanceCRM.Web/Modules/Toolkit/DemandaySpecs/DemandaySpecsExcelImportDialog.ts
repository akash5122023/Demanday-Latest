namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    export class DemandaySpecsExcelImportDialog extends Serenity.PropertyDialog<any, any> {
        protected getFormKey() { return DemandaySpecsExcelImportForm.formKey; }
        protected getIdProperty() { return "__id"; }
        protected getLocalTextPrefix() { return DemandaySpecsRow.localTextPrefix; }
        protected getService() { return DemandaySpecsService.baseUrl; }

        constructor() {
            super();
            this.form = new DemandaySpecsExcelImportForm(this.idPrefix);
        }

        protected form: DemandaySpecsExcelImportForm;

        protected getDialogTitle(): string {
            return "Excel Import - Demanday Specs";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Template',
                    click: () => {
                        Q.postToService({
                            service: 'Toolkit/DemandaySpecs/DownloadTemplate',
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
                            url: Q.resolveUrl('~/Services/Toolkit/DemandaySpecs/ExcelImport'),
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
