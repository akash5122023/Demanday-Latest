
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayContactsGrid extends GridBase<DemandayContactsRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayContacts' }
        protected getDialogType() { return DemandayContactsDialog; }
        protected getIdProperty() { return DemandayContactsRow.idProperty; }
        protected getInsertPermission() { return DemandayContactsRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayContactsRow.localTextPrefix; }
        protected getService() { return DemandayContactsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
		protected getButtons(): Serenity.ToolButton[] {
			let buttons = super.getButtons();

			// Example: remove default add button
			buttons.shift();
			buttons.push({
				title: 'Export All Contacts rows to Excel',
				cssClass: 'export-excel-button',
				icon: 'fa-file-excel',
				onClick: () => {
					const url = '/Services/Demanday/DemandayContacts/ListExcel';
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
				title: 'Download Import Template',
				cssClass: 'download-template-button',
				icon: 'fa-download',
				onClick: () => {
					const url = '/Services/Demanday/DemandayContacts/DownloadTemplate';
					var form = document.createElement('form');
					form.method = 'POST';
					form.action = url;
					form.style.display = 'none';
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
					let fileInput = document.getElementById('fls-excel-import-input') as HTMLInputElement;
					if (!fileInput) {
						fileInput = document.createElement('input');
						fileInput.type = 'file';
						fileInput.accept = '.xlsx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
						fileInput.style.display = 'none';
						fileInput.id = 'fls-excel-import-input';
						document.body.appendChild(fileInput);
					}
					fileInput.onchange = () => {
						if (fileInput.files && fileInput.files.length > 0) {
							const formData = new FormData();
							formData.append('file', fileInput.files[0]);
							fetch('/Services/Demanday/DemandayContacts/ImportExcel', {
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