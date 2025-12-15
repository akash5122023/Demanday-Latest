namespace AdvanceCRM.PurchaseOrder {
    export interface PurchaseOrderTermsRow {
        Id?: number;
        TermsId?: number;
        PurchaseOrderId?: number;
        Terms?: string;
        PurchaseOrderContactsId?: number;
        PurchaseOrderDate?: string;
        PurchaseOrderStatus?: number;
        PurchaseOrderTotal?: number;
        PurchaseOrderDescription?: string;
        PurchaseOrderAdditionalInfo?: string;
        PurchaseOrderSourceId?: number;
        PurchaseOrderBranchId?: number;
        PurchaseOrderTerms?: string;
        PurchaseOrderOwnerId?: number;
        PurchaseOrderAssignedId?: number;
        PurchaseOrderSmsTemplate?: string;
        PurchaseOrderAttachments?: string;
        PurchaseOrderRoundup?: number;
        PurchaseOrderPurchaseOrderNo?: number;
        PurchaseOrderLines?: number;
        PurchaseOrderConversion?: number;
        PurchaseOrderCurrencyConversion?: boolean;
        PurchaseOrderFromCurrency?: number;
        PurchaseOrderToCurrency?: number;
        PurchaseOrderDueDate?: string;
        PurchaseOrderShippingAddress?: string;
    }

    export namespace PurchaseOrderTermsRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'PurchaseOrder.PurchaseOrderTerms';
        export const deletePermission = 'Administration:Read';
        export const insertPermission = 'Administration:Read';
        export const readPermission = 'Administration:Read';
        export const updatePermission = 'Administration:Read';

        export declare const enum Fields {
            Id = "Id",
            TermsId = "TermsId",
            PurchaseOrderId = "PurchaseOrderId",
            Terms = "Terms",
            PurchaseOrderContactsId = "PurchaseOrderContactsId",
            PurchaseOrderDate = "PurchaseOrderDate",
            PurchaseOrderStatus = "PurchaseOrderStatus",
            PurchaseOrderTotal = "PurchaseOrderTotal",
            PurchaseOrderDescription = "PurchaseOrderDescription",
            PurchaseOrderAdditionalInfo = "PurchaseOrderAdditionalInfo",
            PurchaseOrderSourceId = "PurchaseOrderSourceId",
            PurchaseOrderBranchId = "PurchaseOrderBranchId",
            PurchaseOrderTerms = "PurchaseOrderTerms",
            PurchaseOrderOwnerId = "PurchaseOrderOwnerId",
            PurchaseOrderAssignedId = "PurchaseOrderAssignedId",
            PurchaseOrderSmsTemplate = "PurchaseOrderSmsTemplate",
            PurchaseOrderAttachments = "PurchaseOrderAttachments",
            PurchaseOrderRoundup = "PurchaseOrderRoundup",
            PurchaseOrderPurchaseOrderNo = "PurchaseOrderPurchaseOrderNo",
            PurchaseOrderLines = "PurchaseOrderLines",
            PurchaseOrderConversion = "PurchaseOrderConversion",
            PurchaseOrderCurrencyConversion = "PurchaseOrderCurrencyConversion",
            PurchaseOrderFromCurrency = "PurchaseOrderFromCurrency",
            PurchaseOrderToCurrency = "PurchaseOrderToCurrency",
            PurchaseOrderDueDate = "PurchaseOrderDueDate",
            PurchaseOrderShippingAddress = "PurchaseOrderShippingAddress"
        }
    }
}
