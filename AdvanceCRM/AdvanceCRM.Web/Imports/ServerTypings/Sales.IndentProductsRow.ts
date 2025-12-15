namespace AdvanceCRM.Sales {
    export interface IndentProductsRow {
        Id?: number;
        ProductsId?: number;
        Quantity?: number;
        Description?: string;
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
        IndentContactsId?: number;
        IndentDate?: string;
        IndentStatus?: number;
        IndentAdditionalInfo?: string;
        IndentInvoiceNo?: string;
        IndentBranchId?: number;
        IndentOwnerId?: number;
        IndentAssignedId?: number;
        IndentSubject?: string;
        IndentReference?: string;
        IndentContactPersonId?: number;
        IndentLines?: number;
        IndentId?: number;
        Products?: IndentProductsRow[];
    }

    export namespace IndentProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'ProductsName';
        export const localTextPrefix = 'Sales.IndentProducts';
        export const deletePermission = 'IndentProducts';
        export const insertPermission = 'IndentProducts';
        export const readPermission = 'IndentProducts';
        export const updatePermission = 'IndentProducts';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            Quantity = "Quantity",
            Description = "Description",
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
            IndentContactsId = "IndentContactsId",
            IndentDate = "IndentDate",
            IndentStatus = "IndentStatus",
            IndentAdditionalInfo = "IndentAdditionalInfo",
            IndentInvoiceNo = "IndentInvoiceNo",
            IndentBranchId = "IndentBranchId",
            IndentOwnerId = "IndentOwnerId",
            IndentAssignedId = "IndentAssignedId",
            IndentSubject = "IndentSubject",
            IndentReference = "IndentReference",
            IndentContactPersonId = "IndentContactPersonId",
            IndentLines = "IndentLines",
            IndentId = "IndentId",
            Products = "Products"
        }
    }
}
