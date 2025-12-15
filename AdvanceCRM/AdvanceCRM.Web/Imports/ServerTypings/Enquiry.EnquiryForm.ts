namespace AdvanceCRM.Enquiry {
    export interface EnquiryForm {
        ContactsId: Serenity.LookupEditor;
        ContactsContactType: Serenity.IntegerEditor;
        ContactsName: Serenity.StringEditor;
        ContactsEmail: Serenity.StringEditor;
        ContactsPhone: Serenity.StringEditor;
        ContactsWhatsapp: Serenity.StringEditor;
        ContactsAddress: Serenity.TextAreaEditor;
        ContactPersonId: Serenity.LookupEditor;
        ContactPersonName: Serenity.StringEditor;
        ContactPersonPhone: Serenity.StringEditor;
        ContactPersonWhatsapp: Serenity.StringEditor;
        ContactPersonProject: Serenity.StringEditor;
        ContactPersonAddress: Serenity.TextAreaEditor;
        ProjectId: Serenity.LookupEditor;
        DealerId: Serenity.LookupEditor;
        Products: EnquiryProductsEditor;
        Total: Serenity.DecimalEditor;
        EnquiryNo: Serenity.IntegerEditor;
        EnquiryN: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        ExpectedClosingDate: Serenity.DateEditor;
        Status: Serenity.EnumEditor;
        ClosingDate: Serenity.DateEditor;
        ClosingType: Serenity.EnumEditor;
        LostReason: Serenity.StringEditor;
        SourceId: Serenity.LookupEditor;
        StageId: Serenity.LookupEditor;
        Type: Serenity.EnumEditor;
        BranchId: Serenity.LookupEditor;
        WinPercentage: Serenity.EnumEditor;
        ReferenceName: Serenity.StringEditor;
        ReferencePhone: Serenity.MaskedEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        AdditionalInfo2: Serenity.TextAreaEditor;
        EnquiryAddinfoList: Serenity.LookupEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
        OwnerId: Administration.UserEditor;
        AssignedId: Administration.UserEditor;
        MultiAssignList: Serenity.LookupEditor;
        CompanyId: Serenity.LookupEditor;
        NoteList: Common.NotesEditor;
        Timeline: Common.TimelineEditor;
    }

    export class EnquiryForm extends Serenity.PrefixedContext {
        static formKey = 'Enquiry.Enquiry';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EnquiryForm.init)  {
                EnquiryForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.StringEditor;
                var w3 = s.TextAreaEditor;
                var w4 = EnquiryProductsEditor;
                var w5 = s.DecimalEditor;
                var w6 = s.DateEditor;
                var w7 = s.EnumEditor;
                var w8 = s.MaskedEditor;
                var w9 = s.MultipleImageUploadEditor;
                var w10 = Administration.UserEditor;
                var w11 = Common.NotesEditor;
                var w12 = Common.TimelineEditor;

                Q.initFormType(EnquiryForm, [
                    'ContactsId', w0,
                    'ContactsContactType', w1,
                    'ContactsName', w2,
                    'ContactsEmail', w2,
                    'ContactsPhone', w2,
                    'ContactsWhatsapp', w2,
                    'ContactsAddress', w3,
                    'ContactPersonId', w0,
                    'ContactPersonName', w2,
                    'ContactPersonPhone', w2,
                    'ContactPersonWhatsapp', w2,
                    'ContactPersonProject', w2,
                    'ContactPersonAddress', w3,
                    'ProjectId', w0,
                    'DealerId', w0,
                    'Products', w4,
                    'Total', w5,
                    'EnquiryNo', w1,
                    'EnquiryN', w2,
                    'Date', w6,
                    'ExpectedClosingDate', w6,
                    'Status', w7,
                    'ClosingDate', w6,
                    'ClosingType', w7,
                    'LostReason', w2,
                    'SourceId', w0,
                    'StageId', w0,
                    'Type', w7,
                    'BranchId', w0,
                    'WinPercentage', w7,
                    'ReferenceName', w2,
                    'ReferencePhone', w8,
                    'AdditionalInfo', w3,
                    'AdditionalInfo2', w3,
                    'EnquiryAddinfoList', w0,
                    'Attachments', w9,
                    'OwnerId', w10,
                    'AssignedId', w10,
                    'MultiAssignList', w0,
                    'CompanyId', w0,
                    'NoteList', w11,
                    'Timeline', w12
                ]);
            }
        }
    }
}
