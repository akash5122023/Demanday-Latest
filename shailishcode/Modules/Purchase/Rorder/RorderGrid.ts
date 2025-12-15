
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    export class RorderGrid extends Serenity.EntityGrid<RorderRow, any> {
        protected getColumnsKey() { return 'Purchase.Rorder'; }
        protected getDialogType() { return RorderDialog; }
        protected getIdProperty() { return RorderRow.idProperty; }
        protected getInsertPermission() { return RorderRow.insertPermission; }
        protected getLocalTextPrefix() { return RorderRow.localTextPrefix; }
        protected getService() { return RorderService.baseUrl; }

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