namespace AdvanceCRM.Default {

    /**
     * Our select editor with hardcoded values.
     * 
     * When you define a new editor type, make sure you build
     * and transform templates for it to be available 
     * in server side forms, e.g. [HardCodedValuesEditor]
     */
    @Serenity.Decorators.registerEditor()
    export class AccountTypeEditor extends Serenity.Select2Editor<any, any> {

        constructor(container: JQuery) {
            super(container, null);

            // add option accepts a key (id) value and display text value
           
            this.addOption("AccumulatedDepreciation", "Accumulated Depreciation");
            this.addOption("AssetReceivedButNotBilled", "Asset Received But Not Billed");
            this.addOption("Bank", "Bank");
            this.addOption("Cash", "Cash");
            this.addOption("Chargeable", "Chargeable");
            this.addOption("CapitalWorkinProgress", "Capital Work in Progress");
            this.addOption("CostofGoodsSold", "Cost of Goods Sold");

            this.addOption("Depreciation", "Depreciation");
            this.addOption("Equity", "Equity");
            this.addOption("ExpenseAccount", "Expense Account");
            this.addOption("ExpensesIncludedInAssetValuation", "Expenses Included In Asset Valuation");
            this.addOption("ExpensesIncludedInValuation", "Expenses Included In Valuation");
            this.addOption("FixedAsset", "Fixed Asset");
            this.addOption("IncomeAccount", "Income Account");
            this.addOption("Payable", "Payable");
            this.addOption("Receivable", "Receivable");
            this.addOption("RoundOff", "Round Off");
            this.addOption("Stock", "Stock");
            this.addOption("StockAdjustment", "Stock Adjustment");
            this.addOption("StockReceivedButNotBilled", "Stock Received But Not Billed");
            this.addOption("ServiceReceivedButNotBilled", "Service Received But Not Billed");
            this.addOption("Tax", "Tax");
            this.addOption("Temporary", "Temporary");
            
         
        }
    }
}