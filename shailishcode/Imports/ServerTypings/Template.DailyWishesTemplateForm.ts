namespace AdvanceCRM.Template {
    export interface DailyWishesTemplateForm {
        From: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        BirthdayEmail: Serenity.HtmlContentEditor;
        MarriageEmail: Serenity.HtmlContentEditor;
        DofAnniversaryEmail: Serenity.HtmlContentEditor;
        BirthdaySMS: Serenity.TextAreaEditor;
        BirthTempId: Serenity.StringEditor;
        MarriageSMS: Serenity.TextAreaEditor;
        MarriageTempId: Serenity.StringEditor;
        DofAnniversarySMS: Serenity.TextAreaEditor;
        DofTempId: Serenity.StringEditor;
        WaBirTemplate: Serenity.TextAreaEditor;
        WaBirTemplateId: Serenity.StringEditor;
        WaMarTemplate: Serenity.TextAreaEditor;
        WaMarTemplateId: Serenity.StringEditor;
        WaAnnTemplate: Serenity.TextAreaEditor;
        WaAnnTemplateId: Serenity.StringEditor;
    }

    export class DailyWishesTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.DailyWishesTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DailyWishesTemplateForm.init)  {
                DailyWishesTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlContentEditor;
                var w2 = s.TextAreaEditor;

                Q.initFormType(DailyWishesTemplateForm, [
                    'From', w0,
                    'Subject', w0,
                    'BirthdayEmail', w1,
                    'MarriageEmail', w1,
                    'DofAnniversaryEmail', w1,
                    'BirthdaySMS', w2,
                    'BirthTempId', w0,
                    'MarriageSMS', w2,
                    'MarriageTempId', w0,
                    'DofAnniversarySMS', w2,
                    'DofTempId', w0,
                    'WaBirTemplate', w2,
                    'WaBirTemplateId', w0,
                    'WaMarTemplate', w2,
                    'WaMarTemplateId', w0,
                    'WaAnnTemplate', w2,
                    'WaAnnTemplateId', w0
                ]);
            }
        }
    }
}
