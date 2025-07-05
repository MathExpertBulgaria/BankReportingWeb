using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Models;

/// <summary>
/// Library configuration options
/// </summary>
public class LibraryConfig
{
    /// <summary>
    /// Library config
    /// </summary>
    public CfgLib? LibApp { get; set; }

    /// <summary>
    /// Connection string object
    /// </summary>
    public ConnectionStrings? ConnectionStringsData { get; set; }

    /// <summary>
    /// Root execution path
    /// </summary>
    public string BasePath { get; set; } = string.Empty;

}