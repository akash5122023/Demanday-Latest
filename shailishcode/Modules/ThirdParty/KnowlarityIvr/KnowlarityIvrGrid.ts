
namespace AdvanceCRM.ThirdParty {

    import fld = AdvanceCRM.ThirdParty.KnowlarityIvrRow.Fields;
    @Serenity.Decorators.registerClass()
    export class KnowlarityIvrGrid extends GridBase<KnowlarityIvrRow, any> {
        protected getColumnsKey() { return 'ThirdParty.KnowlarityIvr'; }
        protected getDialogType() { return KnowlarityIvrDialog; }
        protected getIdProperty() { return KnowlarityIvrRow.idProperty; }
        protected getInsertPermission() { return KnowlarityIvrRow.insertPermission; }
        protected getLocalTextPrefix() { return KnowlarityIvrRow.localTextPrefix; }
        protected getService() { return KnowlarityIvrService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            KnowlarityIvrService.Sync({},
                response => { }
            );
        }
        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Enquiries',
                onClick: () => {
                    KnowlarityIvrService.Sync({},
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
    }
}