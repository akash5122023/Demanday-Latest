
/// <reference path="../../Common/Helpers/DialogBase.ts" />
namespace AdvanceCRM.Accounting {

    @Serenity.Decorators.registerClass()

    export class CashbookDialog extends DialogBase<CashbookRow, any> {
        protected getFormKey() { return CashbookForm.formKey; }
        protected getIdProperty() { return CashbookRow.idProperty; }
        protected getLocalTextPrefix() { return CashbookRow.localTextPrefix; }
        protected getNameProperty() { return CashbookRow.nameProperty; }
        protected getService() { return CashbookService.baseUrl; }
        protected getDeletePermission() { return CashbookRow.deletePermission; }
        protected getInsertPermission() { return CashbookRow.insertPermission; }
        protected getUpdatePermission() { return CashbookRow.updatePermission; }

        protected form = new CashbookForm(this.idPrefix);

        constructor() {
            super();

            this.form.Date.value = Date.now.toString(); //not working
            this.form.Head.getGridField().toggle(false);
            this.form.ContactsId.getGridField().toggle(false);
            this.form.InvoiceNo.getGridField().toggle(false);
            this.form.CashIn.getGridField().toggle(false);
            this.form.CashOut.getGridField().toggle(false);
            this.form.Narration.getGridField().toggle(false);
            this.form.BankId.getGridField().toggle(false);

            this.form.Type.change(e => {//TODO: Add remove heads according to type
                this.form.Head.value = "";
                if (this.form.Type.value.trim() != "") {
                    this.form.Head.getGridField().toggle(true);
                }
                else {
                    this.form.Head.getGridField().toggle(false);
                }
                this.form.ContactsId.getGridField().toggle(false);
                this.form.InvoiceNo.getGridField().toggle(false);
                this.form.CashIn.getGridField().toggle(false);
                this.form.CashOut.getGridField().toggle(false);
                this.form.Narration.getGridField().toggle(false);
                this.form.BankId.getGridField().toggle(false);

                this.form.Head.filterField = "Type";

                //let selectedType = this.form.Type.value;

                //if (selectedType) {
                //    this.form.Head.filterValue = [selectedType, "Project"].join(",");
                //}

             //   this.form.Head.filterValue = selectedType;

                
                this.form.Head.filterValue = this.form.Type.value;
            });

            //this.form.Head.change(e => {
            //    //Q.alert(this.form.Head.value);
            //    if (this.form.Head.value.trim() == "") {
            //        this.form.ContactsId.getGridField().toggle(false);
            //        this.form.InvoiceNo.getGridField().toggle(false);
            //        this.form.CashIn.getGridField().toggle(false);
            //        this.form.CashOut.getGridField().toggle(false);
            //        this.form.Narration.getGridField().toggle(false);
            //        this.form.BankId.getGridField().toggle(false);
            //    }
            //    if ((this.form.Head.value == "1") || (this.form.Head.value == "11")) {
            //        this.form.ContactsId.getGridField().toggle(true);
            //        this.form.InvoiceNo.getGridField().toggle(true);
            //    }
            //    else {
            //        this.form.ContactsId.getGridField().toggle(false);
            //        this.form.InvoiceNo.getGridField().toggle(false);
            //    }
            //    if ((this.form.Head.value == "47") || (this.form.Head.value == "38")) {
            //        this.form.EmployeeId.getGridField().toggle(true);
            //    }
            //    else {
            //        this.form.EmployeeId.getGridField().toggle(false);
            //    }
            //    if ((this.form.Head.value == "48")) {
            //        this.form.ProjectId.getGridField().toggle(true);
            //        this.form.EmployeeId.getGridField().toggle(true);
            //        this.form.ProjectAmtOut.getGridField().toggle(true);
            //    }
            //    else {
            //        this.form.ProjectId.getGridField().toggle(false);
            //        this.form.ProjectAmtOut.getGridField().toggle(false);
            //       // this.form.EmployeeId.getGridField().toggle(false);
                    
            //    }
            //    if ((this.form.Head.value == "1048")) {
            //        this.form.ProjectId.getGridField().toggle(true);
            //        this.form.ProjectAmtIn.getGridField().toggle(true);
            //        this.form.CashIn.getGridField().toggle(false);
            //        this.form.BankId.getGridField().toggle(false);

            //    }
            //    else {
            //       //this.form.ProjectId.getGridField().toggle(false);
            //        this.form.ProjectAmtIn.getGridField().toggle(false);
            //        this.form.CashIn.getGridField().toggle(true);
            //        this.form.BankId.getGridField().toggle(true);
            //    }
            //    //if (this.form.Head.value == "11") {
            //    //    this.form.ContactsId.getGridField().toggle(true);
            //    //    this.form.InvoiceNo.getGridField().toggle(true);
            //    //}

            //    if (this.form.Head.value == "1048" && this.form.Type.value == "1") {

            //        this.form.CashOut.getGridField().toggle(false);
            //        this.form.CashIn.getGridField().toggle(false);
            //        this.form.BankId.getGridField().toggle(false);
                   
            //    }
            //    else {
            //        this.form.CashIn.getGridField().toggle(true);
            //        this.form.BankId.getGridField().toggle(true);
            //        this.form.CashOut.getGridField().toggle(false);
                    
            //    }
            //    if (this.form.Type.value == "1") {
            //        this.form.CashIn.getGridField().toggle(true);
            //        this.form.BankId.getGridField().toggle(true);
            //        this.form.CashOut.getGridField().toggle(false);

            //    }
            //    if ( this.form.Head.value == "48" && this.form.Type.value == "2" ) {
            //        this.form.CashIn.getGridField().toggle(false);
            //        this.form.CashOut.getGridField().toggle(false);
            //        this.form.BankId.getGridField().toggle(false);
            //    }
            //    else {
            //        this.form.CashIn.getGridField().toggle(false);
            //        this.form.CashOut.getGridField().toggle(true);
            //        this.form.BankId.getGridField().toggle(true);
            //    }
            //    //if (this.form.Type.value == "2") {
            //    //    this.form.CashIn.getGridField().toggle(false);
            //    //    this.form.CashOut.getGridField().toggle(true);
            //    //    this.form.BankId.getGridField().toggle(true);
            //    //}
                
            //    this.form.Narration.getGridField().toggle(true);
            //    /*this.form.BankId.getGridField().toggle(true);*/
            //});


            this.form.Head.change(e => {
                let typeValue = this.form.Type.value;
                let headValue = this.form.Head.value;
                

                // By default, always show Narration field
                this.form.Narration.getGridField().toggle(true);

                // Hide all fields initially to avoid conflicts
                this.form.ContactsId.getGridField().toggle(false);
                this.form.InvoiceNo.getGridField().toggle(false);
                this.form.CashIn.getGridField().toggle(false);
                this.form.CashOut.getGridField().toggle(false);
                this.form.BankId.getGridField().toggle(false);
                this.form.EmployeeId.getGridField().toggle(false);
                this.form.ProjectId.getGridField().toggle(false);
                this.form.ProjectAmtIn.getGridField().toggle(false);
               
                this.form.Purpose.getGridField().toggle(false);
                this.form.IsCashIn.getGridField().toggle(false);

                if (typeValue == "") {
                    this.form.CashIn.getGridField().toggle(false);
                    this.form.CashOut.getGridField().toggle(false);
                }
                if (headValue != "49") {
                    this.form.CashIn.getGridField().toggle(true);
                    
                    this.form.BankId.getGridField().toggle(true);
                }
                if (headValue != "48") {
                    
                    this.form.CashOut.getGridField().toggle(true);
                    this.form.BankId.getGridField().toggle(true);
                }
                if (typeValue == "2") {
                    this.form.CashIn.getGridField().toggle(false);
                    this.form.CashOut.getGridField().toggle(true);
                } else {
                    this.form.CashOut.getGridField().toggle(false);
                    this.form.CashIn.getGridField().toggle(true);
                }

                     if ((this.form.Head.value == "1") || (this.form.Head.value == "11")) {
                    this.form.ContactsId.getGridField().toggle(true);
                    this.form.InvoiceNo.getGridField().toggle(true);
                }
                else {
                    this.form.ContactsId.getGridField().toggle(false);
                    this.form.InvoiceNo.getGridField().toggle(false);
                }
                // Condition: Type 1 & Head 1048 → Show ProjectId & ProjectAmtIn
                if (typeValue == "1" && headValue == "49") {
                    this.form.ProjectId.getGridField().toggle(true);
                    this.form.ProjectAmtIn.getGridField().toggle(false);
                    this.form.IsCashIn.getGridField().toggle(true);
                    this.form.CashIn.getGridField().toggle(true);
                    this.form.CashOut.getGridField().toggle(false);
                    this.form.BankId.getGridField().toggle(true);

                }
                              
                // Condition: Type 2 & Head 48 → Show ProjectId & ProjectAmtOut, Hide Others
                if (typeValue == "2" && headValue == "48") {
                    this.form.ProjectId.getGridField().toggle(true);
                    this.form.Purpose.getGridField().toggle(true);
                    this.form.CashIn.getGridField().toggle(false);
                    this.form.CashOut.getGridField().toggle(true);
                    this.form.BankId.getGridField().toggle(true);
                    this.form.EmployeeId.getGridField().toggle(true);
                }
               
                
            });

            this.form.IsCashIn.change(e => {
                if (this.form.IsCashIn.value == true) {
                    this.form.ProjectAmtIn.getGridField().toggle(true);
                }
                else {
                    this.form.ProjectAmtIn.getGridField().toggle(false);
                }
            });


        }

        protected updateInterface() {
            super.updateInterface();

            this.toolbar.findButton('export-pdf-button').toggle(this.isEditMode());
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'Cashbook.PrintRV', //Change PrintQuotationUnique, PrintQuotation
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));

            buttons.push({
                title: 'Outstanding Balance',
                icon: 'fa-book',
                hint: 'Get outstanding balance of debtor',
                onClick: () => {

                    if (this.form.ContactsId.value > '0') {
                        var SourceName = Masters.AccountingHeadsRow.getLookup().itemById[this.form.Head.value].Head;
                        if (SourceName == "Sundry debtors") {
                            CashbookService.GetOutstandingBalance({
                                Id: Q.toId(this.form.ContactsId.value)
                            },
                                response => {
                                    Q.information("Current outstanding balance is " + response.Status, () => { Q.resolveUrl('#'); });
                                });
                        }
                        else if (SourceName == "Sundry creditors") {
                            CashbookService.GetOutstandingCreditBalance({
                                Id: Q.toId(this.form.ContactsId.value)
                            },
                                response => {
                                    Q.information("Current outstanding balance is " + response.Status, () => { Q.resolveUrl('#'); });
                                });
                        }
                    }
                   
                    else {
                        if (SourceName == "Sundry debtors") { Q.alert('To get Outstanding balance select head as Sundry debtors and select Contact first'); }
                        else { Q.alert('To get Outstanding balance select head as Sundry creditors and select Contact first');}
                      
                        }
                    
                }
            });
            if (Authorization.hasPermission("Cashbook:Can Approve")) {
                buttons.push({
                    title: 'Approve',
                    cssClass: 'approve-button',
                    icon: 'fa-check-circle text-green',
                    hint: 'Approve this Expense',
                    onClick: () => {
                        CashbookService.Approve({
                            Id: Q.toId(this.entityId)
                        },
                            response => {
                                if (response.Status == "Approved") {
                                    Q.notifySuccess(response.Status);
                                    Serenity.SubDialogHelper.triggerDataChange(this);
                                }
                                else {
                                    Q.notifyError(response.Status);
                                }
                            });
                    },
                    separator: true
                });
            }
            return buttons;
        }

        //Custom button inside dialog
        //protected afterLoadEntity() {
        //    super.afterLoadEntity();

        //    $('<a class="inplace-button"><b> OB<\/b><\/a>')
        //        .insertAfter(this.form.ContactsId.element)  // <======================
        //        .click(() => this.myFunction());
        //}

        //private myFunction() {
        //    CashbookService.GetOutstandingBalance({
        //        Id: Q.toId(this.form.ContactsId.value)
        //    },
        //        response => {
        //            Q.information("Current outstanding balance is " + response.Status, () => { Q.resolveUrl('#'); });
        //        });
        //}

        onDialogOpen() {
            super.onDialogOpen();
            if (this.form.RepresentativeId.value <= "1") {
                this.form.RepresentativeId.value = Q.toId(Authorization.userDefinition.UserId);
            }
            //this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();
        }
    }
}