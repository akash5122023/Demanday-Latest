namespace AdvanceCRM.Services {
    export interface CMSRow {
        Id?: number;
        ContactsId?: number;
        Date?: string;
        ProductsId?: number;
        SerialNo?: string;
        ComplaintId?: number;
        Category?: Masters.CMSCategoryMaster;
        Amount?: number;
        Total?: number;
        ExpectedCompletion?: string;
        AssignedBy?: number;
        AssignedTo?: number;
        Instructions?: string;
        BranchId?: number;
        Status?: Masters.CMSStatusMaster;
        CompletionDate?: string;
        Feedback?: string;
        AdditionalInfo?: string;
        Image?: string;
        Phone?: string;
        Address?: string;
        StageId?: number;
        Priority?: Masters.PriorityMaster;
        PMRClosed?: boolean;
        InvestigationBy?: number;
        ActionBy?: number;
        SupervisedBy?: number;
        Observation?: string;
        Action?: string;
        Comments?: string;
        CMSNo?: number;
        DealerId?: number;
        PurchaseDate?: string;
        InvoiceNo?: string;
        EmployeeId?: number;
        ProjectId?: number;
        TicketNo?: number;
        Project?: string;
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
        Cmsn?: string;
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
        ComplaintComplaintType?: string;
        AssignedByUsername?: string;
        AssignedByDisplayName?: string;
        AssignedByEmail?: string;
        AssignedBySource?: string;
        AssignedByPasswordHash?: string;
        AssignedByPasswordSalt?: string;
        AssignedByLastDirectoryUpdate?: string;
        AssignedByUserImage?: string;
        AssignedByInsertDate?: string;
        AssignedByInsertUserId?: number;
        AssignedByUpdateDate?: string;
        AssignedByUpdateUserId?: number;
        AssignedByIsActive?: number;
        AssignedByUpperLevel?: number;
        AssignedByUpperLevel2?: number;
        AssignedByUpperLevel3?: number;
        AssignedByUpperLevel4?: number;
        AssignedByUpperLevel5?: number;
        AssignedByHost?: string;
        AssignedByPort?: number;
        AssignedBySsl?: boolean;
        AssignedByEmailId?: string;
        AssignedByEmailPassword?: string;
        AssignedByPhone?: string;
        AssignedByMcsmtpServer?: string;
        AssignedByMcsmtpPort?: number;
        AssignedByMcimapServer?: string;
        AssignedByMcimapPort?: number;
        AssignedByMcUsername?: string;
        AssignedByMcPassword?: string;
        AssignedByStartTime?: string;
        AssignedByEndTime?: string;
        AssignedByBranchId?: number;
        AssignedByUid?: string;
        AssignedByNonOperational?: boolean;
        AssignedToUsername?: string;
        AssignedDisplayName?: string;
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
        AssignedIsActive?: number;
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
        Branch?: string;
        BranchPhone?: string;
        BranchEmail?: string;
        BranchAddress?: string;
        BranchCityId?: number;
        BranchStateId?: number;
        BranchPin?: string;
        BranchCountry?: number;
        DealerDealerName?: string;
        DealerPhone?: string;
        DealerEmail?: string;
        DealerAddress?: string;
        DealerCityId?: number;
        DealerStateId?: number;
        DealerPin?: string;
        DealerCountry?: number;
        DealerAdditionalInfo?: string;
        Stage?: string;
        StageType?: number;
        InvestigationByUsername?: string;
        InvestigationByDisplayName?: string;
        InvestigationByEmail?: string;
        InvestigationBySource?: string;
        InvestigationByPasswordHash?: string;
        InvestigationByPasswordSalt?: string;
        InvestigationByLastDirectoryUpdate?: string;
        InvestigationByUserImage?: string;
        InvestigationByInsertDate?: string;
        InvestigationByInsertUserId?: number;
        InvestigationByUpdateDate?: string;
        InvestigationByUpdateUserId?: number;
        InvestigationByIsActive?: number;
        InvestigationByUpperLevel?: number;
        InvestigationByUpperLevel2?: number;
        InvestigationByUpperLevel3?: number;
        InvestigationByUpperLevel4?: number;
        InvestigationByUpperLevel5?: number;
        InvestigationByHost?: string;
        InvestigationByPort?: number;
        InvestigationBySsl?: boolean;
        InvestigationByEmailId?: string;
        InvestigationByEmailPassword?: string;
        InvestigationByPhone?: string;
        InvestigationByMcsmtpServer?: string;
        InvestigationByMcsmtpPort?: number;
        InvestigationByMcimapServer?: string;
        InvestigationByMcimapPort?: number;
        InvestigationByMcUsername?: string;
        InvestigationByMcPassword?: string;
        InvestigationByStartTime?: string;
        InvestigationByEndTime?: string;
        InvestigationByBranchId?: number;
        InvestigationByUid?: string;
        InvestigationByNonOperational?: boolean;
        ActionByUsername?: string;
        ActionByDisplayName?: string;
        ActionByEmail?: string;
        ActionBySource?: string;
        ActionByPasswordHash?: string;
        ActionByPasswordSalt?: string;
        ActionByLastDirectoryUpdate?: string;
        ActionByUserImage?: string;
        ActionByInsertDate?: string;
        ActionByInsertUserId?: number;
        ActionByUpdateDate?: string;
        ActionByUpdateUserId?: number;
        ActionByIsActive?: number;
        ActionByUpperLevel?: number;
        ActionByUpperLevel2?: number;
        ActionByUpperLevel3?: number;
        ActionByUpperLevel4?: number;
        ActionByUpperLevel5?: number;
        ActionByHost?: string;
        ActionByPort?: number;
        ActionBySsl?: boolean;
        ActionByEmailId?: string;
        ActionByEmailPassword?: string;
        ActionByPhone?: string;
        ActionByMcsmtpServer?: string;
        ActionByMcsmtpPort?: number;
        ActionByMcimapServer?: string;
        ActionByMcimapPort?: number;
        ActionByMcUsername?: string;
        ActionByMcPassword?: string;
        ActionByStartTime?: string;
        ActionByEndTime?: string;
        ActionByBranchId?: number;
        ActionByUid?: string;
        ActionByNonOperational?: boolean;
        SupervisedByUsername?: string;
        SupervisedByDisplayName?: string;
        SupervisedByEmail?: string;
        SupervisedBySource?: string;
        SupervisedByPasswordHash?: string;
        SupervisedByPasswordSalt?: string;
        SupervisedByLastDirectoryUpdate?: string;
        SupervisedByUserImage?: string;
        SupervisedByInsertDate?: string;
        SupervisedByInsertUserId?: number;
        SupervisedByUpdateDate?: string;
        SupervisedByUpdateUserId?: number;
        SupervisedByIsActive?: number;
        SupervisedByUpperLevel?: number;
        SupervisedByUpperLevel2?: number;
        SupervisedByUpperLevel3?: number;
        SupervisedByUpperLevel4?: number;
        SupervisedByUpperLevel5?: number;
        SupervisedByHost?: string;
        SupervisedByPort?: number;
        SupervisedBySsl?: boolean;
        SupervisedByEmailId?: string;
        SupervisedByEmailPassword?: string;
        SupervisedByPhone?: string;
        SupervisedByMcsmtpServer?: string;
        SupervisedByMcsmtpPort?: number;
        SupervisedByMcimapServer?: string;
        SupervisedByMcimapPort?: number;
        SupervisedByMcUsername?: string;
        SupervisedByMcPassword?: string;
        SupervisedByStartTime?: string;
        SupervisedByEndTime?: string;
        SupervisedByBranchId?: number;
        SupervisedByUid?: string;
        SupervisedByNonOperational?: boolean;
        Products?: CMSProductsRow[];
        NoteList?: Common.NoteRow[];
        Timeline?: Common.TimelineRow[];
        EmployeeEmpCode?: string;
        EmployeeDepartmentId?: number;
        EmployeeName?: string;
        EmployeePhone?: string;
        EmployeeEmail?: string;
        EmployeeAddress?: string;
        EmployeeProfessionalEmail?: string;
        EmployeeCityId?: number;
        EmployeeStateId?: number;
        EmployeePin?: string;
        EmployeeCountry?: number;
        EmployeeAdditionalInfo?: string;
        EmployeeGender?: number;
        EmployeeReligion?: number;
        EmployeeAreaId?: number;
        EmployeeMaritalStatus?: number;
        EmployeeMarriageAnniversary?: string;
        EmployeeBirthdate?: string;
        EmployeeDateOfJoining?: string;
        EmployeeCompanyId?: number;
        EmployeeRolesId?: number;
        EmployeeOwnerId?: number;
        EmployeeAdhaarNo?: string;
        EmployeePanNo?: string;
        EmployeeAttachment?: string;
        EmployeeBankName?: string;
        EmployeeAccountNumber?: string;
        EmployeeIfsc?: string;
        EmployeeBankType?: string;
        EmployeeBranch?: string;
        EmployeeTehsilId?: number;
        EmployeeVillageId?: number;
        Qty?: number;
        Representative?: string;
    }

    export namespace CMSRow {
        export const idProperty = 'Id';
        export const nameProperty = 'SerialNo';
        export const localTextPrefix = 'Services.CMS';
        export const lookupKey = 'Services.CMS';

        export function getLookup(): Q.Lookup<CMSRow> {
            return Q.getLookup<CMSRow>('Services.CMS');
        }
        export const deletePermission = 'CMS:Delete';
        export const insertPermission = 'CMS:Insert';
        export const readPermission = 'CMS:Read';
        export const updatePermission = 'CMS:Update';

        export declare const enum Fields {
            Id = "Id",
            ContactsId = "ContactsId",
            Date = "Date",
            ProductsId = "ProductsId",
            SerialNo = "SerialNo",
            ComplaintId = "ComplaintId",
            Category = "Category",
            Amount = "Amount",
            Total = "Total",
            ExpectedCompletion = "ExpectedCompletion",
            AssignedBy = "AssignedBy",
            AssignedTo = "AssignedTo",
            Instructions = "Instructions",
            BranchId = "BranchId",
            Status = "Status",
            CompletionDate = "CompletionDate",
            Feedback = "Feedback",
            AdditionalInfo = "AdditionalInfo",
            Image = "Image",
            Phone = "Phone",
            Address = "Address",
            StageId = "StageId",
            Priority = "Priority",
            PMRClosed = "PMRClosed",
            InvestigationBy = "InvestigationBy",
            ActionBy = "ActionBy",
            SupervisedBy = "SupervisedBy",
            Observation = "Observation",
            Action = "Action",
            Comments = "Comments",
            CMSNo = "CMSNo",
            DealerId = "DealerId",
            PurchaseDate = "PurchaseDate",
            InvoiceNo = "InvoiceNo",
            EmployeeId = "EmployeeId",
            ProjectId = "ProjectId",
            TicketNo = "TicketNo",
            Project = "Project",
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
            Cmsn = "Cmsn",
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
            ComplaintComplaintType = "ComplaintComplaintType",
            AssignedByUsername = "AssignedByUsername",
            AssignedByDisplayName = "AssignedByDisplayName",
            AssignedByEmail = "AssignedByEmail",
            AssignedBySource = "AssignedBySource",
            AssignedByPasswordHash = "AssignedByPasswordHash",
            AssignedByPasswordSalt = "AssignedByPasswordSalt",
            AssignedByLastDirectoryUpdate = "AssignedByLastDirectoryUpdate",
            AssignedByUserImage = "AssignedByUserImage",
            AssignedByInsertDate = "AssignedByInsertDate",
            AssignedByInsertUserId = "AssignedByInsertUserId",
            AssignedByUpdateDate = "AssignedByUpdateDate",
            AssignedByUpdateUserId = "AssignedByUpdateUserId",
            AssignedByIsActive = "AssignedByIsActive",
            AssignedByUpperLevel = "AssignedByUpperLevel",
            AssignedByUpperLevel2 = "AssignedByUpperLevel2",
            AssignedByUpperLevel3 = "AssignedByUpperLevel3",
            AssignedByUpperLevel4 = "AssignedByUpperLevel4",
            AssignedByUpperLevel5 = "AssignedByUpperLevel5",
            AssignedByHost = "AssignedByHost",
            AssignedByPort = "AssignedByPort",
            AssignedBySsl = "AssignedBySsl",
            AssignedByEmailId = "AssignedByEmailId",
            AssignedByEmailPassword = "AssignedByEmailPassword",
            AssignedByPhone = "AssignedByPhone",
            AssignedByMcsmtpServer = "AssignedByMcsmtpServer",
            AssignedByMcsmtpPort = "AssignedByMcsmtpPort",
            AssignedByMcimapServer = "AssignedByMcimapServer",
            AssignedByMcimapPort = "AssignedByMcimapPort",
            AssignedByMcUsername = "AssignedByMcUsername",
            AssignedByMcPassword = "AssignedByMcPassword",
            AssignedByStartTime = "AssignedByStartTime",
            AssignedByEndTime = "AssignedByEndTime",
            AssignedByBranchId = "AssignedByBranchId",
            AssignedByUid = "AssignedByUid",
            AssignedByNonOperational = "AssignedByNonOperational",
            AssignedToUsername = "AssignedToUsername",
            AssignedDisplayName = "AssignedDisplayName",
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
            AssignedIsActive = "AssignedIsActive",
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
            Branch = "Branch",
            BranchPhone = "BranchPhone",
            BranchEmail = "BranchEmail",
            BranchAddress = "BranchAddress",
            BranchCityId = "BranchCityId",
            BranchStateId = "BranchStateId",
            BranchPin = "BranchPin",
            BranchCountry = "BranchCountry",
            DealerDealerName = "DealerDealerName",
            DealerPhone = "DealerPhone",
            DealerEmail = "DealerEmail",
            DealerAddress = "DealerAddress",
            DealerCityId = "DealerCityId",
            DealerStateId = "DealerStateId",
            DealerPin = "DealerPin",
            DealerCountry = "DealerCountry",
            DealerAdditionalInfo = "DealerAdditionalInfo",
            Stage = "Stage",
            StageType = "StageType",
            InvestigationByUsername = "InvestigationByUsername",
            InvestigationByDisplayName = "InvestigationByDisplayName",
            InvestigationByEmail = "InvestigationByEmail",
            InvestigationBySource = "InvestigationBySource",
            InvestigationByPasswordHash = "InvestigationByPasswordHash",
            InvestigationByPasswordSalt = "InvestigationByPasswordSalt",
            InvestigationByLastDirectoryUpdate = "InvestigationByLastDirectoryUpdate",
            InvestigationByUserImage = "InvestigationByUserImage",
            InvestigationByInsertDate = "InvestigationByInsertDate",
            InvestigationByInsertUserId = "InvestigationByInsertUserId",
            InvestigationByUpdateDate = "InvestigationByUpdateDate",
            InvestigationByUpdateUserId = "InvestigationByUpdateUserId",
            InvestigationByIsActive = "InvestigationByIsActive",
            InvestigationByUpperLevel = "InvestigationByUpperLevel",
            InvestigationByUpperLevel2 = "InvestigationByUpperLevel2",
            InvestigationByUpperLevel3 = "InvestigationByUpperLevel3",
            InvestigationByUpperLevel4 = "InvestigationByUpperLevel4",
            InvestigationByUpperLevel5 = "InvestigationByUpperLevel5",
            InvestigationByHost = "InvestigationByHost",
            InvestigationByPort = "InvestigationByPort",
            InvestigationBySsl = "InvestigationBySsl",
            InvestigationByEmailId = "InvestigationByEmailId",
            InvestigationByEmailPassword = "InvestigationByEmailPassword",
            InvestigationByPhone = "InvestigationByPhone",
            InvestigationByMcsmtpServer = "InvestigationByMcsmtpServer",
            InvestigationByMcsmtpPort = "InvestigationByMcsmtpPort",
            InvestigationByMcimapServer = "InvestigationByMcimapServer",
            InvestigationByMcimapPort = "InvestigationByMcimapPort",
            InvestigationByMcUsername = "InvestigationByMcUsername",
            InvestigationByMcPassword = "InvestigationByMcPassword",
            InvestigationByStartTime = "InvestigationByStartTime",
            InvestigationByEndTime = "InvestigationByEndTime",
            InvestigationByBranchId = "InvestigationByBranchId",
            InvestigationByUid = "InvestigationByUid",
            InvestigationByNonOperational = "InvestigationByNonOperational",
            ActionByUsername = "ActionByUsername",
            ActionByDisplayName = "ActionByDisplayName",
            ActionByEmail = "ActionByEmail",
            ActionBySource = "ActionBySource",
            ActionByPasswordHash = "ActionByPasswordHash",
            ActionByPasswordSalt = "ActionByPasswordSalt",
            ActionByLastDirectoryUpdate = "ActionByLastDirectoryUpdate",
            ActionByUserImage = "ActionByUserImage",
            ActionByInsertDate = "ActionByInsertDate",
            ActionByInsertUserId = "ActionByInsertUserId",
            ActionByUpdateDate = "ActionByUpdateDate",
            ActionByUpdateUserId = "ActionByUpdateUserId",
            ActionByIsActive = "ActionByIsActive",
            ActionByUpperLevel = "ActionByUpperLevel",
            ActionByUpperLevel2 = "ActionByUpperLevel2",
            ActionByUpperLevel3 = "ActionByUpperLevel3",
            ActionByUpperLevel4 = "ActionByUpperLevel4",
            ActionByUpperLevel5 = "ActionByUpperLevel5",
            ActionByHost = "ActionByHost",
            ActionByPort = "ActionByPort",
            ActionBySsl = "ActionBySsl",
            ActionByEmailId = "ActionByEmailId",
            ActionByEmailPassword = "ActionByEmailPassword",
            ActionByPhone = "ActionByPhone",
            ActionByMcsmtpServer = "ActionByMcsmtpServer",
            ActionByMcsmtpPort = "ActionByMcsmtpPort",
            ActionByMcimapServer = "ActionByMcimapServer",
            ActionByMcimapPort = "ActionByMcimapPort",
            ActionByMcUsername = "ActionByMcUsername",
            ActionByMcPassword = "ActionByMcPassword",
            ActionByStartTime = "ActionByStartTime",
            ActionByEndTime = "ActionByEndTime",
            ActionByBranchId = "ActionByBranchId",
            ActionByUid = "ActionByUid",
            ActionByNonOperational = "ActionByNonOperational",
            SupervisedByUsername = "SupervisedByUsername",
            SupervisedByDisplayName = "SupervisedByDisplayName",
            SupervisedByEmail = "SupervisedByEmail",
            SupervisedBySource = "SupervisedBySource",
            SupervisedByPasswordHash = "SupervisedByPasswordHash",
            SupervisedByPasswordSalt = "SupervisedByPasswordSalt",
            SupervisedByLastDirectoryUpdate = "SupervisedByLastDirectoryUpdate",
            SupervisedByUserImage = "SupervisedByUserImage",
            SupervisedByInsertDate = "SupervisedByInsertDate",
            SupervisedByInsertUserId = "SupervisedByInsertUserId",
            SupervisedByUpdateDate = "SupervisedByUpdateDate",
            SupervisedByUpdateUserId = "SupervisedByUpdateUserId",
            SupervisedByIsActive = "SupervisedByIsActive",
            SupervisedByUpperLevel = "SupervisedByUpperLevel",
            SupervisedByUpperLevel2 = "SupervisedByUpperLevel2",
            SupervisedByUpperLevel3 = "SupervisedByUpperLevel3",
            SupervisedByUpperLevel4 = "SupervisedByUpperLevel4",
            SupervisedByUpperLevel5 = "SupervisedByUpperLevel5",
            SupervisedByHost = "SupervisedByHost",
            SupervisedByPort = "SupervisedByPort",
            SupervisedBySsl = "SupervisedBySsl",
            SupervisedByEmailId = "SupervisedByEmailId",
            SupervisedByEmailPassword = "SupervisedByEmailPassword",
            SupervisedByPhone = "SupervisedByPhone",
            SupervisedByMcsmtpServer = "SupervisedByMcsmtpServer",
            SupervisedByMcsmtpPort = "SupervisedByMcsmtpPort",
            SupervisedByMcimapServer = "SupervisedByMcimapServer",
            SupervisedByMcimapPort = "SupervisedByMcimapPort",
            SupervisedByMcUsername = "SupervisedByMcUsername",
            SupervisedByMcPassword = "SupervisedByMcPassword",
            SupervisedByStartTime = "SupervisedByStartTime",
            SupervisedByEndTime = "SupervisedByEndTime",
            SupervisedByBranchId = "SupervisedByBranchId",
            SupervisedByUid = "SupervisedByUid",
            SupervisedByNonOperational = "SupervisedByNonOperational",
            Products = "Products",
            NoteList = "NoteList",
            Timeline = "Timeline",
            EmployeeEmpCode = "EmployeeEmpCode",
            EmployeeDepartmentId = "EmployeeDepartmentId",
            EmployeeName = "EmployeeName",
            EmployeePhone = "EmployeePhone",
            EmployeeEmail = "EmployeeEmail",
            EmployeeAddress = "EmployeeAddress",
            EmployeeProfessionalEmail = "EmployeeProfessionalEmail",
            EmployeeCityId = "EmployeeCityId",
            EmployeeStateId = "EmployeeStateId",
            EmployeePin = "EmployeePin",
            EmployeeCountry = "EmployeeCountry",
            EmployeeAdditionalInfo = "EmployeeAdditionalInfo",
            EmployeeGender = "EmployeeGender",
            EmployeeReligion = "EmployeeReligion",
            EmployeeAreaId = "EmployeeAreaId",
            EmployeeMaritalStatus = "EmployeeMaritalStatus",
            EmployeeMarriageAnniversary = "EmployeeMarriageAnniversary",
            EmployeeBirthdate = "EmployeeBirthdate",
            EmployeeDateOfJoining = "EmployeeDateOfJoining",
            EmployeeCompanyId = "EmployeeCompanyId",
            EmployeeRolesId = "EmployeeRolesId",
            EmployeeOwnerId = "EmployeeOwnerId",
            EmployeeAdhaarNo = "EmployeeAdhaarNo",
            EmployeePanNo = "EmployeePanNo",
            EmployeeAttachment = "EmployeeAttachment",
            EmployeeBankName = "EmployeeBankName",
            EmployeeAccountNumber = "EmployeeAccountNumber",
            EmployeeIfsc = "EmployeeIfsc",
            EmployeeBankType = "EmployeeBankType",
            EmployeeBranch = "EmployeeBranch",
            EmployeeTehsilId = "EmployeeTehsilId",
            EmployeeVillageId = "EmployeeVillageId",
            Qty = "Qty",
            Representative = "Representative"
        }
    }
}
