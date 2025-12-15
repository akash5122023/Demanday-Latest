
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