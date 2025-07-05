using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Models;

// Connection strings
public record ConnectionStrings
{
    /// <summary>
    /// Connection string to main DB
    /// </summary>
    public string DB { get; init; } = string.Empty;
}

