using BankReportingLibrary.Nomen.Consts;

namespace BankReportingLibrary.Models.CoreModels;

/// <summary>
/// Message
/// </summary>
public class CMessage
{
    /// <summary>
    /// Constructor
    /// </summary>
    public CMessage()
    {

    }

    public string Message { get; set; } = string.Empty;

    public CMessageType MessageType { get; set; } = CMessageType.Info;
}
