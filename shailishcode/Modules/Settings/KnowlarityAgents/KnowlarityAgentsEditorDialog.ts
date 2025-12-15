
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />

namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class KnowlarityAgentsEditorDialog extends Common.GridEditorDialog<KnowlarityAgentsRow> {
        protected getFormKey() { return KnowlarityAgentsForm.formKey; }
        protected getLocalTextPrefix() { return KnowlarityAgentsRow.localTextPrefix; }
        protected form = new KnowlarityAgentsForm(this.idPrefix);

        constructor() {
            super();
        }


        onDialogOpen() {
            super.onDialogOpen();
        }

    }
}