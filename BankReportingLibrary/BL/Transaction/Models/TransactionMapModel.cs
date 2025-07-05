using CsvHelper.Configuration;
namespace BankReportingLibrary.BL.Transaction.Models;

/// <summary>
/// Transaction map model
/// </summary>
public class TransactionMapModel : ClassMap<TransactionModel>
{
    public TransactionMapModel()
    {
        Map(m => m.Id).Index(0).Name(Res.Transaction.lId);
        Map(m => m.IdMerchant).Ignore();
        Map(m => m.MerchantName).Index(1).Name(Res.Transaction.lMerchantName);
        Map(m => m.CreateDate).Index(2).Name(Res.Transaction.lCreateDate);
        Map(m => m.Amount).Index(3).Name(Res.Transaction.lAmount);
        Map(m => m.Currency).Index(4).Name(Res.Transaction.lCurrency);
        Map(m => m.DebtorIban).Index(5).Name(Res.Transaction.lDebtorIban);
        Map(m => m.BeneficiaryIban).Index(6).Name(Res.Transaction.lBeneficiaryIban);
        Map(m => m.Status).Index(7).Name(Res.Transaction.lStatus);
        Map(m => m.ExternalId).Index(8).Name(Res.Transaction.lExternalid);
    }
}
