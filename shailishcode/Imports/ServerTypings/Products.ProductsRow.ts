namespace AdvanceCRM.Products {
    export interface ProductsRow {
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
        BranchId?: number;
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
        Branch?: string;
        From?: string;
        To?: string;
        Date?: string;
        Destination?: string;
        Nights?: string;
        Adults?: string;
        Childrens?: string;
        HotelName?: string;
        MealPlan?: string;
        ImageAttachment?: string;
        FileAttachment?: string;
    }

    export namespace ProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Products.Products';
        export const lookupKey = 'Products.Products';

        export function getLookup(): Q.Lookup<ProductsRow> {
            return Q.getLookup<ProductsRow>('Products.Products');
        }
        export const deletePermission = 'Products:Delete';
        export const insertPermission = 'Products:Insert';
        export const readPermission = 'Products:Read';
        export const updatePermission = 'Products:Update';

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
            BranchId = "BranchId",
            DivisionProductsDivision = "DivisionProductsDivision",
            GroupProductsGroup = "GroupProductsGroup",
            TaxId1Name = "TaxId1Name",
            TaxId1Type = "TaxId1Type",
            TaxId1Percentage = "TaxId1Percentage",
            TaxId2Name = "TaxId2Name",
            TaxId2Type = "TaxId2Type",
            TaxId2Percentage = "TaxId2Percentage",
            UnitProductsUnit = "UnitProductsUnit",
            CodePlusName = "CodePlusName",
            Branch = "Branch",
            From = "From",
            To = "To",
            Date = "Date",
            Destination = "Destination",
            Nights = "Nights",
            Adults = "Adults",
            Childrens = "Childrens",
            HotelName = "HotelName",
            MealPlan = "MealPlan",
            ImageAttachment = "ImageAttachment",
            FileAttachment = "FileAttachment"
        }
    }
}
