namespace AdvanceCRM.Default {

    /**
     * Our select editor with hardcoded values.
     * 
     * When you define a new editor type, make sure you build
     * and transform templates for it to be available 
     * in server side forms, e.g. [HardCodedValuesEditor]
     */
    @Serenity.Decorators.registerEditor()
    export class EmployeeEditor extends Serenity.Select2Editor<any, any> {

        constructor(container: JQuery) {
            super(container, null);

            // add option accepts a key (id) value and display text value
           
            Q.serviceCall({
                url: Q.resolveUrl('~/Services/Payroll/TabEmployee/getEmployeeList'),
                async: false,
                onSuccess: response => {
                    var count = Object.keys(response).length;
                    for (var ind = 0; ind < count; ind++) {

                        this.addOption(response[ind]["Value"], response[ind]["Text"]);
                    }
                }
            });
            
         
        }
    }
}