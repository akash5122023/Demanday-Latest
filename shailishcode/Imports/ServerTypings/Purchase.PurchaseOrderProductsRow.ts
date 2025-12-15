namespace AdvanceCRM.Purchase {
    export interface PurchaseOrderProductsRow {
        Id?: number;
        ProductsId?: number;
        Quantity?: number;
        Price?: number;
        Discount?: number;
        DiscountAmount?: number;
        PurchaseOrderId?: number;
        TaxType1?: string;
        Percentage1?: number;
        TaxType2?: string;
        Percentage2?: number;
        Unit?: string;
        ProductsName?: string;
        ProductsCode?: string;
        ProductsDivisionId?: number;
        ProductsGroupId?: number;
        ProductsSellingPrice?: number;
        ProductsMrp?: number;
        ProductsDescription?: string;
        ProductsTaxId1?: number;
        ProductsTaxId2?: number;
        ProductsImage?: string;
        ProductsTechSpecs?: string;
        ProductsHsn?: string;
        ProductsChannelCustomerPrice?: number;
        ProductsResellerPrice?: number;
        ProductsWholesalerPrice?: number;
        ProductsDealerPrice?: number;
        ProductsDistributorPrice?: number;
        ProductsStockiestPrice?: number;
        ProductsNationalDistributorPrice?: number;
        ProductsMinimumStock?: number;
        ProductsMaximumStock?: number;
        ProductsRawMaterial?: boolean;
        ProductsPurchasePrice?: number;
        ProductsOpeningStock?: number;
        ProductsUnitId?: number;
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
        Inclusive?: boolean;
        Tax1Amount?: number;
        Tax2Amount?: number;
        LineTotal?: number;
        GrandTotal?: number;
        From?: string;
        To?: string;
        Date?: string;
        Destination?: string;
        Nights?: string;
        Adults?: string;
        Childrens?: string;
        HotelName?: string;
        MealPlan?: string;
    }

    export namespace PurchaseOrderProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'TaxType1';
        export const localTextPrefix = 'Purchase.PurchaseOrderProducts';
        export const deletePermission = 'PurchaseOrder:Delete';
        export const insertPermission = 'PurchaseOrder:Insert';
        export const readPermission = 'PurchaseOrder:Read';
        export const updatePermission = 'PurchaseOrder:Update';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            Quantity = "Quantity",
            Price = "Price",
            Discount = "Discount",
            DiscountAmount = "DiscountAmount",
            PurchaseOrderId = "PurchaseOrderId",
            TaxType1 = "TaxType1",
            Percentage1 = "Percentage1",
            TaxType2 = "TaxType2",
            Percentage2 = "Percentage2",
            Unit = "Unit",
            ProductsName = "ProductsName",
            ProductsCode = "ProductsCode",
            ProductsDivisionId = "ProductsDivisionId",
            ProductsGroupId = "ProductsGroupId",
            ProductsSellingPrice = "ProductsSellingPrice",
            ProductsMrp = "ProductsMrp",
            ProductsDescription = "ProductsDescription",
            ProductsTaxId1 = "ProductsTaxId1",
            ProductsTaxId2 = "ProductsTaxId2",
            ProductsImage = "ProductsImage",
            ProductsTechSpecs = "ProductsTechSpecs",
            ProductsHsn = "ProductsHsn",
            ProductsChannelCustomerPrice = "ProductsChannelCustomerPrice",
            ProductsResellerPrice = "ProductsResellerPrice",
            ProductsWholesalerPrice = "ProductsWholesalerPrice",
            ProductsDealerPrice = "ProductsDealerPrice",
            ProductsDistributorPrice = "ProductsDistributorPrice",
            ProductsStockiestPrice = "ProductsStockiestPrice",
            ProductsNationalDistributorPrice = "ProductsNationalDistributorPrice",
            ProductsMinimumStock = "ProductsMinimumStock",
            ProductsMaximumStock = "ProductsMaximumStock",
            ProductsRawMaterial = "ProductsRawMaterial",
            ProductsPurchasePrice = "ProductsPurchasePrice",
            ProductsOpeningStock = "ProductsOpeningStock",
            ProductsUnitId = "ProductsUnitId",
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
            Inclusive = "Inclusive",
            Tax1Amount = "Tax1Amount",
            Tax2Amount = "Tax2Amount",
            LineTotal = "LineTotal",
            GrandTotal = "GrandTotal",
            From = "From",
            To = "To",
            Date = "Date",
            Destination = "Destination",
            Nights = "Nights",
            Adults = "Adults",
            Childrens = "Childrens",
            HotelName = "HotelName",
            MealPlan = "MealPlan"
        }
    }
}
