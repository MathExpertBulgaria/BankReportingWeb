using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BankReportingLibrary.BL.Transaction.Models;

/// <summary>
/// Transactions file model
/// </summary>
[XmlRoot("Operation")]
public class TransactionsFileModel
{
    // File date
    public DateOnly FileDate { get; set; }
    // Transactions
    public List<TransactionFileModel> Transactions { get; set; } = new();
}

/// <summary>
/// Transaction file model
/// </summary>
public class TransactionFileModel
{
    public string? ExternalId { get; set; }
    public string? CreateDate { get; set; }
    public TransactionFileAmount? Amount { get; set; }
    public string? Status { get; set; }
    public TransactionFileParty? Debtor { get; set; }
    public TransactionFileParty? Beneficiary { get; set; }
}

/// <summary>
/// Transaction file amount
/// </summary>
public class TransactionFileAmount
{
    public string? Direction { get; set; }
    public decimal Value { get; set; }
    public string? Currency { get; set; }
}

/// <summary>
/// Transaction file party
/// </summary>
public class TransactionFileParty
{
    public string? BankName { get; set; }
    public string? Bic { get; set; }
    public string? Iban { get; set; }
}