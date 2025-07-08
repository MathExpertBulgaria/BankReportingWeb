using BankReportingLibrary.BL.Transaction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.BL.Party.Validators;

public class PartyModelValidator
{
    // Locals
    private List<ValidationResult> vRes = [];

    /// <summary>
    /// Injection constructor
    /// </summary>
    public PartyModelValidator()
    {

    }

    /// <summary>
    /// Validate
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public bool Validate(TransactionFileParty? model, TransactionNomenModel nomen)
    {
        // Locals
        bool res = true;

        // Check
        if (model == null)
        {
            vRes.Add(new ValidationResult(Res.ImportTransaction.lNoData));

            // Return
            res = false;
            return res;
        }

        // Data annotations
        var vCon = new ValidationContext(model);
        if (!Validator.TryValidateObject(model, vCon, vRes, true))
        {
            // Return
            res = false;
        }

        // Return
        return res;
    }

    /// <summary>
    /// Get error messages
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetErrorMessages() => vRes.Select(x => x.ErrorMessage ?? "").Distinct();
}
