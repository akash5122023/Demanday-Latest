namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerEditor()
    export class HierarchyEditor extends Serenity.LookupEditorBase<Serenity.LookupEditorOptions, UserRow> {

        constructor(container, options) {
            super(container, options);
        }

        protected getLookupKey() {
            return 'Administration.User';
        }

        protected getItems(lookup) {
            return super.getItems(lookup);
        }
    }
}