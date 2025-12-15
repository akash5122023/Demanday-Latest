/// <reference path="../Common/UserPreference/UserPreferenceStorage.ts" />

namespace AdvanceCRM.ScriptInitialization {
    Q.Config.responsiveDialogs = true;
    Q.Config.rootNamespaces.push('AdvanceCRM');
    Q.Config.rootNamespaces.push('_Ext');
    Serenity.DataGrid.defaultPersistanceStorage = window.localStorage;
    if (Administration?.CompanyDetailsHelper?.ensureLookupSync) {
        Administration.CompanyDetailsHelper.ensureLookupSync();
    }
}