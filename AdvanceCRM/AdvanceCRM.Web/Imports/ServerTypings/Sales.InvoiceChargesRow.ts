namespace AdvanceCRM.Sales {
    export interface InvoiceChargesRow {
        Id?: number;
        ChargesId?: number;
        InvoiceId?: number;
        ChargesName?: string;
        ChargesPercentage?: number;
        InvoiceContactsId?: number;
        InvoiceDate?: string;
        InvoiceStatus?: number;
        InvoiceType?: number;
        InvoiceAdditionalInfo?: string;
        InvoiceSourceId?: number;
        InvoiceStageId?: number;
        InvoiceBranchId?: number;
        InvoiceOwnerId?: number;
        InvoiceAssignedId?: number;
        InvoiceOtherAddress?: boolean;
        InvoiceShippingAddress?: string;
        InvoicePackagingCharges?: number;
        InvoiceFreightCharges?: number;
        InvoiceAdvacne?: number;
        InvoiceDueDate?: string;
        InvoiceDispatchDetails?: string;
        InvoiceRoundup?: number;
        InvoiceSubject?: string;
        InvoiceReference?: string;
        InvoiceContactPersonId?: number;
        InvoiceLines?: number;
        InvoiceQuotationNo?: number;
        InvoiceQuotationDate?: string;
        InvoiceConversion?: number;
        InvoicePurchaseOrderNo?: string;
        InvoiceClosingDate?: string;
        InvoiceAttachments?: string;
        InvoiceCurrencyConversion?: boolean;
        InvoiceFromCurrency?: number;
        InvoiceToCurrency?: number;
        InvoiceTaxable?: boolean;
    }

    export namespace InvoiceChargesRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Sales.InvoiceCharges';
        export const deletePermission = 'Proforma:Read';
        export const insertPermission = 'Proforma:Read';
        export const readPermission = 'Proforma:Read';
        export const updatePermission = 'Proforma:Read';

        export declare const enum Fields {
            Id = "Id",
            ChargesId = "ChargesId",
            InvoiceId = "InvoiceId",
            ChargesName = "ChargesName",
            ChargesPercentage = "ChargesPercentage",
            InvoiceContactsId = "InvoiceContactsId",
            InvoiceDate = "InvoiceDate",
            InvoiceStatus = "InvoiceStatus",
            InvoiceType = "InvoiceType",
            InvoiceAdditionalInfo = "InvoiceAdditionalInfo",
            InvoiceSourceId = "InvoiceSourceId",
            InvoiceStageId = "InvoiceStageId",
            InvoiceBranchId = "InvoiceBranchId",
            InvoiceOwnerId = "InvoiceOwnerId",
            InvoiceAssignedId = "InvoiceAssignedId",
            InvoiceOtherAddress = "InvoiceOtherAddress",
            InvoiceShippingAddress = "InvoiceShippingAddress",
            InvoicePackagingCharges = "InvoicePackagingCharges",
            InvoiceFreightCharges = "InvoiceFreightCharges",
            InvoiceAdvacne = "InvoiceAdvacne",
            InvoiceDueDate = "InvoiceDueDate",
            InvoiceDispatchDetails = "InvoiceDispatchDetails",
            InvoiceRoundup = "InvoiceRoundup",
            InvoiceSubject = "InvoiceSubject",
            InvoiceReference = "InvoiceReference",
            InvoiceContactPersonId = "InvoiceContactPersonId",
            InvoiceLines = "InvoiceLines",
            InvoiceQuotationNo = "InvoiceQuotationNo",
            InvoiceQuotationDate = "InvoiceQuotationDate",
            InvoiceConversion = "InvoiceConversion",
            InvoicePurchaseOrderNo = "InvoicePurchaseOrderNo",
            InvoiceClosingDate = "InvoiceClosingDate",
            InvoiceAttachments = "InvoiceAttachments",
            InvoiceCurrencyConversion = "InvoiceCurrencyConversion",
            InvoiceFromCurrency = "InvoiceFromCurrency",
            InvoiceToCurrency = "InvoiceToCurrency",
            InvoiceTaxable = "InvoiceTaxable"
        }
    }
}
