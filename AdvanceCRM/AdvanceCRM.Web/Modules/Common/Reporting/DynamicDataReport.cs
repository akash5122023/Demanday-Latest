using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.PropertyGrid;
using Serenity.Localization;
using AdvanceCRM.Web.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serenity.Extensions.DependencyInjection;

namespace Serenity.Reporting
{
    public class DynamicDataReport : IDataOnlyReport
    {
        public IEnumerable Data { get; private set; }
        public IEnumerable<string> ColumnList { get; set; }
        public Type ColumnsType { get; private set; }

        public DynamicDataReport(IEnumerable data, IEnumerable<string> columnList, Type columnsType)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            this.Data = data;
            this.ColumnList = columnList ?? new List<string>();
            this.ColumnsType = columnsType;
        }

        public object GetData()
        {
            return Data;
        }

        public List<ReportColumn> GetColumnList()
        {
            var list = new List<ReportColumn>();

            IList<PropertyItem> propertyItemsList = null;
            IDictionary<string, PropertyItem> propertyItems = null;
            IRow basedOnRow = null;

            if (ColumnsType != null)
            {
                propertyItemsList = LocalCache.Get("DynamicDataReport:ColumnsList:" + ColumnsType.FullName, TimeSpan.Zero,
                    () => PropertyItemHelper.GetPropertyItemsFor(ColumnsType));

                propertyItems = propertyItemsList.ToDictionary(x => x.Name);

                var basedOnAttr = ColumnsType.GetCustomAttribute<BasedOnRowAttribute>();
                if (basedOnAttr != null &&
                    basedOnAttr.RowType != null &&
                    typeof(IRow).IsAssignableFrom(basedOnAttr.RowType))
                {
                    basedOnRow = (IRow)Activator.CreateInstance(basedOnAttr.RowType);
                }
            }

            propertyItems = propertyItems ?? new Dictionary<string, PropertyItem>();

            IEnumerable<string> columnNames = ColumnList;

            if (!columnNames.Any())
            {
                if (propertyItemsList != null && propertyItemsList.Count > 0)
                {
                    columnNames = propertyItemsList
                        .Where(x => x.Visible.GetValueOrDefault(true) && !string.IsNullOrEmpty(x.Name))
                        .Select(x => x.Name);
                }
                else if (basedOnRow != null)
                {
                    columnNames = basedOnRow.GetFields().Select(x => x.PropertyName ?? x.Name);
                }
                else
                {
                    columnNames = Array.Empty<string>();
                }
            }

            foreach (var columnName in columnNames)
            {
                PropertyItem item;
                if (!propertyItems.TryGetValue(columnName, out item))
                {
                    if (basedOnRow != null)
                    {
                        var field = basedOnRow.FindFieldByPropertyName(columnName) ?? basedOnRow.FindField(columnName);
                        if (!ReferenceEquals(null, field))
                        {
                            item = new PropertyItem
                            {
                                Name = columnName,
                                // Field.Title removed in newer Serenity versions; fall back to property/name
                                Title = field.PropertyName ?? field.Name
                            };

                            propertyItems[columnName] = item;
                        }
                    }

                    if (item == null)
                        continue;
                }

                var basedOnField = ReferenceEquals(null, basedOnRow) ? (Field)null :
                    (basedOnRow.FindField(columnName) ?? basedOnRow.FindFieldByPropertyName(columnName));

                list.Add(FromPropertyItem(item, basedOnField));
            }

            return list;
        }

        private ReportColumn FromPropertyItem(PropertyItem item, Field field)
        {
            var result = new ReportColumn();
            result.Name = item.Name;
            result.Title = item.Title ?? item.Name;
            if (result.Title != null)
                result.Title = Dependency.Resolve<ITextLocalizer>().TryGet(result.Title) ?? result.Title;

            if (item.Width != null)
                result.Width = item.Width;

            if (!string.IsNullOrWhiteSpace(item.DisplayFormat))
                result.Format = item.DisplayFormat;
            else
            {
                var dtf = field as DateTimeField;
                if (!ReferenceEquals(null, dtf) &&
                    dtf.DateTimeKind != DateTimeKind.Unspecified)
                {
                    result.Format = "dd/MM/yyyy HH:mm";
                }
                else if (!ReferenceEquals(null, dtf))
                {
                    result.Format = "dd/MM/yyyy";
                }
            }

            var enumField = field as IEnumTypeField;
            if (!ReferenceEquals(null, enumField) && enumField.EnumType != null)
            {
                result.Decorator = new EnumDecorator(enumField.EnumType, Dependency.Resolve<ITextLocalizer>());
            }

            if (!ReferenceEquals(null, field))
            {
                if (result.Title == null)
                    result.Title = field.PropertyName ?? field.Name;

                if (result.Width == null && field is StringField && field.Size != 0)
                    result.Width = field.Size;
            }

            result.DataType = !ReferenceEquals(null, field) ? field.ValueType : null;

            return result;
        }
    }
}