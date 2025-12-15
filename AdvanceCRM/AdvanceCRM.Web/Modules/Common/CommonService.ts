namespace AdvanceCRM.Modules.Common {
    export namespace CommonService {
        export const UploadImage = (request: { FileName: string, Content: Uint8Array }, onSuccess?: (response: Serenity.SaveResponse) => void) => {
            Q.serviceRequest('Common/UploadImage', request, onSuccess);
        };
    }
}
