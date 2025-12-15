namespace AdvanceCRM.Sales {
    export interface SalesFollowupsRow {
        Id?: number;
        FollowupNote?: string;
        Details?: string;
        FollowupDate?: string;
        Status?: Masters.StatusMaster;
        SalesId?: number;
        RepresentativeId?: number;
        ClosingDate?: string;
        SalesContactsId?: number;
        SalesDate?: string;
        SalesStatus?: number;
        SalesType?: number;
        SalesAdditionalInfo?: string;
        SalesSourceId?: number;
        SalesStageId?: number;
        SalesBranchId?: number;
        SalesOwnerId?: number;
        SalesAssignedId?: number;
        SalesOtherAddress?: boolean;
        SalesShippingAddress?: string;
        SalesPackagingCharges?: number;
        SalesFreightCharges?: number;
        SalesAdvacne?: number;
        SalesDueDate?: string;
        SalesDispatchDetails?: string;
        SalesRoundup?: number;
        SalesContactPersonId?: number;
        SalesLines?: number;
        SalesInvoiceNo?: number;
        SalesReverseCharge?: boolean;
        SalesEcomType?: number;
        SalesInvoiceType?: number;
        SalesTrasportationId?: number;
        SalesQuotationNo?: number;
        SalesQuotationDate?: string;
        SalesConversion?: number;
        SalesClosingDate?: string;
        SalesAttachments?: string;
        SalesCurrencyConversion?: boolean;
        SalesFromCurrency?: number;
        SalesToCurrency?: number;
        SalesTaxable?: boolean;
        RepresentativeUsername?: string;
        RepresentativeDisplayName?: string;
        RepresentativeEmail?: string;
        RepresentativeSource?: string;
        RepresentativePasswordHash?: string;
        RepresentativePasswordSalt?: string;
        RepresentativeLastDirectoryUpdate?: string;
        RepresentativeUserImage?: string;
        RepresentativeInsertDate?: string;
        RepresentativeInsertUserId?: number;
        RepresentativeUpdateDate?: string;
        RepresentativeUpdateUserId?: number;
        RepresentativeIsActive?: number;
        RepresentativeUpperLevel?: number;
        RepresentativeUpperLevel2?: number;
        RepresentativeUpperLevel3?: number;
        RepresentativeUpperLevel4?: number;
        RepresentativeUpperLevel5?: number;
        RepresentativeHost?: string;
        RepresentativePort?: number;
        RepresentativeSsl?: boolean;
        RepresentativeEmailId?: string;
        RepresentativeEmailPassword?: string;
        RepresentativePhone?: string;
        RepresentativeMcsmtpServer?: string;
        RepresentativeMcsmtpPort?: number;
        RepresentativeMcimapServer?: string;
        RepresentativeMcimapPort?: number;
        RepresentativeMcUsername?: string;
        RepresentativeMcPassword?: string;
        RepresentativeStartTime?: string;
        RepresentativeEndTime?: string;
        RepresentativeBranchId?: number;
        RepresentativeUid?: string;
        RepresentativeNonOperational?: boolean;
        ContactName?: string;
        ContactPhone?: string;
        ContactEmail?: string;
        ContactAddress?: string;
        ContactPersonName?: string;
        ContactPersonPhone?: string;
        ContactPersonEmail?: string;
        NoteList?: Common.NoteRow[];
    }

    export namespace SalesFollowupsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'FollowupNote';
        export const localTextPrefix = 'Sales.SalesFollowups';
        export const deletePermission = 'Sales:Followups';
        export const insertPermission = 'Sales:Followups';
        export const readPermission = '?';
        export const updatePermission = 'Sales:Followups';

        export declare const enum Fields {
            Id = "Id",
            FollowupNote = "FollowupNote",
            Details = "Details",
            FollowupDate = "FollowupDate",
            Status = "Status",
            SalesId = "SalesId",
            RepresentativeId = "RepresentativeId",
            ClosingDate = "ClosingDate",
            SalesContactsId = "SalesContactsId",
            SalesDate = "SalesDate",
            SalesStatus = "SalesStatus",
            SalesType = "SalesType",
            SalesAdditionalInfo = "SalesAdditionalInfo",
            SalesSourceId = "SalesSourceId",
            SalesStageId = "SalesStageId",
            SalesBranchId = "SalesBranchId",
            SalesOwnerId = "SalesOwnerId",
            SalesAssignedId = "SalesAssignedId",
            SalesOtherAddress = "SalesOtherAddress",
            SalesShippingAddress = "SalesShippingAddress",
            SalesPackagingCharges = "SalesPackagingCharges",
            SalesFreightCharges = "SalesFreightCharges",
            SalesAdvacne = "SalesAdvacne",
            SalesDueDate = "SalesDueDate",
            SalesDispatchDetails = "SalesDispatchDetails",
            SalesRoundup = "SalesRoundup",
            SalesContactPersonId = "SalesContactPersonId",
            SalesLines = "SalesLines",
            SalesInvoiceNo = "SalesInvoiceNo",
            SalesReverseCharge = "SalesReverseCharge",
            SalesEcomType = "SalesEcomType",
            SalesInvoiceType = "SalesInvoiceType",
            SalesTrasportationId = "SalesTrasportationId",
            SalesQuotationNo = "SalesQuotationNo",
            SalesQuotationDate = "SalesQuotationDate",
            SalesConversion = "SalesConversion",
            SalesClosingDate = "SalesClosingDate",
            SalesAttachments = "SalesAttachments",
            SalesCurrencyConversion = "SalesCurrencyConversion",
            SalesFromCurrency = "SalesFromCurrency",
            SalesToCurrency = "SalesToCurrency",
            SalesTaxable = "SalesTaxable",
            RepresentativeUsername = "RepresentativeUsername",
            RepresentativeDisplayName = "RepresentativeDisplayName",
            RepresentativeEmail = "RepresentativeEmail",
            RepresentativeSource = "RepresentativeSource",
            RepresentativePasswordHash = "RepresentativePasswordHash",
            RepresentativePasswordSalt = "RepresentativePasswordSalt",
            RepresentativeLastDirectoryUpdate = "RepresentativeLastDirectoryUpdate",
            RepresentativeUserImage = "RepresentativeUserImage",
            RepresentativeInsertDate = "RepresentativeInsertDate",
            RepresentativeInsertUserId = "RepresentativeInsertUserId",
            RepresentativeUpdateDate = "RepresentativeUpdateDate",
            RepresentativeUpdateUserId = "RepresentativeUpdateUserId",
            RepresentativeIsActive = "RepresentativeIsActive",
            RepresentativeUpperLevel = "RepresentativeUpperLevel",
            RepresentativeUpperLevel2 = "RepresentativeUpperLevel2",
            RepresentativeUpperLevel3 = "RepresentativeUpperLevel3",
            RepresentativeUpperLevel4 = "RepresentativeUpperLevel4",
            RepresentativeUpperLevel5 = "RepresentativeUpperLevel5",
            RepresentativeHost = "RepresentativeHost",
            RepresentativePort = "RepresentativePort",
            RepresentativeSsl = "RepresentativeSsl",
            RepresentativeEmailId = "RepresentativeEmailId",
            RepresentativeEmailPassword = "RepresentativeEmailPassword",
            RepresentativePhone = "RepresentativePhone",
            RepresentativeMcsmtpServer = "RepresentativeMcsmtpServer",
            RepresentativeMcsmtpPort = "RepresentativeMcsmtpPort",
            RepresentativeMcimapServer = "RepresentativeMcimapServer",
            RepresentativeMcimapPort = "RepresentativeMcimapPort",
            RepresentativeMcUsername = "RepresentativeMcUsername",
            RepresentativeMcPassword = "RepresentativeMcPassword",
            RepresentativeStartTime = "RepresentativeStartTime",
            RepresentativeEndTime = "RepresentativeEndTime",
            RepresentativeBranchId = "RepresentativeBranchId",
            RepresentativeUid = "RepresentativeUid",
            RepresentativeNonOperational = "RepresentativeNonOperational",
            ContactName = "ContactName",
            ContactPhone = "ContactPhone",
            ContactEmail = "ContactEmail",
            ContactAddress = "ContactAddress",
            ContactPersonName = "ContactPersonName",
            ContactPersonPhone = "ContactPersonPhone",
            ContactPersonEmail = "ContactPersonEmail",
            NoteList = "NoteList"
        }
    }
}
