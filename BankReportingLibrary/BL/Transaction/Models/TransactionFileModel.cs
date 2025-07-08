using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace BankReportingLibrary.BL.Transaction.Models;

/// <summary>
/// Transactions file model
/// </summary>
[XmlRoot("Operation")]
public class TransactionsFileModel
{
    [Required(ErrorMessageResourceName = "lFileDateRequired", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    public DateTime FileDate { get; set; }
    // Transactions
    [XmlArray("Transactions"), XmlArrayItem("Transaction")]
    public List<TransactionFileModel> Transactions { get; set; } = new();
}

/// <summary>
/// Transaction file model
/// </summary>
[XmlType("Transaction")]
public class TransactionFileModel
{
    [StringLength(50, ErrorMessageResourceName = "lExternalIdMaxLength", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    public string? ExternalId { get; set; }
    [Required(ErrorMessageResourceName = "lCreateDateRequired", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    public DateTime? CreateDate { get; set; }
    [Required(ErrorMessageResourceName = "lAmountRequired", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    public TransactionFileAmount? Amount { get; set; }
    public bool? Status { get; set; }
    [Required(ErrorMessageResourceName = "lDebtorRequired", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    public TransactionFileParty? Debtor { get; set; }
    [Required(ErrorMessageResourceName = "lBeneficiaryRequired", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    public TransactionFileParty? Beneficiary { get; set; }
}

/// <summary>
/// Transaction file amount
/// </summary>
public class TransactionFileAmount
{
    [Required(ErrorMessageResourceName = "lDirectionRequired", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    public string? Direction { get; set; }
    [Required(ErrorMessageResourceName = "lValueRequired", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    public decimal Value { get; set; }
    [Required(ErrorMessageResourceName = "lCurrencyRequired", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    public string? Currency { get; set; }
}

/// <summary>
/// Transaction file party
/// </summary>
public class TransactionFileParty
{
    public string? BankName { get; set; }
    [XmlElement("BIC")]
    public string? Bic { get; set; }
    [XmlElement("IBAN")]
    [Required(ErrorMessageResourceName = "lIbanRequired", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    [StringLength(22, ErrorMessageResourceName = "lIbanMaxLength", ErrorMessageResourceType = typeof(Res.ImportTransaction))]
    public string? Iban { get; set; }
}