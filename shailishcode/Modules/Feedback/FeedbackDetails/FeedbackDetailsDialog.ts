
namespace AdvanceCRM.Feedback {

    @Serenity.Decorators.registerClass()
    export class FeedbackDetailsDialog extends DialogBase<FeedbackDetailsRow, any> {
        protected getFormKey() { return FeedbackDetailsForm.formKey; }
        protected getIdProperty() { return FeedbackDetailsRow.idProperty; }
        protected getLocalTextPrefix() { return FeedbackDetailsRow.localTextPrefix; }
        protected getNameProperty() { return FeedbackDetailsRow.nameProperty; }
        protected getService() { return FeedbackDetailsService.baseUrl; }
        protected getDeletePermission() { return FeedbackDetailsRow.deletePermission; }
        protected getInsertPermission() { return FeedbackDetailsRow.insertPermission; }
        protected getUpdatePermission() { return FeedbackDetailsRow.updatePermission; }

        protected form = new FeedbackDetailsForm(this.idPrefix);

    }
}