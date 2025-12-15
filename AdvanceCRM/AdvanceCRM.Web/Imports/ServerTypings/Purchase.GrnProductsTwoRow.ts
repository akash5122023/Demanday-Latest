namespace AdvanceCRM.Purchase {
    export interface GrnProductsTwoRow {
        Id?: number;
        ProductsId?: number;
        Code?: string;
        BranchId?: number;
        Price?: number;
        OrderQuantity?: number;
        ReceivedQuantity?: number;
        ExtraQuantity?: number;
        RejectedQuantity?: number;
        Description?: string;
        GrnId?: number;
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
        GrnContactsId?: number;
        GrnGrnDate?: string;
        GrnGrnType?: number;
        GrnPo?: string;
        GrnPoDate?: string;
        GrnOwnerId?: number;
        GrnAssignedId?: number;
        GrnStatus?: number;
        GrnDescription?: string;
        GrnInvoiceNo?: string;
        GrnInvoiceDate?: string;
    }

    export namespace GrnProductsTwoRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Code';
        export const localTextPrefix = 'Purchase.GrnProductsTwo';
        export const deletePermission = 'GrnTwo:Read';
        export const insertPermission = 'GrnTwo:Read';
        export const readPermission = 'GrnTwo:Read';
        export const updatePermission = 'GrnTwo:Read';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            Code = "Code",
            BranchId = "BranchId",
            Price = "Price",
            OrderQuantity = "OrderQuantity",
            ReceivedQuantity = "ReceivedQuantity",
            ExtraQuantity = "ExtraQuantity",
            RejectedQuantity = "RejectedQuantity",
            Description = "Description",
            GrnId = "GrnId",
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
            BranchCompanyId = "BranchCompanyId",
            GrnContactsId = "GrnContactsId",
            GrnGrnDate = "GrnGrnDate",
            GrnGrnType = "GrnGrnType",
            GrnPo = "GrnPo",
            GrnPoDate = "GrnPoDate",
            GrnOwnerId = "GrnOwnerId",
            GrnAssignedId = "GrnAssignedId",
            GrnStatus = "GrnStatus",
            GrnDescription = "GrnDescription",
            GrnInvoiceNo = "GrnInvoiceNo",
            GrnInvoiceDate = "GrnInvoiceDate"
        }
    }
}
