using BankReportingLibrary.Nomen.Consts;

namespace BankReportingLibrary.Models.CoreModels;

/// <summary>
/// Messages
/// </summary>
public class CMessages
{
    /// <summary>
    /// Messages
    /// </summary>
    public IList<CMessage> Messages { get; set; } = new List<CMessage>();

    /// <summary>
    /// Constructor
    /// </summary>
    public CMessages()
    {

    }

    /// <summary>
    /// Add generic
    /// </summary>
    /// <param name="message"></param>
    public void AddGeneric(string message) => Messages.Add(new CMessage() { Message = message, MessageType = CMessageType.Generic });
    /// <summary>
    /// Add error
    /// </summary>
    /// <param name="message"></param>
    /// <param name="key"></param>
    public void AddError(string message) => Messages.Add(new CMessage() { Message = message, MessageType = CMessageType.Error });
    /// <summary>
    /// Add warning
    /// </summary>
    /// <param name="message"></param>
    public void AddWarning(string message) => Messages.Add(new CMessage() { Message = message, MessageType = CMessageType.Warning });
    /// <summary>
    /// Add info
    /// </summary>
    /// <param name="message"></param>
    public void AddInfo(string message) => Messages.Add(new CMessage() { Message = message, MessageType = CMessageType.Info });
    /// <summary>
    /// Add success
    /// </summary>
    /// <param name="message"></param>
    public void AddSuccess(string message) => Messages.Add(new CMessage() { Message = message, MessageType = CMessageType.Success });

    /// <summary>
    /// Add messages
    /// </summary>
    /// <param name="messages"></param>
    public void Add(CMessages messages)
    {
        Add(messages.Messages);
    }

    /// <summary>
    /// Add messages
    /// </summary>
    /// <param name="messages"></param>
    public void Add(IEnumerable<CMessage> messages)
    {
        foreach (var message in messages)
        {
            Messages.Add(new CMessage() { Message = message.Message, MessageType = message.MessageType });
        }
    }

    /// <summary>
    /// Has messages
    /// </summary>
    public bool HasMessages => Messages.Any();
}