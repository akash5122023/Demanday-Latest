
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayEnquiryGrid extends GridBase<DemandayEnquiryRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayEnquiry' }
        protected getDialogType() { return DemandayEnquiryDialog; }
        protected getIdProperty() { return DemandayEnquiryRow.idProperty; }
        protected getInsertPermission() { return DemandayEnquiryRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayEnquiryRow.localTextPrefix; }
        protected getService() { return DemandayEnquiryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();

            buttons.push({
                title: "Move To TeamLeader",
                cssClass: "move-to-teamleader-button",
                onClick: () => {
                    const selectedKeys = this.rowSelection.getSelectedKeys().map(x => Number(x));
                    if (!selectedKeys.length) {
                        Q.notifyWarning("Please select at least one record!");
                        return;
                    }
                    Q.confirm("Are you sure you want to move the selected record(s) to Team Leader?", () => {
                        Q.serviceRequest(
                            "Demanday/DemandayEnquiry/MoveToTeamLeader",
                            { Ids: selectedKeys },
                            (response: { Status: string }) => {
                                Q.notifySuccess(response.Status);
                                this.rowSelection.resetCheckedAndRefresh();
                                this.refresh();
                            }
                        );
                    });
                }
            });

            buttons.push({
                title: 'Import from Excel',
                cssClass: 'import-excel-button',
                icon: 'fa-file-import',
                onClick: () => this.showExcelImportDialog()
            });

            return buttons;
        }

        // Opens a small dialog that offers both a template download and the
        // file upload for importing Demanday Enquiry records.
        private showExcelImportDialog() {
            const startFilePick = () => {
                let fileInput = document.getElementById('demandayenquiry-excel-import-input') as HTMLInputElement;
                if (!fileInput) {
                    fileInput = document.createElement('input');
                    fileInput.type = 'file';
                    fileInput.accept = '.xlsx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
                    fileInput.style.display = 'none';
                    fileInput.id = 'demandayenquiry-excel-import-input';
                    document.body.appendChild(fileInput);
                }
                fileInput.onchange = () => {
                    if (fileInput.files && fileInput.files.length > 0) {
                        const formData = new FormData();
                        formData.append('file', fileInput.files[0]);
                        fetch('/Services/Demanday/DemandayEnquiry/ImportExcel', {
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
            };

            const $dlg = $('<div/>')
                .html('<p style="margin:8px 0;">Download the template, fill in your data, then choose the file to import.</p>')
                .appendTo(document.body);
            ($dlg as any).dialog({
                title: 'Import from Excel',
                modal: true,
                width: 440,
                buttons: [
                    {
                        text: 'Download Template',
                        click: () => {
                            Q.postToService({
                                service: 'Demanday/DemandayEnquiry/DownloadTemplate',
                                request: {},
                                target: '_blank'
                            });
                        }
                    },
                    {
                        text: 'Choose File & Import',
                        click: () => {
                            ($dlg as any).dialog('close');
                            startFilePick();
                        }
                    },
                    {
                        text: 'Cancel',
                        click: () => ($dlg as any).dialog('close')
                    }
                ],
                close: () => { $dlg.remove(); }
            });
        }
    }
}