
namespace AdvanceCRM.ThirdParty {

    import fld = AdvanceCRM.ThirdParty.MailInboxDetailsRow.Fields;
    @Serenity.Decorators.registerClass()
    export class MailInboxDetailsGrid extends GridBase<MailInboxDetailsRow, any> {
        protected getColumnsKey() { return 'ThirdParty.MailInboxDetails'; }
        protected getDialogType() { return MailInboxDetailsDialog; }
        protected getIdProperty() { return MailInboxDetailsRow.idProperty; }
        protected getInsertPermission() { return MailInboxDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return MailInboxDetailsRow.localTextPrefix; }
        protected getService() { return MailInboxDetailsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            //MailInboxDetailsService.Sync({},
            //    response => {
            //        this.refresh();
            //    }
            //);
        }

        getButtons() {
            var buttons = super.getButtons();

           /* buttons.shift();*/

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Enquiries',
                onClick: () => {
                    MailInboxDetailsService.Sync({},
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                    this.refresh();
                },
                separator: true
            });

            return buttons;
        }

        //protected getColumns() {
        //    var columns = super.getColumns();
        //    Q.first(columns, x => x.field == fld.CountryFlag).format =
        //        ctx => `<a href="${Q.htmlEncode(ctx.value)};" class="inline-image"><img src="${Q.htmlEncode(ctx.value)}"/></a>`;

        //    return columns;
        //}
    }
}