namespace AdvanceCRM.Template {
    export interface AppointmentTemplateRow {
        Id?: number;
        Sender?: string;
        Subject?: string;
        EmailTemplate?: string;
        Attachment?: string;
        SMSTemplate?: string;
        Host?: string;
        Port?: number;
        SSL?: boolean;
        EmailId?: string;
        EmailPassword?: string;
        MondaySMS?: string;
        TuesdaySMS?: string;
        WednesdaySMS?: string;
        ThursdaySMS?: string;
        FridaySMS?: string;
        SaturdaySMS?: string;
        SundaySMS?: string;
        CompanyId?: number;
        SmsTempId?: string;
        MonTempId?: string;
        TueTempId?: string;
        WedTempId?: string;
        ThurTempId?: string;
        FriTempId?: string;
        SatTempId?: string;
        SunTempId?: string;
        WaTemplate?: string;
        WaTemplateId?: string;
        WaMonTemplate?: string;
        WaMonTemplateId?: string;
        WaTueTemplate?: string;
        WaTueTemplateId?: string;
        WaWedTemplate?: string;
        WaWebTemplateId?: string;
        WaThurTemplate?: string;
        WaThurTemplateId?: string;
        WaFriTemplate?: string;
        WaFriTemplateId?: string;
        WaSatTemplate?: string;
        WaSatTemplateId?: string;
        WaSunTemplate?: string;
        WaSunTemplateId?: string;
    }

    export namespace AppointmentTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Sender';
        export const localTextPrefix = 'Template.AppointmentTemplate';
        export const lookupKey = 'Template.AppointmentTemplate';

        export function getLookup(): Q.Lookup<AppointmentTemplateRow> {
            return Q.getLookup<AppointmentTemplateRow>('Template.AppointmentTemplate');
        }
        export const deletePermission = 'Template:Appointment';
        export const insertPermission = 'Template:Appointment';
        export const readPermission = 'Template:Appointment';
        export const updatePermission = 'Template:Appointment';

        export declare const enum Fields {
            Id = "Id",
            Sender = "Sender",
            Subject = "Subject",
            EmailTemplate = "EmailTemplate",
            Attachment = "Attachment",
            SMSTemplate = "SMSTemplate",
            Host = "Host",
            Port = "Port",
            SSL = "SSL",
            EmailId = "EmailId",
            EmailPassword = "EmailPassword",
            MondaySMS = "MondaySMS",
            TuesdaySMS = "TuesdaySMS",
            WednesdaySMS = "WednesdaySMS",
            ThursdaySMS = "ThursdaySMS",
            FridaySMS = "FridaySMS",
            SaturdaySMS = "SaturdaySMS",
            SundaySMS = "SundaySMS",
            CompanyId = "CompanyId",
            SmsTempId = "SmsTempId",
            MonTempId = "MonTempId",
            TueTempId = "TueTempId",
            WedTempId = "WedTempId",
            ThurTempId = "ThurTempId",
            FriTempId = "FriTempId",
            SatTempId = "SatTempId",
            SunTempId = "SunTempId",
            WaTemplate = "WaTemplate",
            WaTemplateId = "WaTemplateId",
            WaMonTemplate = "WaMonTemplate",
            WaMonTemplateId = "WaMonTemplateId",
            WaTueTemplate = "WaTueTemplate",
            WaTueTemplateId = "WaTueTemplateId",
            WaWedTemplate = "WaWedTemplate",
            WaWebTemplateId = "WaWebTemplateId",
            WaThurTemplate = "WaThurTemplate",
            WaThurTemplateId = "WaThurTemplateId",
            WaFriTemplate = "WaFriTemplate",
            WaFriTemplateId = "WaFriTemplateId",
            WaSatTemplate = "WaSatTemplate",
            WaSatTemplateId = "WaSatTemplateId",
            WaSunTemplate = "WaSunTemplate",
            WaSunTemplateId = "WaSunTemplateId"
        }
    }
}
