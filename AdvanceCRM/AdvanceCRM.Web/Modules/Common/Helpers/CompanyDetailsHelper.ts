namespace AdvanceCRM.Administration {
    export namespace CompanyDetailsHelper {
        let syncedCompanyId: number | null = null;

        function getUserCompanyId(): number | null {
            const user = Q.Authorization.userDefinition as Partial<Administration.UserRow> | undefined;
            return user?.CompanyId ?? null;
        }

        export function ensureLookupSync(): void {
            const companyId = getUserCompanyId();
            if (companyId == null || companyId === syncedCompanyId)
                return;

            const lookup = CompanyDetailsRow.getLookup();
            const currentCompany = lookup.itemById[companyId];

            if (!currentCompany) {
                Q.ScriptData.bindToChange('Lookup.Administration.CompanyDetails', () => {
                    syncedCompanyId = null;
                    ensureLookupSync();
                });
                return;
            }

            if (companyId !== 1) {
                lookup.itemById[1] = currentCompany;
            }

            syncedCompanyId = companyId;
        }

        export function getCurrent(): CompanyDetailsRow | undefined {
            ensureLookupSync();

            const companyId = getUserCompanyId();
            if (companyId == null)
                return undefined;

            return CompanyDetailsRow.getLookup().itemById[companyId];
        }
    }
}
