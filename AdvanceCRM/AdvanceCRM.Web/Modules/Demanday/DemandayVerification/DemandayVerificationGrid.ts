
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayVerificationGrid extends GridBase<DemandayVerificationRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayVerification' }
        protected getDialogType() { return DemandayVerificationDialog; }
        protected getIdProperty() { return DemandayVerificationRow.idProperty; }
        protected getInsertPermission() { return DemandayVerificationRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayVerificationRow.localTextPrefix; }
        protected getService() { return DemandayVerificationService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = DemandayVerificationRow.Fields;

            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: "Masters.DemandayMasterAccount"
                },
                field: fld.MasterAccountId,
                title: 'Master Account'
            });

            // Campaign list follows the Master Account picked above.
            filters.push({
                type: Serenity.LookupEditor,
                options: {
                    lookupKey: "Masters.DemandayCampaignId",
                    cascadeFrom: fld.MasterAccountId,
                    cascadeField: "DemandayMasterAccountId"
                },
                field: fld.CampaignId,
                title: 'Campaign'
            });

            return this.orderQuickFilters(filters);
        }

        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();

            // Example: remove default add button
            //buttons.shift();

            buttons.push({
                title: 'Export All records rows to Excel',
                cssClass: 'export-excel-button',
                icon: 'fa-file-excel',
                onClick: () => {
                    // Downloaded through the progress panel, so a big export shows its live percentage.
                    const fields: any = { Take: '0' };
                    const selectedKeys = this.rowSelection.getSelectedKeys().map(x => Number(x));
                    if (selectedKeys && selectedKeys.length)
                        fields.Ids = selectedKeys.join(',');
                    AdvanceCRM.Common.TransferProgress.download({
                        url: '/Services/Demanday/DemandayVerification/ListExcel',
                        fields: fields,
                        title: 'Exporting to Excel',
                        preparingText: 'Building the Excel file on the server…',
                        fileName: 'DemandayVerification.xlsx'
                    });
                }
            });
            // An empty sheet carrying exactly the headers the import understands - the fields the
            // Verification form itself offers. Built on the server so it can never drift from
            // what ImportExcel actually reads.
            buttons.push({
                title: 'Download Template',
                cssClass: 'download-template-button',
                icon: 'fa-download',
                onClick: () => {
                    AdvanceCRM.Common.TransferProgress.download({
                        url: '/Services/Demanday/DemandayVerification/DownloadTemplate',
                        method: 'POST',
                        title: 'Preparing template',
                        preparingText: 'Building the template on the server…',
                        fileName: 'DemandayVerification_Template.xlsx'
                    });
                }
            });
            buttons.push({
                title: 'Import from Excel',
                cssClass: 'import-excel-button',
                icon: 'fa-file-import',
                onClick: () => {
                    let fileInput = document.getElementById('verification-excel-import-input') as HTMLInputElement;
                    if (!fileInput) {
                        fileInput = document.createElement('input');
                        fileInput.type = 'file';
                        fileInput.accept = '.xlsx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
                        fileInput.style.display = 'none';
                        fileInput.id = 'verification-excel-import-input';
                        document.body.appendChild(fileInput);
                    }
                    fileInput.onchange = () => {
                        if (fileInput.files && fileInput.files.length > 0) {
                            const formData = new FormData();
                            formData.append('file', fileInput.files[0]);
                            // Sent through the progress panel so the upload percentage is visible while a
                            // large sheet goes up, and the user knows to wait for the server to import it.
                            AdvanceCRM.Common.TransferProgress.upload({
                                url: '/Services/Demanday/DemandayVerification/ImportExcel',
                                formData: formData,
                                title: 'Importing from Excel',
                                processingText: 'Uploaded. Importing rows on the server…',
                                onSuccess: msg => {
                                    alert(msg || 'Import completed successfully.');
                                    if (typeof (this as any).refresh === 'function')
                                        (this as any).refresh();
                                    else
                                        window.location.reload();
                                },
                                onError: msg => alert('Excel import failed: ' + msg)
                            });
                        }
                        fileInput.value = '';
                    };
                    fileInput.click();
                }
            });
            return buttons;
        }
    }
}