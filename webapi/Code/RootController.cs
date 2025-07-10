using BankReportingLibrary.Utils;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Code;

/// <summary>
/// Rroot class for all controllers
/// </summary>
/// <typeparam name="T">The class with business logic</typeparam>
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
[ApiController]
public class RootController<T> : ControllerBase, IDisposable where T : DbClassRoot
{
    /// <summary>
    /// The DbClassRoot for the controller
    /// </summary>
    protected readonly T _service;

    protected readonly ILogger _logger;

    #region Constructor and destructor

    /// <summary>
    /// Standard constructor
    /// </summary>
    public RootController(T service,
        ILogger logger)
        : base()
    {
        // Save adapter and others
        _service = service;

        // Logger
        _logger = logger;
    }

    public void Dispose()
    {
        // Container 
    }

    #endregion Constructor and destructor

    protected IActionResult HandleError(string title, Exception err)
    {
        // Log
        _logger.LogError(title, err);

        // Return
        return StatusCode(500);
    }
}