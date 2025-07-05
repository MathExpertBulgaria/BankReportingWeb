using BankReportingLibrary.BL.Models;

namespace BankReportingLibrary.BL.Transaction.Models;

/// <summary>
/// Import transaction model
/// </summary>
public class ImportTransactionModel
{
    // File
    public DownloadFileModel? File { get; set; }
    // Search
    public SearchTransactionModel? Search { get; set; }
}
