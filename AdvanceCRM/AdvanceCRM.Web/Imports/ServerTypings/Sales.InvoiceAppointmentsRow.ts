namespace AdvanceCRM.Sales {
    export interface InvoiceAppointmentsRow {
        Id?: number;
        Details?: string;
        AppointmentDate?: string;
        Status?: Masters.StatusMaster;
        RepresentativeId?: number;
        InvoiceId?: number;
        MinutesOfMeeting?: string;
        Reason?: string;
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
        RepresentativeUid?: string;
        RepresentativeNonOperational?: boolean;
        RepresentativeBranchId?: number;
        InvoiceContactsId?: number;
        InvoiceDate?: string;
        InvoiceStatus?: number;
        InvoiceType?: number;
        InvoiceAdditionalInfo?: string;
        InvoiceSourceId?: number;
        InvoiceStageId?: number;
        InvoiceBranchId?: number;
        InvoiceOwnerId?: number;
        InvoiceAssignedId?: number;
        InvoiceOtherAddress?: boolean;
        InvoiceShippingAddress?: string;
        InvoicePackagingCharges?: number;
        InvoiceFreightCharges?: number;
        InvoiceAdvacne?: number;
        InvoiceDueDate?: string;
        InvoiceDispatchDetails?: string;
        InvoiceRoundup?: number;
        InvoiceSubject?: string;
        InvoiceReference?: string;
        InvoiceContactPersonId?: number;
        InvoiceLines?: number;
        InvoiceQuotationNo?: number;
        InvoiceQuotationDate?: string;
        InvoiceConversion?: number;
        InvoicePurchaseOrderNo?: string;
        InvoiceClosingDate?: string;
        InvoiceAttachments?: string;
        InvoiceCurrencyConversion?: boolean;
        InvoiceFromCurrency?: number;
        InvoiceToCurrency?: number;
        InvoiceTaxable?: boolean;
        ContactName?: string;
        ContactPhone?: string;
        ContactEmail?: string;
        ContactAddress?: string;
        NoteList?: Common.NoteRow[];
    }

    export namespace InvoiceAppointmentsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Details';
        export const localTextPrefix = 'Sales.InvoiceAppointments';
        export const deletePermission = 'Proforma:Appointments';
        export const insertPermission = 'Proforma:Appointments';
        export const readPermission = '?';
        export const updatePermission = 'Proforma:Appointments';

        export declare const enum Fields {
            Id = "Id",
            Details = "Details",
            AppointmentDate = "AppointmentDate",
            Status = "Status",
            RepresentativeId = "RepresentativeId",
            InvoiceId = "InvoiceId",
            MinutesOfMeeting = "MinutesOfMeeting",
            Reason = "Reason",
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
            RepresentativeUid = "RepresentativeUid",
            RepresentativeNonOperational = "RepresentativeNonOperational",
            RepresentativeBranchId = "RepresentativeBranchId",
            InvoiceContactsId = "InvoiceContactsId",
            InvoiceDate = "InvoiceDate",
            InvoiceStatus = "InvoiceStatus",
            InvoiceType = "InvoiceType",
            InvoiceAdditionalInfo = "InvoiceAdditionalInfo",
            InvoiceSourceId = "InvoiceSourceId",
            InvoiceStageId = "InvoiceStageId",
            InvoiceBranchId = "InvoiceBranchId",
            InvoiceOwnerId = "InvoiceOwnerId",
            InvoiceAssignedId = "InvoiceAssignedId",
            InvoiceOtherAddress = "InvoiceOtherAddress",
            InvoiceShippingAddress = "InvoiceShippingAddress",
            InvoicePackagingCharges = "InvoicePackagingCharges",
            InvoiceFreightCharges = "InvoiceFreightCharges",
            InvoiceAdvacne = "InvoiceAdvacne",
            InvoiceDueDate = "InvoiceDueDate",
            InvoiceDispatchDetails = "InvoiceDispatchDetails",
            InvoiceRoundup = "InvoiceRoundup",
            InvoiceSubject = "InvoiceSubject",
            InvoiceReference = "InvoiceReference",
            InvoiceContactPersonId = "InvoiceContactPersonId",
            InvoiceLines = "InvoiceLines",
            InvoiceQuotationNo = "InvoiceQuotationNo",
            InvoiceQuotationDate = "InvoiceQuotationDate",
            InvoiceConversion = "InvoiceConversion",
            InvoicePurchaseOrderNo = "InvoicePurchaseOrderNo",
            InvoiceClosingDate = "InvoiceClosingDate",
            InvoiceAttachments = "InvoiceAttachments",
            InvoiceCurrencyConversion = "InvoiceCurrencyConversion",
            InvoiceFromCurrency = "InvoiceFromCurrency",
            InvoiceToCurrency = "InvoiceToCurrency",
            InvoiceTaxable = "InvoiceTaxable",
            ContactName = "ContactName",
            ContactPhone = "ContactPhone",
            ContactEmail = "ContactEmail",
            ContactAddress = "ContactAddress",
            NoteList = "NoteList"
        }
    }
}
