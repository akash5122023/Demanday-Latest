namespace AdvanceCRM.Sales {
    export interface InwardProductsRow {
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
        InwardId?: number;
        Description?: string;
        Unit?: string;
        TaxType1?: string;
        Percentage1?: number;
        TaxType2?: string;
        Percentage2?: number;
        From?: string;
        To?: string;
        Date?: string;
        Adults?: string;
        Childrens?: string;
        Destination?: string;
        Nights?: string;
        HotelName?: string;
        MealPlan?: string;
        BranchId?: number;
        Branch?: string;
        BranchPhone?: string;
        BranchEmail?: string;
        BranchAddress?: string;
        BranchCityId?: number;
        BranchStateId?: number;
        BranchPin?: string;
        BranchCountry?: number;
        BranchCompanyId?: number;
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
        InwardContactsId?: number;
        InwardDate?: string;
        InwardOtherAddress?: boolean;
        InwardShippingAddress?: string;
        InwardPackagingCharges?: number;
        InwardFreightCharges?: number;
        InwardAdvacne?: number;
        InwardDueDate?: string;
        InwardDispatchDetails?: string;
        InwardStatus?: number;
        InwardType?: number;
        InwardAdditionalInfo?: string;
        InwardSourceId?: number;
        InwardStageId?: number;
        InwardBranchId?: number;
        InwardOwnerId?: number;
        InwardAssignedId?: number;
        InwardTotal?: number;
        InwardInvoiceMade?: boolean;
        InwardContactPersonId?: number;
        InwardContactPersonPhone?: string;
        InwardQuotationNo?: number;
        InwardQuotationDate?: string;
        Code?: string;
        LineTotal?: number;
    }

    export namespace InwardProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Serial';
        export const localTextPrefix = 'Sales.InwardProducts';
        export const deletePermission = 'Inward:Read';
        export const insertPermission = 'Inward:Read';
        export const readPermission = 'Inward:Read';
        export const updatePermission = 'Inward:Read';

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
            InwardId = "InwardId",
            Description = "Description",
            Unit = "Unit",
            TaxType1 = "TaxType1",
            Percentage1 = "Percentage1",
            TaxType2 = "TaxType2",
            Percentage2 = "Percentage2",
            From = "From",
            To = "To",
            Date = "Date",
            Adults = "Adults",
            Childrens = "Childrens",
            Destination = "Destination",
            Nights = "Nights",
            HotelName = "HotelName",
            MealPlan = "MealPlan",
            BranchId = "BranchId",
            Branch = "Branch",
            BranchPhone = "BranchPhone",
            BranchEmail = "BranchEmail",
            BranchAddress = "BranchAddress",
            BranchCityId = "BranchCityId",
            BranchStateId = "BranchStateId",
            BranchPin = "BranchPin",
            BranchCountry = "BranchCountry",
            BranchCompanyId = "BranchCompanyId",
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
            InwardContactsId = "InwardContactsId",
            InwardDate = "InwardDate",
            InwardOtherAddress = "InwardOtherAddress",
            InwardShippingAddress = "InwardShippingAddress",
            InwardPackagingCharges = "InwardPackagingCharges",
            InwardFreightCharges = "InwardFreightCharges",
            InwardAdvacne = "InwardAdvacne",
            InwardDueDate = "InwardDueDate",
            InwardDispatchDetails = "InwardDispatchDetails",
            InwardStatus = "InwardStatus",
            InwardType = "InwardType",
            InwardAdditionalInfo = "InwardAdditionalInfo",
            InwardSourceId = "InwardSourceId",
            InwardStageId = "InwardStageId",
            InwardBranchId = "InwardBranchId",
            InwardOwnerId = "InwardOwnerId",
            InwardAssignedId = "InwardAssignedId",
            InwardTotal = "InwardTotal",
            InwardInvoiceMade = "InwardInvoiceMade",
            InwardContactPersonId = "InwardContactPersonId",
            InwardContactPersonPhone = "InwardContactPersonPhone",
            InwardQuotationNo = "InwardQuotationNo",
            InwardQuotationDate = "InwardQuotationDate",
            Code = "Code",
            LineTotal = "LineTotal"
        }
    }
}
