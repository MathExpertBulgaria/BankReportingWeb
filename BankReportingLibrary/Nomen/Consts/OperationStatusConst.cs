namespace BankReportingLibrary.Nomen.Consts;

/// <summary>
/// Operation status
/// </summary>
public enum OperationStatus
{
    Success = 1,
    InvalidModel = 2,
    NotFound = 3,
    NotFoundInfo = 31,
    ConflictingEntity = 4,
    LockedEntity = 5,
    NotActive = 6,
    NotActiveInfo = 61,
    WriteError = 7,
    GenericError = 9,
    Timeout = 10,
    NetworkError = 11,
    Unauthorized = 12,
    Forbidden = 13,
    Other = 99
}