namespace AdvanceCRM.Masters {
    export enum TransactionTypeMaster {
        Deposit = 1,
        Expense = 2
    }
    Serenity.Decorators.registerEnumType(TransactionTypeMaster, 'AdvanceCRM.Masters.TransactionTypeMaster', 'Masters.TransactionType');
}
