namespace AdvanceCRM.Masters {
    export enum AppointmentTypeMaster {
        Open = 1,
        Appointment = 2,
        NotInterested = 3,
        Interested = 4,
        Pending = 5
    }
    Serenity.Decorators.registerEnumType(AppointmentTypeMaster, 'AdvanceCRM.Masters.AppointmentTypeMaster', 'Masters.AppointmentType');
}
