namespace AdvanceCRM.Products {
    export interface StockTransferProductsRow {
        Id?: number;
        ProductsId?: number;
        Quantity?: number;
        TransferPrice?: number;
        TaxType1?: string;
        Percentage1?: number;
        TaxType2?: string;
        Percentage2?: number;
        StockTransferId?: number;
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
        StockTransferDate?: string;
        StockTransferFromBranchId?: number;
        StockTransferToBranchId?: number;
        StockTransferTotalQty?: number;
        StockTransferAmount?: number;
        StockTransferAdditionalInfo?: string;
        StockTransferRepresentativeId?: number;
        Tax1Amount?: number;
        Tax2Amount?: number;
        LineTotal?: number;
        GrandTotal?: number;
    }

    export namespace StockTransferProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'TaxType1';
        export const localTextPrefix = 'Products.StockTransferProducts';
        export const deletePermission = 'StockTransfer:Modify';
        export const insertPermission = 'StockTransfer:Modify';
        export const readPermission = 'StockTransfer:Read';
        export const updatePermission = 'StockTransfer:Modify';

        export declare const enum Fields {
            Id = "Id",
            ProductsId = "ProductsId",
            Quantity = "Quantity",
            TransferPrice = "TransferPrice",
            TaxType1 = "TaxType1",
            Percentage1 = "Percentage1",
            TaxType2 = "TaxType2",
            Percentage2 = "Percentage2",
            StockTransferId = "StockTransferId",
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
            StockTransferDate = "StockTransferDate",
            StockTransferFromBranchId = "StockTransferFromBranchId",
            StockTransferToBranchId = "StockTransferToBranchId",
            StockTransferTotalQty = "StockTransferTotalQty",
            StockTransferAmount = "StockTransferAmount",
            StockTransferAdditionalInfo = "StockTransferAdditionalInfo",
            StockTransferRepresentativeId = "StockTransferRepresentativeId",
            Tax1Amount = "Tax1Amount",
            Tax2Amount = "Tax2Amount",
            LineTotal = "LineTotal",
            GrandTotal = "GrandTotal"
        }
    }
}
