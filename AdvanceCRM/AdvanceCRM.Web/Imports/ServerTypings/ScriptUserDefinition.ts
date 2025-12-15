namespace AdvanceCRM {
    export interface ScriptUserDefinition {
        UserId?: number;
        Username?: string;
        DisplayName?: string;
        Phone?: string;
        IsAdmin?: boolean;
        Permissions?: { [key: string]: boolean };
        UpperLevel?: number;
        BranchId?: number;
        CompanyId?: number;
    }
}
