
namespace AdvanceCRM.Common {
    @Serenity.Decorators.registerClass()
    export class BulkSMSAction extends BulkServiceAction {

        private service: any;
        private message: string;
        constructor(srv, msg) {
            super();
            this.service = srv;
            this.message = msg;
        }

        protected executeForBatch(batch) {

            Q.serviceRequest(
                this.service + '/SendBulkSMS',
                {
                    Id: Q.parseInteger(batch[0]), SMSType: this.message
                },
                response => { this.set_successCount(this.get_successCount() + batch.length) },
                {
                    blockUI: false,
                    onError: response => this.set_errorCount(this.get_errorCount() + batch.length),
                    onCleanup: () => this.serviceCallCleanup()
                });
        }
    }
}