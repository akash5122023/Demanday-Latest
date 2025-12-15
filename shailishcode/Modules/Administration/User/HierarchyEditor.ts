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
            var items = super.getItems(lookup);

            if (items.find(x => x.UserId == 1))
                return items;

            var adminItem = lookup.items.filter(x => x.UserId === 1);
            return adminItem.concat(items);
        }
    }
}