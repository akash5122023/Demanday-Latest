
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    export class IntractTemplateGrid extends Serenity.EntityGrid<IntractTemplateRow, any> {
        protected getColumnsKey() { return 'Template.IntractTemplate'; }
        protected getDialogType() { return IntractTemplateDialog; }
        protected getIdProperty() { return IntractTemplateRow.idProperty; }
        protected getInsertPermission() { return IntractTemplateRow.insertPermission; }
        protected getLocalTextPrefix() { return IntractTemplateRow.localTextPrefix; }
        protected getService() { return IntractTemplateService.baseUrl; }

        //constructor(container: JQuery) {
        //    super(container);
        //}

        constructor(container: JQuery) {
            super(container);

            IntractTemplateService.Sync({},
                response => { }
            );
        }
        getButtons() {
            var buttons = super.getButtons();

            buttons.shift();

            buttons.push({

                title: 'Sync',
                icon: 'fa-refresh text-blue"',
                hint: 'Click to sync new Template',
                onClick: () => {
                    IntractTemplateService.Sync({},
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