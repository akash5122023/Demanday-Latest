
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingMISGrid extends GridBase<DemandayTeleMarketingMISRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayTeleMarketingMIS' }
        protected getDialogType() { return DemandayTeleMarketingMISDialog; }
        protected getIdProperty() { return DemandayTeleMarketingMISRow.idProperty; }
        protected getInsertPermission() { return DemandayTeleMarketingMISRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingMISRow.localTextPrefix; }
        protected getService() { return DemandayTeleMarketingMISService.baseUrl; }

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

        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();

            // Example: remove default add button
            buttons.shift();
			buttons.push({
				title: "Move to TeleMarketing Contacts",
				cssClass: "move-to-telemarketing-contacts-button",
				onClick: () => {
					const selectedKeys = this.rowSelection.getSelectedKeys().map(x => Number(x));

					if (!selectedKeys.length) {
						Q.notifyWarning("Please select at least one record!");
						return;
					}
					Q.confirm("Are you sure you want to move selected record to TeleMarketing Contacts?", () => {
						Q.serviceRequest(
							"Demanday/DemandayTeleMarketingMIS/MoveToTeleMarketingContacts",
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
				title: 'Export To Excel',
				cssClass: 'export-excel-button',
				icon: 'fa-file-excel',
				onClick: () => {
					// Downloaded through the progress panel, so a big export shows its live percentage.
					const fields: any = { Take: '0' };
					const selectedKeys = this.rowSelection.getSelectedKeys().map(x => Number(x));
					if (selectedKeys && selectedKeys.length)
						fields.Ids = selectedKeys.join(',');
					AdvanceCRM.Common.TransferProgress.download({
						url: '/Services/Demanday/DemandayTeleMarketingMIS/ListExcel',
						fields: fields,
						title: 'Exporting to Excel',
						preparingText: 'Building the Excel file on the server…',
						fileName: 'DemandayTeleMarketingMIS.xlsx'
					});
				}
			});
			buttons.push({
				title: 'Import from Excel',
				cssClass: 'import-excel-button',
				icon: 'fa-file-import',
				onClick: () => {
					let fileInput = document.getElementById('mis-excel-import-input') as HTMLInputElement;
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
							// Sent through the progress panel so the upload percentage is visible while a
							// large sheet goes up, and the user knows to wait for the server to import it.
							AdvanceCRM.Common.TransferProgress.upload({
								url: '/Services/Demanday/DemandayTeleMarketingMIS/ImportExcel',
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