using BankReportingLibrary.BL.Transaction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.BL.Amount.Validators;

public class AmountModelValidator
{
    // Locals
    private List<ValidationResult> vRes = [];

    /// <summary>
    /// Injection constructor
    /// </summary>
    public AmountModelValidator()
    {

    }

    /// <summary>
    /// Validate
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public bool Validate(TransactionFileAmount model, TransactionNomenModel nomen)
    {
        // Locals
        bool res = true;

        // Data annotations
        var vCon = new ValidationContext(model);
        if (!Validator.TryValidateObject(model, vCon, vRes, true))
        {
            // Return
            res = false;
        }

        // Direction
        if (nomen.NTransactionDirection.FirstOrDefault(x => x.Value == model.Direction) == null)
        {
            vRes.Add(new ValidationResult(Res.ImportTransaction.lDirectionInvalid));

            // Return
            res = false;
        }

        // Currency
        if (nomen.NCurrency.FirstOrDefault(x => x.Value == model.Currency) == null)
        {
            vRes.Add(new ValidationResult(Res.ImportTransaction.lCurrencyInvalid));

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
