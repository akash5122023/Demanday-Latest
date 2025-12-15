namespace AdvanceCRM.Services {
    export interface CMSProductsRow {
        Id?: number;
        ProductsId?: number;
        Quantity?: number;
        CMSId?: number;
        Price?: number;
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
        CMSContactsId?: number;
        CMSDate?: string;
        CMSProductsId?: number;
        CMSSerialNo?: string;
        CMSComplaintId?: number;
        CMSCategory?: number;
        CMSAmount?: number;
        CMSExpectedCompletion?: string;
        CMSAssignedBy?: number;
        CMSAssignedTo?: number;
        CMSInstructions?: string;
        CMSBranchId?: number;
        CMSStatus?: number;
        CMSCompletionDate?: string;
        CMSFeedback?: string;
        CMSAdditionalInfo?: string;
        CMSImage?: string;
        CMSPhone?: string;
        CMSAddress?: string;
        CMSStageId?: number;
        CMSSeries?: string;
        CMSAirFilter?: number;
        CMSOilFilter?: number;
        CMSOilSeperator?: number;
        CMSOilChange?: number;
        CMSDateOfReading?: string;
        CMSHmr?: number;
        CMSAfct?: number;
        CMSOfct?: number;
        CMSOsct?: number;
        CMSOct?: number;
        CMSDailyWorkingHours?: number;
        CMSPriority?: number;
        CMSPmrClosed?: boolean;
        CMSInvestigationBy?: number;
        CMSActionBy?: number;
        CMSSupervisedBy?: number;
        CMSObservation?: string;
        CMSAction?: string;
        CMSComments?: string;
        LineTotal?: number;
    }

    export namespace CMSProductsRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Services.CMSProducts';
        export const deletePermission = 'CMS:Delete';
        export const insertPermission = 'CMS:Insert';
        export const readPermission = 'CMS:Read';
        export const updatePermission = 'CMS:Update';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            Quantity = "Quantity",
            CMSId = "CMSId",
            Price = "Price",
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
            CMSContactsId = "CMSContactsId",
            CMSDate = "CMSDate",
            CMSProductsId = "CMSProductsId",
            CMSSerialNo = "CMSSerialNo",
            CMSComplaintId = "CMSComplaintId",
            CMSCategory = "CMSCategory",
            CMSAmount = "CMSAmount",
            CMSExpectedCompletion = "CMSExpectedCompletion",
            CMSAssignedBy = "CMSAssignedBy",
            CMSAssignedTo = "CMSAssignedTo",
            CMSInstructions = "CMSInstructions",
            CMSBranchId = "CMSBranchId",
            CMSStatus = "CMSStatus",
            CMSCompletionDate = "CMSCompletionDate",
            CMSFeedback = "CMSFeedback",
            CMSAdditionalInfo = "CMSAdditionalInfo",
            CMSImage = "CMSImage",
            CMSPhone = "CMSPhone",
            CMSAddress = "CMSAddress",
            CMSStageId = "CMSStageId",
            CMSSeries = "CMSSeries",
            CMSAirFilter = "CMSAirFilter",
            CMSOilFilter = "CMSOilFilter",
            CMSOilSeperator = "CMSOilSeperator",
            CMSOilChange = "CMSOilChange",
            CMSDateOfReading = "CMSDateOfReading",
            CMSHmr = "CMSHmr",
            CMSAfct = "CMSAfct",
            CMSOfct = "CMSOfct",
            CMSOsct = "CMSOsct",
            CMSOct = "CMSOct",
            CMSDailyWorkingHours = "CMSDailyWorkingHours",
            CMSPriority = "CMSPriority",
            CMSPmrClosed = "CMSPmrClosed",
            CMSInvestigationBy = "CMSInvestigationBy",
            CMSActionBy = "CMSActionBy",
            CMSSupervisedBy = "CMSSupervisedBy",
            CMSObservation = "CMSObservation",
            CMSAction = "CMSAction",
            CMSComments = "CMSComments",
            LineTotal = "LineTotal"
        }
    }
}
