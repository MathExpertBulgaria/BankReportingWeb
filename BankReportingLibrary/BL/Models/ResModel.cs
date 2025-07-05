namespace BankReportingLibrary.BL.Models;

/// <summary>
/// Queue res model
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResModel<T>
{
    public List<T>? Res { get; set; }
}
