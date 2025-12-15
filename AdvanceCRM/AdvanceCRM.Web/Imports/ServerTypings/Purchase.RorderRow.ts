namespace AdvanceCRM.Purchase {
    export interface RorderRow {
        Id?: number;
        Name?: string;
        Code?: string;
        DivisionId?: number;
        GroupId?: number;
        SellingPrice?: number;
        Mrp?: number;
        Description?: string;
        TaxId1?: number;
        TaxId2?: number;
        Image?: string;
        TechSpecs?: string;
        HSN?: string;
        ChannelCustomerPrice?: number;
        ResellerPrice?: number;
        WholesalerPrice?: number;
        DealerPrice?: number;
        DistributorPrice?: number;
        StockiestPrice?: number;
        NationalDistributorPrice?: number;
        MinimumStock?: number;
        MaximumStock?: number;
        RawMaterial?: boolean;
        PurchasePrice?: number;
        OpeningStock?: number;
        UnitId?: number;
        CompanyId?: number;
        DivisionProductsDivision?: string;
        GroupProductsGroup?: string;
        TaxId1Name?: string;
        TaxId1Type?: string;
        TaxId1Percentage?: number;
        TaxId2Name?: string;
        TaxId2Type?: string;
        TaxId2Percentage?: number;
        UnitProductsUnit?: string;
        CodePlusName?: string;
    }

    export namespace RorderRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Purchase.Rorder';
        export const lookupKey = 'Products.Rorder';

        export function getLookup(): Q.Lookup<RorderRow> {
            return Q.getLookup<RorderRow>('Products.Rorder');
        }
        export const deletePermission = 'Rorder:Delete';
        export const insertPermission = 'Rorder:Insert';
        export const readPermission = 'Rorder:Read';
        export const updatePermission = 'Rorder:Update';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Code = "Code",
            DivisionId = "DivisionId",
            GroupId = "GroupId",
            SellingPrice = "SellingPrice",
            Mrp = "Mrp",
            Description = "Description",
            TaxId1 = "TaxId1",
            TaxId2 = "TaxId2",
            Image = "Image",
            TechSpecs = "TechSpecs",
            HSN = "HSN",
            ChannelCustomerPrice = "ChannelCustomerPrice",
            ResellerPrice = "ResellerPrice",
            WholesalerPrice = "WholesalerPrice",
            DealerPrice = "DealerPrice",
            DistributorPrice = "DistributorPrice",
            StockiestPrice = "StockiestPrice",
            NationalDistributorPrice = "NationalDistributorPrice",
            MinimumStock = "MinimumStock",
            MaximumStock = "MaximumStock",
            RawMaterial = "RawMaterial",
            PurchasePrice = "PurchasePrice",
            OpeningStock = "OpeningStock",
            UnitId = "UnitId",
            CompanyId = "CompanyId",
            DivisionProductsDivision = "DivisionProductsDivision",
            GroupProductsGroup = "GroupProductsGroup",
            TaxId1Name = "TaxId1Name",
            TaxId1Type = "TaxId1Type",
            TaxId1Percentage = "TaxId1Percentage",
            TaxId2Name = "TaxId2Name",
            TaxId2Type = "TaxId2Type",
            TaxId2Percentage = "TaxId2Percentage",
            UnitProductsUnit = "UnitProductsUnit",
            CodePlusName = "CodePlusName"
        }
    }
}
