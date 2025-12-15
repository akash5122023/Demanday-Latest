namespace AdvanceCRM.Reports {
    export enum AccountingReportType {
        Cashbook = 1,
        AllOutstanding = 2,
        OutstandingBalance = 3,
        AllSupplierOutstanding = 4,
        SupplierOutstandingBalance = 5,
        LedgerBalance = 6,
        ContactwiseCashbook = 7
    }
    Serenity.Decorators.registerEnumType(AccountingReportType, 'AdvanceCRM.Reports.AccountingReportType', 'Reports.AccountingReportType');
}
