namespace BankReportingLibrary.BL.Transaction.Models;

/// <summary>
/// Transaction model
/// </summary>
public class TransactionModel
{
    public decimal Id { get; set; }

    public decimal IdMerchant { get; set; }

    public string? MerchantName { get; set; }

    public DateTime CreateDate { get; set; }

    public string? Direction { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    public string? DebtorIban { get; set; }

    public string? BeneficiaryIban { get; set; }

    public string? Status { get; set; }

    public string? ExternalId { get; set; }
    public decimal? IdTransactionFile { get; set; }
}
