namespace AdvanceCRM.Products {
    export interface InventoryRow {
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
        Hsn?: string;
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
        UnitId?: number;
        CompanyId?: number;
        ProductTypeId?: number;
        ModelSegmentId?: number;
        ModelNameId?: number;
        ModelCodeId?: number;
        ModelVarientId?: number;
        ModelColorId?: number;
        SerialNo?: string;
        ExShowroomPrice?: number;
        InsuranceAmount?: number;
        RegistrationAmount?: number;
        RoadTax?: number;
        OnRoadPrice?: number;
        OtherTaxes?: number;
        ExtendedWarranty?: number;
        Rsa?: number;
        ImageAttachment?: string;
        FileAttachment?: string;
        From?: string;
        To?: string;
        Date?: string;
        Adults?: string;
        Childrens?: string;
        Destination?: string;
        Nights?: string;
        HotelName?: string;
        MealPlan?: string;
        BranchId?: number;
        Quantity?: number;
        TrackInventory?: boolean;
        ProductsId?: number;
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
        ProductsBranchId?: number;
        ProductsQuantity?: number;
        ProductsTrackInventory?: boolean;
        OpeningStock?: number;
        DivisionProductsDivision?: string;
        GroupProductsGroup?: string;
        TaxId1Name?: string;
        TaxId1Type?: string;
        TaxId1Percentage?: number;
        TaxId2Name?: string;
        TaxId2Type?: string;
        TaxId2Percentage?: number;
        UnitProductsUnit?: string;
        Branch?: string;
        CodePlusName?: string;
    }

    export namespace InventoryRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Products.Inventory';
        export const lookupKey = 'Products.Inventory';

        export function getLookup(): Q.Lookup<InventoryRow> {
            return Q.getLookup<InventoryRow>('Products.Inventory');
        }
        export const deletePermission = 'Inventory:Delete';
        export const insertPermission = 'Inventory:Insert';
        export const readPermission = 'Inventory:Read';
        export const updatePermission = 'Inventory:Update';

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
            Hsn = "Hsn",
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
            UnitId = "UnitId",
            CompanyId = "CompanyId",
            ProductTypeId = "ProductTypeId",
            ModelSegmentId = "ModelSegmentId",
            ModelNameId = "ModelNameId",
            ModelCodeId = "ModelCodeId",
            ModelVarientId = "ModelVarientId",
            ModelColorId = "ModelColorId",
            SerialNo = "SerialNo",
            ExShowroomPrice = "ExShowroomPrice",
            InsuranceAmount = "InsuranceAmount",
            RegistrationAmount = "RegistrationAmount",
            RoadTax = "RoadTax",
            OnRoadPrice = "OnRoadPrice",
            OtherTaxes = "OtherTaxes",
            ExtendedWarranty = "ExtendedWarranty",
            Rsa = "Rsa",
            ImageAttachment = "ImageAttachment",
            FileAttachment = "FileAttachment",
            From = "From",
            To = "To",
            Date = "Date",
            Adults = "Adults",
            Childrens = "Childrens",
            Destination = "Destination",
            Nights = "Nights",
            HotelName = "HotelName",
            MealPlan = "MealPlan",
            BranchId = "BranchId",
            Quantity = "Quantity",
            TrackInventory = "TrackInventory",
            ProductsId = "ProductsId",
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
            ProductsBranchId = "ProductsBranchId",
            ProductsQuantity = "ProductsQuantity",
            ProductsTrackInventory = "ProductsTrackInventory",
            OpeningStock = "OpeningStock",
            DivisionProductsDivision = "DivisionProductsDivision",
            GroupProductsGroup = "GroupProductsGroup",
            TaxId1Name = "TaxId1Name",
            TaxId1Type = "TaxId1Type",
            TaxId1Percentage = "TaxId1Percentage",
            TaxId2Name = "TaxId2Name",
            TaxId2Type = "TaxId2Type",
            TaxId2Percentage = "TaxId2Percentage",
            UnitProductsUnit = "UnitProductsUnit",
            Branch = "Branch",
            CodePlusName = "CodePlusName"
        }
    }
}
