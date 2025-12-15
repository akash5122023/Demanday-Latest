namespace AdvanceCRM.Default {

    /**
     * Our select editor with hardcoded values.
     * 
     * When you define a new editor type, make sure you build
     * and transform templates for it to be available 
     * in server side forms, e.g. [HardCodedValuesEditor]
     */
	@Serenity.Decorators.registerEditor()
	export class EmployeeLookupTextEditor extends Serenity.LookupEditorBase<Serenity.LookupEditorOptions, Payroll.TabEmployeeRow> {
		protected getColumnsKey() { return 'Payroll.TabEmployee'; }

		protected getLocalTextPrefix() { return Payroll.TabEmployeeRow.localTextPrefix; }
		constructor(container: JQuery, options: Serenity.LookupEditorOptions) {
			super(container, options);
			
		}

		protected getLookupKey() {


			return Payroll.TabEmployeeRow.lookupKey;



		}

		protected getItemText(item: Payroll.TabEmployeeRow, lookup: Q.Lookup<Payroll.TabEmployeeRow>) {
			return super.getItemText(item, lookup) +
				' (' + item.EmployeeName + ')'
		}
    }
}