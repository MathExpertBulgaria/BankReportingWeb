namespace BankReportingLibrary.BL.Merchant.Models;

/// <summary>
/// Merchant model
/// </summary>
public class MerchantModel
{
    public decimal Id { get; set; }

    public decimal IdPartner { get; set; }
    public string? PartnerName { get; set; }

    public string? Name { get; set; }

    public DateTime? BoardingDate { get; set; }

    public string? Url { get; set; }

    public string? Country { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }
}
