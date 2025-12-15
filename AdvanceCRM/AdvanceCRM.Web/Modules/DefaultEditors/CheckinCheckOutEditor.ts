namespace AdvanceCRM.Default {

    /**
     * Our select editor with hardcoded values.
     * 
     * When you define a new editor type, make sure you build
     * and transform templates for it to be available 
     * in server side forms, e.g. [HardCodedValuesEditor]
     */
    @Serenity.Decorators.registerEditor()
    export class CheckinCheckOutEditor extends Serenity.Select2Editor<any, any> {

        constructor(container: JQuery) {
            super(container, null);

            // add option accepts a key (id) value and display text value
           
            this.addOption("Alternating entries as IN and OUT during the same shift", "Alternating entries as IN and OUT during the same shift");
            this.addOption("Strictly based on Log Type in Employee Checkin", "Strictly based on Log Type in Employee Checkin");
          
            
         
        }
    }
}