namespace AdvanceCRM.Quotation {
    export interface QuotationChargesRow {
        Id?: number;
        ChargesId?: number;
        QuotationId?: number;
        ChargesName?: string;
        ChargesPercentage?: number;
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

    export namespace QuotationChargesRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Quotation.QuotationCharges';
        export const deletePermission = 'Quotation:Read';
        export const insertPermission = 'Quotation:Read';
        export const readPermission = 'Quotation:Read';
        export const updatePermission = 'Quotation:Read';

        export declare const enum Fields {
            Id = "Id",
            ChargesId = "ChargesId",
            QuotationId = "QuotationId",
            ChargesName = "ChargesName",
            ChargesPercentage = "ChargesPercentage",
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
