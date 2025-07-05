namespace BankReportingLibrary.Models;

// Connection strings
public record ConnectionStrings
{
    /// <summary>
    /// Connection string to main DB
    /// </summary>
    public string DB { get; init; } = string.Empty;
}

