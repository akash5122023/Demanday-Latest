namespace AdvanceCRM.Template {
    export interface AppointmentTemplateForm {
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        Attachment: LargeFileUploadEditor;
        SMSTemplate: Serenity.TextAreaEditor;
        SmsTempId: Serenity.StringEditor;
        MondaySMS: Serenity.TextAreaEditor;
        MonTempId: Serenity.StringEditor;
        TuesdaySMS: Serenity.TextAreaEditor;
        TueTempId: Serenity.StringEditor;
        WednesdaySMS: Serenity.TextAreaEditor;
        WedTempId: Serenity.StringEditor;
        ThursdaySMS: Serenity.TextAreaEditor;
        ThurTempId: Serenity.StringEditor;
        FridaySMS: Serenity.TextAreaEditor;
        FriTempId: Serenity.StringEditor;
        SaturdaySMS: Serenity.TextAreaEditor;
        SatTempId: Serenity.StringEditor;
        SundaySMS: Serenity.TextAreaEditor;
        SunTempId: Serenity.StringEditor;
        WaTemplate: Serenity.TextAreaEditor;
        WaTemplateId: Serenity.StringEditor;
        WaMonTemplate: Serenity.TextAreaEditor;
        WaMonTemplateId: Serenity.StringEditor;
        WaTueTemplate: Serenity.TextAreaEditor;
        WaTueTemplateId: Serenity.StringEditor;
        WaWedTemplate: Serenity.TextAreaEditor;
        WaWebTemplateId: Serenity.StringEditor;
        WaThurTemplate: Serenity.TextAreaEditor;
        WaThurTemplateId: Serenity.StringEditor;
        WaFriTemplate: Serenity.TextAreaEditor;
        WaFriTemplateId: Serenity.StringEditor;
        WaSatTemplate: Serenity.TextAreaEditor;
        WaSatTemplateId: Serenity.StringEditor;
        WaSunTemplate: Serenity.TextAreaEditor;
        WaSunTemplateId: Serenity.StringEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        SSL: Serenity.BooleanEditor;
        EmailId: Serenity.EmailEditor;
        EmailPassword: Serenity.PasswordEditor;
    }

    export class AppointmentTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.AppointmentTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AppointmentTemplateForm.init)  {
                AppointmentTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlContentEditor;
                var w2 = LargeFileUploadEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.IntegerEditor;
                var w5 = s.BooleanEditor;
                var w6 = s.EmailEditor;
                var w7 = s.PasswordEditor;

                Q.initFormType(AppointmentTemplateForm, [
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w1,
                    'Attachment', w2,
                    'SMSTemplate', w3,
                    'SmsTempId', w0,
                    'MondaySMS', w3,
                    'MonTempId', w0,
                    'TuesdaySMS', w3,
                    'TueTempId', w0,
                    'WednesdaySMS', w3,
                    'WedTempId', w0,
                    'ThursdaySMS', w3,
                    'ThurTempId', w0,
                    'FridaySMS', w3,
                    'FriTempId', w0,
                    'SaturdaySMS', w3,
                    'SatTempId', w0,
                    'SundaySMS', w3,
                    'SunTempId', w0,
                    'WaTemplate', w3,
                    'WaTemplateId', w0,
                    'WaMonTemplate', w3,
                    'WaMonTemplateId', w0,
                    'WaTueTemplate', w3,
                    'WaTueTemplateId', w0,
                    'WaWedTemplate', w3,
                    'WaWebTemplateId', w0,
                    'WaThurTemplate', w3,
                    'WaThurTemplateId', w0,
                    'WaFriTemplate', w3,
                    'WaFriTemplateId', w0,
                    'WaSatTemplate', w3,
                    'WaSatTemplateId', w0,
                    'WaSunTemplate', w3,
                    'WaSunTemplateId', w0,
                    'Host', w0,
                    'Port', w4,
                    'SSL', w5,
                    'EmailId', w6,
                    'EmailPassword', w7
                ]);
            }
        }
    }
}
