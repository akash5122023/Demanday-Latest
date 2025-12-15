/// <reference path="../../_Ext/Bases/ReportPanelBase.ts" />

namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class AccountingReportPanel extends _Ext.ReportPanelBase<AccountingReportRequest> {
        protected getReportTitle() { return 'Accounting Report'; }
        protected getReportKey() { return 'Reports.AccountingReport'; }
        protected getFormKey() { return AccountingReportForm.formKey; }

        private form: AccountingReportForm = new AccountingReportForm(this.idPrefix);

        constructor(container: JQuery) {
            super(container);
            this.form.Contact.getGridField().toggle(false)
            this.form.Contact.getGridField().toggle(false)
            this.form.Employee.getGridField().toggle(false);
            this.form.Project.getGridField().toggle(false);
            this.form.Bank.getGridField().toggle(false);
            this.form.CashIn.getGridField().toggle(false)
            this.form.CashOut.getGridField().toggle(false)
            this.form.Type.changeSelect2(e => {
                if (this.form.Type.value == '1') {
                    this.form.Contact.getGridField().toggle(false)
                    this.form.Head.getGridField().toggle(false)
                    this.form.DateFrom.getGridField().toggle(true)
                    this.form.DateTo.getGridField().toggle(true)
                    this.form.Bank.getGridField().toggle(true)
                    this.form.CashIn.getGridField().toggle(true)
                    this.form.CashOut.getGridField().toggle(true)
                }
                else if (this.form.Type.value == '2' || this.form.Type.value == '4') {
                    this.form.Contact.getGridField().toggle(false)
                    this.form.Head.getGridField().toggle(false)
                    this.form.DateFrom.getGridField().toggle(false)
                    this.form.DateTo.getGridField().toggle(false)
                }
                else if (this.form.Type.value == '3' || this.form.Type.value == '5') {
                    this.form.Contact.getGridField().toggle(true)
                    this.form.Head.getGridField().toggle(false)
                    this.form.DateFrom.getGridField().toggle(true)
                    this.form.DateTo.getGridField().toggle(true)
                }
                else if (this.form.Type.value == '6') {
                    this.form.Contact.getGridField().toggle(false)
                    this.form.Head.getGridField().toggle(true)
                    this.form.DateFrom.getGridField().toggle(true)
                    this.form.DateTo.getGridField().toggle(true)
                }
                else if (this.form.Type.value == '7') {
                    this.form.Contact.getGridField().toggle(true)
                    this.form.Head.getGridField().toggle(false)
                    this.form.DateFrom.getGridField().toggle(true)
                    this.form.DateTo.getGridField().toggle(true)
                }
                else {
                    this.form.Contact.getGridField().toggle(true)
                    this.form.Head.getGridField().toggle(false)
                    this.form.DateFrom.getGridField().toggle(true)
                    this.form.DateTo.getGridField().toggle(true)
                }
            });
            this.form.Head.change(e => {

                if ((this.form.Head.value == "38") || (this.form.Head.value == "47")) {
                    this.form.Employee.getGridField().toggle(true);
                    this.form.Bank.getGridField().toggle(true);

                }
                else if (this.form.Head.value == "48") {

                    this.form.Project.getGridField().toggle(true);
                    this.form.Employee.getGridField().toggle(true);

                }
                else if (this.form.Head.value == "49") {

                    this.form.Project.getGridField().toggle(true);
                    this.form.Employee.getGridField().toggle(false);

                }
                else {
                    this.form.Project.getGridField().toggle(false);
                    this.form.Employee.getGridField().toggle(false);

                }

            });
        }
    }
}