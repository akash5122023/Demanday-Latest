
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailTradeIdiaDialog extends DialogBase<BizMailTradeIdiaRow, any> {
        protected getFormKey() { return BizMailTradeIdiaForm.formKey; }
        protected getIdProperty() { return BizMailTradeIdiaRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailTradeIdiaRow.localTextPrefix; }
        protected getNameProperty() { return BizMailTradeIdiaRow.nameProperty; }
        protected getService() { return BizMailTradeIdiaService.baseUrl; }
        protected getDeletePermission() { return BizMailTradeIdiaRow.deletePermission; }
        protected getInsertPermission() { return BizMailTradeIdiaRow.insertPermission; }
        protected getUpdatePermission() { return BizMailTradeIdiaRow.updatePermission; }

        protected form = new BizMailTradeIdiaForm(this.idPrefix);

    }
}