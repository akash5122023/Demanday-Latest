/// <reference path="BulkServiceAction.ts" />

namespace AdvanceCRM.Common {
    @Serenity.Decorators.registerClass()

    export class BulkDeleteAction extends BulkServiceAction {

        private service: any;
        constructor(srv) {
            super();
            this.service = srv;
        }

        protected executeForBatch(batch) {

            Q.serviceRequest(
                this.service + '/Delete',
                {
                    EntityId: Q.parseInteger(batch[0])
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