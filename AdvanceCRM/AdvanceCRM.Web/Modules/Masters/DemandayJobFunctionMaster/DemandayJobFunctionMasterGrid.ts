
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayJobFunctionMasterGrid extends Serenity.EntityGrid<DemandayJobFunctionMasterRow, any> {
        protected getColumnsKey() { return DemandayJobFunctionMasterColumns.columnsKey; }
        protected getDialogType() { return DemandayJobFunctionMasterDialog; }
        protected getIdProperty() { return DemandayJobFunctionMasterRow.idProperty; }
        protected getInsertPermission() { return DemandayJobFunctionMasterRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayJobFunctionMasterRow.localTextPrefix; }
        protected getService() { return DemandayJobFunctionMasterService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            let buttons = super.getButtons();
            buttons.push({
                title: 'Import from Excel',
                cssClass: 'import-excel-button',
                icon: 'fa-file-import',
                onClick: () => {
                    let fileInput = document.createElement('input');
                    fileInput.type = 'file';
                    fileInput.accept = '.xlsx';
                    fileInput.style.display = 'none';
                    document.body.appendChild(fileInput);
                    fileInput.onchange = () => {
                        if (fileInput.files && fileInput.files.length > 0) {
                            let fd = new FormData();
                            fd.append('file', fileInput.files[0]);
                            // Sent through the progress panel so the upload percentage is visible.
                            AdvanceCRM.Common.TransferProgress.upload({
                                url: '/Services/' + this.getService() + '/ImportExcel',
                                formData: fd,
                                title: 'Importing from Excel',
                                processingText: 'Uploaded. Importing rows on the server…',
                                onSuccess: msg => {
                                    alert(msg || 'Import completed.');
                                    (this as any).refresh();
                                },
                                onError: msg => alert('Import failed:\n' + msg)
                            });
                        }
                        document.body.removeChild(fileInput);
                    };
                    fileInput.click();
                }
            });
            return buttons;
        }
    }
}