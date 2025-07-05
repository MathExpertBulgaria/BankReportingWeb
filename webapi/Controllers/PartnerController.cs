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
    public PartnerController(PartnerAdp service
        ) : base(service)
    {

    }

    /// <summary>
    /// Search
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("[action]")]
    public async Task<IActionResult> Search(SearchPartnerModel model)
    {
        try
        {
            // Search
            var res = await Service.SearchAsync(model);

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
    [HttpGet("[action]")]
    public async Task<IActionResult> GetById(ObjectRefModel model)
    {
        try
        {
            // Get
            var res = await Service.GetByIdAsync(model);

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
    [HttpGet("[action]")]
    public async Task<IActionResult> GetCsv(SearchPartnerModel model)
    {
        try
        {
            // Get
            var res = await Service.GetCsvAsync(model);

            // Return
            return Ok(res);
        }
        catch (Exception err)
        {
            return HandleError("PartnerController.GetCsv", err);
        }
    }
}