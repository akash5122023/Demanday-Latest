namespace AdvanceCRM.Template {
    export interface OtherTemplatesForm {
        TicketSmsTemplate: Serenity.TextAreaEditor;
        TicketSmsTemplateId: Serenity.StringEditor;
        TicketWaTemplate: Serenity.TextAreaEditor;
        TicketWaTemplateId: Serenity.StringEditor;
        TaskSmsTemplate: Serenity.TextAreaEditor;
        TaskSmsTemplateId: Serenity.StringEditor;
        TaskWaTemplate: Serenity.TextAreaEditor;
        TaskWaTemplateId: Serenity.StringEditor;
        FeedbackSmsTemplate: Serenity.TextAreaEditor;
        FeedbackSmsTemplateId: Serenity.StringEditor;
        FeedbackWaTemplate: Serenity.TextAreaEditor;
        FeedbackSwaTemplateId: Serenity.StringEditor;
        OtpsmsTemplate: Serenity.TextAreaEditor;
        OtpsmsTemplateId: Serenity.StringEditor;
        OtpwaTemplate: Serenity.TextAreaEditor;
        OtpwaTemplateId: Serenity.StringEditor;
    }

    export class OtherTemplatesForm extends Serenity.PrefixedContext {
        static formKey = 'Template.OtherTemplates';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!OtherTemplatesForm.init)  {
                OtherTemplatesForm.init = true;

                var s = Serenity;
                var w0 = s.TextAreaEditor;
                var w1 = s.StringEditor;

                Q.initFormType(OtherTemplatesForm, [
                    'TicketSmsTemplate', w0,
                    'TicketSmsTemplateId', w1,
                    'TicketWaTemplate', w0,
                    'TicketWaTemplateId', w1,
                    'TaskSmsTemplate', w0,
                    'TaskSmsTemplateId', w1,
                    'TaskWaTemplate', w0,
                    'TaskWaTemplateId', w1,
                    'FeedbackSmsTemplate', w0,
                    'FeedbackSmsTemplateId', w1,
                    'FeedbackWaTemplate', w0,
                    'FeedbackSwaTemplateId', w1,
                    'OtpsmsTemplate', w0,
                    'OtpsmsTemplateId', w1,
                    'OtpwaTemplate', w0,
                    'OtpwaTemplateId', w1
                ]);
            }
        }
    }
}
