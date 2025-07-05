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
    protected readonly T Service;

    #region Constructor and destructor

    /// <summary>
    /// Standard constructor
    /// </summary>
    public RootController(T service)
        : base()
    {
        // Save adapter and others
        Service = service;
    }

    public void Dispose()
    {
        // Container 
    }

    #endregion Constructor and destructor

    protected IActionResult HandleError(string title, Exception err)
    {
        // log.SaveError(title, err);

        // Return
        return StatusCode(500);
    }
}