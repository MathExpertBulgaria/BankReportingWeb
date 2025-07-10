using BankReportingLibrary.BL.Models;
using BankReportingLibrary.BL.Partner;
using BankReportingLibrary.BL.Partner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Code;

namespace webapi.Controllers;

/// <summary>
/// Partner controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PartnerController : RootController<PartnerAdp>
{
    // Injection constructor
    public PartnerController(PartnerAdp service,
        ILogger<PartnerController> logger
        ) : base(service, logger)
    {

    }

    /// <summary>
    /// Search
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Search([FromBody] SearchPartnerModel model)
    {
        try
        {
            // Search
            var res = await _service.SearchAsync(model);

            // Return
            return Ok(res);
        }
        catch (Exception err)
        {
            return HandleError("PartnerController.Search", err);
        }
    }

    /// <summary>
    /// Get by id
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> GetById([FromBody] ObjectRefModel model)
    {
        try
        {
            // Get
            var res = await _service.GetByIdAsync(model);

            // Return
            return Ok(res);
        }
        catch (Exception err)
        {
            return HandleError("PartnerController.GetById", err);
        }
    }

    /// <summary>
    /// Get csv
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> GetCsv([FromBody] SearchPartnerModel model)
    {
        try
        {
            // Get
            var res = await _service.GetCsvAsync(model);

            // Return
            return Ok(res);
        }
        catch (Exception err)
        {
            return HandleError("PartnerController.GetCsv", err);
        }
    }
}