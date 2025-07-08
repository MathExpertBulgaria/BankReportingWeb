using BankReportingLibrary.BL.Amount.Validators;
using BankReportingLibrary.BL.Party.Validators;
using BankReportingLibrary.BL.Transaction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.BL.TransactionFileModel.Validators;

public class TransactionFileModelValidator
{
    // Locals
    private List<ValidationResult> vRes = [];

    // Validators
    private readonly AmountModelValidator _amountModelValidator;
    private readonly PartyModelValidator _partyModelValidator;

    /// <summary>
    /// Injection constructor
    /// </summary>
    public TransactionFileModelValidator(
        // Validators
        AmountModelValidator amountModelValidator, 
        PartyModelValidator partyModelValidator)
    {
        // Validators
        _amountModelValidator = amountModelValidator;
        _partyModelValidator = partyModelValidator;
    }

    /// <summary>
    /// Validate
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public bool Validate(Transaction.Models.TransactionFileModel model, TransactionNomenModel nomen)
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

        // Amount
        if (!_amountModelValidator.Validate(model.Amount, nomen))
        {
            _amountModelValidator.GetErrorMessages().ToList()
                .ForEach(x => vRes.Add(new ValidationResult(x)));
            
            // Return
            res = false;
        }

        // Debtor
        if (!_partyModelValidator.Validate(model.Debtor, nomen))
        {
            _partyModelValidator.GetErrorMessages().ToList()
                .ForEach(x => vRes.Add(new ValidationResult($"{Res.ImportTransaction.lDebtor}, {x}")));

            // Return
            res = false;
        }

        // Beneficiary
        if (!_partyModelValidator.Validate(model.Beneficiary, nomen))
        {
            _partyModelValidator.GetErrorMessages().ToList()
                .ForEach(x => vRes.Add(new ValidationResult($"{Res.ImportTransaction.lBeneficiary}, {x}")));

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
