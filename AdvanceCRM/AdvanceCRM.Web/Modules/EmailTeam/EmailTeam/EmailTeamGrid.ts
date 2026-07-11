
namespace AdvanceCRM.EmailTeam {

    @Serenity.Decorators.registerClass()
    export class EmailTeamGrid extends GridBase<EmailTeamRow, any> {
        protected getColumnsKey() { return "EmailTeam.EmailTeam"; }
        protected getDialogType() { return EmailTeamDialog; }
        protected getIdProperty() { return EmailTeamRow.idProperty; }
        protected getInsertPermission() { return EmailTeamRow.insertPermission; }
        protected getLocalTextPrefix() { return EmailTeamRow.localTextPrefix; }
        protected getService() { return EmailTeamService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters() {
            var filters = super.getQuickFilters();
            var fld = EmailTeamRow.Fields;

            filters.push({
                type: Serenity.EnumEditor,
                options: { enumKey: 'EmailTeam.EmailTeamStatus' },
                field: fld.Status,
                title: 'Status'
            });

            return this.orderQuickFilters(filters);
        }
    }
}
