using BankReportingLibrary.BL.Models;

namespace BankReportingLibrary.BL.Transaction.Models;

/// <summary>
/// Search model
/// </summary>
public class SearchTransactionModel : SearchModel
{
    public decimal? IdMerchant { get; set; }
    public DateTime? CreateDateFrom { get; set; }
    public DateTime? CreateDateTo { get; set; }
    public string? IdDirection { get; set; }
    public decimal? AmountFrom { get; set; }
    public decimal? AmountTo { get; set; }
    public string? IdCcy { get; set; }
    public string? DebtorIban { get; set; }
    public string? BeneficiaryIban { get; set; }
    public bool? Status { get; set; }
    public string? ExternalId { get; set; }

    // Nomen
    public TransactionNomenModel? Nomen { get; set; }

}