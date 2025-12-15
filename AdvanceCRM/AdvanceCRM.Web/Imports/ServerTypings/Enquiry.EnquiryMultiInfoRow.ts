namespace AdvanceCRM.Enquiry {
    export interface EnquiryMultiInfoRow {
        Id?: number;
        AdditionalInfoId?: number;
        EnquiryId?: number;
        AdditionalInfo?: string;
        EnquiryContactsId?: number;
        EnquiryDate?: string;
        EnquiryStatus?: number;
        EnquiryType?: number;
        EnquiryAdditionalInfo?: string;
        EnquirySourceId?: number;
        EnquiryStageId?: number;
        EnquiryBranchId?: number;
        EnquiryOwnerId?: number;
        EnquiryAssignedId?: number;
        EnquiryReferenceName?: string;
        EnquiryReferencePhone?: string;
        EnquiryClosingType?: number;
        EnquiryLostReason?: string;
        EnquiryClosingDate?: string;
        EnquiryContactPersonId?: number;
        EnquiryAttachments?: string;
        EnquiryEnquiryN?: string;
        EnquiryEnquiryNo?: number;
        EnquiryCompanyId?: number;
        EnquiryAdditionalInfo2?: string;
    }

    export namespace EnquiryMultiInfoRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Enquiry.EnquiryMultiInfo';
        export const deletePermission = 'Enquiry:Delete';
        export const insertPermission = 'Enquiry:Insert';
        export const readPermission = 'Enquiry:Read';
        export const updatePermission = 'Enquiry:Update';

        export declare const enum Fields {
            Id = "Id",
            AdditionalInfoId = "AdditionalInfoId",
            EnquiryId = "EnquiryId",
            AdditionalInfo = "AdditionalInfo",
            EnquiryContactsId = "EnquiryContactsId",
            EnquiryDate = "EnquiryDate",
            EnquiryStatus = "EnquiryStatus",
            EnquiryType = "EnquiryType",
            EnquiryAdditionalInfo = "EnquiryAdditionalInfo",
            EnquirySourceId = "EnquirySourceId",
            EnquiryStageId = "EnquiryStageId",
            EnquiryBranchId = "EnquiryBranchId",
            EnquiryOwnerId = "EnquiryOwnerId",
            EnquiryAssignedId = "EnquiryAssignedId",
            EnquiryReferenceName = "EnquiryReferenceName",
            EnquiryReferencePhone = "EnquiryReferencePhone",
            EnquiryClosingType = "EnquiryClosingType",
            EnquiryLostReason = "EnquiryLostReason",
            EnquiryClosingDate = "EnquiryClosingDate",
            EnquiryContactPersonId = "EnquiryContactPersonId",
            EnquiryAttachments = "EnquiryAttachments",
            EnquiryEnquiryN = "EnquiryEnquiryN",
            EnquiryEnquiryNo = "EnquiryEnquiryNo",
            EnquiryCompanyId = "EnquiryCompanyId",
            EnquiryAdditionalInfo2 = "EnquiryAdditionalInfo2"
        }
    }
}
