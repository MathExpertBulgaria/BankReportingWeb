using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.BL.Models;

/// <summary>
/// Search model
/// </summary>
public class SearchModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 5;
}
