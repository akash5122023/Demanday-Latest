using OfficeOpenXml;
using OfficeOpenXml.Style;
using Serenity.Reporting;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace AdvanceCRM.Web.Modules.Common.Reporting
{
    /// <summary>
    /// EPPlus based implementation of Serenity's <see cref="IExcelExporter"/>.
    /// The concrete exporter normally ships in the Serenity.Extensions package, which this
    /// project does not reference, so the grid "Export to Excel" action (used by every Toolkit
    /// module and others) failed at runtime with
    /// "No service for type 'Serenity.Reporting.IExcelExporter' has been registered.".
    /// This restores that service using the EPPlus package already referenced by the project.
    /// Registered in Startup.ConfigureServices.
    /// </summary>
    public class ExcelExporter : IExcelExporter
    {
        private readonly IServiceProvider provider;
        private readonly IDataReportExcelRenderer renderer;

        public ExcelExporter(IServiceProvider provider, IDataReportExcelRenderer renderer)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public byte[] Export(IEnumerable data, IEnumerable<ReportColumn> columns)
        {
            return renderer.Render(new TabularDataReport(data, columns));
        }

        public byte[] Export(IEnumerable data, Type columnsType)
        {
            return Export(data, columnsType, null);
        }

        public byte[] Export(IEnumerable data, Type columnsType, IEnumerable<string> exportColumns)
        {
            return renderer.Render(new TabularDataReport(data, columnsType, exportColumns, provider));
        }
    }

    /// <summary>
    /// EPPlus based implementation of <see cref="IDataReportExcelRenderer"/>. Renders the column
    /// list and data of a tabular (data only) report to an .xlsx byte array.
    /// </summary>
    public class ExcelDataReportRenderer : IDataReportExcelRenderer
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> propertyCache = new();

        public byte[] Render(IDataOnlyReport report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var columns = report.GetColumnList() ?? new List<ReportColumn>();
            var data = GetData(report);

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Sheet1");

            // Header row
            for (int i = 0; i < columns.Count; i++)
            {
                var cell = ws.Cells[1, i + 1];
                cell.Value = columns[i].Title ?? columns[i].Name;
                cell.Style.Font.Bold = true;
            }

            int rowIndex = 2;
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (item != null)
                    {
                        var props = GetProperties(item.GetType());
                        for (int c = 0; c < columns.Count; c++)
                        {
                            var name = columns[c].Name;
                            if (string.IsNullOrEmpty(name))
                                continue;

                            object value = null;
                            if (props.TryGetValue(name, out var pi))
                                value = pi.GetValue(item);

                            WriteValue(ws.Cells[rowIndex, c + 1], value);
                        }
                    }
                    rowIndex++;
                }
            }

            if (ws.Dimension != null)
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }

        private static void WriteValue(ExcelRange cell, object value)
        {
            if (value == null)
                return;

            switch (value)
            {
                case DateTime dt:
                    cell.Value = dt;
                    cell.Style.Numberformat.Format = dt.TimeOfDay == TimeSpan.Zero
                        ? "dd-mm-yyyy"
                        : "dd-mm-yyyy hh:mm";
                    break;
                case bool b:
                    cell.Value = b ? "Yes" : "No";
                    break;
                case Enum e:
                    cell.Value = e.ToString();
                    break;
                default:
                    cell.Value = value;
                    break;
            }
        }

        private static Dictionary<string, PropertyInfo> GetProperties(Type type)
        {
            return propertyCache.GetOrAdd(type, t =>
            {
                var dict = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
                foreach (var pi in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (pi.GetIndexParameters().Length == 0 && pi.CanRead)
                        dict[pi.Name] = pi;
                }
                return dict;
            });
        }

        private static IEnumerable GetData(IDataOnlyReport report)
        {
            // IDataOnlyReport itself only exposes GetColumnList(); the data is exposed by the
            // concrete report (e.g. TabularDataReport.GetData()). Resolve it reflectively so any
            // IDataOnlyReport implementation works.
            var method = report.GetType().GetMethod("GetData", Type.EmptyTypes);
            var result = method?.Invoke(report, null);
            return result as IEnumerable;
        }
    }
}
