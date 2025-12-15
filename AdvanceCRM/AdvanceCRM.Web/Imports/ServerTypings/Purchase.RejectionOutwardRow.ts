namespace AdvanceCRM.Purchase {
    export interface RejectionOutwardRow {
        Id?: number;
        Date?: string;
        QcNumber?: number;
        ProductId?: number;
        QtyRejected?: number;
        PurchaseFromId?: number;
        Status?: Masters.StatusMaster;
        BranchId?: number;
        AdditionalInfo?: string;
        Attachments?: string;
        SentToSupplier?: boolean;
        SentDate?: string;
        ClosingDate?: string;
        ProductName?: string;
        ProductCode?: string;
        ProductDivisionId?: number;
        ProductGroupId?: number;
        ProductSellingPrice?: number;
        ProductMrp?: number;
        ProductDescription?: string;
        ProductTaxId1?: number;
        ProductTaxId2?: number;
        ProductImage?: string;
        ProductTechSpecs?: string;
        ProductHsn?: string;
        ProductChannelCustomerPrice?: number;
        ProductResellerPrice?: number;
        ProductWholesalerPrice?: number;
        ProductDealerPrice?: number;
        ProductDistributorPrice?: number;
        ProductStockiestPrice?: number;
        ProductNationalDistributorPrice?: number;
        ProductMinimumStock?: number;
        ProductMaximumStock?: number;
        ProductRawMaterial?: boolean;
        ProductPurchasePrice?: number;
        ProductOpeningStock?: number;
        ProductUnitId?: number;
        ProductCompanyId?: number;
        ProductProductTypeId?: number;
        ProductModelSegmentId?: number;
        ProductModelNameId?: number;
        ProductModelCodeId?: number;
        ProductModelVarientId?: number;
        ProductModelColorId?: number;
        ProductSerialNo?: string;
        ProductExShowroomPrice?: number;
        ProductInsuranceAmount?: number;
        ProductRegistrationAmount?: number;
        ProductRoadTax?: number;
        ProductOnRoadPrice?: number;
        ProductOtherTaxes?: number;
        ProductExtendedWarranty?: number;
        ProductRsa?: number;
        ProductImageAttachment?: string;
        ProductFileAttachment?: string;
        ProductFrom?: string;
        ProductTo?: string;
        ProductDate?: string;
        ProductAdults?: string;
        ProductChildrens?: string;
        ProductDestination?: string;
        ProductNights?: string;
        ProductHotelName?: string;
        ProductMealPlan?: string;
        PurchaseFromContactType?: number;
        PurchaseFromName?: string;
        PurchaseFromPhone?: string;
        PurchaseFromEmail?: string;
        PurchaseFromAddress?: string;
        PurchaseFromCityId?: number;
        PurchaseFromStateId?: number;
        PurchaseFromPin?: string;
        PurchaseFromCountry?: number;
        PurchaseFromWebsite?: string;
        PurchaseFromAdditionalInfo?: string;
        PurchaseFromResidentialPhone?: string;
        PurchaseFromOfficePhone?: string;
        PurchaseFromGender?: number;
        PurchaseFromReligion?: number;
        PurchaseFromAreaId?: number;
        PurchaseFromMaritalStatus?: number;
        PurchaseFromMarriageAnniversary?: string;
        PurchaseFromBirthdate?: string;
        PurchaseFromDateOfIncorporation?: string;
        PurchaseFromCategoryId?: number;
        PurchaseFromGradeId?: number;
        PurchaseFromType?: number;
        PurchaseFromOwnerId?: number;
        PurchaseFromAssignedId?: number;
        PurchaseFromChannelCategory?: number;
        PurchaseFromNationalDistributor?: number;
        PurchaseFromStockist?: number;
        PurchaseFromDistributor?: number;
        PurchaseFromDealer?: number;
        PurchaseFromWholesaler?: number;
        PurchaseFromReseller?: number;
        PurchaseFromGstin?: string;
        PurchaseFromPanNo?: string;
        PurchaseFromCcEmails?: string;
        PurchaseFromBccEmails?: string;
        PurchaseFromAttachment?: string;
        PurchaseFromEComGstin?: string;
        PurchaseFromCreditorsOpening?: number;
        PurchaseFromDebtorsOpening?: number;
        PurchaseFromBankName?: string;
        PurchaseFromAccountNumber?: string;
        PurchaseFromIfsc?: string;
        PurchaseFromBankType?: string;
        PurchaseFromBranch?: string;
        PurchaseFromAccountsEmail?: string;
        PurchaseFromPurchaseEmail?: string;
        PurchaseFromServiceEmail?: string;
        PurchaseFromSalesEmail?: string;
        PurchaseFromCreditDays?: number;
        PurchaseFromCustomerType?: number;
        PurchaseFromTrasportationId?: number;
        PurchaseFromTehsilId?: number;
        PurchaseFromVillageId?: number;
        PurchaseFromWhatsapp?: string;
        PurchaseFromConsentForCalling?: boolean;
        PurchaseFromAdditionalInfo2?: string;
        PurchaseFromDateCreated?: string;
        PurchaseFromPassportNumber?: string;
        PurchaseFromFirstName?: string;
        PurchaseFromLastName?: string;
        PurchaseFromExpiryDate?: string;
        PurchaseFromAadharNo?: string;
        Branch?: string;
        BranchPhone?: string;
        BranchEmail?: string;
        BranchAddress?: string;
        BranchCityId?: number;
        BranchStateId?: number;
        BranchPin?: string;
        BranchCountry?: number;
        BranchCompanyId?: number;
    }

    export namespace RejectionOutwardRow {
        export const idProperty = 'Id';
        export const nameProperty = 'ProductId';
        export const localTextPrefix = 'Purchase.RejectionOutward';
        export const lookupKey = 'Purchase.RejectionOutwardRow';

        export function getLookup(): Q.Lookup<RejectionOutwardRow> {
            return Q.getLookup<RejectionOutwardRow>('Purchase.RejectionOutwardRow');
        }
        export const deletePermission = 'RejectionOutward:Delete';
        export const insertPermission = 'RejectionOutward:Insert';
        export const readPermission = 'RejectionOutward:Read';
        export const updatePermission = 'RejectionOutward:Update';

        export declare const enum Fields {
            Id = "Id",
            Date = "Date",
            QcNumber = "QcNumber",
            ProductId = "ProductId",
            QtyRejected = "QtyRejected",
            PurchaseFromId = "PurchaseFromId",
            Status = "Status",
            BranchId = "BranchId",
            AdditionalInfo = "AdditionalInfo",
            Attachments = "Attachments",
            SentToSupplier = "SentToSupplier",
            SentDate = "SentDate",
            ClosingDate = "ClosingDate",
            ProductName = "ProductName",
            ProductCode = "ProductCode",
            ProductDivisionId = "ProductDivisionId",
            ProductGroupId = "ProductGroupId",
            ProductSellingPrice = "ProductSellingPrice",
            ProductMrp = "ProductMrp",
            ProductDescription = "ProductDescription",
            ProductTaxId1 = "ProductTaxId1",
            ProductTaxId2 = "ProductTaxId2",
            ProductImage = "ProductImage",
            ProductTechSpecs = "ProductTechSpecs",
            ProductHsn = "ProductHsn",
            ProductChannelCustomerPrice = "ProductChannelCustomerPrice",
            ProductResellerPrice = "ProductResellerPrice",
            ProductWholesalerPrice = "ProductWholesalerPrice",
            ProductDealerPrice = "ProductDealerPrice",
            ProductDistributorPrice = "ProductDistributorPrice",
            ProductStockiestPrice = "ProductStockiestPrice",
            ProductNationalDistributorPrice = "ProductNationalDistributorPrice",
            ProductMinimumStock = "ProductMinimumStock",
            ProductMaximumStock = "ProductMaximumStock",
            ProductRawMaterial = "ProductRawMaterial",
            ProductPurchasePrice = "ProductPurchasePrice",
            ProductOpeningStock = "ProductOpeningStock",
            ProductUnitId = "ProductUnitId",
            ProductCompanyId = "ProductCompanyId",
            ProductProductTypeId = "ProductProductTypeId",
            ProductModelSegmentId = "ProductModelSegmentId",
            ProductModelNameId = "ProductModelNameId",
            ProductModelCodeId = "ProductModelCodeId",
            ProductModelVarientId = "ProductModelVarientId",
            ProductModelColorId = "ProductModelColorId",
            ProductSerialNo = "ProductSerialNo",
            ProductExShowroomPrice = "ProductExShowroomPrice",
            ProductInsuranceAmount = "ProductInsuranceAmount",
            ProductRegistrationAmount = "ProductRegistrationAmount",
            ProductRoadTax = "ProductRoadTax",
            ProductOnRoadPrice = "ProductOnRoadPrice",
            ProductOtherTaxes = "ProductOtherTaxes",
            ProductExtendedWarranty = "ProductExtendedWarranty",
            ProductRsa = "ProductRsa",
            ProductImageAttachment = "ProductImageAttachment",
            ProductFileAttachment = "ProductFileAttachment",
            ProductFrom = "ProductFrom",
            ProductTo = "ProductTo",
            ProductDate = "ProductDate",
            ProductAdults = "ProductAdults",
            ProductChildrens = "ProductChildrens",
            ProductDestination = "ProductDestination",
            ProductNights = "ProductNights",
            ProductHotelName = "ProductHotelName",
            ProductMealPlan = "ProductMealPlan",
            PurchaseFromContactType = "PurchaseFromContactType",
            PurchaseFromName = "PurchaseFromName",
            PurchaseFromPhone = "PurchaseFromPhone",
            PurchaseFromEmail = "PurchaseFromEmail",
            PurchaseFromAddress = "PurchaseFromAddress",
            PurchaseFromCityId = "PurchaseFromCityId",
            PurchaseFromStateId = "PurchaseFromStateId",
            PurchaseFromPin = "PurchaseFromPin",
            PurchaseFromCountry = "PurchaseFromCountry",
            PurchaseFromWebsite = "PurchaseFromWebsite",
            PurchaseFromAdditionalInfo = "PurchaseFromAdditionalInfo",
            PurchaseFromResidentialPhone = "PurchaseFromResidentialPhone",
            PurchaseFromOfficePhone = "PurchaseFromOfficePhone",
            PurchaseFromGender = "PurchaseFromGender",
            PurchaseFromReligion = "PurchaseFromReligion",
            PurchaseFromAreaId = "PurchaseFromAreaId",
            PurchaseFromMaritalStatus = "PurchaseFromMaritalStatus",
            PurchaseFromMarriageAnniversary = "PurchaseFromMarriageAnniversary",
            PurchaseFromBirthdate = "PurchaseFromBirthdate",
            PurchaseFromDateOfIncorporation = "PurchaseFromDateOfIncorporation",
            PurchaseFromCategoryId = "PurchaseFromCategoryId",
            PurchaseFromGradeId = "PurchaseFromGradeId",
            PurchaseFromType = "PurchaseFromType",
            PurchaseFromOwnerId = "PurchaseFromOwnerId",
            PurchaseFromAssignedId = "PurchaseFromAssignedId",
            PurchaseFromChannelCategory = "PurchaseFromChannelCategory",
            PurchaseFromNationalDistributor = "PurchaseFromNationalDistributor",
            PurchaseFromStockist = "PurchaseFromStockist",
            PurchaseFromDistributor = "PurchaseFromDistributor",
            PurchaseFromDealer = "PurchaseFromDealer",
            PurchaseFromWholesaler = "PurchaseFromWholesaler",
            PurchaseFromReseller = "PurchaseFromReseller",
            PurchaseFromGstin = "PurchaseFromGstin",
            PurchaseFromPanNo = "PurchaseFromPanNo",
            PurchaseFromCcEmails = "PurchaseFromCcEmails",
            PurchaseFromBccEmails = "PurchaseFromBccEmails",
            PurchaseFromAttachment = "PurchaseFromAttachment",
            PurchaseFromEComGstin = "PurchaseFromEComGstin",
            PurchaseFromCreditorsOpening = "PurchaseFromCreditorsOpening",
            PurchaseFromDebtorsOpening = "PurchaseFromDebtorsOpening",
            PurchaseFromBankName = "PurchaseFromBankName",
            PurchaseFromAccountNumber = "PurchaseFromAccountNumber",
            PurchaseFromIfsc = "PurchaseFromIfsc",
            PurchaseFromBankType = "PurchaseFromBankType",
            PurchaseFromBranch = "PurchaseFromBranch",
            PurchaseFromAccountsEmail = "PurchaseFromAccountsEmail",
            PurchaseFromPurchaseEmail = "PurchaseFromPurchaseEmail",
            PurchaseFromServiceEmail = "PurchaseFromServiceEmail",
            PurchaseFromSalesEmail = "PurchaseFromSalesEmail",
            PurchaseFromCreditDays = "PurchaseFromCreditDays",
            PurchaseFromCustomerType = "PurchaseFromCustomerType",
            PurchaseFromTrasportationId = "PurchaseFromTrasportationId",
            PurchaseFromTehsilId = "PurchaseFromTehsilId",
            PurchaseFromVillageId = "PurchaseFromVillageId",
            PurchaseFromWhatsapp = "PurchaseFromWhatsapp",
            PurchaseFromConsentForCalling = "PurchaseFromConsentForCalling",
            PurchaseFromAdditionalInfo2 = "PurchaseFromAdditionalInfo2",
            PurchaseFromDateCreated = "PurchaseFromDateCreated",
            PurchaseFromPassportNumber = "PurchaseFromPassportNumber",
            PurchaseFromFirstName = "PurchaseFromFirstName",
            PurchaseFromLastName = "PurchaseFromLastName",
            PurchaseFromExpiryDate = "PurchaseFromExpiryDate",
            PurchaseFromAadharNo = "PurchaseFromAadharNo",
            Branch = "Branch",
            BranchPhone = "BranchPhone",
            BranchEmail = "BranchEmail",
            BranchAddress = "BranchAddress",
            BranchCityId = "BranchCityId",
            BranchStateId = "BranchStateId",
            BranchPin = "BranchPin",
            BranchCountry = "BranchCountry",
            BranchCompanyId = "BranchCompanyId"
        }
    }
}
