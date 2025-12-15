/// <reference path="../../_Ext/Bases/ReportPanelBase.ts" />

namespace AdvanceCRM.Reports {

    @Serenity.Decorators.registerClass()
    export class InventoryReportPanel extends _Ext.ReportPanelBase<LeadsReportRequest> {
        protected getReportTitle() { return 'Inventory Report'; }
        protected getReportKey() { return 'Reports.InventoryReport'; }
        protected getFormKey() { return StockReportForm.formKey; }

        private form: StockReportForm = new StockReportForm(this.idPrefix);

        constructor(container: JQuery) {
            super(container);
            this.form.Branch.getGridField().toggle(false)
            this.form.Division.getGridField().toggle(false)
            this.form.Group.getGridField().toggle(false)
            this.form.Product.getGridField().toggle(false)
            this.form.Status.getGridField().toggle(false)
            this.form.PurchaseOrderNo.getGridField().toggle(false)

            this.form.Type.changeSelect2(e => {
                if (this.form.Type.value == '3') {
                    this.form.Branch.getGridField().toggle(true)
                    this.form.Division.getGridField().toggle(false)
                    this.form.Group.getGridField().toggle(false)
                    this.form.Product.getGridField().toggle(false)
                }
                else if (this.form.Type.value == '4') {
                    this.form.Branch.getGridField().toggle(false)
                    this.form.Division.getGridField().toggle(true)
                    this.form.Group.getGridField().toggle(false)
                    this.form.Product.getGridField().toggle(false)
                }
                else if (this.form.Type.value == '7') {
                    this.form.Branch.getGridField().toggle(false)
                    this.form.Division.getGridField().toggle(true)
                    this.form.Product.getGridField().toggle(false)
                    this.form.Group.getGridField().toggle(false)
                }
                else if (this.form.Type.value == '8') {
                    this.form.Group.getGridField().toggle(false)
                    this.form.Product.getGridField().toggle(true)
                    this.form.Branch.getGridField().toggle(false)
                    this.form.Group.getGridField().toggle(false)
                }
                else if (this.form.Type.value == '5') {
                    this.form.Branch.getGridField().toggle(false)
                    this.form.Product.getGridField().toggle(false)
                    this.form.Division.getGridField().toggle(false)
                    this.form.Group.getGridField().toggle(true)
                }
                else
                {
                    this.form.Branch.getGridField().toggle(false)
                    this.form.Division.getGridField().toggle(false)
                    this.form.Group.getGridField().toggle(false)
                }
            });
        }
    }
}