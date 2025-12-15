
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class SalesReturnDialog extends DialogBase<SalesReturnRow, any> {
        protected getFormKey() { return SalesReturnForm.formKey; }
        protected getIdProperty() { return SalesReturnRow.idProperty; }
        protected getLocalTextPrefix() { return SalesReturnRow.localTextPrefix; }
        protected getNameProperty() { return SalesReturnRow.nameProperty; }
        protected getService() { return SalesReturnService.baseUrl; }
        protected getDeletePermission() { return SalesReturnRow.deletePermission; }
        protected getInsertPermission() { return SalesReturnRow.insertPermission; }
        protected getUpdatePermission() { return SalesReturnRow.updatePermission; }

        protected form = new SalesReturnForm(this.idPrefix);

        constructor() {
            super();

            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));

            Common.NavigationService.MultiLocation({},
                response => {
                    if (response.Status != "Remove") {
                        this.form.BranchId.element.addClass(" required");

                        if (Authorization.hasPermission("Purchase:Change Branch")) {
                            Serenity.EditorUtils.setReadonly(this.form.BranchId.element, false);
                        }
                        else {
                            Serenity.EditorUtils.setReadonly(this.form.BranchId.element, true);
                        }
                    }
                }
            );

            this.form.InvoiceNo.change(e => {

                var invNo = Q.toId(this.form.InvoiceNo.value);

                //Q.notifyInfo(Q.tryFirst(SalesRow.getLookup().items, x => x.InvoiceNo == invNo).ContactsId.toString());


                let sale = Q.tryFirst(SalesRow.getLookup().items, x => x.InvoiceNo == invNo);

                if (sale && sale.ContactsId != null) {
                    Q.notifyInfo(sale.ContactsId.toString());
                } else {
                    Q.notifyWarning("No matching sale found for Invoice No: " + invNo);
                }


                //if (Q.tryFirst(SalesRow.getLookup().items, x => x.InvoiceNo == invNo).ContactsId.toString() != "") {
                //    this.form.ContactsId.value = Q.tryFirst(SalesRow.getLookup().items, x => x.InvoiceNo == invNo).ContactsId.toString();
                //    this.form.InvoiceDate.value = Q.tryFirst(SalesRow.getLookup().items, x => x.InvoiceNo == invNo).Date;
                //}
                //else {
                //    this.form.ContactsId.value = Q.tryFirst(SalesRow.getLookup().items, x => x.Id == invNo).ContactsId.toString();
                //    this.form.InvoiceDate.value = Q.tryFirst(SalesRow.getLookup().items, x => x.Id == invNo).Date;
                //}

                let s = Q.tryFirst(SalesRow.getLookup().items, x => x.InvoiceNo == invNo);

                if (s && s.ContactsId != null) {
                    this.form.ContactsId.value = s.ContactsId.toString();
                    this.form.InvoiceDate.value = s.Date;
                } else {
                    let fallbackSale = Q.tryFirst(SalesRow.getLookup().items, x => x.Id == invNo);
                    if (fallbackSale && fallbackSale.ContactsId != null) {
                        this.form.ContactsId.value = fallbackSale.ContactsId.toString();
                        this.form.InvoiceDate.value = fallbackSale.Date;
                    } else {
                        Q.notifyWarning("No matching sale found for InvoiceNo or Id: " + invNo);
                        this.form.ContactsId.value = "";
                        this.form.InvoiceDate.value = "";
                    }
                }


            });

        }


        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'SalesReturn.PrintSalesReturn',
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));
            return buttons;
        }


        onDialogOpen() {
            super.onDialogOpen();

            if (this.form.OwnerId.value < "1") {
                this.form.OwnerId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedId.value < "1") {
                this.form.AssignedId.value = Q.toId(Authorization.userDefinition.UserId);
            }

            if (this.form.BranchId.value < "1" && Authorization.userDefinition.BranchId) {
                this.form.BranchId.value = Authorization.userDefinition.BranchId.toString().toString();
            }
        }
    }
}