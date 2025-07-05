using BankReportingLibrary.BL.Partner.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

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

        using var memoryStream = new MemoryStream();
        using (var writer = new StreamWriter(memoryStream))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            // Map
            csv.Context.RegisterClassMap(new M());

            // Header
            csv.WriteHeader<T>();
            await csv.NextRecordAsync();

            // Data
            await csv.WriteRecordsAsync(model);

            // Set
            res = memoryStream.ToArray();
        }

        // Return
        return res;
    }
}