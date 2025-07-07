using BankReportingLibrary.BL.Merchant;
using BankReportingLibrary.BL.Merchant.Models;
using BankReportingLibrary.BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Code;

namespace webapi.Controllers;

/// <summary>
/// Merchant controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class MerchantController : RootController<MerchantAdp>
{
    // Injection constructor
    public MerchantController(MerchantAdp service
        ) : base(service)
    {

    }

    /// <summary>
    /// Search
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Search([FromBody] SearchMerchantModel model)
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
            return HandleError("MerchantController.Search", err);
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
            var res = await Service.GetByIdAsync(model);

            // Return
            return Ok(res);
        }
        catch (Exception err)
        {
            return HandleError("MerchantController.GetById", err);
        }
    }

    /// <summary>
    /// Get csv
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> GetCsv([FromBody] SearchMerchantModel model)
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
            return HandleError("MerchantController.GetCsv", err);
        }
    }
}