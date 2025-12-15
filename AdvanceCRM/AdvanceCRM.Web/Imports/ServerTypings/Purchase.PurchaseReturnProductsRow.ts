namespace AdvanceCRM.Purchase {
    export interface PurchaseReturnProductsRow {
        Id?: number;
        ProductsId?: number;
        Serial?: string;
        Batch?: string;
        Quantity?: number;
        Price?: number;
        TaxType1?: string;
        Percentage1?: number;
        TaxType2?: string;
        Percentage2?: number;
        Description?: string;
        PurchaseReturnId?: number;
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
        PurchaseReturnInvoiceNo?: string;
        PurchaseReturnPurchaseFromId?: number;
        PurchaseReturnInvoiceDate?: string;
        PurchaseReturnTotal?: number;
        PurchaseReturnStatus?: number;
        PurchaseReturnType?: number;
        PurchaseReturnAdditionalInfo?: string;
        PurchaseReturnBranchId?: number;
        PurchaseReturnOwnerId?: number;
        PurchaseReturnAssignedId?: number;
        PurchaseReturnReverseCharge?: boolean;
        PurchaseReturnInvoiceType?: number;
        PurchaseReturnItcEligibility?: number;
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

    export namespace PurchaseReturnProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Serial';
        export const localTextPrefix = 'Purchase.PurchaseReturnProducts';
        export const deletePermission = 'PurchaseReturn:Delete';
        export const insertPermission = 'PurchaseReturn:Insert';
        export const readPermission = 'PurchaseReturn:Read';
        export const updatePermission = 'PurchaseReturn:Update';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            Serial = "Serial",
            Batch = "Batch",
            Quantity = "Quantity",
            Price = "Price",
            TaxType1 = "TaxType1",
            Percentage1 = "Percentage1",
            TaxType2 = "TaxType2",
            Percentage2 = "Percentage2",
            Description = "Description",
            PurchaseReturnId = "PurchaseReturnId",
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
            PurchaseReturnInvoiceNo = "PurchaseReturnInvoiceNo",
            PurchaseReturnPurchaseFromId = "PurchaseReturnPurchaseFromId",
            PurchaseReturnInvoiceDate = "PurchaseReturnInvoiceDate",
            PurchaseReturnTotal = "PurchaseReturnTotal",
            PurchaseReturnStatus = "PurchaseReturnStatus",
            PurchaseReturnType = "PurchaseReturnType",
            PurchaseReturnAdditionalInfo = "PurchaseReturnAdditionalInfo",
            PurchaseReturnBranchId = "PurchaseReturnBranchId",
            PurchaseReturnOwnerId = "PurchaseReturnOwnerId",
            PurchaseReturnAssignedId = "PurchaseReturnAssignedId",
            PurchaseReturnReverseCharge = "PurchaseReturnReverseCharge",
            PurchaseReturnInvoiceType = "PurchaseReturnInvoiceType",
            PurchaseReturnItcEligibility = "PurchaseReturnItcEligibility",
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
