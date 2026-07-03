
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayCountryMasterGrid extends Serenity.EntityGrid<DemandayCountryMasterRow, any> {
        protected getColumnsKey() { return DemandayCountryMasterColumns.columnsKey; }
        protected getDialogType() { return DemandayCountryMasterDialog; }
        protected getIdProperty() { return DemandayCountryMasterRow.idProperty; }
        protected getInsertPermission() { return DemandayCountryMasterRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayCountryMasterRow.localTextPrefix; }
        protected getService() { return DemandayCountryMasterService.baseUrl; }

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
                            fetch('/Services/' + this.getService() + '/ImportExcel', { method: 'POST', body: fd })
                                .then(r => r.text().then(msg => {
                                    if (!r.ok) { alert('Import failed:\n' + msg); return; }
                                    alert(msg || 'Import completed.');
                                    (this as any).refresh();
                                }))
                                .catch(err => alert('Import failed: ' + err));
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