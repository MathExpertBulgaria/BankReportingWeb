using BankReportingLibrary.Nomen.Consts;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BankReportingLibrary.Models.CoreModels;

/// <summary>
/// Data operation result
/// </summary>
/// <typeparam name="T">Data type</typeparam>
public class DataOperationResult<T>
{
    /// <summary>
    /// Data
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Operation status
    /// </summary>
    public OperationStatus OperationStatus { get; set; }

    /// <summary>
    /// Messages
    /// </summary>
    public CMessages Messages { get; set; }

    /// <summary>
    /// Exceptions
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public ICollection<Exception> Exceptions { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public DataOperationResult()
    {
        Messages = new CMessages();
        Exceptions = new List<Exception>();
    }

    #region Properties

    /// <summary>
    /// Has data
    /// </summary>
    public bool HasData => Data != null;
    /// <summary>
    /// Is success
    /// </summary>
    public bool IsSuccess => OperationStatus == OperationStatus.Success;
    /// <summary>
    /// Has messages
    /// </summary>
    public bool HasMessages => Messages != null && Messages.HasMessages;
    /// <summary>
    /// Has exceptions
    /// </summary>
    [IgnoreDataMember]
    [JsonIgnore]
    public bool HasExceptions => Exceptions != null && Exceptions.Any();
    /// <summary>
    /// Status code
    /// </summary>
    public int StatusCode => (int)HttpStatusCode;
    /// <summary>
    /// Get HttpStatusCode as enum, based on OperationStatus
    /// </summary>
    public System.Net.HttpStatusCode HttpStatusCode
    {
        get
        {
            switch (OperationStatus)
            {
                case OperationStatus.Success:
                    return System.Net.HttpStatusCode.OK;
                case OperationStatus.InvalidModel:
                    return System.Net.HttpStatusCode.BadRequest;
                case OperationStatus.NotFound:
                    return System.Net.HttpStatusCode.NotFound;
                case OperationStatus.NotFoundInfo:
                    return System.Net.HttpStatusCode.NotFound;
                case OperationStatus.Other:
                    return System.Net.HttpStatusCode.BadRequest;
                case OperationStatus.ConflictingEntity:
                    return System.Net.HttpStatusCode.Conflict;
                case OperationStatus.NotActive:
                    return System.Net.HttpStatusCode.Gone;
                case OperationStatus.NotActiveInfo:
                    return System.Net.HttpStatusCode.Gone;
                case OperationStatus.WriteError:
                    return System.Net.HttpStatusCode.NotAcceptable;
                case OperationStatus.LockedEntity:
                    return System.Net.HttpStatusCode.UnprocessableEntity;
                case OperationStatus.GenericError:
                    return System.Net.HttpStatusCode.InternalServerError;
                case OperationStatus.Timeout:
                    return System.Net.HttpStatusCode.RequestTimeout;
                case OperationStatus.NetworkError:
                    return System.Net.HttpStatusCode.ServiceUnavailable;
                case OperationStatus.Unauthorized:
                    return System.Net.HttpStatusCode.Unauthorized;
                case OperationStatus.Forbidden:
                    return System.Net.HttpStatusCode.Forbidden;
                default:
                    return System.Net.HttpStatusCode.BadRequest;
            }
        }
    }

    #endregion Properties
}
