namespace AdvanceCRM.Services {
    export interface TeleCallingRow {
        Id?: number;
        ContactsId?: number;
        ProductsId?: number;
        Status?: Masters.AppointmentTypeMaster;
        Feedback?: string;
        SourceId?: number;
        StageId?: number;
        BranchId?: number;
        RepresentativeId?: number;
        Date?: string;
        AppointmentDate?: string;
        Details?: string;
        AssignedTo?: number;
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
        ContactsCCEmails?: string;
        ContactsBCCEmails?: string;
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
        Source?: string;
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
        RepresentativeUsername?: string;
        RepresentativeDisplayName?: string;
        RepresentativeEmail?: string;
        RepresentativeSource?: string;
        RepresentativePasswordHash?: string;
        RepresentativePasswordSalt?: string;
        RepresentativeLastDirectoryUpdate?: string;
        RepresentativeUserImage?: string;
        RepresentativeInsertDate?: string;
        RepresentativeInsertUserId?: number;
        RepresentativeUpdateDate?: string;
        RepresentativeUpdateUserId?: number;
        RepresentativeIsActive?: number;
        RepresentativeUpperLevel?: number;
        RepresentativeUpperLevel2?: number;
        RepresentativeUpperLevel3?: number;
        RepresentativeUpperLevel4?: number;
        RepresentativeUpperLevel5?: number;
        RepresentativeHost?: string;
        RepresentativePort?: number;
        RepresentativeSsl?: boolean;
        RepresentativeEmailId?: string;
        RepresentativeEmailPassword?: string;
        RepresentativePhone?: string;
        RepresentativeMcsmtpServer?: string;
        RepresentativeMcsmtpPort?: number;
        RepresentativeMcimapServer?: string;
        RepresentativeMcimapPort?: number;
        RepresentativeMcUsername?: string;
        RepresentativeMcPassword?: string;
        RepresentativeStartTime?: string;
        RepresentativeEndTime?: string;
        RepresentativeBranchId?: number;
        RepresentativeUid?: string;
        RepresentativeNonOperational?: boolean;
        AssignedToUsername?: string;
        AssignedToDisplayName?: string;
        AssignedToEmail?: string;
        AssignedToSource?: string;
        AssignedToPasswordHash?: string;
        AssignedToPasswordSalt?: string;
        AssignedToLastDirectoryUpdate?: string;
        AssignedToUserImage?: string;
        AssignedToInsertDate?: string;
        AssignedToInsertUserId?: number;
        AssignedToUpdateDate?: string;
        AssignedToUpdateUserId?: number;
        AssignedToIsActive?: number;
        AssignedToUpperLevel?: number;
        AssignedToUpperLevel2?: number;
        AssignedToUpperLevel3?: number;
        AssignedToUpperLevel4?: number;
        AssignedToUpperLevel5?: number;
        AssignedToHost?: string;
        AssignedToPort?: number;
        AssignedToSsl?: boolean;
        AssignedToEmailId?: string;
        AssignedToEmailPassword?: string;
        AssignedToPhone?: string;
        AssignedToMcsmtpServer?: string;
        AssignedToMcsmtpPort?: number;
        AssignedToMcimapServer?: string;
        AssignedToMcimapPort?: number;
        AssignedToMcUsername?: string;
        AssignedToMcPassword?: string;
        AssignedToStartTime?: string;
        AssignedToEndTime?: string;
        AssignedToBranchId?: number;
        AssignedToUid?: string;
        AssignedToNonOperational?: boolean;
        NoteList?: Common.NoteRow[];
        Timeline?: Common.TimelineRow[];
    }

    export namespace TeleCallingRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Feedback';
        export const localTextPrefix = 'Services.TeleCalling';
        export const lookupKey = 'Services.TeleCallingRow';

        export function getLookup(): Q.Lookup<TeleCallingRow> {
            return Q.getLookup<TeleCallingRow>('Services.TeleCallingRow');
        }
        export const deletePermission = 'TeleCalling:Delete';
        export const insertPermission = 'TeleCalling:Insert';
        export const readPermission = 'TeleCalling:Read';
        export const updatePermission = 'TeleCalling:Update';

        export declare const enum Fields {
            Id = "Id",
            ContactsId = "ContactsId",
            ProductsId = "ProductsId",
            Status = "Status",
            Feedback = "Feedback",
            SourceId = "SourceId",
            StageId = "StageId",
            BranchId = "BranchId",
            RepresentativeId = "RepresentativeId",
            Date = "Date",
            AppointmentDate = "AppointmentDate",
            Details = "Details",
            AssignedTo = "AssignedTo",
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
            ContactsCCEmails = "ContactsCCEmails",
            ContactsBCCEmails = "ContactsBCCEmails",
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
            Source = "Source",
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
            RepresentativeUsername = "RepresentativeUsername",
            RepresentativeDisplayName = "RepresentativeDisplayName",
            RepresentativeEmail = "RepresentativeEmail",
            RepresentativeSource = "RepresentativeSource",
            RepresentativePasswordHash = "RepresentativePasswordHash",
            RepresentativePasswordSalt = "RepresentativePasswordSalt",
            RepresentativeLastDirectoryUpdate = "RepresentativeLastDirectoryUpdate",
            RepresentativeUserImage = "RepresentativeUserImage",
            RepresentativeInsertDate = "RepresentativeInsertDate",
            RepresentativeInsertUserId = "RepresentativeInsertUserId",
            RepresentativeUpdateDate = "RepresentativeUpdateDate",
            RepresentativeUpdateUserId = "RepresentativeUpdateUserId",
            RepresentativeIsActive = "RepresentativeIsActive",
            RepresentativeUpperLevel = "RepresentativeUpperLevel",
            RepresentativeUpperLevel2 = "RepresentativeUpperLevel2",
            RepresentativeUpperLevel3 = "RepresentativeUpperLevel3",
            RepresentativeUpperLevel4 = "RepresentativeUpperLevel4",
            RepresentativeUpperLevel5 = "RepresentativeUpperLevel5",
            RepresentativeHost = "RepresentativeHost",
            RepresentativePort = "RepresentativePort",
            RepresentativeSsl = "RepresentativeSsl",
            RepresentativeEmailId = "RepresentativeEmailId",
            RepresentativeEmailPassword = "RepresentativeEmailPassword",
            RepresentativePhone = "RepresentativePhone",
            RepresentativeMcsmtpServer = "RepresentativeMcsmtpServer",
            RepresentativeMcsmtpPort = "RepresentativeMcsmtpPort",
            RepresentativeMcimapServer = "RepresentativeMcimapServer",
            RepresentativeMcimapPort = "RepresentativeMcimapPort",
            RepresentativeMcUsername = "RepresentativeMcUsername",
            RepresentativeMcPassword = "RepresentativeMcPassword",
            RepresentativeStartTime = "RepresentativeStartTime",
            RepresentativeEndTime = "RepresentativeEndTime",
            RepresentativeBranchId = "RepresentativeBranchId",
            RepresentativeUid = "RepresentativeUid",
            RepresentativeNonOperational = "RepresentativeNonOperational",
            AssignedToUsername = "AssignedToUsername",
            AssignedToDisplayName = "AssignedToDisplayName",
            AssignedToEmail = "AssignedToEmail",
            AssignedToSource = "AssignedToSource",
            AssignedToPasswordHash = "AssignedToPasswordHash",
            AssignedToPasswordSalt = "AssignedToPasswordSalt",
            AssignedToLastDirectoryUpdate = "AssignedToLastDirectoryUpdate",
            AssignedToUserImage = "AssignedToUserImage",
            AssignedToInsertDate = "AssignedToInsertDate",
            AssignedToInsertUserId = "AssignedToInsertUserId",
            AssignedToUpdateDate = "AssignedToUpdateDate",
            AssignedToUpdateUserId = "AssignedToUpdateUserId",
            AssignedToIsActive = "AssignedToIsActive",
            AssignedToUpperLevel = "AssignedToUpperLevel",
            AssignedToUpperLevel2 = "AssignedToUpperLevel2",
            AssignedToUpperLevel3 = "AssignedToUpperLevel3",
            AssignedToUpperLevel4 = "AssignedToUpperLevel4",
            AssignedToUpperLevel5 = "AssignedToUpperLevel5",
            AssignedToHost = "AssignedToHost",
            AssignedToPort = "AssignedToPort",
            AssignedToSsl = "AssignedToSsl",
            AssignedToEmailId = "AssignedToEmailId",
            AssignedToEmailPassword = "AssignedToEmailPassword",
            AssignedToPhone = "AssignedToPhone",
            AssignedToMcsmtpServer = "AssignedToMcsmtpServer",
            AssignedToMcsmtpPort = "AssignedToMcsmtpPort",
            AssignedToMcimapServer = "AssignedToMcimapServer",
            AssignedToMcimapPort = "AssignedToMcimapPort",
            AssignedToMcUsername = "AssignedToMcUsername",
            AssignedToMcPassword = "AssignedToMcPassword",
            AssignedToStartTime = "AssignedToStartTime",
            AssignedToEndTime = "AssignedToEndTime",
            AssignedToBranchId = "AssignedToBranchId",
            AssignedToUid = "AssignedToUid",
            AssignedToNonOperational = "AssignedToNonOperational",
            NoteList = "NoteList",
            Timeline = "Timeline"
        }
    }
}
