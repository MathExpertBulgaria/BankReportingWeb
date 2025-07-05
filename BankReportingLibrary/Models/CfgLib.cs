using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Models;

/// <summary>
///  Configuration parameters
/// </summary>
public record CfgLib
{
    /// <summary>
    /// Timeout in seconds
    /// </summary>
    public int DbCmdTimeout { get; init; } = 15;

    /// <summary>
    /// max number of statements
    /// </summary>
    public int EFBatchSize { get; init; } = 500;
}