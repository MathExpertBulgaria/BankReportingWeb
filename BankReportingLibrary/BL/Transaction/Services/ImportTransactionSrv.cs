using BankReportingDb.Context;
using BankReportingDb.Models;
using BankReportingLibrary.BL.Models;
using BankReportingLibrary.BL.Transaction.Models;
using BankReportingLibrary.BL.Transaction.Validators;
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
    // Validators
    private readonly Lazy<TransactionsFileModelValidator> _transactionFileModelValidator;

    public ImportTransactionSrv(BankReporingContext Ent,
        // Validators
        Lazy<TransactionsFileModelValidator> transactionFileModelValidator) : base(Ent)
    {
        // Services
        _transactionFileModelValidator = transactionFileModelValidator;
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
        TransactionsFileModel? data = null;

        string xml = System.Text.Encoding.UTF8.GetString(model.File.Contents);

        try
        {
            data = xml.FromXML<TransactionsFileModel>();
        }
        catch { }

        // check data null, pass invalid xml, like another format or invalid structurre

        #region Validation

        // Validate
        if (!await _transactionFileModelValidator.Value.ValidateAsync(data, model.Search))
        {
            res.OperationStatus = OperationStatus.InvalidModel;

            foreach (string error in _transactionFileModelValidator.Value.GetErrorMessages())
            {
                res.Messages.AddError(error);
            }

            // Return
            return res;
        }

        #endregion

        // Transaction file
        var dbTransactionFile = new RTransactionFile()
        {
            Contents = model.File.Contents,
            ContentType = model.File.ContentType,
            DateCreated = DateTime.Now,
            Name = model.File.Filename,
            FileDate = data.FileDate
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
                // Set
                dbTransactions[i].IdTransactionFile = dbTransactionFile.Id;
                // Add
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
