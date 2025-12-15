/// <reference path="../../_Ext/Bases/ReportPanelBase.ts" />

namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class SalesReportPanel extends _Ext.ReportPanelBase<SalesReportRequest> {
        protected getReportTitle() { return 'Sales Report'; }
        protected getReportKey() { return 'Reports.SalesReport'; }
        protected getFormKey() { return SalesReportForm.formKey; }

        private form: SalesReportForm = new SalesReportForm(this.idPrefix);

        constructor(container: JQuery) {
            super(container);
            this.form.Contact.getGridField().toggle(false)
            this.form.Representative.getGridField().toggle(false)

            this.form.Type.changeSelect2(e => {
                if (this.form.Type.value == '1') {
                    this.form.Contact.getGridField().toggle(true)
                    this.form.Representative.getGridField().toggle(false)
                }
                else if (this.form.Type.value == '5') {
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