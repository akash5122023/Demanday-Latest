namespace AdvanceCRM.ThirdParty {
    export interface TradeIndiaDetailsRow {
        Id?: number;
        RfiId?: number;
        Source?: string;
        ProductSource?: string;
        GeneratedDateTime?: string;
        InquiryType?: string;
        Subject?: string;
        ProductName?: string;
        Quantity?: string;
        OrderValueMin?: number;
        Message?: string;
        SenderCo?: string;
        SenderName?: string;
        SenderMobile?: string;
        SenderEmail?: string;
        SenderAddress?: string;
        SenderCity?: string;
        SenderState?: string;
        SenderCountry?: string;
        MonthSlot?: string;
        LandlineNumber?: string;
        PrefSuppLocation?: string;
        Feedback?: string;
        IsMoved?: boolean;
    }

    export namespace TradeIndiaDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Source';
        export const localTextPrefix = 'ThirdParty.TradeIndiaDetails';
        export const deletePermission = 'TradeIndia:Inbox';
        export const insertPermission = 'TradeIndia:Inbox';
        export const readPermission = 'TradeIndia:Inbox';
        export const updatePermission = 'TradeIndia:Inbox';

        export declare const enum Fields {
            Id = "Id",
            RfiId = "RfiId",
            Source = "Source",
            ProductSource = "ProductSource",
            GeneratedDateTime = "GeneratedDateTime",
            InquiryType = "InquiryType",
            Subject = "Subject",
            ProductName = "ProductName",
            Quantity = "Quantity",
            OrderValueMin = "OrderValueMin",
            Message = "Message",
            SenderCo = "SenderCo",
            SenderName = "SenderName",
            SenderMobile = "SenderMobile",
            SenderEmail = "SenderEmail",
            SenderAddress = "SenderAddress",
            SenderCity = "SenderCity",
            SenderState = "SenderState",
            SenderCountry = "SenderCountry",
            MonthSlot = "MonthSlot",
            LandlineNumber = "LandlineNumber",
            PrefSuppLocation = "PrefSuppLocation",
            Feedback = "Feedback",
            IsMoved = "IsMoved"
        }
    }
}
