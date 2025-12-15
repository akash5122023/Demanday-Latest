namespace AdvanceCRM.Default {

    /**
     * Our select editor with hardcoded values.
     * 
     * When you define a new editor type, make sure you build
     * and transform templates for it to be available 
     * in server side forms, e.g. [HardCodedValuesEditor]
     */
    @Serenity.Decorators.registerEditor()
    export class BloodGroupEditor extends Serenity.Select2Editor<any, any> {

        constructor(container: JQuery) {
            super(container, null);

            // add option accepts a key (id) value and display text value
           
            this.addOption("A+", "A+");
            this.addOption("A-", "A-");
            this.addOption("B+", "B+");
            this.addOption("B-", "B-");
            this.addOption("AB+", "AB+");
            this.addOption("AB-", "AB-");
            this.addOption("O+", "O+");
            this.addOption("O-", "O-");
            
         
        }
    }
}