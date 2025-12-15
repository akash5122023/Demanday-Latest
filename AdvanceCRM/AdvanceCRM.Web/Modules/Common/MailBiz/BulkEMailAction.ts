
namespace AdvanceCRM.Common {
    @Serenity.Decorators.registerClass()
    export class BulkEmailAction extends BulkServiceAction {

        private service: any;
        private message: string;
        private subject: string;
        private date: Date;
        constructor(srv, msg,sbj,dt) {
            super();
            this.service = srv;
            this.message = msg;
            this.subject = sbj;
            this.date = dt;
        }

        protected executeForBatch(batch) {

            Q.serviceRequest(
                this.service + '/SendBulkMail',
                {
                    Id: Q.parseInteger(batch[0]), EmailType: this.message, Subject: this.subject, Senddate: this.date
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