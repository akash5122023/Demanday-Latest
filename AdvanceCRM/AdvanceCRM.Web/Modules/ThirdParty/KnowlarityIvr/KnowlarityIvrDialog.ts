
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class KnowlarityIvrDialog extends Serenity.EntityDialog<KnowlarityIvrRow, any> {
        protected getFormKey() { return KnowlarityIvrForm.formKey; }
        protected getIdProperty() { return KnowlarityIvrRow.idProperty; }
        protected getLocalTextPrefix() { return KnowlarityIvrRow.localTextPrefix; }
        protected getNameProperty() { return KnowlarityIvrRow.nameProperty; }
        protected getService() { return KnowlarityIvrService.baseUrl; }
        protected getDeletePermission() { return KnowlarityIvrRow.deletePermission; }
        protected getInsertPermission() { return KnowlarityIvrRow.insertPermission; }
        protected getUpdatePermission() { return KnowlarityIvrRow.updatePermission; }

        protected form = new KnowlarityIvrForm(this.idPrefix);
        protected updateInterface(): void {
            super.updateInterface();
            Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
            this.element.find('sup').hide();
            //if (this.form.IsMoved.value == true)
            //    this.toolbar.element.toggle(false);
        }

    }
}