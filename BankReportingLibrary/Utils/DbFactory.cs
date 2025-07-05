using BankReportingDb.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Utils;

/// <summary>
/// Db factory
/// </summary>
internal class DbFactory
{
    /// <summary>
    /// The container
    /// </summary>
    private readonly IServiceProvider _Cont;

    /// <summary>
    /// The connection string
    /// </summary>
    private static string _connectionString = string.Empty;

    /// <summary>
    /// Injection constructor
    /// </summary>
    /// <param name="cont">The container</param>
    public DbFactory(IServiceProvider cont)
    {
        _Cont = cont;
    }

    public DB CreateInstanceDb()
    {
        // Create options builder
        var dbContxOpt = new DbContextOptionsBuilder<DB>();

        // Check
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            // Create local scope
            using (var scope = _Cont.CreateScope())
            {
                try
                {
                    // Read connection string
                    _connectionString = RegistrationModuleEx.config.ConnectionStringsData.DB;
                }
                catch (Exception err)
                {
                    //Trace
                    // log.Trace($"BankReportingLibrary.DbFactory.CreateInstanceDb={_connectionString}. Error={err.Message}");
                }
            }
        }

        // Set it
        dbContxOpt.UseLazyLoadingProxies(true)
                  .UseSqlServer(_connectionString, opt =>
                  {
                      if (RegistrationModuleEx.config?.LibApp != null)
                      {
                          opt.CommandTimeout(RegistrationModuleEx.config.LibApp.DbCmdTimeout);
                          opt.MaxBatchSize(RegistrationModuleEx.config.LibApp.EFBatchSize);
                      }
                  });

        // Create
        return new DB(dbContxOpt.Options);
    }
}

