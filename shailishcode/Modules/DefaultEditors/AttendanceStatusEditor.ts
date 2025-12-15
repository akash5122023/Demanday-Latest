namespace AdvanceCRM.Default {

    /**
     * Our select editor with hardcoded values.
     * 
     * When you define a new editor type, make sure you build
     * and transform templates for it to be available 
     * in server side forms, e.g. [HardCodedValuesEditor]
     */
    @Serenity.Decorators.registerEditor()
    export class AttendanceStatusEditor extends Serenity.Select2Editor<any, any> {

        constructor(container: JQuery) {
            super(container, null);

            // add option accepts a key (id) value and display text value
           
            this.addOption("Present", "Present");
            this.addOption("Absent", "Absent");
            this.addOption("OnLeave", "On Leave");
            this.addOption("HalfDay", "Half Day");
            this.addOption("WorkFromHome", "Work From Home");
            
         
        }
    }
}