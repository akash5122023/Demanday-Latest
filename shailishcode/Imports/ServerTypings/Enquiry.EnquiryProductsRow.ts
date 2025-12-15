namespace AdvanceCRM.Enquiry {
    export interface EnquiryProductsRow {
        Id?: number;
        ProductsId?: number;
        Capacity?: string;
        Quantity?: number;
        Mrp?: number;
        SellingPrice?: number;
        Price?: number;
        Discount?: number;
        EnquiryId?: number;
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
        EnquiryContactsId?: number;
        EnquiryDate?: string;
        EnquiryStatus?: number;
        EnquiryType?: number;
        EnquiryAdditionalInfo?: string;
        EnquirySourceId?: number;
        EnquiryStageId?: number;
        EnquiryBranchId?: number;
        EnquiryOwnerId?: number;
        EnquiryAssignedId?: number;
        EnquiryReferenceName?: string;
        EnquiryReferencePhone?: string;
        EnquiryClosingType?: number;
        EnquiryLostReason?: string;
        EnquiryClosingDate?: string;
        EnquiryContactPersonId?: number;
        EnquiryAttachments?: string;
        Unit?: string;
        LineTotal?: number;
        Code?: string;
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

    export namespace EnquiryProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Description';
        export const localTextPrefix = 'Enquiry.EnquiryProducts';
        export const deletePermission = 'Enquiry:Read';
        export const insertPermission = 'Enquiry:Read';
        export const readPermission = 'Enquiry:Read';
        export const updatePermission = 'Enquiry:Read';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            Capacity = "Capacity",
            Quantity = "Quantity",
            Mrp = "Mrp",
            SellingPrice = "SellingPrice",
            Price = "Price",
            Discount = "Discount",
            EnquiryId = "EnquiryId",
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
            EnquiryContactsId = "EnquiryContactsId",
            EnquiryDate = "EnquiryDate",
            EnquiryStatus = "EnquiryStatus",
            EnquiryType = "EnquiryType",
            EnquiryAdditionalInfo = "EnquiryAdditionalInfo",
            EnquirySourceId = "EnquirySourceId",
            EnquiryStageId = "EnquiryStageId",
            EnquiryBranchId = "EnquiryBranchId",
            EnquiryOwnerId = "EnquiryOwnerId",
            EnquiryAssignedId = "EnquiryAssignedId",
            EnquiryReferenceName = "EnquiryReferenceName",
            EnquiryReferencePhone = "EnquiryReferencePhone",
            EnquiryClosingType = "EnquiryClosingType",
            EnquiryLostReason = "EnquiryLostReason",
            EnquiryClosingDate = "EnquiryClosingDate",
            EnquiryContactPersonId = "EnquiryContactPersonId",
            EnquiryAttachments = "EnquiryAttachments",
            Unit = "Unit",
            LineTotal = "LineTotal",
            Code = "Code",
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
