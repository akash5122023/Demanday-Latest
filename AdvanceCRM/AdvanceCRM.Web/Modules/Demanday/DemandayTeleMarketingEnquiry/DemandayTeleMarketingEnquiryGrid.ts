
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingEnquiryGrid extends GridBase<DemandayTeleMarketingEnquiryRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayTeleMarketingEnquiry' }
        protected getDialogType() { return DemandayTeleMarketingEnquiryDialog; }
        protected getIdProperty() { return DemandayTeleMarketingEnquiryRow.idProperty; }
        protected getInsertPermission() { return DemandayTeleMarketingEnquiryRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryRow.localTextPrefix; }
        protected getService() { return DemandayTeleMarketingEnquiryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        // Show up to 100000 rows on a single page (effectively "all" for this module).
        protected getViewOptions() {
            let opt = super.getViewOptions();
            opt.rowsPerPage = 100000;
            return opt;
        }

        // Make 100000 an actual, selected option in the pager's page-size dropdown
        // (otherwise the dropdown shows blank since 100000 isn't a default option).
        protected getPagerOptions() {
            let opt = super.getPagerOptions();
            opt.rowsPerPage = 100000;
            opt.rowsPerPageOptions = [2500, 5000, 10000, 50000, 100000];
            return opt;
        }

        protected getColumns() {
            let columns = super.getColumns();

            let attachmentsCol = columns.find(x => x.field === 'Attachments');
            if (attachmentsCol) {
                attachmentsCol.format = (ctx) => {
                    if (!ctx.value)
                        return '';

                    let files = ctx.value.split('|').filter(f => f.trim());
                    if (files.length === 0)
                        return '';

                    let html = '<div class="audio-player-container">';
                    files.forEach(file => {
                        let filename = file.split('/').pop();
                        html += `
                            <div class="audio-item" style="margin: 5px 0;">
                                <audio controls preload="none" style="height: 30px; max-width: 200px;">
                                    <source src="${Q.resolveUrl('~/upload/')}${file}" type="audio/mpeg">
                                    Your browser does not support audio.
                                </audio>
                                <a href="${Q.resolveUrl('~/upload/')}${file}" download="${filename}"
                                   class="btn btn-sm btn-info" style="margin-left: 5px;" title="Download">
                                    <i class="fa fa-download"></i>
                                </a>
                            </div>`;
                    });
                    html += '</div>';
                    return html;
                };
            }

            return columns;
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
                            "Demanday/DemandayTeleMarketingEnquiry/MoveToTeamLeader",
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
        // file upload for importing TM Enquiry records.
        private showExcelImportDialog() {
            const startFilePick = () => {
                let fileInput = document.getElementById('tmenquiry-excel-import-input') as HTMLInputElement;
                if (!fileInput) {
                    fileInput = document.createElement('input');
                    fileInput.type = 'file';
                    fileInput.accept = '.xlsx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
                    fileInput.style.display = 'none';
                    fileInput.id = 'tmenquiry-excel-import-input';
                    document.body.appendChild(fileInput);
                }
                fileInput.onchange = () => {
                    if (fileInput.files && fileInput.files.length > 0) {
                        const formData = new FormData();
                        formData.append('file', fileInput.files[0]);
                        // Sent through the progress panel so the upload percentage is visible while a
                        // large sheet goes up, and the user knows to wait for the server to import it.
                        AdvanceCRM.Common.TransferProgress.upload({
                            url: '/Services/Demanday/DemandayTeleMarketingEnquiry/ImportExcel',
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
                            // Downloaded through the progress panel, so the user sees it arriving.
                            AdvanceCRM.Common.TransferProgress.download({
                                url: '~/Services/Demanday/DemandayTeleMarketingEnquiry/DownloadTemplate',
                                request: {},
                                title: 'Downloading import template',
                                fileName: 'DemandayTeleMarketingEnquiryTemplate.xlsx'
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