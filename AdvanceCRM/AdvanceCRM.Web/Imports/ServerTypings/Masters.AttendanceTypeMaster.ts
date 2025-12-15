namespace AdvanceCRM.Masters {
    export enum AttendanceTypeMaster {
        Present = 1,
        Absent = 2,
        HalfDay = 3
    }
    Serenity.Decorators.registerEnumType(AttendanceTypeMaster, 'AdvanceCRM.Masters.AttendanceTypeMaster', 'Masters.AttendanceType');
}
