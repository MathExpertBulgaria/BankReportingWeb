using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Models.CoreModels;

/// <summary>
/// Nomen model
/// </summary>
public class NomenModel<T>
{
    public T? Value { get; set; }
    public string? Description { get; set; } 
}
