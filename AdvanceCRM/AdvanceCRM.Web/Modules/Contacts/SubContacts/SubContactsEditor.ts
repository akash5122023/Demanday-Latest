
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Contacts {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class SubContactsEditor extends Common.GridEditorBase<SubContactsRow> {
        protected getColumnsKey() { return 'Contacts.SubContacts'; }
        protected getDialogType() { return SubContactsEditorDialog; }
        protected getLocalTextPrefix() { return SubContactsRow.localTextPrefix; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}