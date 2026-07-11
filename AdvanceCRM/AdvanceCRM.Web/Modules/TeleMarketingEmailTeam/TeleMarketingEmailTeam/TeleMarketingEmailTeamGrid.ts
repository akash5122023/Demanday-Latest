
namespace AdvanceCRM.TeleMarketingEmailTeam {

    @Serenity.Decorators.registerClass()
    export class TeleMarketingEmailTeamGrid extends GridBase<TeleMarketingEmailTeamRow, any> {
        protected getColumnsKey() { return "TeleMarketingEmailTeam.TeleMarketingEmailTeam"; }
        protected getDialogType() { return TeleMarketingEmailTeamDialog; }
        protected getIdProperty() { return TeleMarketingEmailTeamRow.idProperty; }
        protected getInsertPermission() { return TeleMarketingEmailTeamRow.insertPermission; }
        protected getLocalTextPrefix() { return TeleMarketingEmailTeamRow.localTextPrefix; }
        protected getService() { return TeleMarketingEmailTeamService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = TeleMarketingEmailTeamRow.Fields;

            filters.push({
                type: Serenity.EnumEditor,
                options: { enumKey: 'TeleMarketingEmailTeam.TeleMarketingEmailTeamStatus' },
                field: fld.Status,
                title: 'Status'
            });

            return this.orderQuickFilters(filters);
        }
    }
}
