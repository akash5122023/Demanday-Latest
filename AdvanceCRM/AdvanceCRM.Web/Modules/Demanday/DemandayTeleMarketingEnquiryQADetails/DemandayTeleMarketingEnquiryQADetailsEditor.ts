/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class DemandayTeleMarketingEnquiryQADetailsEditor extends Common.GridEditorBase<DemandayTeleMarketingEnquiryQADetailsRow> {
        protected getColumnsKey() { return 'Demanday.DemandayTeleMarketingEnquiryQADetails'; }
        protected getDialogType() { return DemandayTeleMarketingEnquiryQADetailsEditorDialog; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryQADetailsRow.localTextPrefix; }

        private campaignId: string = null;

        constructor(container: JQuery) {
            super(container);
        }

        protected editItem(entityOrId: any): void {
            var id = entityOrId;
            var item = this.view.getItemById(id);
            if (item && !item.CampaignId && this.campaignId) {
                item.CampaignId = this.campaignId;
            }
            super.editItem(entityOrId);
        }

        protected enableDeleteColumn() {
            return true;
        }

        public setReadOnly(value: boolean) {
            // Keep QADetails always editable as requested
        }

        public set_readOnly(value: boolean) {
            // Keep QADetails always editable as requested
        }

        public setCampaignId(value: string): void {
            this.campaignId = value || null;
        }

        protected getNewEntity(): DemandayTeleMarketingEnquiryQADetailsRow {
            var entity = super.getNewEntity() as DemandayTeleMarketingEnquiryQADetailsRow;
            if (this.campaignId) {
                entity.CampaignId = this.campaignId;
            }
            return entity;
        }

        validateEntity(row: DemandayTeleMarketingEnquiryQADetailsRow, id) {
            row.QuestionId = Q.toId(row.QuestionId);
            row.AnswerId = Q.toId(row.AnswerId);

            if (!row.CampaignId && this.campaignId) {
                row.CampaignId = this.campaignId;
            }

            var sameQuestion = Q.tryFirst(this.view.getItems(), x => x.QuestionId === row.QuestionId);
            if (sameQuestion && this.id(sameQuestion) !== id) {
                Q.alert('This question is already added!');
                return false;
            }

            var questionItem = Q.getLookup<Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow>(
                'Masters.DemandayTeleMarketingEnquiryCampaignQuestions').itemById[row.QuestionId];
            row.QuestionText = questionItem ? questionItem.QuestionText : null;

            var answerItem = Q.getLookup<Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow>(
                'Masters.DemandayTeleMarketingEnquiryQuestionAnswers').itemById[row.AnswerId];
            row.AnswerText = answerItem ? answerItem.AnswerText : null;

            return true;
        }
    }
}
