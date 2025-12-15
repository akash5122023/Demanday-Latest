namespace AdvanceCRM.Sales {
    export interface SalesRow {
        Id?: number;
        ContactsId?: number;
        Date?: string;
        Status?: Masters.StatusMaster;
        Type?: Masters.InvoiceTypeMaster;
        AdditionalInfo?: string;
        SourceId?: number;
        StageId?: number;
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
        ContactPersonId?: number;
        Lines?: number;
        InvoiceNo?: number;
        ReverseCharge?: boolean;
        EcomType?: Masters.GSTEcomTypeMaster;
        InvoiceType?: Masters.GSTInvoiceTypeMaster;
        CompanyId?: number;
        InvoiceN?: string;
        QuotationN?: string;
        DealerId?: number;
        DealerDealerName?: string;
        DealerPhone?: string;
        DealerEmail?: string;
        DealerAddress?: string;
        DealerCityId?: number;
        DealerStateId?: number;
        DealerPin?: string;
        DealerCountry?: number;
        DealerAdditionalInfo?: string;
        TrasportationId?: number;
        QuotationNo?: number;
        QuotationDate?: string;
        Conversion?: number;
        PurchaseOrderNo?: string;
        ClosingDate?: string;
        Attachments?: string;
        CurrencyConversion?: boolean;
        FromCurrency?: Masters.Currencies;
        ToCurrency?: Masters.Currencies;
        Taxable?: boolean;
        BillingAddress?: boolean;
        ContactsContactType?: number;
        ContactsName?: string;
        ContactsPhone?: string;
        ContactsEmail?: string;
        ContactsAddress?: string;
        ContactsCityId?: number;
        ContactsStateId?: number;
        ContactsPin?: string;
        ContactsCountry?: number;
        ContactsWebsite?: string;
        ContactsAdditionalInfo?: string;
        ContactsResidentialPhone?: string;
        ContactsOfficePhone?: string;
        ContactsGender?: number;
        ContactsReligion?: number;
        ContactsAreaId?: number;
        ContactsMaritalStatus?: number;
        ContactsMarriageAnniversary?: string;
        ContactsBirthdate?: string;
        ContactsDateOfIncorporation?: string;
        ContactsCategoryId?: number;
        ContactsGradeId?: number;
        ContactsType?: number;
        ContactsOwnerId?: number;
        ContactsAssignedId?: number;
        ContactsChannelCategory?: number;
        ContactsNationalDistributor?: number;
        ContactsStockist?: number;
        ContactsDistributor?: number;
        ContactsDealer?: number;
        ContactsWholesaler?: number;
        ContactsReseller?: number;
        ContactsGstin?: string;
        ContactsPanNo?: string;
        ContactsCcEmails?: string;
        ContactsBccEmails?: string;
        ContactsAttachment?: string;
        ContactsEComGstin?: string;
        ContactsCreditorsOpening?: number;
        ContactsDebtorsOpening?: number;
        ContactsBankName?: string;
        ContactsAccountNumber?: string;
        ContactsIfsc?: string;
        ContactsBankType?: string;
        ContactsBranch?: string;
        ContactsAccountsEmail?: string;
        ContactsPurchaseEmail?: string;
        ContactsServiceEmail?: string;
        ContactsSalesEmail?: string;
        ContactsCreditDays?: number;
        ContactsCustomerType?: number;
        ContactsTrasportationId?: number;
        ContactsTehsilId?: number;
        ContactsVillageId?: number;
        ContactsWhatsapp?: string;
        Message?: string;
        Source?: string;
        PurchaseOrderDate?: string;
        Stage?: string;
        StageType?: number;
        Branch?: string;
        BranchPhone?: string;
        BranchEmail?: string;
        BranchAddress?: string;
        BranchCityId?: number;
        BranchStateId?: number;
        BranchPin?: string;
        BranchCountry?: number;
        OwnerUsername?: string;
        OwnerDisplayName?: string;
        OwnerEmail?: string;
        OwnerSource?: string;
        OwnerPasswordHash?: string;
        OwnerPasswordSalt?: string;
        OwnerLastDirectoryUpdate?: string;
        OwnerUserImage?: string;
        OwnerInsertDate?: string;
        OwnerInsertUserId?: number;
        OwnerUpdateDate?: string;
        OwnerUpdateUserId?: number;
        OwnerIsActive?: number;
        OwnerUpperLevel?: number;
        OwnerUpperLevel2?: number;
        OwnerUpperLevel3?: number;
        OwnerUpperLevel4?: number;
        OwnerUpperLevel5?: number;
        OwnerHost?: string;
        OwnerPort?: number;
        OwnerSsl?: boolean;
        OwnerEmailId?: string;
        OwnerEmailPassword?: string;
        OwnerPhone?: string;
        OwnerMcsmtpServer?: string;
        OwnerMcsmtpPort?: number;
        OwnerMcimapServer?: string;
        OwnerMcimapPort?: number;
        OwnerMcUsername?: string;
        OwnerMcPassword?: string;
        OwnerStartTime?: string;
        OwnerEndTime?: string;
        OwnerUid?: string;
        OwnerNonOperational?: boolean;
        OwnerBranchId?: number;
        AssignedTeamsId?: number;
        OwnerTeamsId?: number;
        AssignedUsername?: string;
        AssignedDisplayName?: string;
        AssignedEmail?: string;
        AssignedSource?: string;
        AssignedPasswordHash?: string;
        AssignedPasswordSalt?: string;
        AssignedLastDirectoryUpdate?: string;
        AssignedUserImage?: string;
        AssignedInsertDate?: string;
        AssignedInsertUserId?: number;
        AssignedUpdateDate?: string;
        AssignedUpdateUserId?: number;
        AssignedIsActive?: number;
        AssignedUpperLevel?: number;
        AssignedUpperLevel2?: number;
        AssignedUpperLevel3?: number;
        AssignedUpperLevel4?: number;
        AssignedUpperLevel5?: number;
        AssignedHost?: string;
        AssignedPort?: number;
        AssignedSsl?: boolean;
        AssignedEmailId?: string;
        AssignedEmailPassword?: string;
        AssignedPhone?: string;
        AssignedMcsmtpServer?: string;
        AssignedMcsmtpPort?: number;
        AssignedMcimapServer?: string;
        AssignedMcimapPort?: number;
        AssignedMcUsername?: string;
        AssignedMcPassword?: string;
        AssignedStartTime?: string;
        AssignedEndTime?: string;
        AssignedUid?: string;
        AssignedNonOperational?: boolean;
        AssignedBranchId?: number;
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
        TrasportationName?: string;
        TrasportationAddress?: string;
        TrasportationPhone?: string;
        TrasportationEmail?: string;
        TrasportationContactPerson?: string;
        TrasportationContactPersonPhone?: string;
        TrasportationGstin?: string;
        Total?: number;
        Tax1?: number;
        Tax2?: number;
        GrandTotal?: number;
        Subject?: string;
        Reference?: string;
        MessageId?: number;
        ApprovedByUsername?: string;
        ApprovedByDisplayName?: string;
        ApprovedByEmail?: string;
        ApprovedBySource?: string;
        ApprovedByPasswordHash?: string;
        ApprovedByPasswordSalt?: string;
        ApprovedByLastDirectoryUpdate?: string;
        ApprovedByUserImage?: string;
        ApprovedByInsertDate?: string;
        ApprovedByInsertUserId?: number;
        ApprovedByUpdateDate?: string;
        ApprovedByUpdateUserId?: number;
        ApprovedByIsActive?: number;
        ApprovedByUpperLevel?: number;
        ApprovedByUpperLevel2?: number;
        ApprovedByUpperLevel3?: number;
        ApprovedByUpperLevel4?: number;
        ApprovedByUpperLevel5?: number;
        ApprovedByHost?: string;
        ApprovedByPort?: number;
        ApprovedBySsl?: boolean;
        ApprovedByEmailId?: string;
        ApprovedByEmailPassword?: string;
        ApprovedByPhone?: string;
        ApprovedByMcsmtpServer?: string;
        ApprovedByMcsmtpPort?: number;
        ApprovedByMcimapServer?: string;
        ApprovedByMcimapPort?: number;
        ApprovedByMcUsername?: string;
        ApprovedByMcPassword?: string;
        ApprovedByStartTime?: string;
        ApprovedByEndTime?: string;
        ApprovedByBranchId?: number;
        ApprovedByUid?: string;
        ApprovedByNonOperational?: boolean;
        ApprovedBy?: number;
        Products?: SalesProductsRow[];
        TermsList?: number[];
        ChargesList?: number[];
        ConcessionList?: number[];
        NoteList?: Common.NoteRow[];
        Timeline?: Common.TimelineRow[];
    }

    export namespace SalesRow {
        export const idProperty = 'Id';
        export const nameProperty = 'AdditionalInfo';
        export const localTextPrefix = 'Sales.Sales';
        export const lookupKey = 'Sales.Sales';

        export function getLookup(): Q.Lookup<SalesRow> {
            return Q.getLookup<SalesRow>('Sales.Sales');
        }
        export const deletePermission = 'Sales:Delete';
        export const insertPermission = 'Sales:Insert';
        export const readPermission = 'Sales:Read';
        export const updatePermission = 'Sales:Update';

        export declare const enum Fields {
            Id = "Id",
            ContactsId = "ContactsId",
            Date = "Date",
            Status = "Status",
            Type = "Type",
            AdditionalInfo = "AdditionalInfo",
            SourceId = "SourceId",
            StageId = "StageId",
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
            ContactPersonId = "ContactPersonId",
            Lines = "Lines",
            InvoiceNo = "InvoiceNo",
            ReverseCharge = "ReverseCharge",
            EcomType = "EcomType",
            InvoiceType = "InvoiceType",
            CompanyId = "CompanyId",
            InvoiceN = "InvoiceN",
            QuotationN = "QuotationN",
            DealerId = "DealerId",
            DealerDealerName = "DealerDealerName",
            DealerPhone = "DealerPhone",
            DealerEmail = "DealerEmail",
            DealerAddress = "DealerAddress",
            DealerCityId = "DealerCityId",
            DealerStateId = "DealerStateId",
            DealerPin = "DealerPin",
            DealerCountry = "DealerCountry",
            DealerAdditionalInfo = "DealerAdditionalInfo",
            TrasportationId = "TrasportationId",
            QuotationNo = "QuotationNo",
            QuotationDate = "QuotationDate",
            Conversion = "Conversion",
            PurchaseOrderNo = "PurchaseOrderNo",
            ClosingDate = "ClosingDate",
            Attachments = "Attachments",
            CurrencyConversion = "CurrencyConversion",
            FromCurrency = "FromCurrency",
            ToCurrency = "ToCurrency",
            Taxable = "Taxable",
            BillingAddress = "BillingAddress",
            ContactsContactType = "ContactsContactType",
            ContactsName = "ContactsName",
            ContactsPhone = "ContactsPhone",
            ContactsEmail = "ContactsEmail",
            ContactsAddress = "ContactsAddress",
            ContactsCityId = "ContactsCityId",
            ContactsStateId = "ContactsStateId",
            ContactsPin = "ContactsPin",
            ContactsCountry = "ContactsCountry",
            ContactsWebsite = "ContactsWebsite",
            ContactsAdditionalInfo = "ContactsAdditionalInfo",
            ContactsResidentialPhone = "ContactsResidentialPhone",
            ContactsOfficePhone = "ContactsOfficePhone",
            ContactsGender = "ContactsGender",
            ContactsReligion = "ContactsReligion",
            ContactsAreaId = "ContactsAreaId",
            ContactsMaritalStatus = "ContactsMaritalStatus",
            ContactsMarriageAnniversary = "ContactsMarriageAnniversary",
            ContactsBirthdate = "ContactsBirthdate",
            ContactsDateOfIncorporation = "ContactsDateOfIncorporation",
            ContactsCategoryId = "ContactsCategoryId",
            ContactsGradeId = "ContactsGradeId",
            ContactsType = "ContactsType",
            ContactsOwnerId = "ContactsOwnerId",
            ContactsAssignedId = "ContactsAssignedId",
            ContactsChannelCategory = "ContactsChannelCategory",
            ContactsNationalDistributor = "ContactsNationalDistributor",
            ContactsStockist = "ContactsStockist",
            ContactsDistributor = "ContactsDistributor",
            ContactsDealer = "ContactsDealer",
            ContactsWholesaler = "ContactsWholesaler",
            ContactsReseller = "ContactsReseller",
            ContactsGstin = "ContactsGstin",
            ContactsPanNo = "ContactsPanNo",
            ContactsCcEmails = "ContactsCcEmails",
            ContactsBccEmails = "ContactsBccEmails",
            ContactsAttachment = "ContactsAttachment",
            ContactsEComGstin = "ContactsEComGstin",
            ContactsCreditorsOpening = "ContactsCreditorsOpening",
            ContactsDebtorsOpening = "ContactsDebtorsOpening",
            ContactsBankName = "ContactsBankName",
            ContactsAccountNumber = "ContactsAccountNumber",
            ContactsIfsc = "ContactsIfsc",
            ContactsBankType = "ContactsBankType",
            ContactsBranch = "ContactsBranch",
            ContactsAccountsEmail = "ContactsAccountsEmail",
            ContactsPurchaseEmail = "ContactsPurchaseEmail",
            ContactsServiceEmail = "ContactsServiceEmail",
            ContactsSalesEmail = "ContactsSalesEmail",
            ContactsCreditDays = "ContactsCreditDays",
            ContactsCustomerType = "ContactsCustomerType",
            ContactsTrasportationId = "ContactsTrasportationId",
            ContactsTehsilId = "ContactsTehsilId",
            ContactsVillageId = "ContactsVillageId",
            ContactsWhatsapp = "ContactsWhatsapp",
            Message = "Message",
            Source = "Source",
            PurchaseOrderDate = "PurchaseOrderDate",
            Stage = "Stage",
            StageType = "StageType",
            Branch = "Branch",
            BranchPhone = "BranchPhone",
            BranchEmail = "BranchEmail",
            BranchAddress = "BranchAddress",
            BranchCityId = "BranchCityId",
            BranchStateId = "BranchStateId",
            BranchPin = "BranchPin",
            BranchCountry = "BranchCountry",
            OwnerUsername = "OwnerUsername",
            OwnerDisplayName = "OwnerDisplayName",
            OwnerEmail = "OwnerEmail",
            OwnerSource = "OwnerSource",
            OwnerPasswordHash = "OwnerPasswordHash",
            OwnerPasswordSalt = "OwnerPasswordSalt",
            OwnerLastDirectoryUpdate = "OwnerLastDirectoryUpdate",
            OwnerUserImage = "OwnerUserImage",
            OwnerInsertDate = "OwnerInsertDate",
            OwnerInsertUserId = "OwnerInsertUserId",
            OwnerUpdateDate = "OwnerUpdateDate",
            OwnerUpdateUserId = "OwnerUpdateUserId",
            OwnerIsActive = "OwnerIsActive",
            OwnerUpperLevel = "OwnerUpperLevel",
            OwnerUpperLevel2 = "OwnerUpperLevel2",
            OwnerUpperLevel3 = "OwnerUpperLevel3",
            OwnerUpperLevel4 = "OwnerUpperLevel4",
            OwnerUpperLevel5 = "OwnerUpperLevel5",
            OwnerHost = "OwnerHost",
            OwnerPort = "OwnerPort",
            OwnerSsl = "OwnerSsl",
            OwnerEmailId = "OwnerEmailId",
            OwnerEmailPassword = "OwnerEmailPassword",
            OwnerPhone = "OwnerPhone",
            OwnerMcsmtpServer = "OwnerMcsmtpServer",
            OwnerMcsmtpPort = "OwnerMcsmtpPort",
            OwnerMcimapServer = "OwnerMcimapServer",
            OwnerMcimapPort = "OwnerMcimapPort",
            OwnerMcUsername = "OwnerMcUsername",
            OwnerMcPassword = "OwnerMcPassword",
            OwnerStartTime = "OwnerStartTime",
            OwnerEndTime = "OwnerEndTime",
            OwnerUid = "OwnerUid",
            OwnerNonOperational = "OwnerNonOperational",
            OwnerBranchId = "OwnerBranchId",
            AssignedTeamsId = "AssignedTeamsId",
            OwnerTeamsId = "OwnerTeamsId",
            AssignedUsername = "AssignedUsername",
            AssignedDisplayName = "AssignedDisplayName",
            AssignedEmail = "AssignedEmail",
            AssignedSource = "AssignedSource",
            AssignedPasswordHash = "AssignedPasswordHash",
            AssignedPasswordSalt = "AssignedPasswordSalt",
            AssignedLastDirectoryUpdate = "AssignedLastDirectoryUpdate",
            AssignedUserImage = "AssignedUserImage",
            AssignedInsertDate = "AssignedInsertDate",
            AssignedInsertUserId = "AssignedInsertUserId",
            AssignedUpdateDate = "AssignedUpdateDate",
            AssignedUpdateUserId = "AssignedUpdateUserId",
            AssignedIsActive = "AssignedIsActive",
            AssignedUpperLevel = "AssignedUpperLevel",
            AssignedUpperLevel2 = "AssignedUpperLevel2",
            AssignedUpperLevel3 = "AssignedUpperLevel3",
            AssignedUpperLevel4 = "AssignedUpperLevel4",
            AssignedUpperLevel5 = "AssignedUpperLevel5",
            AssignedHost = "AssignedHost",
            AssignedPort = "AssignedPort",
            AssignedSsl = "AssignedSsl",
            AssignedEmailId = "AssignedEmailId",
            AssignedEmailPassword = "AssignedEmailPassword",
            AssignedPhone = "AssignedPhone",
            AssignedMcsmtpServer = "AssignedMcsmtpServer",
            AssignedMcsmtpPort = "AssignedMcsmtpPort",
            AssignedMcimapServer = "AssignedMcimapServer",
            AssignedMcimapPort = "AssignedMcimapPort",
            AssignedMcUsername = "AssignedMcUsername",
            AssignedMcPassword = "AssignedMcPassword",
            AssignedStartTime = "AssignedStartTime",
            AssignedEndTime = "AssignedEndTime",
            AssignedUid = "AssignedUid",
            AssignedNonOperational = "AssignedNonOperational",
            AssignedBranchId = "AssignedBranchId",
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
            TrasportationName = "TrasportationName",
            TrasportationAddress = "TrasportationAddress",
            TrasportationPhone = "TrasportationPhone",
            TrasportationEmail = "TrasportationEmail",
            TrasportationContactPerson = "TrasportationContactPerson",
            TrasportationContactPersonPhone = "TrasportationContactPersonPhone",
            TrasportationGstin = "TrasportationGstin",
            Total = "Total",
            Tax1 = "Tax1",
            Tax2 = "Tax2",
            GrandTotal = "GrandTotal",
            Subject = "Subject",
            Reference = "Reference",
            MessageId = "MessageId",
            ApprovedByUsername = "ApprovedByUsername",
            ApprovedByDisplayName = "ApprovedByDisplayName",
            ApprovedByEmail = "ApprovedByEmail",
            ApprovedBySource = "ApprovedBySource",
            ApprovedByPasswordHash = "ApprovedByPasswordHash",
            ApprovedByPasswordSalt = "ApprovedByPasswordSalt",
            ApprovedByLastDirectoryUpdate = "ApprovedByLastDirectoryUpdate",
            ApprovedByUserImage = "ApprovedByUserImage",
            ApprovedByInsertDate = "ApprovedByInsertDate",
            ApprovedByInsertUserId = "ApprovedByInsertUserId",
            ApprovedByUpdateDate = "ApprovedByUpdateDate",
            ApprovedByUpdateUserId = "ApprovedByUpdateUserId",
            ApprovedByIsActive = "ApprovedByIsActive",
            ApprovedByUpperLevel = "ApprovedByUpperLevel",
            ApprovedByUpperLevel2 = "ApprovedByUpperLevel2",
            ApprovedByUpperLevel3 = "ApprovedByUpperLevel3",
            ApprovedByUpperLevel4 = "ApprovedByUpperLevel4",
            ApprovedByUpperLevel5 = "ApprovedByUpperLevel5",
            ApprovedByHost = "ApprovedByHost",
            ApprovedByPort = "ApprovedByPort",
            ApprovedBySsl = "ApprovedBySsl",
            ApprovedByEmailId = "ApprovedByEmailId",
            ApprovedByEmailPassword = "ApprovedByEmailPassword",
            ApprovedByPhone = "ApprovedByPhone",
            ApprovedByMcsmtpServer = "ApprovedByMcsmtpServer",
            ApprovedByMcsmtpPort = "ApprovedByMcsmtpPort",
            ApprovedByMcimapServer = "ApprovedByMcimapServer",
            ApprovedByMcimapPort = "ApprovedByMcimapPort",
            ApprovedByMcUsername = "ApprovedByMcUsername",
            ApprovedByMcPassword = "ApprovedByMcPassword",
            ApprovedByStartTime = "ApprovedByStartTime",
            ApprovedByEndTime = "ApprovedByEndTime",
            ApprovedByBranchId = "ApprovedByBranchId",
            ApprovedByUid = "ApprovedByUid",
            ApprovedByNonOperational = "ApprovedByNonOperational",
            ApprovedBy = "ApprovedBy",
            Products = "Products",
            TermsList = "TermsList",
            ChargesList = "ChargesList",
            ConcessionList = "ConcessionList",
            NoteList = "NoteList",
            Timeline = "Timeline"
        }
    }
}
