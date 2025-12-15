
/// <reference path="../../Common/Helpers/DialogBase.ts" />
namespace AdvanceCRM.Accounting {

    @Serenity.Decorators.registerClass()

    export class ExpenseManagementDialog extends DialogBase<ExpenseManagementRow, any> {
        protected getFormKey() { return ExpenseManagementForm.formKey; }
        protected getIdProperty() { return ExpenseManagementRow.idProperty; }
        protected getLocalTextPrefix() { return ExpenseManagementRow.localTextPrefix; }
        protected getNameProperty() { return ExpenseManagementRow.nameProperty; }
        protected getService() { return ExpenseManagementService.baseUrl; }
        protected getDeletePermission() { return ExpenseManagementRow.deletePermission; }
        protected getInsertPermission() { return ExpenseManagementRow.insertPermission; }
        protected getUpdatePermission() { return ExpenseManagementRow.updatePermission; }

        protected form = new ExpenseManagementForm(this.idPrefix);

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            if (Authorization.hasPermission("ExpenseManagement:Can Approve")) {
                buttons.push({
                    title: 'Approve',
                    cssClass: 'fa-check',
                    icon: 'fa-check',
                    hint: 'Approve this Expense',
                    onClick: () => {
                        ExpenseManagementService.Approve({
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
                            }
                        );
                    },
                    separator: true
                });
            }

            return buttons;
        }

        protected updateInterface() {
            super.updateInterface();

            this.toolbar.findButton('fa-check').toggle(this.isEditMode());

            if (this.isEditMode() == true) {
                if (this.entity.ApprovedBy != null) {
                    this.toolbar.findButton('fa-check').toggle(false);
                }
                else {
                    this.toolbar.findButton('fa-check').toggle(true);
                }
            }
        }

        onDialogOpen() {
            super.onDialogOpen();
            this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();

            if (this.form.RepresentativeId.value <= "1") {
                this.form.RepresentativeId.value = Q.toId(Authorization.userDefinition.UserId);
            }
        }
    }
}