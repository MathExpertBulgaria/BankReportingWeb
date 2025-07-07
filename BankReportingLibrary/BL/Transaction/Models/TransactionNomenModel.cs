using BankReportingLibrary.Models.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.BL.Transaction.Models;

/// <summary>
/// Transaction nomen model
/// </summary>
public class TransactionNomenModel
{
    public List<NomenModel<string>>? NCurrency { get; set; }
    public List<NomenModel<string>>? NTransactionDirection { get; set; }
}