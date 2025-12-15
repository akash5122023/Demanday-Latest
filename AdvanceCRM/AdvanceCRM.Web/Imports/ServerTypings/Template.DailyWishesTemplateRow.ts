namespace AdvanceCRM.Template {
    export interface DailyWishesTemplateRow {
        Id?: number;
        From?: string;
        Subject?: string;
        BirthdaySMS?: string;
        MarriageSMS?: string;
        DofAnniversarySMS?: string;
        BirthdayEmail?: string;
        MarriageEmail?: string;
        DofAnniversaryEmail?: string;
        CompanyId?: number;
        BirthTempId?: string;
        MarriageTempId?: string;
        DofTempId?: string;
        WaBirTemplate?: string;
        WaBirTemplateId?: string;
        WaMarTemplate?: string;
        WaMarTemplateId?: string;
        WaAnnTemplate?: string;
        WaAnnTemplateId?: string;
    }

    export namespace DailyWishesTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'From';
        export const localTextPrefix = 'Template.DailyWishesTemplate';
        export const lookupKey = 'Template.DailyWishesTemplateRow';

        export function getLookup(): Q.Lookup<DailyWishesTemplateRow> {
            return Q.getLookup<DailyWishesTemplateRow>('Template.DailyWishesTemplateRow');
        }
        export const deletePermission = 'Template:DailyWishes';
        export const insertPermission = 'Template:DailyWishes';
        export const readPermission = 'Template:DailyWishes';
        export const updatePermission = 'Template:DailyWishes';

        export declare const enum Fields {
            Id = "Id",
            From = "From",
            Subject = "Subject",
            BirthdaySMS = "BirthdaySMS",
            MarriageSMS = "MarriageSMS",
            DofAnniversarySMS = "DofAnniversarySMS",
            BirthdayEmail = "BirthdayEmail",
            MarriageEmail = "MarriageEmail",
            DofAnniversaryEmail = "DofAnniversaryEmail",
            CompanyId = "CompanyId",
            BirthTempId = "BirthTempId",
            MarriageTempId = "MarriageTempId",
            DofTempId = "DofTempId",
            WaBirTemplate = "WaBirTemplate",
            WaBirTemplateId = "WaBirTemplateId",
            WaMarTemplate = "WaMarTemplate",
            WaMarTemplateId = "WaMarTemplateId",
            WaAnnTemplate = "WaAnnTemplate",
            WaAnnTemplateId = "WaAnnTemplateId"
        }
    }
}
