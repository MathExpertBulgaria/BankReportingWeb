using BankReportingLibrary.BL.Models;
using BankReportingLibrary.BL.Transaction;
using BankReportingLibrary.BL.Transaction.Models;
using BankReportingLibrary.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Code;

namespace webapi.Controllers;

/// <summary>
/// Transaction controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TransactionController : RootController<TransactionAdp>
{
    // Injection constructor
    public TransactionController(TransactionAdp service
        ) : base(service)
    {

    }

    /// <summary>
    /// Search
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("[action]")]
    public async Task<IActionResult> Search()
    {
        try
        {
            // Search
            var res = await Service.Search();

            // Return
            return Ok(res);
        }
        catch (Exception err)
        {
            return HandleError("TransactionController.Search get", err);
        }
    }

    /// <summary>
    /// Search
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Search([FromBody] SearchTransactionModel model)
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
            return HandleError("TransactionController.Search", err);
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
            return HandleError("TransactionController.GetById", err);
        }
    }

    /// <summary>
    /// Get csv
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> GetCsv([FromBody] SearchTransactionModel model)
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
            return HandleError("TransactionController.GetCsv", err);
        }
    }

    /// <summary>
    /// Import xml
    /// </summary>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> ImportXml(CancellationToken ct)
    {
        ImportTransactionModel model = new();

        try
        {
            // File
            IFormFile file = Request.Form.Files[0];
            using (var data = new MemoryStream())
            {
                file.CopyTo(data);
                data.Seek(0, SeekOrigin.Begin);
                var buf = new byte[data.Length];
                data.Read(buf, 0, buf.Length);

                model.File = new()
                {
                    Filename = file.FileName,
                    Contents = buf,
                    ContentType = file.ContentType
                };
            }

            // Search
            var search = Request.Form["search"].ToString().DeserializeObject<SearchTransactionModel>();
            model.Search = search;

            // Get
            var res = await Service.ImportXmlAsync(model, ct);

            // Return
            return Ok(res);
        }
        catch (Exception err)
        {
            return HandleError("TransactionController.ImportXml", err);
        }
    }
}