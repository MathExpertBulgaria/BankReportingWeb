using CsvHelper.Configuration;

namespace BankReportingLibrary.BL.Partner.Models;

/// <summary>
/// Partner map model
/// </summary>
public class PartnerMapModel : ClassMap<PartnerModel>
{
    public PartnerMapModel()
    {
        Map(m => m.Id).Index(0).Name(Res.Partner.lId);
        Map(m => m.Name).Index(1).Name(Res.Partner.lName);
    }
}