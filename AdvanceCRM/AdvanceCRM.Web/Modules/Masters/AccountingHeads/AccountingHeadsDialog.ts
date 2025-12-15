
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class AccountingHeadsDialog extends DialogBase<AccountingHeadsRow, any> {
        protected getFormKey() { return AccountingHeadsForm.formKey; }
        protected getIdProperty() { return AccountingHeadsRow.idProperty; }
        protected getLocalTextPrefix() { return AccountingHeadsRow.localTextPrefix; }
        protected getNameProperty() { return AccountingHeadsRow.nameProperty; }
        protected getService() { return AccountingHeadsService.baseUrl; }
        protected getDeletePermission() { return AccountingHeadsRow.deletePermission; }
        protected getInsertPermission() { return AccountingHeadsRow.insertPermission; }
        protected getUpdatePermission() { return AccountingHeadsRow.updatePermission; }

        protected form = new AccountingHeadsForm(this.idPrefix);

        constructor() {
            super();
        }

        protected getToolbarButtons(): Serenity.ToolButton[] {

            //Adding confirmation for saving
            let btns = super.getToolbarButtons();

            var btnSave = Q.first(btns, x => x.cssClass == "save-and-close-button");
            var btnApply = Q.first(btns, x => x.cssClass == "apply-changes-button");

            var oldSaveClick = btnSave.onClick;
            var oldApplyClick = btnApply.onClick;

            btnSave.onClick = e => { this.confirmBeforeSave(oldSaveClick, e); };
            btnApply.onClick = e => { this.confirmBeforeSave(oldApplyClick, e); };


            //Removing delete button
            btns.splice(Q.indexOf(btns, x => x.cssClass == "delete-button"), 1);

            return btns;
        }

        private confirmBeforeSave(oldEvt, e) {
            Q.confirm("Accounting heads once added cannot be deleted \n To continue adding please press Yes \n If you want to change details and then save press No", () => {
                oldEvt(e);
            });
        }
    }
}