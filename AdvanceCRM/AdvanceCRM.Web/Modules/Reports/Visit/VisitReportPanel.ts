/// <reference path="../../_Ext/Bases/ReportPanelBase.ts" />

namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class VisitReportPanel extends _Ext.ReportPanelBase<VisitReportRequest> {
        protected getReportTitle() { return 'Visit Report'; }
        protected getReportKey() { return 'Reports.VisitReport'; }
        protected getFormKey() { return VisitReportForm.formKey; }

        private form: VisitReportForm = new VisitReportForm(this.idPrefix);

        constructor(container: JQuery) {
            super(container);
            this.form.Representative.getGridField().toggle(false)

            this.form.Type.changeSelect2(e => {
                if (this.form.Type.value == '2')
                {                 
                    this.form.Representative.getGridField().toggle(true)
                }
                else
                {
                    this.form.Representative.getGridField().toggle(false)
                }
            });
            
        }
    }
}