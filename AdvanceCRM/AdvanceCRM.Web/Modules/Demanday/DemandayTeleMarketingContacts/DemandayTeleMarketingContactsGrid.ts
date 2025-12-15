
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingContactsGrid extends GridBase<DemandayTeleMarketingContactsRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayTeleMarketingContacts' }
        protected getDialogType() { return DemandayTeleMarketingContactsDialog; }
        protected getIdProperty() { return DemandayTeleMarketingContactsRow.idProperty; }
        protected getInsertPermission() { return DemandayTeleMarketingContactsRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingContactsRow.localTextPrefix; }
        protected getService() { return DemandayTeleMarketingContactsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();

            // Example: remove default add button
            buttons.shift();
			buttons.push({
				title: 'Export To Excel',
				cssClass: 'export-excel-button',
				icon: 'fa-file-excel',
				onClick: () => {
					const url = '/Services/Demanday/DemandayTeleMarketingContacts/ListExcel';
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
							fetch('/Services/Demanday/DemandayTeleMarketingContacts/ImportExcel', {
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