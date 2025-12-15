namespace AdvanceCRM.Sales {
    export interface OutwardProductsRow {
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
        OutwardId?: number;
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
        ProductsCompanyId?: number;
        ProductsProductTypeId?: number;
        ProductsModelSegmentId?: number;
        ProductsModelNameId?: number;
        ProductsModelCodeId?: number;
        ProductsModelVarientId?: number;
        ProductsModelColorId?: number;
        ProductsSerialNo?: string;
        ProductsExShowroomPrice?: number;
        ProductsInsuranceAmount?: number;
        ProductsRegistrationAmount?: number;
        ProductsRoadTax?: number;
        ProductsOnRoadPrice?: number;
        ProductsOtherTaxes?: number;
        ProductsExtendedWarranty?: number;
        ProductsRsa?: number;
        ProductsImageAttachment?: string;
        ProductsFileAttachment?: string;
        ProductsFrom?: string;
        ProductsTo?: string;
        ProductsDate?: string;
        ProductsAdults?: string;
        ProductsChildrens?: string;
        ProductsDestination?: string;
        ProductsNights?: string;
        ProductsHotelName?: string;
        ProductsMealPlan?: string;
        Branch?: string;
        BranchPhone?: string;
        BranchEmail?: string;
        BranchAddress?: string;
        BranchCityId?: number;
        BranchStateId?: number;
        BranchPin?: string;
        BranchCountry?: number;
        BranchCompanyId?: number;
    }

    export namespace OutwardProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Serial';
        export const localTextPrefix = 'Sales.OutwardProducts';
        export const deletePermission = 'Outward:Read';
        export const insertPermission = 'Outward:Read';
        export const readPermission = 'Outward:Read';
        export const updatePermission = 'Outward:Read';

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
            OutwardId = "OutwardId",
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
            ProductsCompanyId = "ProductsCompanyId",
            ProductsProductTypeId = "ProductsProductTypeId",
            ProductsModelSegmentId = "ProductsModelSegmentId",
            ProductsModelNameId = "ProductsModelNameId",
            ProductsModelCodeId = "ProductsModelCodeId",
            ProductsModelVarientId = "ProductsModelVarientId",
            ProductsModelColorId = "ProductsModelColorId",
            ProductsSerialNo = "ProductsSerialNo",
            ProductsExShowroomPrice = "ProductsExShowroomPrice",
            ProductsInsuranceAmount = "ProductsInsuranceAmount",
            ProductsRegistrationAmount = "ProductsRegistrationAmount",
            ProductsRoadTax = "ProductsRoadTax",
            ProductsOnRoadPrice = "ProductsOnRoadPrice",
            ProductsOtherTaxes = "ProductsOtherTaxes",
            ProductsExtendedWarranty = "ProductsExtendedWarranty",
            ProductsRsa = "ProductsRsa",
            ProductsImageAttachment = "ProductsImageAttachment",
            ProductsFileAttachment = "ProductsFileAttachment",
            ProductsFrom = "ProductsFrom",
            ProductsTo = "ProductsTo",
            ProductsDate = "ProductsDate",
            ProductsAdults = "ProductsAdults",
            ProductsChildrens = "ProductsChildrens",
            ProductsDestination = "ProductsDestination",
            ProductsNights = "ProductsNights",
            ProductsHotelName = "ProductsHotelName",
            ProductsMealPlan = "ProductsMealPlan",
            Branch = "Branch",
            BranchPhone = "BranchPhone",
            BranchEmail = "BranchEmail",
            BranchAddress = "BranchAddress",
            BranchCityId = "BranchCityId",
            BranchStateId = "BranchStateId",
            BranchPin = "BranchPin",
            BranchCountry = "BranchCountry",
            BranchCompanyId = "BranchCompanyId"
        }
    }
}
