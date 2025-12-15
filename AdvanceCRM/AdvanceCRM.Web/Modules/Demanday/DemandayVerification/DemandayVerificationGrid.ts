
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
        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();

            // Example: remove default add button
            //buttons.shift();

            buttons.push({
                title: 'Export All records rows to Excel',
                cssClass: 'export-excel-button',
                icon: 'fa-file-excel',
                onClick: () => {
                    const url = '/Services/Demanday/DemandayVerification/ListExcel';
                    var form = document.createElement('form');
                    form.method = 'POST';
                    form.action = url;
                    form.style.display = 'none';
                    var take = document.createElement('input');
                    take.type = 'hidden';
                    take.name = 'Take';
                    take.value = '0';
                    form.appendChild(take);
                    document.body.appendChild(form);
                    form.submit();
                    document.body.removeChild(form);
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
                            fetch('/Services/Demanday/DemandayVerification/ImportExcel', {
                                method: 'POST',
                                body: formData
                            }).then(r => r.text().then(msg => {
                                if (r.ok) return msg;
                                alert('Backend error:\n' + msg);
                                throw new Error(msg || 'Failed to import.');
                            })).then(msg => {
                                alert(msg || 'Import completed successfully.');
                                if (typeof (this as any).refresh === 'function')
                                    (this as any).refresh();
                                else
                                    window.location.reload();
                            }).catch(err => {
                                alert('Excel import failed: ' + err.message);
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