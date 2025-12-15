
namespace AdvanceCRM.Feedback {

    @Serenity.Decorators.registerClass()
    export class FeedbackDetailsGrid extends GridBase<FeedbackDetailsRow, any> {
        protected getColumnsKey() { return 'Feedback.FeedbackDetails'; }
        protected getDialogType() { return FeedbackDetailsDialog; }
        protected getIdProperty() { return FeedbackDetailsRow.idProperty; }
        protected getInsertPermission() { return FeedbackDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return FeedbackDetailsRow.localTextPrefix; }
        protected getService() { return FeedbackDetailsService.baseUrl; }

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