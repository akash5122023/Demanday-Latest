namespace AdvanceCRM.Default {

    /**
     * Our select editor with hardcoded values.
     * 
     * When you define a new editor type, make sure you build
     * and transform templates for it to be available 
     * in server side forms, e.g. [HardCodedValuesEditor]
     */
    @Serenity.Decorators.registerEditor()
    export class WorkingHoursCalculationBasedOnEditor extends Serenity.Select2Editor<any, any> {

        constructor(container: JQuery) {
            super(container, null);

            // add option accepts a key (id) value and display text value
           
            this.addOption("First Check-in and Last Check-out", "First Check-in and Last Check-out");
            this.addOption("Every Valid Check-in and Check-out", "Every Valid Check-in and Check-out");
            
            
         
        }
    }
}