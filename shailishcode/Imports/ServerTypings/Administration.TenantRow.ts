namespace AdvanceCRM.Administration {
    export interface TenantRow {
        TenantId?: number;
        Name?: string;
        Subdomain?: string;
        DbName?: string;
        Port?: number;
        Plan?: string;
        Modules?: string;
        LicenseStartDate?: string;
        LicenseEndDate?: string;
        DnsStatus?: string;
    }

    export namespace TenantRow {
        export const idProperty = 'TenantId';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Administration.Tenant';
        export const lookupKey = 'Administration.Tenant';

        export function getLookup(): Q.Lookup<TenantRow> {
            return Q.getLookup<TenantRow>('Administration.Tenant');
        }
        export const deletePermission = 'Administration:Security';
        export const insertPermission = 'Administration:Security';
        export const readPermission = 'Administration:Security';
        export const updatePermission = 'Administration:Security';

        export declare const enum Fields {
            TenantId = "TenantId",
            Name = "Name",
            Subdomain = "Subdomain",
            DbName = "DbName",
            Port = "Port",
            Plan = "Plan",
            Modules = "Modules",
            LicenseStartDate = "LicenseStartDate",
            LicenseEndDate = "LicenseEndDate",
            DnsStatus = "DnsStatus"
        }
    }
}
