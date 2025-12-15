namespace AdvanceCRM.ThirdParty {
    export interface SIndiaMartDetailsRow {
        Id?: number;
        Rn?: number;
        QueryId?: string;
        QueryType?: string;
        SenderName?: string;
        SenderEmail?: string;
        Subject?: string;
        DateRe?: string;
        DateR?: string;
        DateTimeRe?: string;
        GlUserCompanyName?: string;
        ReadStatus?: number;
        SenderGlUserId?: string;
        Mob?: string;
        CountryFlag?: string;
        QueryModId?: string;
        Feedback?: string;
        LogTime?: string;
        QueryModRefId?: string;
        DirQueryModrefType?: number;
        OrgSenderGlUserId?: string;
        EnqMessage?: string;
        EnqAddress?: string;
        EnqCallDuration?: string;
        EnqReceiverMob?: string;
        EnqCity?: string;
        EnqState?: string;
        ProductName?: string;
        CountryIso?: string;
        EmailAlt?: string;
        MobileAlt?: string;
        Phone?: string;
        PhoneAlt?: string;
        ImmemberSince?: string;
        TotalCnt?: number;
        IsMoved?: boolean;
        Source?: Masters.IndiaMARTSource;
    }

    export namespace SIndiaMartDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'QueryId';
        export const localTextPrefix = 'ThirdParty.SIndiaMartDetails';
        export const deletePermission = 'IndiaMART:Inbox';
        export const insertPermission = 'IndiaMART:Inbox';
        export const readPermission = 'IndiaMART:Inbox';
        export const updatePermission = 'IndiaMART:Inbox';

        export declare const enum Fields {
            Id = "Id",
            Rn = "Rn",
            QueryId = "QueryId",
            QueryType = "QueryType",
            SenderName = "SenderName",
            SenderEmail = "SenderEmail",
            Subject = "Subject",
            DateRe = "DateRe",
            DateR = "DateR",
            DateTimeRe = "DateTimeRe",
            GlUserCompanyName = "GlUserCompanyName",
            ReadStatus = "ReadStatus",
            SenderGlUserId = "SenderGlUserId",
            Mob = "Mob",
            CountryFlag = "CountryFlag",
            QueryModId = "QueryModId",
            Feedback = "Feedback",
            LogTime = "LogTime",
            QueryModRefId = "QueryModRefId",
            DirQueryModrefType = "DirQueryModrefType",
            OrgSenderGlUserId = "OrgSenderGlUserId",
            EnqMessage = "EnqMessage",
            EnqAddress = "EnqAddress",
            EnqCallDuration = "EnqCallDuration",
            EnqReceiverMob = "EnqReceiverMob",
            EnqCity = "EnqCity",
            EnqState = "EnqState",
            ProductName = "ProductName",
            CountryIso = "CountryIso",
            EmailAlt = "EmailAlt",
            MobileAlt = "MobileAlt",
            Phone = "Phone",
            PhoneAlt = "PhoneAlt",
            ImmemberSince = "ImmemberSince",
            TotalCnt = "TotalCnt",
            IsMoved = "IsMoved",
            Source = "Source"
        }
    }
}
