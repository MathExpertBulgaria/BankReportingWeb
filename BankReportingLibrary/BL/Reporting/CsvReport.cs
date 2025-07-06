using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace BankReportingLibrary.BL.Reporting;

/// <summary>
/// Csv report
/// </summary>
public class CsvReport
{
    /// <summary>
    /// Generate report
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<byte[]> GenerateReportAsync<T, M>(List<T> model) where M : ClassMap, new()
    {
        // Locals
        byte[] res;

        using (var sw = new StringWriter())
        using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
        {
            // Map
            csv.Context.RegisterClassMap(new M());

            // Header
            csv.WriteHeader<T>();
            await csv.NextRecordAsync();

            // Data
            await csv.WriteRecordsAsync(model);

            res = Encoding.UTF8.GetBytes(sw.ToString());
        }

        // Return
        return res;
    }
}