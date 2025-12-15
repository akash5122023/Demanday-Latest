namespace AdvanceCRM.Sales {
    export interface InvoiceConcessionRow {
        Id?: number;
        ConcessionId?: number;
        InvoiceId?: number;
        ConcessionName?: string;
        ConcessionAmount?: number;
        ConcessionPercentage?: number;
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

    export namespace InvoiceConcessionRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Sales.InvoiceConcession';
        export const deletePermission = 'Proforma:Read';
        export const insertPermission = 'Proforma:Read';
        export const readPermission = 'Proforma:Read';
        export const updatePermission = 'Proforma:Read';

        export declare const enum Fields {
            Id = "Id",
            ConcessionId = "ConcessionId",
            InvoiceId = "InvoiceId",
            ConcessionName = "ConcessionName",
            ConcessionAmount = "ConcessionAmount",
            ConcessionPercentage = "ConcessionPercentage",
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
