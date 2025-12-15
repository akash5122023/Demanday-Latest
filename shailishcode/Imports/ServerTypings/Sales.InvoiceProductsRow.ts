namespace AdvanceCRM.Sales {
    export interface InvoiceProductsRow {
        Id?: number;
        ProductsId?: number;
        Capacity?: string;
        Quantity?: number;
        Mrp?: number;
        SellingPrice?: number;
        Price?: number;
        Discount?: number;
        TaxType1?: string;
        Percentage1?: number;
        TaxType2?: string;
        Percentage2?: number;
        WarrantyStart?: string;
        WarrantyEnd?: string;
        InvoiceId?: number;
        DiscountAmount?: number;
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

    export namespace InvoiceProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Description';
        export const localTextPrefix = 'Sales.InvoiceProducts';
        export const deletePermission = 'Proforma:Read';
        export const insertPermission = 'Proforma:Read';
        export const readPermission = 'Proforma:Read';
        export const updatePermission = 'Proforma:Read';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            Capacity = "Capacity",
            Quantity = "Quantity",
            Mrp = "Mrp",
            SellingPrice = "SellingPrice",
            Price = "Price",
            Discount = "Discount",
            TaxType1 = "TaxType1",
            Percentage1 = "Percentage1",
            TaxType2 = "TaxType2",
            Percentage2 = "Percentage2",
            WarrantyStart = "WarrantyStart",
            WarrantyEnd = "WarrantyEnd",
            InvoiceId = "InvoiceId",
            DiscountAmount = "DiscountAmount",
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
            InvoiceTaxable = "InvoiceTaxable",
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
