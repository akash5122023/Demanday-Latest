namespace AdvanceCRM.Default {

    /**
     * Our select editor with hardcoded values.
     * 
     * When you define a new editor type, make sure you build
     * and transform templates for it to be available 
     * in server side forms, e.g. [HardCodedValuesEditor]
     */
    @Serenity.Decorators.registerEditor()
    export class ApplicantStatusEditor extends Serenity.Select2Editor<any, any> {

        constructor(container: JQuery) {
            super(container, null);

            // add option accepts a key (id) value and display text value
           
            this.addOption("Open", "Open");
            this.addOption("Replied", "Replied");
            this.addOption("Rejected", "Rejected");
            this.addOption("Hold", "Hold");
            this.addOption("Accepted", "Accepted");
            
         
        }
    }
}