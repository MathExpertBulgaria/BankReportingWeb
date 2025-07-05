using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.BL.Models;

/// <summary>
/// General file model
/// </summary>
public class DownloadFileModel
{
    public string? Filename { get; set; }
    public long Size { get; set; }
    public byte[]? Contents { get; set; }
    public string? ContentType { get; set; }
}
