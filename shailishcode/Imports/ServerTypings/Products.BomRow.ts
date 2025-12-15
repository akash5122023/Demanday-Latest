namespace AdvanceCRM.Products {
    export interface BomRow {
        Id?: number;
        ContactsId?: number;
        Date?: string;
        Status?: number;
        Type?: number;
        AdditionalInfo?: string;
        BranchId?: number;
        OwnerId?: number;
        AssignedId?: number;
        OtherAddress?: boolean;
        ShippingAddress?: string;
        PackagingCharges?: number;
        FreightCharges?: number;
        Advacne?: number;
        DueDate?: string;
        DispatchDetails?: string;
        Roundup?: number;
        Subject?: string;
        Reference?: string;
        ContactPersonId?: number;
        Lines?: number;
        QuotationNo?: number;
        QuotationDate?: string;
        Conversion?: number;
        PurchaseOrderNo?: string;
        ItemName?: string;
        OperationCost?: number;
        RawMaterialCost?: number;
        ScrapMaterialCost?: number;
        TotalMaterialCost?: number;
        OperationName?: string;
        WorkStationName?: string;
        OperatinngTime?: string;
        OperatingCost?: number;
        ProcessLoss?: number;
        ProcessLossQty?: number;
        Attachments?: string;
        CompanyId?: number;
        Taxable?: number;
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
        DiscountAmount?: number;
        Description?: string;
        Unit?: string;
        Image?: string;
        TechSpecs?: string;
        Hsn?: string;
        Code?: string;
        ProductsId?: number;
        ContactPersonName?: string;
        ContactPersonPhone?: string;
        ContactPersonResidentialPhone?: string;
        ContactPersonEmail?: string;
        ContactPersonDesignation?: string;
        ContactPersonAddress?: string;
        ContactPersonGender?: number;
        ContactPersonReligion?: number;
        ContactPersonMaritalStatus?: number;
        ContactPersonMarriageAnniversary?: string;
        ContactPersonBirthdate?: string;
        ContactPersonContactsId?: number;
        ContactPersonProject?: string;
        ContactPersonWhatsapp?: string;
        ContactPersonPassportNumber?: string;
        ContactPersonFirstName?: string;
        ContactPersonLastName?: string;
        ContactPersonExpiryDate?: string;
        ContactPersonAadharNo?: string;
        ContactPersonPanNo?: string;
        ContactPersonFileAttachments?: string;
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
        Products?: BomProductsRow[];
    }

    export namespace BomRow {
        export const idProperty = 'Id';
        export const nameProperty = 'AdditionalInfo';
        export const localTextPrefix = 'Products.Bom';
        export const lookupKey = 'Products.Bom';

        export function getLookup(): Q.Lookup<BomRow> {
            return Q.getLookup<BomRow>('Products.Bom');
        }
        export const deletePermission = 'Administration:Modify';
        export const insertPermission = 'Administration:Modify';
        export const readPermission = 'Administration:Read';
        export const updatePermission = 'Administration:Modify';

        export declare const enum Fields {
            Id = "Id",
            ContactsId = "ContactsId",
            Date = "Date",
            Status = "Status",
            Type = "Type",
            AdditionalInfo = "AdditionalInfo",
            BranchId = "BranchId",
            OwnerId = "OwnerId",
            AssignedId = "AssignedId",
            OtherAddress = "OtherAddress",
            ShippingAddress = "ShippingAddress",
            PackagingCharges = "PackagingCharges",
            FreightCharges = "FreightCharges",
            Advacne = "Advacne",
            DueDate = "DueDate",
            DispatchDetails = "DispatchDetails",
            Roundup = "Roundup",
            Subject = "Subject",
            Reference = "Reference",
            ContactPersonId = "ContactPersonId",
            Lines = "Lines",
            QuotationNo = "QuotationNo",
            QuotationDate = "QuotationDate",
            Conversion = "Conversion",
            PurchaseOrderNo = "PurchaseOrderNo",
            ItemName = "ItemName",
            OperationCost = "OperationCost",
            RawMaterialCost = "RawMaterialCost",
            ScrapMaterialCost = "ScrapMaterialCost",
            TotalMaterialCost = "TotalMaterialCost",
            OperationName = "OperationName",
            WorkStationName = "WorkStationName",
            OperatinngTime = "OperatinngTime",
            OperatingCost = "OperatingCost",
            ProcessLoss = "ProcessLoss",
            ProcessLossQty = "ProcessLossQty",
            Attachments = "Attachments",
            CompanyId = "CompanyId",
            Taxable = "Taxable",
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
            DiscountAmount = "DiscountAmount",
            Description = "Description",
            Unit = "Unit",
            Image = "Image",
            TechSpecs = "TechSpecs",
            Hsn = "Hsn",
            Code = "Code",
            ProductsId = "ProductsId",
            ContactPersonName = "ContactPersonName",
            ContactPersonPhone = "ContactPersonPhone",
            ContactPersonResidentialPhone = "ContactPersonResidentialPhone",
            ContactPersonEmail = "ContactPersonEmail",
            ContactPersonDesignation = "ContactPersonDesignation",
            ContactPersonAddress = "ContactPersonAddress",
            ContactPersonGender = "ContactPersonGender",
            ContactPersonReligion = "ContactPersonReligion",
            ContactPersonMaritalStatus = "ContactPersonMaritalStatus",
            ContactPersonMarriageAnniversary = "ContactPersonMarriageAnniversary",
            ContactPersonBirthdate = "ContactPersonBirthdate",
            ContactPersonContactsId = "ContactPersonContactsId",
            ContactPersonProject = "ContactPersonProject",
            ContactPersonWhatsapp = "ContactPersonWhatsapp",
            ContactPersonPassportNumber = "ContactPersonPassportNumber",
            ContactPersonFirstName = "ContactPersonFirstName",
            ContactPersonLastName = "ContactPersonLastName",
            ContactPersonExpiryDate = "ContactPersonExpiryDate",
            ContactPersonAadharNo = "ContactPersonAadharNo",
            ContactPersonPanNo = "ContactPersonPanNo",
            ContactPersonFileAttachments = "ContactPersonFileAttachments",
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
            Products = "Products"
        }
    }
}
