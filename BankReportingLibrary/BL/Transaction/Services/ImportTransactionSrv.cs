using BankReportingDb.Context;
using BankReportingLibrary.BL.Models;
using BankReportingLibrary.BL.Transaction.Models;
using BankReportingLibrary.Models.CoreModels;
using BankReportingLibrary.Nomen.Consts;
using BankReportingLibrary.Utils;

namespace BankReportingLibrary.BL.Transaction.Services;

public class ImportTransactionSrv : DbClassRoot
{
    public ImportTransactionSrv(DB Ent) : base(Ent)
    {
    }

    public async Task<DataOperationResult<ResModel<TransactionModel>>> ImportAsync(ImportTransactionModel model,
        CancellationToken ct)
    {
        // Locals
        var res = new DataOperationResult<ResModel<TransactionModel>>();
        res.OperationStatus = OperationStatus.Success;

        string xml = System.Text.Encoding.UTF8.GetString(model.File.Contents);
        var data = xml.FromXML<TransactionsFileModel>();

        // Save file
        // Save transaction
        // Save
        await Db.SaveChangesAsync(ct)
            .ConfigureAwait(false);

        // Return
        return res;
    }
}
