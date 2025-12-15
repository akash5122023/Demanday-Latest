namespace AdvanceCRM.Sales {
    export interface SalesReturnProductsRow {
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
        SalesReturnId?: number;
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
        SalesReturnContactsId?: number;
        SalesReturnDate?: string;
        SalesReturnStatus?: number;
        SalesReturnType?: number;
        SalesReturnAdditionalInfo?: string;
        SalesReturnSourceId?: number;
        SalesReturnStageId?: number;
        SalesReturnBranchId?: number;
        SalesReturnOwnerId?: number;
        SalesReturnAssignedId?: number;
        SalesReturnOtherAddress?: boolean;
        SalesReturnShippingAddress?: string;
        SalesReturnPackagingCharges?: number;
        SalesReturnFreightCharges?: number;
        SalesReturnAdvacne?: number;
        SalesReturnDueDate?: string;
        SalesReturnDispatchDetails?: string;
        SalesReturnRoundup?: number;
        SalesReturnContactPersonId?: number;
        SalesReturnLines?: number;
        SalesReturnInvoiceNo?: number;
        SalesReturnReverseCharge?: boolean;
        SalesReturnEcomType?: number;
        SalesReturnInvoiceType?: number;
        SalesReturnTrasportationId?: number;
        SalesReturnQuotationNo?: number;
        SalesReturnQuotationDate?: string;
        SalesReturnConversion?: number;
        SalesReturnPurchaseOrderNo?: string;
        SalesReturnClosingDate?: string;
        SalesReturnAttachments?: string;
        SalesReturnCurrencyConversion?: boolean;
        SalesReturnFromCurrency?: number;
        SalesReturnToCurrency?: number;
        SalesReturnTaxable?: boolean;
        Code?: string;
        Inclusive?: boolean;
        LineTotal?: number;
        Tax1Amount?: number;
        Tax2Amount?: number;
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

    export namespace SalesReturnProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Serial';
        export const localTextPrefix = 'Sales.SalesReturnProducts';
        export const deletePermission = 'SalesReturn:Delete';
        export const insertPermission = 'SalesReturn:Insert';
        export const readPermission = 'SalesReturn:Read';
        export const updatePermission = 'SalesReturn:Update';

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
            SalesReturnId = "SalesReturnId",
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
            SalesReturnContactsId = "SalesReturnContactsId",
            SalesReturnDate = "SalesReturnDate",
            SalesReturnStatus = "SalesReturnStatus",
            SalesReturnType = "SalesReturnType",
            SalesReturnAdditionalInfo = "SalesReturnAdditionalInfo",
            SalesReturnSourceId = "SalesReturnSourceId",
            SalesReturnStageId = "SalesReturnStageId",
            SalesReturnBranchId = "SalesReturnBranchId",
            SalesReturnOwnerId = "SalesReturnOwnerId",
            SalesReturnAssignedId = "SalesReturnAssignedId",
            SalesReturnOtherAddress = "SalesReturnOtherAddress",
            SalesReturnShippingAddress = "SalesReturnShippingAddress",
            SalesReturnPackagingCharges = "SalesReturnPackagingCharges",
            SalesReturnFreightCharges = "SalesReturnFreightCharges",
            SalesReturnAdvacne = "SalesReturnAdvacne",
            SalesReturnDueDate = "SalesReturnDueDate",
            SalesReturnDispatchDetails = "SalesReturnDispatchDetails",
            SalesReturnRoundup = "SalesReturnRoundup",
            SalesReturnContactPersonId = "SalesReturnContactPersonId",
            SalesReturnLines = "SalesReturnLines",
            SalesReturnInvoiceNo = "SalesReturnInvoiceNo",
            SalesReturnReverseCharge = "SalesReturnReverseCharge",
            SalesReturnEcomType = "SalesReturnEcomType",
            SalesReturnInvoiceType = "SalesReturnInvoiceType",
            SalesReturnTrasportationId = "SalesReturnTrasportationId",
            SalesReturnQuotationNo = "SalesReturnQuotationNo",
            SalesReturnQuotationDate = "SalesReturnQuotationDate",
            SalesReturnConversion = "SalesReturnConversion",
            SalesReturnPurchaseOrderNo = "SalesReturnPurchaseOrderNo",
            SalesReturnClosingDate = "SalesReturnClosingDate",
            SalesReturnAttachments = "SalesReturnAttachments",
            SalesReturnCurrencyConversion = "SalesReturnCurrencyConversion",
            SalesReturnFromCurrency = "SalesReturnFromCurrency",
            SalesReturnToCurrency = "SalesReturnToCurrency",
            SalesReturnTaxable = "SalesReturnTaxable",
            Code = "Code",
            Inclusive = "Inclusive",
            LineTotal = "LineTotal",
            Tax1Amount = "Tax1Amount",
            Tax2Amount = "Tax2Amount",
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
