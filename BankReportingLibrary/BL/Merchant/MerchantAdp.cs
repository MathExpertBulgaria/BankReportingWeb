using BankReportingDb.Context;
using BankReportingLibrary.BL.Merchant.Models;
using BankReportingLibrary.BL.Models;
using BankReportingLibrary.BL.Reporting;
using BankReportingLibrary.Models.CoreModels;
using BankReportingLibrary.Nomen.Consts;
using BankReportingLibrary.Utils;
using Microsoft.EntityFrameworkCore;

namespace BankReportingLibrary.BL.Merchant;

/// <summary>
/// Merchant adapter
/// </summary>
public class MerchantAdp : DbClassRoot
{
    // Report
    private readonly Lazy<CsvReport> _csvReport;

    /// <summary>
    /// Injection constructor
    /// </summary>
    /// <param name="Ent"></param>
    /// <param name="csvRport"></param>
    public MerchantAdp(DB Ent,
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
    /// <param name="isPaging">Apply paging</param>
    /// <returns></returns>
    public async Task<DataOperationResult<ResModel<MerchantModel>>> SearchAsync(SearchMerchantModel model, bool isPaging = true)
    {
        // Locals
        var res = new DataOperationResult<ResModel<MerchantModel>>();
        res.OperationStatus = OperationStatus.Success;

        DateTime? boardingdateFrom = model.BoardingDateFrom.HasValue ? new DateTime(model.BoardingDateFrom.Value.Year, model.BoardingDateFrom.Value.Month, model.BoardingDateFrom.Value.Day) : null;
        DateTime? boardingdateTo = model.BoardingDateTo.HasValue ? new DateTime(model.BoardingDateTo.Value.Year, model.BoardingDateTo.Value.Month, model.BoardingDateTo.Value.Day).AddDays(1) : null;

        // Transaction
        using var tran = await Db.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadUncommitted);
        {
            // Search
            var search = Db.RMerchants.AsNoTracking()
                        .Where(x =>
                            (!model.IdPartner.HasValue || x.IdPartner == model.IdPartner) &&
                            (string.IsNullOrEmpty(model.Name) || x.Name.Contains(model.Name)) &&
                            (!boardingdateFrom.HasValue || boardingdateFrom <= x.BoardingDate) &&
                            (!boardingdateTo.HasValue || boardingdateTo >= x.BoardingDate) &&
                            (string.IsNullOrEmpty(model.Country) || x.Country.Contains(model.Country))
                            );

            // Total
            var total = await search.CountAsync();

            // Paging
            if (isPaging)
            {
                search = search
                    .Skip(model.PageIndex * model.PageSize)
                    .Take(model.PageSize);
            }

            // Data
            res.Data = new ResModel<MerchantModel>()
            {
                Res = await search
                        .Select(x => new MerchantModel()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            BoardingDate = x.BoardingDate,
                            Url = x.Url,
                            Country = x.Country,
                            Address1 = x.Address1,
                            Address2 = x.Addres2,
                            PartnerName = x.IdPartnerNavigation.Name
                        })
                        .ToListAsync()
                        .ConfigureAwait(false),

                // Total
                Total = await search.CountAsync()
            };

            // Commit
            await tran.CommitAsync()
                .ConfigureAwait(false);
        }

        // Return
        return res;
    }

    /// <summary>
    /// Get by Id
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<DataOperationResult<MerchantModel>> GetByIdAsync(ObjectRefModel model)
    {
        // Locals
        var res = new DataOperationResult<MerchantModel>();
        res.OperationStatus = OperationStatus.Success;
        res.Data = new MerchantModel();

        // Transaction
        using var tran = await Db.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);
        {
            // Data
            res.Data = await Db.RMerchants.AsNoTracking()
                .Select(x => new MerchantModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    BoardingDate = x.BoardingDate,
                    Url = x.Url,
                    Country = x.Country,
                    Address1 = x.Address1,
                    Address2 = x.Addres2,
                    PartnerName = x.IdPartnerNavigation.Name
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
    public async Task<DataOperationResult<DownloadFileModel>> GetCsvAsync(SearchMerchantModel model)
    {
        // Locals
        var res = new DataOperationResult<DownloadFileModel>();
        res.OperationStatus = OperationStatus.Success;
        res.Data = new DownloadFileModel()
        {
            Filename = $"{Res.Merchant.lCsvFilename}_{DateTime.Now.ToString(DateFormatConst.ISO_DATE)}.csv",
            ContentType = ContentTypeConst.Csv
        };

        // Data
        var data = await SearchAsync(model, false)
            .ConfigureAwait(false);

        // Report
        res.Data.Contents = await _csvReport.Value
            .GenerateReportAsync<MerchantModel, MerchantMapModel>(data.Data.Res)
            .ConfigureAwait(false);

        // Return
        return res;
    }
}