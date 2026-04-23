
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayQualityGrid extends GridBase<DemandayQualityRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayQuality' }
        protected getDialogType() { return DemandayQualityDialog; }
        protected getIdProperty() { return DemandayQualityRow.idProperty; }
        protected getInsertPermission() { return DemandayQualityRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayQualityRow.localTextPrefix; }
        protected getService() { return DemandayQualityService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();
            buttons.shift();
            buttons.push({
                title: "Move to MIS",
                cssClass: "move-to-mis-button",
                onClick: () => {
                    const selectedKeys = this.rowSelection.getSelectedKeys().map(x => Number(x));

                    if (!selectedKeys.length) {
                        Q.notifyWarning("Please select at least one record!");
                        return;
                    }
                    Q.confirm("Are you sure you want to move this record to MIS?", () => {
                        Q.serviceRequest(
                            "Demanday/DemandayQuality/MoveToMIS", // your endpoint
                            { Ids: selectedKeys },
                            (response: { Status: string }) => {
                                Q.notifySuccess(response.Status);
                                this.refresh();
                            }
                        );
                    });
                }
            });
            buttons.push({
                title: 'Export to Excel',
                cssClass: 'export-excel-button',
                icon: 'fa-file-excel',
                onClick: () => {
                    const url = '/Services/Demanday/DemandayQuality/ListExcel';
                    var form = document.createElement('form');
                    form.method = 'POST';
                    form.action = url;
                    form.style.display = 'none';
                    var take = document.createElement('input');
                    take.type = 'hidden';
                    take.name = 'Take';
                    take.value = '0';
                    form.appendChild(take);
                    const selectedKeys = this.rowSelection.getSelectedKeys().map(x => Number(x));
                    if (selectedKeys && selectedKeys.length) {
                        var idsInput = document.createElement('input');
                        idsInput.type = 'hidden';
                        idsInput.name = 'Ids';
                        idsInput.value = selectedKeys.join(',');
                        form.appendChild(idsInput);
                    }
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
                    let fileInput = document.getElementById('quality-excel-import-input') as HTMLInputElement;
                    if (!fileInput) {
                        fileInput = document.createElement('input');
                        fileInput.type = 'file';
                        fileInput.accept = '.xlsx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
                        fileInput.style.display = 'none';
                        fileInput.id = 'quality-excel-import-input';
                        document.body.appendChild(fileInput);
                    }
                    fileInput.onchange = () => {
                        if (fileInput.files && fileInput.files.length > 0) {
                            const formData = new FormData();
                            formData.append('file', fileInput.files[0]);
                            fetch('/Services/Demanday/DemandayQuality/ImportExcel', {
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