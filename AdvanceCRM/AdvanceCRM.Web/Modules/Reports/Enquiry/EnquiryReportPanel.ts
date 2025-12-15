/// <reference path="../../_Ext/Bases/ReportPanelBase.ts" />

namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class EnquiryReportPanel extends _Ext.ReportPanelBase<LeadsReportRequest> {
        protected getReportTitle() { return 'Enquiry Report'; }
        protected getReportKey() { return 'Reports.EnquiryReport'; }
        protected getFormKey() { return LeadsReportForm.formKey; }

        private form: LeadsReportForm = new LeadsReportForm(this.idPrefix);

        constructor(container: JQuery) {
            super(container);
            this.form.Contact.getGridField().toggle(false)
            this.form.Representative.getGridField().toggle(false)

            this.form.Type.changeSelect2(e => {
                if (this.form.Type.value == '1') {
                    this.form.Contact.getGridField().toggle(true)
                    this.form.Representative.getGridField().toggle(false)
                }
                else if (this.form.Type.value == '7') {
                    this.form.Contact.getGridField().toggle(false)
                    this.form.Representative.getGridField().toggle(true)
                }
               else {
                    this.form.Contact.getGridField().toggle(false)
                    this.form.Representative.getGridField().toggle(false)
                }
            });
        }
    }
}