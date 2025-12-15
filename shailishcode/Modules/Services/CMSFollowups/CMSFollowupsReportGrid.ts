
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class CMSFollowupsReportGrid extends GridBase<CMSFollowupsRow, any> {
        protected getColumnsKey() { return 'Services.CMSFollowups'; }
        protected getDialogType() { return CMSFollowupsDialog; }
        protected getIdProperty() { return CMSFollowupsRow.idProperty; }
        protected getInsertPermission() { return CMSFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return CMSFollowupsRow.localTextPrefix; }
        protected getService() { return CMSFollowupsService.baseUrl; }

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