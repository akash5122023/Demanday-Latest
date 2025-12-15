namespace AdvanceCRM.Products {
    export interface BomProductsRow {
        Id?: number;
        ProductsId?: number;
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
        BomId?: number;
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
        BomContactsId?: number;
        BomDate?: string;
        BomStatus?: number;
        BomType?: number;
        BomAdditionalInfo?: string;
        BomBranchId?: number;
        BomOwnerId?: number;
        BomAssignedId?: number;
        BomOtherAddress?: boolean;
        BomShippingAddress?: string;
        BomPackagingCharges?: number;
        BomFreightCharges?: number;
        BomAdvacne?: number;
        BomDueDate?: string;
        BomDispatchDetails?: string;
        BomRoundup?: number;
        BomSubject?: string;
        BomReference?: string;
        BomContactPersonId?: number;
        BomLines?: number;
        BomQuotationNo?: number;
        BomQuotationDate?: string;
        BomConversion?: number;
        BomPurchaseOrderNo?: string;
        BomItemName?: string;
        BomOperationCost?: number;
        BomRawMaterialCost?: number;
        BomScrapMaterialCost?: number;
        BomTotalMaterialCost?: number;
        BomOperationName?: string;
        BomWorkStationName?: string;
        BomOperatinngTime?: string;
        BomOperatingCost?: number;
        BomProcessLoss?: number;
        BomProcessLossQty?: number;
        BomAttachments?: string;
        BomCompanyId?: number;
        BomTaxable?: number;
        BomQuantity?: number;
        BomMrp?: number;
        BomSellingPrice?: number;
        BomPrice?: number;
        BomDiscount?: number;
        BomTaxType1?: string;
        BomPercentage1?: number;
        BomTaxType2?: string;
        BomPercentage2?: number;
        BomWarrantyStart?: string;
        BomWarrantyEnd?: string;
        BomDiscountAmount?: number;
        BomDescription?: string;
        BomUnit?: string;
        BomImage?: string;
        BomTechSpecs?: string;
        BomHsn?: string;
        BomCode?: string;
        BomProductsId?: number;
        Code?: string;
        Inclusive?: boolean;
        DiscPrice?: number;
        Tax1Amount?: number;
        Tax2Amount?: number;
        LineTotal?: number;
        GrandTotal?: number;
    }

    export namespace BomProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'TaxType1';
        export const localTextPrefix = 'Products.BomProducts';
        export const deletePermission = 'Bom:Read';
        export const insertPermission = 'Bom:Read';
        export const readPermission = 'Bom:Read';
        export const updatePermission = 'Bom:Read';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
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
            BomId = "BomId",
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
            BomContactsId = "BomContactsId",
            BomDate = "BomDate",
            BomStatus = "BomStatus",
            BomType = "BomType",
            BomAdditionalInfo = "BomAdditionalInfo",
            BomBranchId = "BomBranchId",
            BomOwnerId = "BomOwnerId",
            BomAssignedId = "BomAssignedId",
            BomOtherAddress = "BomOtherAddress",
            BomShippingAddress = "BomShippingAddress",
            BomPackagingCharges = "BomPackagingCharges",
            BomFreightCharges = "BomFreightCharges",
            BomAdvacne = "BomAdvacne",
            BomDueDate = "BomDueDate",
            BomDispatchDetails = "BomDispatchDetails",
            BomRoundup = "BomRoundup",
            BomSubject = "BomSubject",
            BomReference = "BomReference",
            BomContactPersonId = "BomContactPersonId",
            BomLines = "BomLines",
            BomQuotationNo = "BomQuotationNo",
            BomQuotationDate = "BomQuotationDate",
            BomConversion = "BomConversion",
            BomPurchaseOrderNo = "BomPurchaseOrderNo",
            BomItemName = "BomItemName",
            BomOperationCost = "BomOperationCost",
            BomRawMaterialCost = "BomRawMaterialCost",
            BomScrapMaterialCost = "BomScrapMaterialCost",
            BomTotalMaterialCost = "BomTotalMaterialCost",
            BomOperationName = "BomOperationName",
            BomWorkStationName = "BomWorkStationName",
            BomOperatinngTime = "BomOperatinngTime",
            BomOperatingCost = "BomOperatingCost",
            BomProcessLoss = "BomProcessLoss",
            BomProcessLossQty = "BomProcessLossQty",
            BomAttachments = "BomAttachments",
            BomCompanyId = "BomCompanyId",
            BomTaxable = "BomTaxable",
            BomQuantity = "BomQuantity",
            BomMrp = "BomMrp",
            BomSellingPrice = "BomSellingPrice",
            BomPrice = "BomPrice",
            BomDiscount = "BomDiscount",
            BomTaxType1 = "BomTaxType1",
            BomPercentage1 = "BomPercentage1",
            BomTaxType2 = "BomTaxType2",
            BomPercentage2 = "BomPercentage2",
            BomWarrantyStart = "BomWarrantyStart",
            BomWarrantyEnd = "BomWarrantyEnd",
            BomDiscountAmount = "BomDiscountAmount",
            BomDescription = "BomDescription",
            BomUnit = "BomUnit",
            BomImage = "BomImage",
            BomTechSpecs = "BomTechSpecs",
            BomHsn = "BomHsn",
            BomCode = "BomCode",
            BomProductsId = "BomProductsId",
            Code = "Code",
            Inclusive = "Inclusive",
            DiscPrice = "DiscPrice",
            Tax1Amount = "Tax1Amount",
            Tax2Amount = "Tax2Amount",
            LineTotal = "LineTotal",
            GrandTotal = "GrandTotal"
        }
    }
}
