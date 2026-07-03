
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayJobLevelMasterGrid extends Serenity.EntityGrid<DemandayJobLevelMasterRow, any> {
        protected getColumnsKey() { return DemandayJobLevelMasterColumns.columnsKey; }
        protected getDialogType() { return DemandayJobLevelMasterDialog; }
        protected getIdProperty() { return DemandayJobLevelMasterRow.idProperty; }
        protected getInsertPermission() { return DemandayJobLevelMasterRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayJobLevelMasterRow.localTextPrefix; }
        protected getService() { return DemandayJobLevelMasterService.baseUrl; }

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