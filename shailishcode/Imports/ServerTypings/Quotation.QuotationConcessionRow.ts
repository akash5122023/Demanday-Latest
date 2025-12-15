namespace AdvanceCRM.Quotation {
    export interface QuotationConcessionRow {
        Id?: number;
        ConcessionId?: number;
        QuotationId?: number;
        ConcessionName?: string;
        ConcessionPercentage?: number;
        ConcessionAmount?: number;
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
    }

    export namespace QuotationConcessionRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Quotation.QuotationConcession';
        export const deletePermission = 'Quotation:Read';
        export const insertPermission = 'Quotation:Read';
        export const readPermission = 'Quotation:Read';
        export const updatePermission = 'Quotation:Read';

        export declare const enum Fields {
            Id = "Id",
            ConcessionId = "ConcessionId",
            QuotationId = "QuotationId",
            ConcessionName = "ConcessionName",
            ConcessionPercentage = "ConcessionPercentage",
            ConcessionAmount = "ConcessionAmount",
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
            QuotationTaxable = "QuotationTaxable"
        }
    }
}
