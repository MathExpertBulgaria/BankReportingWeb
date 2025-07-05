using BankReportingDb.Context;
using BankReportingLibrary.BL.Models;
using BankReportingLibrary.BL.Transaction.Models;
using BankReportingLibrary.Models.CoreModels;
using BankReportingLibrary.Utils;

namespace BankReportingLibrary.BL.Transaction.Services;

public class ImportTransactionSrv : DbClassRoot
{
    public ImportTransactionSrv(DB Ent) : base(Ent)
    {
    }

    public Task<DataOperationResult<ResModel<TransactionModel>>> ImportAsync(ImportTransactionModel model)
    {
        string xml = System.Text.Encoding.UTF8.GetString(model.File.Contents);
        var data = xml.FromXML<TransactionsFileModel>();
    }
}
