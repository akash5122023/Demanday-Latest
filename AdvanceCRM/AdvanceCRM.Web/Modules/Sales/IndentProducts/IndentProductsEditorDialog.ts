
namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class IndentProductsEditorDialog extends Common.GridEditorDialog<IndentProductsRow> {
        protected getFormKey() { return IndentProductsForm.formKey; }
        protected getLocalTextPrefix() { return IndentProductsRow.localTextPrefix; }
        protected getNameProperty() { return IndentProductsRow.nameProperty; }

        protected form = new IndentProductsForm(this.idPrefix);

        constructor() {
            super();

            // Optionally: auto-fill product name when selected
            this.form.ProductsId.changeSelect2(e => {
                var PID = Q.toId(this.form.ProductsId.value);
                if (PID != null) {
                    // If needed, you can do something here
                }
            });
        }
    }
}