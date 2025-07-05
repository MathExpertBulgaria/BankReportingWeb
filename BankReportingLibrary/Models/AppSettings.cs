namespace BankReportingLibrary.Models;

public record AppSettings
{
    /// <summary>
    /// Min threads for pool. Default 4
    /// </summary>
    public int MinThreads { get; init; } = 4;

    /// <summary>
    /// Min IO threads for pool. Default 4
    /// </summary>
    public int MinIOThreads { get; init; } = 4;

    /// <summary>
    /// Max queue rows
    /// </summary>
    public int MaxQueueRows { get; set; } = 5000;
}