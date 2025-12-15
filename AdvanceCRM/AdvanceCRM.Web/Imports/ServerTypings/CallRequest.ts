namespace AdvanceCRM {
    export interface CallRequest extends Serenity.ServiceRequest {
        IVRNumber?: string;
        AgentNumber?: number;
        CustomerNumber?: string;
    }
}
