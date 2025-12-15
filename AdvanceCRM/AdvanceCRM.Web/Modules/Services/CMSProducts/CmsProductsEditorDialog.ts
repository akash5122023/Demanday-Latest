
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />

namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class CMSProductsEditorDialog extends Common.GridEditorDialog<CMSProductsRow> {
        protected getFormKey() { return CMSProductsForm.formKey; }
        protected getLocalTextPrefix() { return CMSProductsRow.localTextPrefix; }
        protected form = new CMSProductsForm(this.idPrefix);

        protected getDialogOptions() {
            let opt = super.getDialogOptions();
            opt.width = "800px";
            return opt;
        }
    }
}