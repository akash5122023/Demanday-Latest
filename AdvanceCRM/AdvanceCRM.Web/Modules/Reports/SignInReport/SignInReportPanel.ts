/// <reference path="../../_Ext/Bases/ReportPanelBase.ts" />

namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class SignInReportPanel extends _Ext.ReportPanelBase<LeadsReportRequest> {
        protected getReportTitle() { return 'SignIn Report'; }
        protected getReportKey() { return 'Reports.SignInReport'; }
        protected getFormKey() { return SignInReportForm.formKey; }

        private form: SignInReportForm = new SignInReportForm(this.idPrefix);

        constructor(container: JQuery) {
            super(container);
            this.form.Representative.getGridField().toggle(true)
            
        }
    }
}