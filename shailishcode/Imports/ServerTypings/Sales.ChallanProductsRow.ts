namespace AdvanceCRM.Sales {
    export interface ChallanProductsRow {
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
        ChallanId?: number;
        Description?: string;
        Unit?: string;
        TaxType1?: string;
        Percentage1?: number;
        TaxType2?: string;
        Percentage2?: number;
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
        ChallanContactsId?: number;
        ChallanDate?: string;
        ChallanOtherAddress?: boolean;
        ChallanShippingAddress?: string;
        ChallanPackagingCharges?: number;
        ChallanFreightCharges?: number;
        ChallanAdvacne?: number;
        ChallanDueDate?: string;
        ChallanDispatchDetails?: string;
        ChallanStatus?: number;
        ChallanType?: number;
        ChallanAdditionalInfo?: string;
        ChallanSourceId?: number;
        ChallanStageId?: number;
        ChallanBranchId?: number;
        ChallanOwnerId?: number;
        ChallanAssignedId?: number;
        ChallanTotal?: number;
        ChallanInvoiceMade?: boolean;
        ChallanContactPersonId?: number;
        ChallanContactPersonPhone?: string;
        ChallanQuotationNo?: number;
        ChallanQuotationDate?: string;
        Tax1Amount?: number;
        Tax2Amount?: number;
        Code?: string;
        LineTotal?: number;
        From?: string;
        To?: string;
        Date?: string;
        Destination?: string;
        Nights?: string;
        Adults?: string;
        Childrens?: string;
        HotelName?: string;
        MealPlan?: string;
        BranchId?: number;
    }

    export namespace ChallanProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Serial';
        export const localTextPrefix = 'Sales.ChallanProducts';
        export const deletePermission = 'Challan:Read';
        export const insertPermission = 'Challan:Read';
        export const readPermission = 'Challan:Read';
        export const updatePermission = 'Challan:Read';

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
            ChallanId = "ChallanId",
            Description = "Description",
            Unit = "Unit",
            TaxType1 = "TaxType1",
            Percentage1 = "Percentage1",
            TaxType2 = "TaxType2",
            Percentage2 = "Percentage2",
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
            ChallanContactsId = "ChallanContactsId",
            ChallanDate = "ChallanDate",
            ChallanOtherAddress = "ChallanOtherAddress",
            ChallanShippingAddress = "ChallanShippingAddress",
            ChallanPackagingCharges = "ChallanPackagingCharges",
            ChallanFreightCharges = "ChallanFreightCharges",
            ChallanAdvacne = "ChallanAdvacne",
            ChallanDueDate = "ChallanDueDate",
            ChallanDispatchDetails = "ChallanDispatchDetails",
            ChallanStatus = "ChallanStatus",
            ChallanType = "ChallanType",
            ChallanAdditionalInfo = "ChallanAdditionalInfo",
            ChallanSourceId = "ChallanSourceId",
            ChallanStageId = "ChallanStageId",
            ChallanBranchId = "ChallanBranchId",
            ChallanOwnerId = "ChallanOwnerId",
            ChallanAssignedId = "ChallanAssignedId",
            ChallanTotal = "ChallanTotal",
            ChallanInvoiceMade = "ChallanInvoiceMade",
            ChallanContactPersonId = "ChallanContactPersonId",
            ChallanContactPersonPhone = "ChallanContactPersonPhone",
            ChallanQuotationNo = "ChallanQuotationNo",
            ChallanQuotationDate = "ChallanQuotationDate",
            Tax1Amount = "Tax1Amount",
            Tax2Amount = "Tax2Amount",
            Code = "Code",
            LineTotal = "LineTotal",
            From = "From",
            To = "To",
            Date = "Date",
            Destination = "Destination",
            Nights = "Nights",
            Adults = "Adults",
            Childrens = "Childrens",
            HotelName = "HotelName",
            MealPlan = "MealPlan",
            BranchId = "BranchId"
        }
    }
}
