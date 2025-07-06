using BankReportingDb.Context;
using BankReportingLibrary.BL.Models;
using BankReportingLibrary.BL.Partner.Models;
using BankReportingLibrary.BL.Reporting;
using BankReportingLibrary.Models.CoreModels;
using BankReportingLibrary.Nomen.Consts;
using BankReportingLibrary.Utils;
using Microsoft.EntityFrameworkCore;

namespace BankReportingLibrary.BL.Partner;

/// <summary>
/// Partner adapter
/// </summary>
public class PartnerAdp : DbClassRoot
{
    // Report
    private readonly Lazy<CsvReport> _csvReport;

    /// <summary>
    /// Injection constructor
    /// </summary>
    /// <param name="Ent"></param>
    /// <param name="csvRport"></param>
    public PartnerAdp(DB Ent,
        // Report
        Lazy<CsvReport> csvRport) : base(Ent)
    {
        // Report
        _csvReport = csvRport;
    }

    /// <summary>
    /// Search
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<DataOperationResult<ResModel<PartnerModel>>> SearchAsync(SearchPartnerModel model)
    {
        // Locals
        var res = new DataOperationResult<ResModel<PartnerModel>>();
        res.OperationStatus = OperationStatus.Success;

        // Transaction
        using var tran = await Db.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadUncommitted);
        {
            // Data
            res.Data = new ResModel<PartnerModel>()
            {
                Res = await Db.RPartners.AsNoTracking()
                        .Where(x => string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name))
                        // Paging
                        .Skip(model.PageIndex * model.PageSize)
                        .Take(model.PageSize)
                        .Select(x => new PartnerModel()
                        {
                            Id = x.Id,
                            Name = x.Name
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
    public async Task<DataOperationResult<PartnerModel>> GetByIdAsync(ObjectRefModel model)
    {
        // Locals
        var res = new DataOperationResult<PartnerModel>();
        res.OperationStatus = OperationStatus.Success;
        res.Data = new PartnerModel();

        // Transaction
        using var tran = await Db.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);
        {
            // Data
            res.Data = await Db.RPartners.AsNoTracking()
                .Select(x => new PartnerModel()
                {
                    Id = x.Id,
                    Name = x.Name
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
    public async Task<DataOperationResult<DownloadFileModel>> GetCsvAsync(SearchPartnerModel model)
    {
        // Locals
        var res = new DataOperationResult<DownloadFileModel>();
        res.OperationStatus = OperationStatus.Success;
        res.Data = new DownloadFileModel()
        {
            Filename = $"{Res.Partner.lCsvFilename}_{DateTime.Now.ToString(DateFormatConst.ISO_DATE)}.csv",
            ContentType = ContentTypeConst.Csv
        };

        // Data
        var data = await SearchAsync(model)
            .ConfigureAwait(false);

        // Report
        res.Data.Contents = await _csvReport.Value
            .GenerateReportAsync<PartnerModel, PartnerMapModel>(data.Data.Res)
            .ConfigureAwait(false);

        // Return
        return res;
    }
}