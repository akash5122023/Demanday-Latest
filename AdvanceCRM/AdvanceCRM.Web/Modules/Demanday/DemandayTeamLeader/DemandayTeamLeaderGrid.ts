
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayTeamLeaderGrid extends GridBase<DemandayTeamLeaderRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayTeamLeader' }
        protected getDialogType() { return DemandayTeamLeaderDialog; }
        protected getIdProperty() { return DemandayTeamLeaderRow.idProperty; }
        protected getInsertPermission() { return DemandayTeamLeaderRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayTeamLeaderRow.localTextPrefix; }
        protected getService() { return DemandayTeamLeaderService.baseUrl; }

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
                            "Demanday/DemandayTeamLeader/MoveToQuality",
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