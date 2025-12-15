
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayQualityGrid extends GridBase<DemandayQualityRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayQuality' }
        protected getDialogType() { return DemandayQualityDialog; }
        protected getIdProperty() { return DemandayQualityRow.idProperty; }
        protected getInsertPermission() { return DemandayQualityRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayQualityRow.localTextPrefix; }
        protected getService() { return DemandayQualityService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();
            buttons.shift();
            buttons.push({
                title: "Move to MIS",
                cssClass: "move-to-mis-button",
                onClick: () => {
                    const selectedKeys = this.rowSelection.getSelectedKeys().map(x => Number(x));

                    if (!selectedKeys.length) {
                        Q.notifyWarning("Please select at least one record!");
                        return;
                    }
                    Q.confirm("Are you sure you want to move this record to MIS?", () => {
                        Q.serviceRequest(
                            "Demanday/DemandayQuality/MoveToMIS", // your endpoint
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
                title: 'Export to Excel',
                cssClass: 'export-excel-button',
                icon: 'fa-file-excel',
                onClick: () => {
                    const url = '/Services/Demanday/DemandayQuality/ListExcel';
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
            return buttons;
        }
    }
}