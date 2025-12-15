namespace AdvanceCRM.Purchase {
    export interface PurchaseProductsRow {
        Id?: number;
        ProductsId?: number;
        Serial?: string;
        Batch?: string;
        Quantity?: number;
        Price?: number;
        SellingPrice?: number;
        Mrp?: number;
        Discount?: number;
        DiscountAmount?: number;
        TaxType1?: string;
        Percentage1?: number;
        TaxType2?: string;
        Percentage2?: number;
        WarrantyStart?: string;
        WarrantyEnd?: string;
        Sold?: boolean;
        PurchaseId?: number;
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
        PurchaseInvoiceNo?: string;
        PurchasePurchaseFromId?: number;
        PurchaseInvoiceDate?: string;
        PurchaseTotal?: number;
        PurchaseStatus?: number;
        PurchaseType?: number;
        PurchaseAdditionalInfo?: string;
        PurchaseBranchId?: number;
        PurchaseOwnerId?: number;
        PurchaseAssignedId?: number;
        PurchaseReverseCharge?: boolean;
        PurchaseInvoiceType?: number;
        PurchaseItcEligibility?: number;
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

    export namespace PurchaseProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Serial';
        export const localTextPrefix = 'Purchase.PurchaseProducts';
        export const lookupKey = 'Purchase.PurchaseProductsRow';

        export function getLookup(): Q.Lookup<PurchaseProductsRow> {
            return Q.getLookup<PurchaseProductsRow>('Purchase.PurchaseProductsRow');
        }
        export const deletePermission = 'Purchase:Read';
        export const insertPermission = 'Purchase:Read';
        export const readPermission = 'Purchase:Read';
        export const updatePermission = 'Purchase:Read';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            Serial = "Serial",
            Batch = "Batch",
            Quantity = "Quantity",
            Price = "Price",
            SellingPrice = "SellingPrice",
            Mrp = "Mrp",
            Discount = "Discount",
            DiscountAmount = "DiscountAmount",
            TaxType1 = "TaxType1",
            Percentage1 = "Percentage1",
            TaxType2 = "TaxType2",
            Percentage2 = "Percentage2",
            WarrantyStart = "WarrantyStart",
            WarrantyEnd = "WarrantyEnd",
            Sold = "Sold",
            PurchaseId = "PurchaseId",
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
            PurchaseInvoiceNo = "PurchaseInvoiceNo",
            PurchasePurchaseFromId = "PurchasePurchaseFromId",
            PurchaseInvoiceDate = "PurchaseInvoiceDate",
            PurchaseTotal = "PurchaseTotal",
            PurchaseStatus = "PurchaseStatus",
            PurchaseType = "PurchaseType",
            PurchaseAdditionalInfo = "PurchaseAdditionalInfo",
            PurchaseBranchId = "PurchaseBranchId",
            PurchaseOwnerId = "PurchaseOwnerId",
            PurchaseAssignedId = "PurchaseAssignedId",
            PurchaseReverseCharge = "PurchaseReverseCharge",
            PurchaseInvoiceType = "PurchaseInvoiceType",
            PurchaseItcEligibility = "PurchaseItcEligibility",
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
