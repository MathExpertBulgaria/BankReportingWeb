using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Utils;

public static class SearchExtensions
{
    /// <summary>
    /// Page
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="search"></param>
    /// <param name="isPaging"></param>
    /// <param name="pageIdx"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static IQueryable<T> Page<T>(this IQueryable<T> search, bool isPaging, int pageIdx, int pageSize)
    {
        // Check
        if (isPaging)
        {
            // Return
            return search
                .Skip(pageIdx * pageSize)
                .Take(pageSize);
        }

        // Return
        return search;
    }
}
