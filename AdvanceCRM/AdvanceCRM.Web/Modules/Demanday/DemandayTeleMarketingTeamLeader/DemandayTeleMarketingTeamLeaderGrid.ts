
namespace AdvanceCRM.Demanday {

	@Serenity.Decorators.registerClass()
	export class DemandayTeleMarketingTeamLeaderGrid extends GridBase<DemandayTeleMarketingTeamLeaderRow, any> {
		protected getColumnsKey() { return 'Demanday.DemandayTeleMarketingTeamLeader' }
		protected getDialogType() { return DemandayTeleMarketingTeamLeaderDialog; }
		protected getIdProperty() { return DemandayTeleMarketingTeamLeaderRow.idProperty; }
		protected getInsertPermission() { return DemandayTeleMarketingTeamLeaderRow.insertPermission; }
		protected getLocalTextPrefix() { return DemandayTeleMarketingTeamLeaderRow.localTextPrefix; }
		protected getService() { return DemandayTeleMarketingTeamLeaderService.baseUrl; }

		constructor(container: JQuery) {
			super(container);
		}

		protected getColumns() {
			let columns = super.getColumns();

			// Add audio player formatter for Attachments column
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
			buttons.shift();
			buttons.push({
				title: "Move to Quality",
				cssClass: "move-to-quality-button",
				onClick: () => {
					const selectedKeys = this.rowSelection.getSelectedKeys().map(x => Number(x));

					if (!selectedKeys.length) {
						Q.notifyWarning("Please select at least one record!");
						return;
					}

					Q.confirm("Are you sure you want to move selected record to Quality?", () => {
						Q.serviceRequest(
							"Demanday/DemandayTeleMarketingTeamLeader/MoveToQuality",
							{ Ids: selectedKeys },
							(response: { Status: string }) => {
								Q.notifySuccess(response.Status);
								this.refresh();
							}
						);
					});
				}
			});
			return buttons;
		}
    }
}