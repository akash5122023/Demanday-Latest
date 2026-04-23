
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingQualiltyGrid extends GridBase<DemandayTeleMarketingQualiltyRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayTeleMarketingQualilty' }
        protected getDialogType() { return DemandayTeleMarketingQualiltyDialog; }
        protected getIdProperty() { return DemandayTeleMarketingQualiltyRow.idProperty; }
        protected getInsertPermission() { return DemandayTeleMarketingQualiltyRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingQualiltyRow.localTextPrefix; }
        protected getService() { return DemandayTeleMarketingQualiltyService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
		protected getButtons(): Serenity.ToolButton[] {
			let buttons = super.getButtons();

			// Example: remove default add button
			buttons.shift();

			// Add Move to Quality button
			buttons.push({
				title: "Move to TM MIS",
				cssClass: "move-to-mis-button",
				onClick: () => {
					const selectedKeys = this.rowSelection.getSelectedKeys().map(x => Number(x));

					if (!selectedKeys.length) {
						Q.notifyWarning("Please select at least one record!");
						return;
					}
					Q.confirm("Are you sure you want to move this record to MIS?", () => {
						Q.serviceRequest(
							"Demanday/DemandayTeleMarketingQualilty/MoveToTeleMarketingMIS", // your endpoint
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
					const url = '/Services/Demanday/DemandayTeleMarketingQualilty/ListExcel';
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
					let fileInput = document.getElementById('tmquality-excel-import-input') as HTMLInputElement;
					if (!fileInput) {
						fileInput = document.createElement('input');
						fileInput.type = 'file';
						fileInput.accept = '.xlsx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
						fileInput.style.display = 'none';
						fileInput.id = 'tmquality-excel-import-input';
						document.body.appendChild(fileInput);
					}
					fileInput.onchange = () => {
						if (fileInput.files && fileInput.files.length > 0) {
							const formData = new FormData();
							formData.append('file', fileInput.files[0]);
							fetch('/Services/Demanday/DemandayTeleMarketingQualilty/ImportExcel', {
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