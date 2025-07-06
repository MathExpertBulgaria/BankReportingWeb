using BankReportingDb.Context;
using BankReportingLibrary.BL.Transaction.Models;
using BankReportingLibrary.Models.CoreModels;
using BankReportingLibrary.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.BL.Transaction.Services;

/// <summary>
/// Transaction nomen service
/// </summary>
public class TransactionNomenSrv : DbClassRoot
{
    public TransactionNomenSrv(DB Ent) : base(Ent)
    {
    }

    /// <summary>
    /// Get nomen
    /// </summary>
    /// <returns></returns>
    public async Task<TransactionNomenModel> GetNomens()
    {
        // Locals
        TransactionNomenModel res = new();

        // Transaction
        using var tran = await Db.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadUncommitted);
        {
            // Currency
            res.NCurrency = await Db.NCurrencies
            .Select(x => new NomenModel<int>()
            {
                Value = x.Id,
                Description = x.Ccy
            })
            .ToListAsync()
            .ConfigureAwait(false);

            // Transaction direction
            res.NTransactionDirection = await Db.NTransactionDirections
                .Select(x => new NomenModel<int>()
                {
                    Value = x.Id,
                    Description = x.Direction
                })
                .ToListAsync()
                .ConfigureAwait(false);

            // Commit
            await tran.CommitAsync()
                .ConfigureAwait(false);
        }

        // Return 
        return res;
    }
}
