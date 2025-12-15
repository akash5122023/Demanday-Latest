namespace AdvanceCRM.Reports {
    export interface EnquiryProductsRow {
        Id?: number;
        ProductsId?: number;
        Quantity?: number;
        Mrp?: number;
        SellingPrice?: number;
        Price?: number;
        Discount?: number;
        EnquiryId?: number;
        Description?: string;
        Capacity?: string;
        LineTotal?: number;
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
        EnquiryContactsId?: number;
        EnquiryDate?: string;
        EnquiryStatus?: Masters.StatusMaster;
        EnquiryType?: Masters.EnquiryTypeMaster;
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
        EnquiryEnquiryN?: string;
        EnquiryEnquiryNo?: number;
        EnquiryCompanyId?: number;
        EnquiryContacts?: string;
        EnquiryContactsMobile?: string;
        EnquiryContactMail?: string;
        EnquirySubContact?: string;
        EnquirySubContactMobile?: string;
        EnquirySubContactMail?: string;
        EnquiryContactAddress?: string;
        EnquirySource?: string;
        EnquiryStage?: string;
        EnquiryBranch?: string;
        EnquiryOwner?: string;
        EnquiryAssigned?: string;
    }

    export namespace EnquiryProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Description';
        export const localTextPrefix = 'Reports.EnquiryProducts';
        export const deletePermission = 'Administration:General';
        export const insertPermission = 'Administration:General';
        export const readPermission = 'Administration:General';
        export const updatePermission = 'Administration:General';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            Quantity = "Quantity",
            Mrp = "Mrp",
            SellingPrice = "SellingPrice",
            Price = "Price",
            Discount = "Discount",
            EnquiryId = "EnquiryId",
            Description = "Description",
            Capacity = "Capacity",
            LineTotal = "LineTotal",
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
            EnquiryEnquiryN = "EnquiryEnquiryN",
            EnquiryEnquiryNo = "EnquiryEnquiryNo",
            EnquiryCompanyId = "EnquiryCompanyId",
            EnquiryContacts = "EnquiryContacts",
            EnquiryContactsMobile = "EnquiryContactsMobile",
            EnquiryContactMail = "EnquiryContactMail",
            EnquirySubContact = "EnquirySubContact",
            EnquirySubContactMobile = "EnquirySubContactMobile",
            EnquirySubContactMail = "EnquirySubContactMail",
            EnquiryContactAddress = "EnquiryContactAddress",
            EnquirySource = "EnquirySource",
            EnquiryStage = "EnquiryStage",
            EnquiryBranch = "EnquiryBranch",
            EnquiryOwner = "EnquiryOwner",
            EnquiryAssigned = "EnquiryAssigned"
        }
    }
}
