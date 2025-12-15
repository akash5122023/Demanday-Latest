namespace AdvanceCRM.Services {
    export interface CMSForm {
        ProjectId: Serenity.LookupEditor;
        Date: Serenity.DateTimeEditor;
        Cmsn: Serenity.StringEditor;
        SerialNo: Serenity.StringEditor;
        CMSNo: Serenity.IntegerEditor;
        ComplaintId: Serenity.LookupEditor;
        Instructions: Serenity.TextAreaEditor;
        ContactsId: Serenity.LookupEditor;
        ContactsName: Serenity.StringEditor;
        ContactsPhone: Serenity.StringEditor;
        ContactsAddress: Serenity.TextAreaEditor;
        ContactsWhatsapp: Serenity.StringEditor;
        DealerId: Serenity.LookupEditor;
        DealerPhone: Serenity.StringEditor;
        DealerEmail: Serenity.StringEditor;
        EmployeeId: Serenity.LookupEditor;
        EmployeePhone: Serenity.StringEditor;
        EmployeeEmail: Serenity.StringEditor;
        ProductsId: Serenity.LookupEditor;
        Qty: Serenity.IntegerEditor;
        PurchaseDate: Serenity.DateEditor;
        InvoiceNo: Serenity.StringEditor;
        Priority: Serenity.EnumEditor;
        Status: Serenity.EnumEditor;
        Category: Serenity.EnumEditor;
        StageId: Serenity.LookupEditor;
        BranchId: Serenity.LookupEditor;
        Amount: Serenity.DecimalEditor;
        ExpectedCompletion: Serenity.DateEditor;
        Representative: Serenity.StringEditor;
        CompletionDate: Serenity.DateEditor;
        Feedback: Serenity.TextAreaEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Image: Serenity.MultipleImageUploadEditor;
        Products: CMSProductsEditor;
        InvestigationBy: Administration.UserEditor;
        Observation: Serenity.TextAreaEditor;
        ActionBy: Administration.UserEditor;
        Action: Serenity.TextAreaEditor;
        SupervisedBy: Administration.UserEditor;
        Comments: Serenity.TextAreaEditor;
        AssignedBy: Administration.UserEditor;
        AssignedTo: Administration.UserEditor;
        NoteList: Common.NotesEditor;
        Timeline: Common.TimelineEditor;
    }

    export class CMSForm extends Serenity.PrefixedContext {
        static formKey = 'Services.CMS';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CMSForm.init)  {
                CMSForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.DateTimeEditor;
                var w2 = s.StringEditor;
                var w3 = s.IntegerEditor;
                var w4 = s.TextAreaEditor;
                var w5 = s.DateEditor;
                var w6 = s.EnumEditor;
                var w7 = s.DecimalEditor;
                var w8 = s.MultipleImageUploadEditor;
                var w9 = CMSProductsEditor;
                var w10 = Administration.UserEditor;
                var w11 = Common.NotesEditor;
                var w12 = Common.TimelineEditor;

                Q.initFormType(CMSForm, [
                    'ProjectId', w0,
                    'Date', w1,
                    'Cmsn', w2,
                    'SerialNo', w2,
                    'CMSNo', w3,
                    'ComplaintId', w0,
                    'Instructions', w4,
                    'ContactsId', w0,
                    'ContactsName', w2,
                    'ContactsPhone', w2,
                    'ContactsAddress', w4,
                    'ContactsWhatsapp', w2,
                    'DealerId', w0,
                    'DealerPhone', w2,
                    'DealerEmail', w2,
                    'EmployeeId', w0,
                    'EmployeePhone', w2,
                    'EmployeeEmail', w2,
                    'ProductsId', w0,
                    'Qty', w3,
                    'PurchaseDate', w5,
                    'InvoiceNo', w2,
                    'Priority', w6,
                    'Status', w6,
                    'Category', w6,
                    'StageId', w0,
                    'BranchId', w0,
                    'Amount', w7,
                    'ExpectedCompletion', w5,
                    'Representative', w2,
                    'CompletionDate', w5,
                    'Feedback', w4,
                    'AdditionalInfo', w4,
                    'Image', w8,
                    'Products', w9,
                    'InvestigationBy', w10,
                    'Observation', w4,
                    'ActionBy', w10,
                    'Action', w4,
                    'SupervisedBy', w10,
                    'Comments', w4,
                    'AssignedBy', w10,
                    'AssignedTo', w10,
                    'NoteList', w11,
                    'Timeline', w12
                ]);
            }
        }
    }
}
