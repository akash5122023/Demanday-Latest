namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    export class RawTelecallImportDialog extends Serenity.PropertyDialog<any, any> {

        private form: RawTelecallImportForm;

        constructor() {
            super();

            this.form = new RawTelecallImportForm(this.idPrefix);
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
                            service: 'ThirdParty/RawTelecall/DownloadTemplate',
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

                        if (this.form.FileName.value == null ||
                            Q.isEmptyOrNull(this.form.FileName.value.Filename)) {
                            Q.notifyError("Please select a file!");
                            return;
                        }

                        RawTelecallService.ExcelImport({
                            FileName: this.form.FileName.value.Filename,
                            UIds: this.form.UIds.values
                        }, response => {

                            if (response.Inserted < 1) {
                                Q.notifyError('No enquiries found in selected Excel sheet');
                            }
                            else {
                                Q.notifyInfo((response.Inserted || 0) + ' Enquiries added successfully');
                            }

                            if (response.ErrorList != null && response.ErrorList.length > 0) {
                                Q.notifyError(response.ErrorList.join(',\r\n '));
                            }

                            Q.reloadLookup(Contacts.ContactsRow.lookupKey);
                            Q.reloadLookup(Contacts.SubContactsRow.lookupKey);
                            this.dialogClose();
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