
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailQuotationDialog extends DialogBase<BizMailQuotationRow, any> {
        protected getFormKey() { return BizMailQuotationForm.formKey; }
        protected getIdProperty() { return BizMailQuotationRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailQuotationRow.localTextPrefix; }
        protected getNameProperty() { return BizMailQuotationRow.nameProperty; }
        protected getService() { return BizMailQuotationService.baseUrl; }
        protected getDeletePermission() { return BizMailQuotationRow.deletePermission; }
        protected getInsertPermission() { return BizMailQuotationRow.insertPermission; }
        protected getUpdatePermission() { return BizMailQuotationRow.updatePermission; }

        protected form = new BizMailQuotationForm(this.idPrefix);
        constructor() {
            super();

            this.form.ClosingType.getGridField().toggle(false);
            this.form.QuotationStatus.getGridField().toggle(false);
            this.form.Type.getGridField().toggle(false);
            this.form.SourceId.getGridField().toggle(false);
            this.form.StageId.getGridField().toggle(false);

            this.form.Rule.changeSelect2(e => {//Reference
                if (this.form.Rule.value == '1') {
                    this.form.ClosingType.getGridField().toggle(false);
                    this.form.QuotationStatus.getGridField().toggle(true);
                    this.form.Type.getGridField().toggle(false);
                    this.form.SourceId.getGridField().toggle(false);
                    this.form.StageId.getGridField().toggle(false);
                }
                else if (this.form.Rule.value == '2') {
                    this.form.ClosingType.getGridField().toggle(false);
                    this.form.QuotationStatus.getGridField().toggle(false);
                    this.form.Type.getGridField().toggle(false);
                    this.form.SourceId.getGridField().toggle(false);
                    this.form.StageId.getGridField().toggle(true);
                }
                else if (this.form.Rule.value == '3') {
                    this.form.ClosingType.getGridField().toggle(false);
                    this.form.QuotationStatus.getGridField().toggle(false);
                    this.form.Type.getGridField().toggle(false);
                    this.form.SourceId.getGridField().toggle(true);
                    this.form.StageId.getGridField().toggle(false);
                }
                else if (this.form.Rule.value == '4') {
                    this.form.ClosingType.getGridField().toggle(false);
                    this.form.QuotationStatus.getGridField().toggle(false);
                    this.form.Type.getGridField().toggle(true);
                    this.form.SourceId.getGridField().toggle(false);
                    this.form.StageId.getGridField().toggle(false);
                }
                else if (this.form.Rule.value == '5') {
                    this.form.ClosingType.getGridField().toggle(true);
                    this.form.QuotationStatus.getGridField().toggle(false);
                    this.form.Type.getGridField().toggle(false);
                    this.form.SourceId.getGridField().toggle(false);
                    this.form.StageId.getGridField().toggle(false);
                }
                else {
                    this.form.ClosingType.getGridField().toggle(false);
                    this.form.QuotationStatus.getGridField().toggle(false);
                    this.form.Type.getGridField().toggle(false);
                    this.form.SourceId.getGridField().toggle(false);
                    this.form.StageId.getGridField().toggle(false);
                }
            });

        }
        onDialogOpen() {
            super.onDialogOpen();
            this.form.CompanyId.value = Q.toId(Authorization.userDefinition.CompanyId);

        }

    }
}