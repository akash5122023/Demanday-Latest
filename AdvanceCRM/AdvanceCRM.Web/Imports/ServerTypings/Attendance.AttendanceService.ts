namespace AdvanceCRM.Attendance {
    export namespace AttendanceService {
        export const baseUrl = 'Attendance';

        export declare function Create(request: Serenity.SaveRequest<AttendanceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<AttendanceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<AttendanceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<AttendanceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function PunchIn(request: Serenity.SaveRequest<AttendanceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function PunchOut(request: Serenity.SaveRequest<AttendanceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Approve(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Attendance/Create",
            Update = "Attendance/Update",
            Delete = "Attendance/Delete",
            Retrieve = "Attendance/Retrieve",
            List = "Attendance/List",
            PunchIn = "Attendance/PunchIn",
            PunchOut = "Attendance/PunchOut",
            Approve = "Attendance/Approve"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'PunchIn', 
            'PunchOut', 
            'Approve'
        ].forEach(x => {
            (<any>AttendanceService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
