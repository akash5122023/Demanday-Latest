
namespace AdvanceCRM.BizMail {

    @Serenity.Decorators.registerClass()
    export class BizMailEnquiryDialog extends Serenity.EntityDialog<BizMailEnquiryRow, any> {
        protected getFormKey() { return BizMailEnquiryForm.formKey; }
        protected getIdProperty() { return BizMailEnquiryRow.idProperty; }
        protected getLocalTextPrefix() { return BizMailEnquiryRow.localTextPrefix; }
        protected getNameProperty() { return BizMailEnquiryRow.nameProperty; }
        protected getService() { return BizMailEnquiryService.baseUrl; }
        protected getDeletePermission() { return BizMailEnquiryRow.deletePermission; }
        protected getInsertPermission() { return BizMailEnquiryRow.insertPermission; }
        protected getUpdatePermission() { return BizMailEnquiryRow.updatePermission; }

        protected form = new BizMailEnquiryForm(this.idPrefix);

        constructor() {
            super();

            this.form.ClosingType.getGridField().toggle(false);
            this.form.EnquiryStatus.getGridField().toggle(false);
            this.form.Type.getGridField().toggle(false);
            this.form.SourceId.getGridField().toggle(false);
            this.form.StageId.getGridField().toggle(false);

            this.form.Rule.changeSelect2(e => {//Reference
                if (this.form.Rule.value == '1') {
                    this.form.ClosingType.getGridField().toggle(false);
                    this.form.EnquiryStatus.getGridField().toggle(true);
                    this.form.Type.getGridField().toggle(false);
                    this.form.SourceId.getGridField().toggle(false);
                    this.form.StageId.getGridField().toggle(false);
                }
                else if (this.form.Rule.value == '2') {
                    this.form.ClosingType.getGridField().toggle(false);
                    this.form.EnquiryStatus.getGridField().toggle(false);
                    this.form.Type.getGridField().toggle(false);
                    this.form.SourceId.getGridField().toggle(false);
                    this.form.StageId.getGridField().toggle(true);
                }
                else if (this.form.Rule.value == '3') {
                    this.form.ClosingType.getGridField().toggle(false);
                    this.form.EnquiryStatus.getGridField().toggle(false);
                    this.form.Type.getGridField().toggle(false);
                    this.form.SourceId.getGridField().toggle(true);
                    this.form.StageId.getGridField().toggle(false);
                }
                else if (this.form.Rule.value == '4') {
                    this.form.ClosingType.getGridField().toggle(false);
                    this.form.EnquiryStatus.getGridField().toggle(false);
                    this.form.Type.getGridField().toggle(true);
                    this.form.SourceId.getGridField().toggle(false);
                    this.form.StageId.getGridField().toggle(false);
                }
                else if (this.form.Rule.value == '5') {
                    this.form.ClosingType.getGridField().toggle(true);
                    this.form.EnquiryStatus.getGridField().toggle(false);
                    this.form.Type.getGridField().toggle(false);
                    this.form.SourceId.getGridField().toggle(false);
                    this.form.StageId.getGridField().toggle(false);
                }
                else {
                    this.form.ClosingType.getGridField().toggle(false);
                    this.form.EnquiryStatus.getGridField().toggle(false);
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