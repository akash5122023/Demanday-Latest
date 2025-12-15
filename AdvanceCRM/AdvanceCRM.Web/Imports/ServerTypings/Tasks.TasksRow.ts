namespace AdvanceCRM.Tasks {
    export interface TasksRow {
        Id?: number;
        TaskTitle?: string;
        TaskId?: number;
        Details?: string;
        CreationDate?: string;
        ExpectedCompletion?: string;
        AssignedBy?: number;
        AssignedTo?: number;
        StatusId?: number;
        CompletionDate?: string;
        TypeId?: number;
        Attachments?: string;
        Priority?: Masters.PriorityMaster;
        Resolution?: string;
        ContactsId?: number;
        ProductId?: number;
        ProjectId?: number;
        Recurring?: boolean;
        Period?: Masters.TaskPeriodMaster;
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
        Status?: string;
        Type?: string;
        Task?: string;
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
        Project?: string;
        WatcherList?: number[];
        NoteList?: Common.NoteRow[];
        Timeline?: Common.TimelineRow[];
    }

    export namespace TasksRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Task';
        export const localTextPrefix = 'Tasks.Tasks';
        export const lookupKey = 'Tasks.Tasks';

        export function getLookup(): Q.Lookup<TasksRow> {
            return Q.getLookup<TasksRow>('Tasks.Tasks');
        }
        export const deletePermission = 'Tasks:Delete';
        export const insertPermission = 'Tasks:Insert';
        export const readPermission = 'Tasks:Read';
        export const updatePermission = 'Tasks:Update';

        export declare const enum Fields {
            Id = "Id",
            TaskTitle = "TaskTitle",
            TaskId = "TaskId",
            Details = "Details",
            CreationDate = "CreationDate",
            ExpectedCompletion = "ExpectedCompletion",
            AssignedBy = "AssignedBy",
            AssignedTo = "AssignedTo",
            StatusId = "StatusId",
            CompletionDate = "CompletionDate",
            TypeId = "TypeId",
            Attachments = "Attachments",
            Priority = "Priority",
            Resolution = "Resolution",
            ContactsId = "ContactsId",
            ProductId = "ProductId",
            ProjectId = "ProjectId",
            Recurring = "Recurring",
            Period = "Period",
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
            Status = "Status",
            Type = "Type",
            Task = "Task",
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
            Project = "Project",
            WatcherList = "WatcherList",
            NoteList = "NoteList",
            Timeline = "Timeline"
        }
    }
}
