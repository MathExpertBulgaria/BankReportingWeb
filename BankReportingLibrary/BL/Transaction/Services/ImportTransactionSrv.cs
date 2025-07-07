using BankReportingDb.Context;
using BankReportingDb.Models;
using BankReportingLibrary.BL.Models;
using BankReportingLibrary.BL.Transaction.Models;
using BankReportingLibrary.Models.CoreModels;
using BankReportingLibrary.Nomen.Consts;
using BankReportingLibrary.Utils;
using Microsoft.EntityFrameworkCore;

namespace BankReportingLibrary.BL.Transaction.Services;

/// <summary>
/// Import transaction
/// </summary>
public class ImportTransactionSrv : DbClassRoot
{
    // Services
    private readonly Lazy<TransactionNomenSrv> _nomenSrv;

    public ImportTransactionSrv(DB Ent, 
        // Services
        Lazy<TransactionNomenSrv> nomenSrv) : base(Ent)
    {
        // Services
        _nomenSrv = nomenSrv;
    }

    /// <summary>
    /// Import
    /// </summary>
    /// <param name="model"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<DataOperationResult<ResModel<TransactionModel>>> ImportAsync(ImportTransactionModel model,
        CancellationToken ct)
    {
        // Locals
        var res = new DataOperationResult<ResModel<TransactionModel>>();
        res.OperationStatus = OperationStatus.Success;

        string xml = System.Text.Encoding.UTF8.GetString(model.File.Contents);
        var data = xml.FromXML<TransactionsFileModel>();

        // Validate

        // Transaction file
        var dbTransactionFile = new RTransactionFile()
        {
            Contents = model.File.Contents,
            ContentType = model.File.ContentType,
            DateCreated = DateTime.Now,
            Name = model.File.Filename,
            FileDate = data.FileDate.ToDateTime(new TimeOnly())
        };

        // Transactions
        List<RTransaction> dbTransactions = new();
        data.Transactions.ForEach(x =>
            dbTransactions.Add(new RTransaction()
            {
                Amount = x.Amount.Value,
                BeneficiaryIban = x.Beneficiary.Iban,
                CreateDate = x.CreateDate!.Value,
                IdCcy = x.Amount.Currency,
                DebtorIban = x.Debtor.Iban,
                IdDirection = x.Amount.Direction,
                ExternalId = x.ExternalId,
                Status = x.Status,
                IdMerchant = model.Search.IdMerchant!.Value,
                IdTransactionFile = dbTransactionFile.Id
            }));

        // Transaction
        using var tran = await Db.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);
        {
            // Add transaction file
            _ = await Db.RTransactionFiles.AddAsync(dbTransactionFile, ct);

            // Save
            await Db.SaveChangesAsync(ct)
                .ConfigureAwait(false);

            // Add transaction
            for (var i = 0; i < dbTransactions.Count; i++)
            {
                _ = await Db.RTransactions.AddAsync(dbTransactions[i], ct);
            }

            // Save
            await Db.SaveChangesAsync(ct)
                .ConfigureAwait(false);

            // Commit
            await tran.CommitAsync(ct)
                .ConfigureAwait(false);
        }

        // Return
        return res;
    }
}
