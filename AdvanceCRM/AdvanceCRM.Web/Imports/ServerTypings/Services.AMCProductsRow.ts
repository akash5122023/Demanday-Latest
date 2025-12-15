namespace AdvanceCRM.Services {
    export interface AMCProductsRow {
        Id?: number;
        ProductsId?: number;
        SerialNo?: string;
        Rate?: number;
        Type?: Masters.AMCTypeMaster;
        Quantity?: number;
        Visits?: number;
        Discount?: number;
        TaxType1?: string;
        Percentage1?: number;
        TaxType2?: string;
        Percentage2?: number;
        AMCId?: number;
        DiscountAmount?: number;
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
        AMCDate?: string;
        AMCContactsId?: number;
        AMCStatus?: number;
        AMCStartDate?: string;
        AMCEndDate?: string;
        AMCAdditionalInfo?: string;
        AMCOwnerId?: number;
        AMCAssignedId?: number;
        AMCAttachment?: string;
        AMCLines?: number;
        AMCTerms?: string;
        Code?: string;
        Inclusive?: boolean;
        Tax1Amount?: number;
        Tax2Amount?: number;
        LineTotal?: number;
        GrandTotal?: number;
    }

    export namespace AMCProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'SerialNo';
        export const localTextPrefix = 'Services.AMCProducts';
        export const deletePermission = 'AMC:Delete';
        export const insertPermission = 'AMC:Insert';
        export const readPermission = 'AMC:Read';
        export const updatePermission = 'AMC:Update';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            SerialNo = "SerialNo",
            Rate = "Rate",
            Type = "Type",
            Quantity = "Quantity",
            Visits = "Visits",
            Discount = "Discount",
            TaxType1 = "TaxType1",
            Percentage1 = "Percentage1",
            TaxType2 = "TaxType2",
            Percentage2 = "Percentage2",
            AMCId = "AMCId",
            DiscountAmount = "DiscountAmount",
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
            AMCDate = "AMCDate",
            AMCContactsId = "AMCContactsId",
            AMCStatus = "AMCStatus",
            AMCStartDate = "AMCStartDate",
            AMCEndDate = "AMCEndDate",
            AMCAdditionalInfo = "AMCAdditionalInfo",
            AMCOwnerId = "AMCOwnerId",
            AMCAssignedId = "AMCAssignedId",
            AMCAttachment = "AMCAttachment",
            AMCLines = "AMCLines",
            AMCTerms = "AMCTerms",
            Code = "Code",
            Inclusive = "Inclusive",
            Tax1Amount = "Tax1Amount",
            Tax2Amount = "Tax2Amount",
            LineTotal = "LineTotal",
            GrandTotal = "GrandTotal"
        }
    }
}
