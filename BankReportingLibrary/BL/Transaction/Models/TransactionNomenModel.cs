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
    public List<NomenModel<int>>? NCurrency { get; set; }
    public List<NomenModel<int>>? NTransactionDirection { get; set; }
}