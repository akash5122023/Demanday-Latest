namespace AdvanceCRM.Reports {
    export interface QuotationProductsRow {
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
        QuotationId?: number;
        DiscountAmount?: number;
        LineTotal?: number;
        Description?: string;
        Unit?: string;
        Capacity?: string;
        ProductsDivision?: string;
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
        QuotationContactsId?: number;
        QuotationDate?: string;
        QuotationStatus?: number;
        QuotationType?: number;
        QuotationAdditionalInfo?: string;
        QuotationSourceId?: number;
        QuotationStageId?: number;
        QuotationBranchId?: number;
        QuotationOwnerId?: number;
        QuotationAssignedId?: number;
        QuotationReferenceName?: string;
        QuotationReferencePhone?: string;
        QuotationClosingType?: number;
        QuotationLostReason?: string;
        QuotationSubject?: string;
        QuotationReference?: string;
        QuotationAttachment?: string;
        QuotationLines?: number;
        QuotationContactPersonId?: number;
        QuotationClosingDate?: string;
        QuotationEnquiryNo?: number;
        QuotationEnquiryDate?: string;
        QuotationConversion?: number;
        QuotationCurrencyConversion?: boolean;
        QuotationFromCurrency?: number;
        QuotationToCurrency?: number;
        QuotationTaxable?: boolean;
        QuotationQuotationNo?: number;
        QuotationRoundup?: number;
        QuotationMessageId?: number;
        QuotationQuotationN?: string;
        QuotationCompanyId?: number;
        QuotationEnquiryN?: string;
        QuotationAdditionalInfo2?: string;
        QuotationContacts?: string;
        QuotationContactsMobile?: string;
        QuotationContactMail?: string;
        QuotationSubContact?: string;
        QuotationSubContactMobile?: string;
        QuotationSubContactMail?: string;
        QuotationContactAddress?: string;
        QuotationSource?: string;
        QuotationStage?: string;
        QuotationBranch?: string;
        QuotationOwner?: string;
        QuotationAssigned?: string;
    }

    export namespace QuotationProductsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'TaxType1';
        export const localTextPrefix = 'Reports.QuotationProducts';
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
            TaxType1 = "TaxType1",
            Percentage1 = "Percentage1",
            TaxType2 = "TaxType2",
            Percentage2 = "Percentage2",
            QuotationId = "QuotationId",
            DiscountAmount = "DiscountAmount",
            LineTotal = "LineTotal",
            Description = "Description",
            Unit = "Unit",
            Capacity = "Capacity",
            ProductsDivision = "ProductsDivision",
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
            QuotationContactsId = "QuotationContactsId",
            QuotationDate = "QuotationDate",
            QuotationStatus = "QuotationStatus",
            QuotationType = "QuotationType",
            QuotationAdditionalInfo = "QuotationAdditionalInfo",
            QuotationSourceId = "QuotationSourceId",
            QuotationStageId = "QuotationStageId",
            QuotationBranchId = "QuotationBranchId",
            QuotationOwnerId = "QuotationOwnerId",
            QuotationAssignedId = "QuotationAssignedId",
            QuotationReferenceName = "QuotationReferenceName",
            QuotationReferencePhone = "QuotationReferencePhone",
            QuotationClosingType = "QuotationClosingType",
            QuotationLostReason = "QuotationLostReason",
            QuotationSubject = "QuotationSubject",
            QuotationReference = "QuotationReference",
            QuotationAttachment = "QuotationAttachment",
            QuotationLines = "QuotationLines",
            QuotationContactPersonId = "QuotationContactPersonId",
            QuotationClosingDate = "QuotationClosingDate",
            QuotationEnquiryNo = "QuotationEnquiryNo",
            QuotationEnquiryDate = "QuotationEnquiryDate",
            QuotationConversion = "QuotationConversion",
            QuotationCurrencyConversion = "QuotationCurrencyConversion",
            QuotationFromCurrency = "QuotationFromCurrency",
            QuotationToCurrency = "QuotationToCurrency",
            QuotationTaxable = "QuotationTaxable",
            QuotationQuotationNo = "QuotationQuotationNo",
            QuotationRoundup = "QuotationRoundup",
            QuotationMessageId = "QuotationMessageId",
            QuotationQuotationN = "QuotationQuotationN",
            QuotationCompanyId = "QuotationCompanyId",
            QuotationEnquiryN = "QuotationEnquiryN",
            QuotationAdditionalInfo2 = "QuotationAdditionalInfo2",
            QuotationContacts = "QuotationContacts",
            QuotationContactsMobile = "QuotationContactsMobile",
            QuotationContactMail = "QuotationContactMail",
            QuotationSubContact = "QuotationSubContact",
            QuotationSubContactMobile = "QuotationSubContactMobile",
            QuotationSubContactMail = "QuotationSubContactMail",
            QuotationContactAddress = "QuotationContactAddress",
            QuotationSource = "QuotationSource",
            QuotationStage = "QuotationStage",
            QuotationBranch = "QuotationBranch",
            QuotationOwner = "QuotationOwner",
            QuotationAssigned = "QuotationAssigned"
        }
    }
}
