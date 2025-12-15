/// <reference path="../../_Ext/Bases/ReportPanelBase.ts" />

namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class AttendanceReportPanel extends _Ext.ReportPanelBase<AttendanceReportRequest> {
        protected getReportTitle() { return 'Attendance Report'; }
        protected getReportKey() { return 'Reports.AttendanceReport'; }
        protected getFormKey() { return AttendanceReportForm.formKey; }

        private form: AttendanceReportForm = new AttendanceReportForm(this.idPrefix);

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