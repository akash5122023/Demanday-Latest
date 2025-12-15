namespace AdvanceCRM.Quotation {
    export interface QuotationMultiInfoRow {
        Id?: number;
        AdditionalInfoId?: number;
        QuotationId?: number;
        AdditionalInfo?: string;
        QuotationContactsId?: number;
        QuotationDate?: string;
        QuotationStatus?: number;
        QuotationType?: number;
        QuotationAdditionalInfo?: string;
        QuotationSourceId?: number;
        QuotationStageId?: number;
        QuotationBranchId?: number;
        QuotationOwnerId?: number;
        QuotationAssignedId?: number;
        QuotationReferenceName?: string;
        QuotationReferencePhone?: string;
        QuotationClosingType?: number;
        QuotationLostReason?: string;
        QuotationSubject?: string;
        QuotationReference?: string;
        QuotationAttachment?: string;
        QuotationLines?: number;
        QuotationContactPersonId?: number;
        QuotationClosingDate?: string;
        QuotationEnquiryNo?: number;
        QuotationEnquiryDate?: string;
        QuotationConversion?: number;
        QuotationCurrencyConversion?: boolean;
        QuotationFromCurrency?: number;
        QuotationToCurrency?: number;
        QuotationTaxable?: boolean;
        QuotationQuotationNo?: number;
        QuotationRoundup?: number;
        QuotationMessageId?: number;
        QuotationQuotationN?: string;
        QuotationCompanyId?: number;
        QuotationEnquiryN?: string;
        QuotationAdditionalInfo2?: string;
    }

    export namespace QuotationMultiInfoRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Quotation.QuotationMultiInfo';
        export const deletePermission = 'Quotation:Delete';
        export const insertPermission = 'Quotation:Insert';
        export const readPermission = 'Quotation:Read';
        export const updatePermission = 'Quotation:Update';

        export declare const enum Fields {
            Id = "Id",
            AdditionalInfoId = "AdditionalInfoId",
            QuotationId = "QuotationId",
            AdditionalInfo = "AdditionalInfo",
            QuotationContactsId = "QuotationContactsId",
            QuotationDate = "QuotationDate",
            QuotationStatus = "QuotationStatus",
            QuotationType = "QuotationType",
            QuotationAdditionalInfo = "QuotationAdditionalInfo",
            QuotationSourceId = "QuotationSourceId",
            QuotationStageId = "QuotationStageId",
            QuotationBranchId = "QuotationBranchId",
            QuotationOwnerId = "QuotationOwnerId",
            QuotationAssignedId = "QuotationAssignedId",
            QuotationReferenceName = "QuotationReferenceName",
            QuotationReferencePhone = "QuotationReferencePhone",
            QuotationClosingType = "QuotationClosingType",
            QuotationLostReason = "QuotationLostReason",
            QuotationSubject = "QuotationSubject",
            QuotationReference = "QuotationReference",
            QuotationAttachment = "QuotationAttachment",
            QuotationLines = "QuotationLines",
            QuotationContactPersonId = "QuotationContactPersonId",
            QuotationClosingDate = "QuotationClosingDate",
            QuotationEnquiryNo = "QuotationEnquiryNo",
            QuotationEnquiryDate = "QuotationEnquiryDate",
            QuotationConversion = "QuotationConversion",
            QuotationCurrencyConversion = "QuotationCurrencyConversion",
            QuotationFromCurrency = "QuotationFromCurrency",
            QuotationToCurrency = "QuotationToCurrency",
            QuotationTaxable = "QuotationTaxable",
            QuotationQuotationNo = "QuotationQuotationNo",
            QuotationRoundup = "QuotationRoundup",
            QuotationMessageId = "QuotationMessageId",
            QuotationQuotationN = "QuotationQuotationN",
            QuotationCompanyId = "QuotationCompanyId",
            QuotationEnquiryN = "QuotationEnquiryN",
            QuotationAdditionalInfo2 = "QuotationAdditionalInfo2"
        }
    }
}
