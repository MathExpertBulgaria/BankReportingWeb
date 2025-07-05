using BankReportingDb.Context;
using BankReportingLibrary.BL.Models;
using BankReportingLibrary.BL.Reporting;
using BankReportingLibrary.BL.Transaction.Models;
using BankReportingLibrary.BL.Transaction.Services;
using BankReportingLibrary.Models.CoreModels;
using BankReportingLibrary.Nomen.Consts;
using BankReportingLibrary.Utils;
using Microsoft.EntityFrameworkCore;

namespace BankReportingLibrary.BL.Transaction;

public class TransactionAdp : DbClassRoot
{
    // Report
    private readonly Lazy<CsvReport> _csvReport;
    // Import
    private readonly Lazy<ImportTransactionSrv> _xmlImport;

    /// <summary>
    /// Injection constructor
    /// </summary>
    /// <param name="Ent"></param>
    /// <param name="csvRport"></param>
    public TransactionAdp(DB Ent,
        // Report
        Lazy<CsvReport> csvRport,
        // Import
        Lazy<ImportTransactionSrv> xmlImport) : base(Ent)
    {
        // Report
        _csvReport = csvRport;
        // Import
        _xmlImport = xmlImport;
    }

    /// <summary>
    /// Search
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<DataOperationResult<ResModel<TransactionModel>>> SearchAsync(SearchTransactionModel model)
    {
        // Locals
        var res = new DataOperationResult<ResModel<TransactionModel>>();
        res.OperationStatus = OperationStatus.Success;

        DateTime? createDateFrom = model.CreateDateFrom.HasValue ? new DateTime(model.CreateDateFrom.Value.Year, model.CreateDateFrom.Value.Month, model.CreateDateFrom.Value.Day) : null;
        DateTime? createDateTo = model.CreateDateTo.HasValue ? new DateTime(model.CreateDateTo.Value.Year, model.CreateDateTo.Value.Month, model.CreateDateTo.Value.Day).AddDays(1) : null;

        // Transaction
        using var tran = await Db.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadUncommitted);
        {
            // Data
            res.Data = new ResModel<TransactionModel>()
            {
                Res = await Db.RTransactions.AsNoTracking()
                        .Where(x =>
                            model.IdMerchant == x.IdMerchant &&
                            (!createDateFrom.HasValue || createDateFrom <= x.CreateDate) &&
                            (!createDateTo.HasValue || createDateTo >= x.CreateDate) &&
                            (string.IsNullOrEmpty(model.Direction) || x.Direction.Contains(model.Direction)) &&
                            (!model.AmountFrom.HasValue || model.AmountFrom.Value <= x.Amount) &&
                            (!model.AmountTo.HasValue || model.AmountTo.Value >= x.Amount) &&
                            (string.IsNullOrEmpty(model.DebtorIban) || x.DebtorIban.Contains(model.DebtorIban)) &&
                            (string.IsNullOrEmpty(model.BeneficiaryIban) || x.BeneficiaryIban.Contains(model.BeneficiaryIban)) &&
                            (string.IsNullOrEmpty(model.Status) || x.Status.Contains(model.Status)) &&
                            (string.IsNullOrEmpty(model.ExternalId) || x.ExternalId.Contains(model.ExternalId))
                            )
                        // Paging
                        .Skip(model.PageIndex * model.PageSize)
                        .Take(model.PageSize)
                        .Select(x => new TransactionModel()
                        {
                            Id = x.Id,
                            CreateDate = x.CreateDate,
                            Direction = x.Direction
                        })
                        .ToListAsync()
                        .ConfigureAwait(false)
            };

            // Commit
            await tran.CommitAsync().ConfigureAwait(false);
        }

        // Return
        return res;
    }

    /// <summary>
    /// Get by Id
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<DataOperationResult<TransactionModel>> GetByIdAsync(ObjectRefModel model)
    {
        // Locals
        var res = new DataOperationResult<TransactionModel>();
        res.OperationStatus = OperationStatus.Success;
        res.Data = new TransactionModel();

        // Transaction
        using var tran = await Db.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);
        {
            // Data
            res.Data = await Db.RTransactions.AsNoTracking()
                .Select(x => new TransactionModel()
                {
                    Id = x.Id,
                    CreateDate = x.CreateDate,
                    Direction = x.Direction,
                    Amount = x.Amount,
                    Currency = x.Currency,
                    DebtorIban = x.DebtorIban,
                    BeneficiaryIban = x.BeneficiaryIban,
                    Status = x.Status,
                    ExternalId = x.ExternalId,
                    MerchantName = x.IdMerchantNavigation.Name
                })
                .FirstOrDefaultAsync(x => x.Id == model.Id)
                .ConfigureAwait(false);

            // Commit
            await tran.CommitAsync()
                .ConfigureAwait(false);
        }

        // Check
        if (res.Data == null)
        {
            res.Messages.AddError("NOT_FOUND");
        }

        // Return
        return res;
    }

    /// <summary>
    /// Get csv
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<DataOperationResult<DownloadFileModel>> GetCsvAsync(SearchTransactionModel model)
    {
        // Locals
        var res = new DataOperationResult<DownloadFileModel>();
        res.OperationStatus = OperationStatus.Success;
        res.Data = new DownloadFileModel()
        {
            Filename = $"{Res.Transaction.lCsvFilename}_{DateTime.Now.ToLongDateString()}.csv",
            ContentType = ContentTypeConst.Csv
        };

        // Data
        var data = await SearchAsync(model)
            .ConfigureAwait(false);

        // Report
        res.Data.Contents = await _csvReport.Value
            .GenerateReportAsync<TransactionModel, TransactionMapModel>(data.Data.Res)
            .ConfigureAwait(false);

        // Return
        return res;
    }

    /// <summary>
    /// Import xml
    /// </summary>
    /// <param name="model"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<DataOperationResult<ResModel<TransactionModel>>> ImportXmlAsync(ImportTransactionModel model,
        CancellationToken ct)
    {
        // Import
        var res = await _xmlImport.Value.ImportAsync(model, ct)
            .ConfigureAwait(false);

        var messages = res.Messages;

        // Check
        if (res.IsSuccess)
        {
            // Search
            res = await SearchAsync(model.Search)
                .ConfigureAwait(false);

            // Message
            res.Messages.Add(messages);
        }

        // Return
        return res;
    }
}
