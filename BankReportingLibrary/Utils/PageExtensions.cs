using BankReportingLibrary.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Utils;

public static class PageExtensions
{
    /// <summary>
    /// Page
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="search"></param>
    /// <param name="isPaging"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public static IQueryable<T> Page<T>(this IQueryable<T> search, bool isPaging, PageModel? page)
    {
        // Check
        if (!isPaging)
        {
            // Return
            return search;
        }

        // Check
        if (page == null)
        {
            // Return
            return search;
        }

        // Return
        return search
            .Skip(page.PageIndex * page.PageSize)
            .Take(page.PageSize);
    }
}
