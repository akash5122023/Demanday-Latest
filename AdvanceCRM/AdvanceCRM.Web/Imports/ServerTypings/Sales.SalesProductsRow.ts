namespace AdvanceCRM.Sales {
    export interface SalesProductsRow {
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
        SalesId?: number;
        Description?: string;
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
        Code?: string;
        Inclusive?: boolean;
        DiscPrice?: number;
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

    export namespace SalesProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Serial';
        export const localTextPrefix = 'Sales.SalesProducts';
        export const lookupKey = 'Sales.SalesProducts';

        export function getLookup(): Q.Lookup<SalesProductsRow> {
            return Q.getLookup<SalesProductsRow>('Sales.SalesProducts');
        }
        export const deletePermission = 'Sales:Read';
        export const insertPermission = 'Sales:Read';
        export const readPermission = 'Sales:Read';
        export const updatePermission = 'Sales:Read';

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
            SalesId = "SalesId",
            Description = "Description",
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
            SalesTaxable = "SalesTaxable",
            Code = "Code",
            Inclusive = "Inclusive",
            DiscPrice = "DiscPrice",
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
