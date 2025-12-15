
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class TeleCallingFollowupsReportGrid extends GridBase<TeleCallingFollowupsRow, any> {
        protected getColumnsKey() { return 'Services.TeleCallingFollowups'; }
        protected getDialogType() { return TeleCallingFollowupsReportDialog; }
        protected getIdProperty() { return TeleCallingFollowupsRow.idProperty; }
        protected getInsertPermission() { return TeleCallingFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return TeleCallingFollowupsRow.localTextPrefix; }
        protected getService() { return TeleCallingFollowupsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            return buttons;
        }
    }
}