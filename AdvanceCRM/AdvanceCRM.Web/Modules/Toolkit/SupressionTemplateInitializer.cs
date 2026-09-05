using OfficeOpenXml;
using System;
using System.IO;

namespace AdvanceCRM.Toolkit
{
    /// <summary>
    /// Builds the Master / Client Suppression import templates.
    ///
    /// Client Suppression is imported campaign-wise: the Campaign is chosen in the dialog and
    /// the sheet carries no identifying column.
    ///
    /// Master Suppression is imported account-wise and can hold a lot of rows spanning several
    /// accounts, so it keeps an "Account Number" column. A blank cell falls back to the Master
    /// Account picked in the dialog.
    ///
    /// Because old template files are still on disk, existence alone is not enough to consider a
    /// template current — the header row is compared and the file is rewritten when it differs.
    /// </summary>
    public static class SupressionTemplateInitializer
    {
        public static readonly string[] MasterSupressionHeaders =
        {
            "Sr No",          // 1 – upsert key; blank gets the next number
            "Account Number", // 2 – blank falls back to the dialog's Master Account
            "Campaign Id",    // 3 – optional; blank leaves the row unlinked to a Campaign
            "Company Name",   // 4
            "First Name",     // 5
            "Last Name",      // 6
            "Email",          // 7
            "Domain",         // 8
            "Date"            // 9
        };

        public static readonly string[] ClientSupressionHeaders =
        {
            "Sr No",              // 1 – upsert key; blank gets the next number
            "Master Account Id",  // 2 – blank falls back to the dialog's Campaign
            "Campaign Id",        // 3 – blank falls back to the dialog's Campaign
            "Company Name",       // 4
            "First Name",         // 5
            "Last Name",          // 6
            "Email",              // 7
            "Domain",             // 8
            "Date"                // 9
        };

        public static void EnsureTemplateExists(string templatePath, string sheetName, string[] headers)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                if (File.Exists(templatePath) && HasExpectedHeaders(templatePath, headers))
                    return;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(sheetName);

                    for (int col = 1; col <= headers.Length; col++)
                    {
                        worksheet.Cells[1, col].Value = headers[col - 1];
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                        worksheet.Column(col).Width = 22;
                    }

                    Directory.CreateDirectory(Path.GetDirectoryName(templatePath));

                    // SaveAs will not overwrite in place, so drop the stale template first.
                    if (File.Exists(templatePath))
                        File.Delete(templatePath);

                    package.SaveAs(new FileInfo(templatePath));
                }
            }
            catch (Exception ex)
            {
                // Never break the import screen over a template; it can be recreated by hand.
                System.Diagnostics.Debug.WriteLine(
                    $"Error initializing suppression template '{templatePath}': {ex.Message}");
            }
        }

        private static bool HasExpectedHeaders(string templatePath, string[] headers)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(templatePath)))
                {
                    var worksheet = package.Workbook.Worksheets.Count > 0
                        ? package.Workbook.Worksheets[0]
                        : null;

                    if (worksheet?.Dimension == null)
                        return false;

                    // An extra trailing column means an older layout, so require an exact width.
                    if (worksheet.Dimension.End.Column != headers.Length)
                        return false;

                    for (int col = 1; col <= headers.Length; col++)
                    {
                        var actual = Convert.ToString(worksheet.Cells[1, col].Value ?? "").Trim();
                        if (!string.Equals(actual, headers[col - 1], StringComparison.OrdinalIgnoreCase))
                            return false;
                    }

                    return true;
                }
            }
            catch
            {
                // Unreadable / corrupt file – treat it as needing a rewrite.
                return false;
            }
        }
    }
}
