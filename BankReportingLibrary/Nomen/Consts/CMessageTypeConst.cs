using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Nomen.Consts;

/// <summary>
/// Message type
/// </summary>
public enum CMessageType
{
    Generic = 0,
    Info = 1,
    Success = 2,
    Warning = 3,
    Error = 4
}