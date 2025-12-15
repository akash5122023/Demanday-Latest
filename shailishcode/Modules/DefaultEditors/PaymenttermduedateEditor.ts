namespace AdvanceCRM.Default {

    /**
     * Our select editor with hardcoded values.
     * 
     * When you define a new editor type, make sure you build
     * and transform templates for it to be available 
     * in server side forms, e.g. [HardCodedValuesEditor]
     */
    @Serenity.Decorators.registerEditor()
    export class PaymenttermduedateEditor extends Serenity.Select2Editor<any, any> {

        constructor(container: JQuery) {
            super(container, null);

            // add option accepts a key (id) value and display text value
           
            this.addOption("Day(s) after invoice date", "Day(s) after invoice date");
            this.addOption("Day(s) after the end of the invoice month", "Day(s) after the end of the invoice month");
            this.addOption("Month(s) after the end of the invoice month", "Month(s) after the end of the invoice month");
           
            
         
        }
    }
}