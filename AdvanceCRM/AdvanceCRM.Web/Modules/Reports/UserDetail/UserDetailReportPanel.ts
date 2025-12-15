/// <reference path="../../_Ext/Bases/ReportPanelBase.ts" />

namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class UserDetailReportPanel extends _Ext.ReportPanelBase<LeadsReportRequest> {
        protected getReportTitle() { return 'UserDetail Report'; }
        protected getReportKey() { return 'Reports.UserDetailReport'; }
        protected getFormKey() { return LeadsReportForm.formKey; }

        private form: LeadsReportForm = new LeadsReportForm(this.idPrefix);

        constructor(container: JQuery) {
            super(container);
           
        }
    }
}