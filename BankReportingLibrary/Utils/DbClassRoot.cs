using BankReportingDb.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Utils;

/// <summary>
/// Root class to acces DB
/// </summary>
public class DbClassRoot : ClassRoot
{
    private readonly DB _ent;

    /// <summary>
    /// DB context
    /// </summary>
    protected DB Db => _ent;

    #region Constructor and Destructor

    /// <summary>
    /// Constructor 
    /// </summary>
    public DbClassRoot(DB Ent)
    {
        // Set connection
        _ent = Ent;
    }

    protected override void Dispose(bool flag)
    {
        // Managed by DI
    }

    protected override ValueTask DisposeAsync(bool flag)
    {
        // Managed by DI - if needed add 
        return ValueTask.CompletedTask;
    }

    #endregion

    #region Transactions

    public IDbContextTransaction BeginTransaction()
    {
        return BeginTransaction(IsolationLevel.ReadCommitted);
    }

    public IDbContextTransaction BeginTransaction(IsolationLevel il)
    {
        // Return
        return Db.Database.BeginTransaction(il);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel il, CancellationToken cancellationToken = default)
    {
        // Return
        return await Db.Database.BeginTransactionAsync(il, cancellationToken);
    }

    #endregion Transactions
}
