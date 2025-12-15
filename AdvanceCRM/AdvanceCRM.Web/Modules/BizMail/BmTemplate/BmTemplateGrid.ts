
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BmTemplateGrid extends GridBase<BmTemplateRow, any> {
        protected getColumnsKey() { return 'BizMail.BmTemplate'; }
        protected getDialogType() { return BmTemplateDialog; }
        protected getIdProperty() { return BmTemplateRow.idProperty; }
        protected getInsertPermission() { return BmTemplateRow.insertPermission; }
        protected getLocalTextPrefix() { return BmTemplateRow.localTextPrefix; }
        protected getService() { return BmTemplateService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        getButtons() {
            var buttons = super.getButtons();

           // buttons.shift();

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Enquiries',
                onClick: () => {
                    BmTemplateService.Sync({},
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
    }
}