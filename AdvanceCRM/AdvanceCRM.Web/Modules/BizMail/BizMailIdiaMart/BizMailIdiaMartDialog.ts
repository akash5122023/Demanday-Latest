
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailIdiaMartDialog extends DialogBase<BizMailIdiaMartRow, any> {
        protected getFormKey() { return BizMailIdiaMartForm.formKey; }
        protected getIdProperty() { return BizMailIdiaMartRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailIdiaMartRow.localTextPrefix; }
        protected getNameProperty() { return BizMailIdiaMartRow.nameProperty; }
        protected getService() { return BizMailIdiaMartService.baseUrl; }
        protected getDeletePermission() { return BizMailIdiaMartRow.deletePermission; }
        protected getInsertPermission() { return BizMailIdiaMartRow.insertPermission; }
        protected getUpdatePermission() { return BizMailIdiaMartRow.updatePermission; }

        protected form = new BizMailIdiaMartForm(this.idPrefix);

    }
}