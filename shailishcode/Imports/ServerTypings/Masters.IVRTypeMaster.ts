namespace AdvanceCRM.Masters {
    export enum IVRTypeMaster {
        Knowlarity = 1,
        TeleCMI = 2,
        way2voice = 3,
        Cloud_Connect = 4
    }
    Serenity.Decorators.registerEnumType(IVRTypeMaster, 'AdvanceCRM.Masters.IVRTypeMaster', 'Masters.IVRType');
}
