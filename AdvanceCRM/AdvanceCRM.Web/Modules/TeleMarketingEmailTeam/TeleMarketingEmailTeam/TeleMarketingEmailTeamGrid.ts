
namespace AdvanceCRM.TeleMarketingEmailTeam {

    @Serenity.Decorators.registerClass()
    export class TeleMarketingEmailTeamGrid extends GridBase<TeleMarketingEmailTeamRow, any> {
        protected getColumnsKey() { return "TeleMarketingEmailTeam.TeleMarketingEmailTeam"; }
        protected getDialogType() { return TeleMarketingEmailTeamDialog; }
        protected getIdProperty() { return TeleMarketingEmailTeamRow.idProperty; }
        protected getInsertPermission() { return TeleMarketingEmailTeamRow.insertPermission; }
        protected getLocalTextPrefix() { return TeleMarketingEmailTeamRow.localTextPrefix; }
        protected getService() { return TeleMarketingEmailTeamService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = TeleMarketingEmailTeamRow.Fields;

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

            filters.push({
                type: Serenity.EnumEditor,
                options: { enumKey: 'TeleMarketingEmailTeam.TeleMarketingEmailTeamStatus' },
                field: fld.Status,
                title: 'Status'
            });

            return this.orderQuickFilters(filters);
        }

        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();

            buttons.push({
                title: 'Export to Excel',
                cssClass: 'export-excel-button',
                icon: 'fa-file-excel',
                onClick: () => {
                    // Downloaded through the progress panel, so a big export shows its live percentage.
                    const fields: any = { Take: '0' };
                    const selectedKeys = this.rowSelection.getSelectedKeys().map(x => Number(x));
                    if (selectedKeys && selectedKeys.length)
                        fields.Ids = selectedKeys.join(',');
                    AdvanceCRM.Common.TransferProgress.download({
                        url: '/Services/TeleMarketingEmailTeam/TeleMarketingEmailTeam/ListExcel',
                        fields: fields,
                        title: 'Exporting to Excel',
                        preparingText: 'Building the Excel file on the server…',
                        fileName: 'TMEmailTeam.xlsx'
                    });
                }
            });

            // An empty sheet with exactly the headers the import understands.
            buttons.push({
                title: 'Download Template',
                cssClass: 'download-template-button',
                icon: 'fa-download',
                onClick: () => {
                    AdvanceCRM.Common.TransferProgress.download({
                        url: '/Services/TeleMarketingEmailTeam/TeleMarketingEmailTeam/DownloadTemplate',
                        method: 'POST',
                        title: 'Preparing template',
                        preparingText: 'Building the template on the server…',
                        fileName: 'TMEmailTeam_Template.xlsx'
                    });
                }
            });

            buttons.push({
                title: 'Import from Excel',
                cssClass: 'import-excel-button',
                icon: 'fa-file-import',
                onClick: () => {
                    let fileInput = document.getElementById('tmemailteam-excel-import-input') as HTMLInputElement;
                    if (!fileInput) {
                        fileInput = document.createElement('input');
                        fileInput.type = 'file';
                        fileInput.accept = '.xlsx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
                        fileInput.style.display = 'none';
                        fileInput.id = 'tmemailteam-excel-import-input';
                        document.body.appendChild(fileInput);
                    }
                    fileInput.onchange = () => {
                        if (fileInput.files && fileInput.files.length > 0) {
                            const formData = new FormData();
                            formData.append('file', fileInput.files[0]);
                            AdvanceCRM.Common.TransferProgress.upload({
                                url: '/Services/TeleMarketingEmailTeam/TeleMarketingEmailTeam/ImportExcel',
                                formData: formData,
                                title: 'Importing from Excel',
                                processingText: 'Uploaded. Importing rows on the server…',
                                onSuccess: msg => {
                                    alert(msg || 'Import completed successfully.');
                                    this.refresh();
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
