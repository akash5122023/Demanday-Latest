namespace AdvanceCRM.Services {
    export interface AMCForm {
        AMCNo: Serenity.IntegerEditor;
        ContactsId: Serenity.LookupEditor;
        ContactsName: Serenity.StringEditor;
        ContactsPhone: Serenity.StringEditor;
        ContactsWhatsapp: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        Status: Serenity.EnumEditor;
        StartDate: Serenity.DateEditor;
        EndDate: Serenity.DateEditor;
        DueDate: Serenity.DateEditor;
        Lines: Serenity.IntegerEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        Attachment: Serenity.MultipleImageUploadEditor;
        Products: AMCProductsEditor;
        TermsList: Serenity.LookupEditor;
        OwnerId: Administration.UserEditor;
        AssignedId: Administration.UserEditor;
        NoteList: Common.NotesEditor;
        Timeline: Common.TimelineEditor;
    }

    export class AMCForm extends Serenity.PrefixedContext {
        static formKey = 'Services.AMC';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AMCForm.init)  {
                AMCForm.init = true;

                var s = Serenity;
                var w0 = s.IntegerEditor;
                var w1 = s.LookupEditor;
                var w2 = s.StringEditor;
                var w3 = s.DateEditor;
                var w4 = s.EnumEditor;
                var w5 = s.TextAreaEditor;
                var w6 = s.MultipleImageUploadEditor;
                var w7 = AMCProductsEditor;
                var w8 = Administration.UserEditor;
                var w9 = Common.NotesEditor;
                var w10 = Common.TimelineEditor;

                Q.initFormType(AMCForm, [
                    'AMCNo', w0,
                    'ContactsId', w1,
                    'ContactsName', w2,
                    'ContactsPhone', w2,
                    'ContactsWhatsapp', w2,
                    'Date', w3,
                    'Status', w4,
                    'StartDate', w3,
                    'EndDate', w3,
                    'DueDate', w3,
                    'Lines', w0,
                    'AdditionalInfo', w5,
                    'Attachment', w6,
                    'Products', w7,
                    'TermsList', w1,
                    'OwnerId', w8,
                    'AssignedId', w8,
                    'NoteList', w9,
                    'Timeline', w10
                ]);
            }
        }
    }
}
