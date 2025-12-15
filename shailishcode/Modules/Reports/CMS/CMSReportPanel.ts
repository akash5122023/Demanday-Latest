/// <reference path="../../_Ext/Bases/ReportPanelBase.ts" />

namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class CMSReportPanel extends _Ext.ReportPanelBase<CMSReportRequest> {
        protected getReportTitle() { return 'CMS Report'; }
        protected getReportKey() { return 'Reports.CMSReport'; }
        protected getFormKey() { return CMSReportForm.formKey; }

        private form: CMSReportForm = new CMSReportForm(this.idPrefix);

        constructor(container: JQuery) {
            super(container);
            this.form.Contact.getGridField().toggle(false)
            this.form.Representative.getGridField().toggle(false)
            //this.form.Project.getGridField().toggle(false)
            

            this.form.Type.changeSelect2(e => {
                if (this.form.Type.value == '1') {
                    this.form.Contact.getGridField().toggle(true)
                    this.form.Representative.getGridField().toggle(false)
                //    this.form.Project.getGridField().toggle(false)
                }
                else if (this.form.Type.value == '7') {
                    this.form.Representative.getGridField().toggle(true)
                    this.form.Contact.getGridField().toggle(false)
                    
                }
                else if (this.form.Type.value == '10') {
                    //this.form.Project.getGridField().toggle(true);
                    this.form.Contact.getGridField().toggle(false)
                    this.form.Representative.getGridField().toggle(false)
                    
                    
                }
               else {
                    this.form.Contact.getGridField().toggle(false)
                    this.form.Representative.getGridField().toggle(false)
                }
            });
        }
    }
}