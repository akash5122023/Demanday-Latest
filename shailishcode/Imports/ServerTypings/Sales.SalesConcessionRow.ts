namespace AdvanceCRM.Sales {
    export interface SalesConcessionRow {
        Id?: number;
        ConcessionId?: number;
        SalesId?: number;
        ConcessionName?: string;
        ConcessionAmount?: number;
        ConcessionPercentage?: number;
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
        SalesPurchaseOrderNo?: string;
        SalesClosingDate?: string;
        SalesAttachments?: string;
        SalesCurrencyConversion?: boolean;
        SalesFromCurrency?: number;
        SalesToCurrency?: number;
        SalesTaxable?: boolean;
    }

    export namespace SalesConcessionRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Sales.SalesConcession';
        export const deletePermission = 'Sales:Read';
        export const insertPermission = 'Sales:Read';
        export const readPermission = 'Sales:Read';
        export const updatePermission = 'Sales:Read';

        export declare const enum Fields {
            Id = "Id",
            ConcessionId = "ConcessionId",
            SalesId = "SalesId",
            ConcessionName = "ConcessionName",
            ConcessionAmount = "ConcessionAmount",
            ConcessionPercentage = "ConcessionPercentage",
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
            SalesPurchaseOrderNo = "SalesPurchaseOrderNo",
            SalesClosingDate = "SalesClosingDate",
            SalesAttachments = "SalesAttachments",
            SalesCurrencyConversion = "SalesCurrencyConversion",
            SalesFromCurrency = "SalesFromCurrency",
            SalesToCurrency = "SalesToCurrency",
            SalesTaxable = "SalesTaxable"
        }
    }
}
