
/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class KnowlarityAgentsEditor extends Common.GridEditorBase<KnowlarityAgentsRow> {
        protected getColumnsKey() { return 'Settings.KnowlarityAgents'; }
        protected getDialogType() { return KnowlarityAgentsEditorDialog; }
        protected getLocalTextPrefix() { return KnowlarityAgentsRow.localTextPrefix; }

        public ContactsId: number;


        constructor(container: JQuery) {
            super(container);
        }

        protected initEntityDialog(itemType: string, dialog: Serenity.Widget<any>) {
            super.initEntityDialog(itemType, dialog);
        }
    }
}