using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using Serenity;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using MyRow = AdvanceCRM.Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow;
using AccountRow = AdvanceCRM.Masters.DemandayMasterAccountRow;
using CampaignRow = AdvanceCRM.Masters.DemandayCampaignIdRow;
using AnswerRow = AdvanceCRM.Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow;
using AdvanceCRM.Web.Helpers;

namespace AdvanceCRM.Masters.Endpoints
{
    [Route("Services/Masters/DemandayTeleMarketingEnquiryCampaignQuestions/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DemandayTeleMarketingEnquiryCampaignQuestionsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _env;

        public DemandayTeleMarketingEnquiryCampaignQuestionsController(ISqlConnections connections, IWebHostEnvironment env)
        {
            _connections = connections;
            _env = env;
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeleMarketingEnquiryCampaignQuestionsSaveHandler handler)
        {
            return handler.Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request,
            [FromServices] IDemandayTeleMarketingEnquiryCampaignQuestionsSaveHandler handler)
        {
            return handler.Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryCampaignQuestionsDeleteHandler handler)
        {
            return handler.Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryCampaignQuestionsRetrieveHandler handler)
        {
            return handler.Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryCampaignQuestionsListHandler handler)
        {
            return handler.List(connection, request);
        }

        public FileContentResult ListExcel(IDbConnection connection, ListRequest request,
            [FromServices] IDemandayTeleMarketingEnquiryCampaignQuestionsListHandler handler,
            [FromServices] IExcelExporter exporter)
        {
            var data = List(connection, request, handler).Entities;
            var bytes = exporter.Export(data, typeof(Columns.DemandayTeleMarketingEnquiryCampaignQuestionsColumns), request.ExportColumns);
            return ExcelContentResult.Create(bytes, "DemandayTeleMarketingEnquiryCampaignQuestionsList_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".xlsx");
        }

        [HttpPost, ServiceAuthorize("Masters:Modify")]
        public ExcelImportResponse ExcelImportQuestionsWithAnswers(IUnitOfWork uow, ExcelImportRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentNullException("filename");

            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/"))
                throw new ArgumentOutOfRangeException("filename");

            ExcelPackage ep = new ExcelPackage();
            using (var fs = new FileStream(UploadHelper.DbFilePath(request.FileName), FileMode.Open, FileAccess.Read))
                ep.Load(fs);

            var response = new ExcelImportResponse();
            response.ErrorList = new List<string>();

            var worksheet = ep.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new ValidationError("Excel file mein koi worksheet nahi hai!");

            // Field references
            var accFields = AccountRow.Fields;
            var campFields = CampaignRow.Fields;
            var qFields = MyRow.Fields;
            var ansFields = AnswerRow.Fields;

            // Dictionary to cache created records and prevent duplication
            var accountCache = new Dictionary<string, int?>();  // AccountNumber -> AccountId
            var campaignCache = new Dictionary<string, int?>();  // "AccountId_CampaignId" -> CampaignId
            var questionCache = new Dictionary<string, int?>();  // "CampaignId_QuestionText" -> QuestionId

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    // Read Excel columns
                    var accountNumber = Convert.ToString(worksheet.Cells[row, 1].Value ?? "").Trim();
                    var campaignIdStr = Convert.ToString(worksheet.Cells[row, 2].Value ?? "").Trim();
                    var questionText = Convert.ToString(worksheet.Cells[row, 3].Value ?? "").Trim();
                    var answerText = Convert.ToString(worksheet.Cells[row, 4].Value ?? "").Trim();

                    // Validation
                    if (string.IsNullOrWhiteSpace(accountNumber))
                        continue;
                    if (string.IsNullOrWhiteSpace(campaignIdStr))
                        throw new ValidationError($"Row {row}: Campaign ID khali hai!");
                    if (string.IsNullOrWhiteSpace(questionText))
                        throw new ValidationError($"Row {row}: Question Text khali hai!");
                    if (string.IsNullOrWhiteSpace(answerText))
                        throw new ValidationError($"Row {row}: Answer Text khali hai!");

                    // Step 1: Get or Create Master Account
                    int? accountId = null;
                    string accountCacheKey = accountNumber;

                    if (!accountCache.TryGetValue(accountCacheKey, out accountId))
                    {
                        var existingAccount = uow.Connection.TryFirst<AccountRow>(q => q
                            .Select(accFields.Id)
                            .Where(accFields.AccountNumber == accountNumber));

                        if (existingAccount == null)
                        {
                            var newAccount = new AccountRow { AccountNumber = accountNumber };
                            var saveResp = uow.Connection.InsertAndGetID(newAccount);
                            accountId = (int?)saveResp;
                        }
                        else
                        {
                            accountId = existingAccount.Id;
                        }

                        accountCache[accountCacheKey] = accountId;
                    }

                    if (accountId == null || accountId <= 0)
                        throw new ValidationError($"Row {row}: Account ID create nahi ho saki!");

                    // Step 2: Get or Create Campaign under Account
                    int? campaignRowId = null;
                    string campaignCacheKey = $"{accountId}_{campaignIdStr}";

                    if (!campaignCache.TryGetValue(campaignCacheKey, out campaignRowId))
                    {
                        var existingCampaign = uow.Connection.TryFirst<CampaignRow>(q => q
                            .Select(campFields.Id)
                            .Where(campFields.CampaignId == campaignIdStr &&
                                   campFields.DemandayMasterAccountId == (int)accountId));

                        if (existingCampaign == null)
                        {
                            var newCampaign = new CampaignRow
                            {
                                CampaignId = campaignIdStr,
                                DemandayMasterAccountId = accountId
                            };
                            var saveResp = uow.Connection.InsertAndGetID(newCampaign);
                            campaignRowId = (int?)saveResp;
                        }
                        else
                        {
                            campaignRowId = existingCampaign.Id;
                        }

                        campaignCache[campaignCacheKey] = campaignRowId;
                    }

                    if (campaignRowId == null || campaignRowId <= 0)
                        throw new ValidationError($"Row {row}: Campaign ID create nahi ho saki!");

                    // Step 3: Get or Create Question
                    int? questionId = null;
                    string questionCacheKey = $"{campaignRowId}_{questionText}";

                    if (!questionCache.TryGetValue(questionCacheKey, out questionId))
                    {
                        var existingQuestion = uow.Connection.TryFirst<MyRow>(q => q
                            .Select(qFields.Id)
                            .Where(qFields.QuestionText == questionText &&
                                   qFields.CampaignId == (int)campaignRowId));

                        if (existingQuestion == null)
                        {
                            var newQuestion = new MyRow
                            {
                                QuestionText = questionText,
                                CampaignId = (int)campaignRowId
                            };
                            var saveResp = uow.Connection.InsertAndGetID(newQuestion);
                            questionId = (int?)saveResp;
                        }
                        else
                        {
                            questionId = existingQuestion.Id;
                        }

                        questionCache[questionCacheKey] = questionId;
                    }

                    if (questionId == null || questionId <= 0)
                        throw new ValidationError($"Row {row}: Question ID create nahi ho saki!");

                    // Step 4: Create Answer (always create new)
                    var newAnswer = new AnswerRow
                    {
                        CampaignId = campaignRowId,
                        QuestionId = questionId,
                        AnswerText = answerText
                    };

                    uow.Connection.Insert(newAnswer);
                    response.Inserted++;
                }
                catch (Exception ex)
                {
                    response.ErrorList.Add($"Row {row}: {ex.Message}");
                }
            }

            return response;
        }

        [HttpGet, ServiceAuthorize("Masters:Read")]
        public ActionResult DownloadTemplate()
        {
            var ep = new ExcelPackage();
            var worksheet = ep.Workbook.Worksheets.Add("QuestionsAnswers");

            worksheet.Cells[1, 1].Value = "Account Number";
            worksheet.Cells[1, 2].Value = "Campaign ID";
            worksheet.Cells[1, 3].Value = "Question Text";
            worksheet.Cells[1, 4].Value = "Answer Text";

            worksheet.Cells[2, 1].Value = "ACC001";
            worksheet.Cells[2, 2].Value = "CAMP2024";
            worksheet.Cells[2, 3].Value = "Aapka naam kya hai?";
            worksheet.Cells[2, 4].Value = "Answer 1.1";

            worksheet.Cells[3, 1].Value = "ACC001";
            worksheet.Cells[3, 2].Value = "CAMP2024";
            worksheet.Cells[3, 3].Value = "Aapka naam kya hai?";
            worksheet.Cells[3, 4].Value = "Answer 1.2";

            worksheet.Cells[4, 1].Value = "ACC001";
            worksheet.Cells[4, 2].Value = "CAMP2024";
            worksheet.Cells[4, 3].Value = "Aapka age kya hai?";
            worksheet.Cells[4, 4].Value = "Answer 2.1";

            for (int col = 1; col <= 4; col++)
            {
                worksheet.Cells[1, col].Style.Font.Bold = true;
                worksheet.Cells[1, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[1, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            worksheet.Column(1).Width = 20;
            worksheet.Column(2).Width = 20;
            worksheet.Column(3).Width = 30;
            worksheet.Column(4).Width = 30;

            var bytes = ep.GetAsByteArray();
            var Output = File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet,
                "QuestionsAnswers_Template_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
            return Output;
        }
    }
}