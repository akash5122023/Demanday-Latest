namespace AdvanceCRM
{
    using Serenity;
    using Serenity.PropertyGrid;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Localization;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Serenity.Extensions.DependencyInjection;

    public class ReportRepository
    {
        private readonly IReportRegistry _reportRegistry;
        private readonly ITextLocalizer _localizer;

        public ReportRepository(IReportRegistry reportRegistry, ITextLocalizer localizer)
        {
            _reportRegistry = reportRegistry ?? throw new ArgumentNullException(nameof(reportRegistry));
            _localizer = localizer ?? NullTextLocalizer.Instance;
        }

        public ReportRepository()
            : this(Dependency.Resolve<IReportRegistry>(), Dependency.Resolve<ITextLocalizer>())
        {
        }

        public byte[] Render(IDataOnlyReport report)
        {
            var columns = report.GetColumnList();

            var data = new List<object>();
            var input = report.GetData();
            var list = (input as IEnumerable) ?? new List<object> { input };
            foreach (var item in list)
                data.Add(item);

            return ExcelReportGenerator.GeneratePackageBytes(columns, data);
        }

        public ReportTree GetReportTree(string category)
        {
            var reports = _reportRegistry.GetAvailableReportsInCategory(category);
            return ReportTree.FromList(reports, category);
        }

        public ReportRetrieveResponse Retrieve(ReportRetrieveRequest request)
        {
            Check.NotNull(request, "request");

            if (request.ReportKey.IsEmptyOrNull())
                throw new ArgumentNullException("reportKey");

            var reportInfo = _reportRegistry.GetReport(request.ReportKey);
            if (reportInfo == null)
                throw new ArgumentOutOfRangeException("reportKey");

            if (reportInfo.Permission != null)
                Authorization.ValidatePermission(reportInfo.Permission);

            var response = new ReportRetrieveResponse();

            response.Properties = PropertyItemHelper.GetPropertyItemsFor(reportInfo.Type);
            response.ReportKey = reportInfo.Key;
            response.Title = reportInfo.Title;
            var reportInstance = Activator.CreateInstance(reportInfo.Type);
            response.InitialSettings = reportInstance;
            response.IsDataOnlyReport = reportInstance is IDataOnlyReport;

            return response;
        }
    }
}