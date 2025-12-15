namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerEditor()
    export class UserEditor extends Serenity.LookupEditorBase<Serenity.LookupEditorOptions, UserRow> {

        constructor(container, options) {
            super(container, options);
        }

        protected getLookupKey() {
            return 'Administration.UserLookup';
        }

        protected getItemText(item, lookup) {
            if (item.IsActive == 0)
                return super.getItemText(item, lookup) + ' [X]';

            if (item.Branch != 'undefined')
                return super.getItemText(item, lookup);
            else
                return super.getItemText(item, lookup) + ' [' + item.Branch + ']';

        }
    }
}