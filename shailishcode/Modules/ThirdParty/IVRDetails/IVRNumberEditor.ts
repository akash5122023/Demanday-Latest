
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerEditor()
    export class IVRNumberEditor extends
        Serenity.LookupEditorBase<Serenity.LookupEditorOptions, any> {

        constructor(container: JQuery, opt: Serenity.LookupEditorOptions) {
            super(container, opt);
        }


        protected getLookupKey() {
            return 'Settings.IVR';
        }

        protected getItems(lookup: Q.Lookup<any>) {

            let items: any = AdvanceCRM.Settings.IVRConfigurationRow.getLookup().itemById[1].IVRNumber.split(",");

            return items;
        }

        protected getItemText(item, lookup) {

            return item;
        }
    }

}