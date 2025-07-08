using BankReportingLibrary.BL.Transaction.Models;
using BankReportingLibrary.BL.Transaction.Services;
using BankReportingLibrary.BL.TransactionFileModel.Validators;
using Castle.DynamicProxy.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.BL.Transaction.Validators;

public class TransactionsFileModelValidator
{
    // Locals
    private List<ValidationResult> vRes = [];

    // Nomen service
    private readonly Lazy<TransactionNomenSrv> _transactionNomenSrv;

    // Validators
    private readonly TransactionFileModelValidator _transactionFileModelValidator;

    /// <summary>
    /// Injection constructor
    /// </summary>
    public TransactionsFileModelValidator(
        // Nomens
        Lazy<TransactionNomenSrv> transactionNomenSrv,
        // Validators
        TransactionFileModelValidator transactionFileModelValidator
        )
    {
        // Nomens   
        _transactionNomenSrv = transactionNomenSrv;

        // Validators
        _transactionFileModelValidator = transactionFileModelValidator;
    }

    /// <summary>
    /// Validate
    /// </summary>
    /// <param name="model"></param
    /// <param name="search"></param>
    /// <returns></returns>
    public async Task<bool> ValidateAsync(TransactionsFileModel? model, SearchTransactionModel search)
    {
        // Locals
        bool res = true;

        // No transactions
        if (model == null)
        {
            vRes.Add(new ValidationResult(Res.ImportTransaction.lInvalidXml));

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

        // No transactions
        if (model.Transactions.Count == 0)
        {
            vRes.Add(new ValidationResult(Res.ImportTransaction.lNoTransactions));

            // Return
            res = false;
        }

        // No merchant
        if (!search.IdMerchant.HasValue)
        {
            vRes.Add(new ValidationResult(Res.ImportTransaction.lNoMerchant));

            // Return
            res = false;
        }

        // Nomens
        var nomens = await _transactionNomenSrv.Value.GetNomens()
            .ConfigureAwait(false);

        // Validate transactions
        for (var i = 0; i < model.Transactions.Count; i++)
        {
            if (!_transactionFileModelValidator.Validate(model.Transactions[i], nomens))
            {
                _transactionFileModelValidator.GetErrorMessages().ToList()
                    .ForEach(x => vRes.Add(new ValidationResult($"{Res.ImportTransaction.lTransaction} #{i + 1}, {x}")));

                // Return
                res = false;
            }
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
