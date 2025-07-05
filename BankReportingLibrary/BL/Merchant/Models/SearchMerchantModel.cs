using BankReportingLibrary.BL.Models;

namespace BankReportingLibrary.BL.Merchant.Models;

/// <summary>
/// Search model
/// </summary>
public class SearchMerchantModel : SearchModel
{
    public decimal IdPartner { get; set; }
    public string? Name { get; set; }
    public DateTime? BoardingDateFrom { get; set; }
    public DateTime? BoardingDateTo { get; set; }
    public string? Country { get; set; }
}