using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AdvanceCRM.Web.Modules.Common.AppServices
{
    /// <summary>
    /// Shared reading / writing of enum columns in Excel sheets, so an export and the import that
    /// reads it back speak the same text. A sheet carries the value the user sees in the grid -
    /// the enum's [Description] - never the number stored in the column.
    /// </summary>
    public static class ExcelEnumHelper
    {
        /// <summary>The text an export writes for an enum value ("Pending"); null stays empty.</summary>
        public static string Text<TEnum>(TEnum? value) where TEnum : struct, Enum
        {
            if (value == null)
                return null;

            var name = Enum.GetName(typeof(TEnum), value.Value);
            if (name == null)
                return Convert.ToInt32(value.Value, CultureInfo.InvariantCulture)
                    .ToString(CultureInfo.InvariantCulture);   // value not in the enum any more

            return DescriptionOf<TEnum>(name) ?? name;
        }

        /// <summary>
        /// Parses a cell back into the enum. Accepts the description, the member name, or the
        /// plain number, all case-insensitively. Returns false only when the cell holds something
        /// that matches none of those - a blank cell is a valid "not set" and returns true with a
        /// null value, leaving the module's own default (e.g. Pending on create) to apply.
        /// </summary>
        public static bool TryParse<TEnum>(string raw, out TEnum? value) where TEnum : struct, Enum
        {
            value = null;
            if (string.IsNullOrWhiteSpace(raw))
                return true;

            raw = raw.Trim();

            foreach (var name in Enum.GetNames(typeof(TEnum)))
            {
                if (string.Equals(name, raw, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(DescriptionOf<TEnum>(name), raw, StringComparison.OrdinalIgnoreCase))
                {
                    value = (TEnum)Enum.Parse(typeof(TEnum), name);
                    return true;
                }
            }

            if (int.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out int number) &&
                Enum.IsDefined(typeof(TEnum), number))
            {
                value = (TEnum)Enum.ToObject(typeof(TEnum), number);
                return true;
            }

            return false;
        }

        /// <summary>Every accepted value, for the error a rejected row reports.</summary>
        public static string ValueList<TEnum>() where TEnum : struct, Enum
        {
            return string.Join(", ", Enum.GetNames(typeof(TEnum))
                .Select(x => DescriptionOf<TEnum>(x) ?? x));
        }

        private static string DescriptionOf<TEnum>(string name) where TEnum : struct, Enum
        {
            return typeof(TEnum).GetField(name)?
                .GetCustomAttribute<DescriptionAttribute>()?.Description;
        }
    }
}
