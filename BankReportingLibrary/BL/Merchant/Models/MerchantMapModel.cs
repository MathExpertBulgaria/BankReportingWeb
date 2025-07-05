using CsvHelper.Configuration;

namespace BankReportingLibrary.BL.Merchant.Models;

/// <summary>
/// Merchant map model
/// </summary>
public class MerchantMapModel : ClassMap<MerchantModel>
{
    public MerchantMapModel()
    {
        Map(m => m.Id).Index(0).Name(Res.Merchant.lId);
        Map(m => m.Name).Index(1).Name(Res.Merchant.lName);
        Map(m => m.IdPartner).Ignore();
        Map(m => m.PartnerName).Index(2).Name(Res.Merchant.lPartnerName);
        Map(m => m.BoardingDate).Index(3).Name(Res.Merchant.lBoardingDate);
        Map(m => m.Url).Index(4).Name(Res.Merchant.lUrl);
        Map(m => m.Country).Index(5).Name(Res.Merchant.lCountry);
        Map(m => m.Address1).Index(6).Name(Res.Merchant.lAddress1);
        Map(m => m.Address2).Index(7).Name(Res.Merchant.lAddress2);
    }
}
