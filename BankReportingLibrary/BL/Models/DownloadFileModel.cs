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
