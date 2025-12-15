namespace AdvanceCRM.Sales {
    export interface ChallanForm {
        ContactsId: Serenity.LookupEditor;
        ContactsContactType: Serenity.IntegerEditor;
        ContactsName: Serenity.StringEditor;
        ContactsPhone: Serenity.StringEditor;
        ContactsWhatsapp: Serenity.StringEditor;
        ContactsAddress: Serenity.TextAreaEditor;
        ContactPersonId: Serenity.LookupEditor;
        ContactPersonName: Serenity.StringEditor;
        ContactPersonPhone: Serenity.StringEditor;
        ContactPersonWhatsapp: Serenity.StringEditor;
        ContactPersonProject: Serenity.StringEditor;
        ContactPersonAddress: Serenity.StringEditor;
        Products: ChallanProductsEditor;
        PackagingCharges: Serenity.DecimalEditor;
        FreightCharges: Serenity.DecimalEditor;
        Advacne: Serenity.DecimalEditor;
        ChallanNo: Serenity.IntegerEditor;
        Date: Serenity.DateEditor;
        Status: Serenity.EnumEditor;
        DueDate: Serenity.DateEditor;
        ClosingDate: Serenity.DateEditor;
        Type: Serenity.EnumEditor;
        SourceId: Serenity.LookupEditor;
        StageId: Serenity.LookupEditor;
        BranchId: Serenity.LookupEditor;
        InvoiceMade: BooleanSwitchEditor;
        OtherAddress: BooleanSwitchEditor;
        DispatchDetails: Serenity.TextAreaEditor;
        ShippingAddress: Serenity.TextAreaEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
        OwnerId: Serenity.LookupEditor;
        AssignedId: Serenity.LookupEditor;
        NoteList: Common.NotesEditor;
    }

    export class ChallanForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.Challan';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ChallanForm.init)  {
                ChallanForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.StringEditor;
                var w3 = s.TextAreaEditor;
                var w4 = ChallanProductsEditor;
                var w5 = s.DecimalEditor;
                var w6 = s.DateEditor;
                var w7 = s.EnumEditor;
                var w8 = BooleanSwitchEditor;
                var w9 = s.MultipleImageUploadEditor;
                var w10 = Common.NotesEditor;

                Q.initFormType(ChallanForm, [
                    'ContactsId', w0,
                    'ContactsContactType', w1,
                    'ContactsName', w2,
                    'ContactsPhone', w2,
                    'ContactsWhatsapp', w2,
                    'ContactsAddress', w3,
                    'ContactPersonId', w0,
                    'ContactPersonName', w2,
                    'ContactPersonPhone', w2,
                    'ContactPersonWhatsapp', w2,
                    'ContactPersonProject', w2,
                    'ContactPersonAddress', w2,
                    'Products', w4,
                    'PackagingCharges', w5,
                    'FreightCharges', w5,
                    'Advacne', w5,
                    'ChallanNo', w1,
                    'Date', w6,
                    'Status', w7,
                    'DueDate', w6,
                    'ClosingDate', w6,
                    'Type', w7,
                    'SourceId', w0,
                    'StageId', w0,
                    'BranchId', w0,
                    'InvoiceMade', w8,
                    'OtherAddress', w8,
                    'DispatchDetails', w3,
                    'ShippingAddress', w3,
                    'AdditionalInfo', w3,
                    'Attachments', w9,
                    'OwnerId', w0,
                    'AssignedId', w0,
                    'NoteList', w10
                ]);
            }
        }
    }
}
