using BankReportingLibrary.BL.Partner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.BL.Models;

/// <summary>
/// Queue res model
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResModel<T>
{
    public List<T>? Res { get; set; }
}
