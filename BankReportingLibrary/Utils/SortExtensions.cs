using BankReportingLibrary.BL.Models;
using BankReportingLibrary.Nomen.Consts;
using Castle.Components.DictionaryAdapter.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Utils;

public static class SortExtensions
{
    /// <summary>
    /// Sort
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="search"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    public static IQueryable<T> Sort<T>(this IQueryable<T> search, SortModel? sort)
    {
        // Check
        if (sort == null)
        {
            // Return
            return search;
        }

        // Check
        if (string.IsNullOrEmpty(sort.SortBy) || string.IsNullOrEmpty(sort.SortDirection))
        {
            // Return
            return search;
        }

        if (sort.SortDirection == SortDirectionConst.Ascending)
        {
            search = search.OrderByColumn(sort.SortBy);
        }
        else
        {
            search = search.OrderByColumnDescending(sort.SortBy);
        }

        // Return
        return search;
    }

    private static IOrderedQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnPath)
        => source.OrderByColumnUsing(columnPath, "OrderBy");

    public static IOrderedQueryable<T> OrderByColumnDescending<T>(this IQueryable<T> source, string columnPath)
        => source.OrderByColumnUsing(columnPath, "OrderByDescending");

    private static IOrderedQueryable<T> OrderByColumnUsing<T>(this IQueryable<T> source, string columnPath, string method)
    {
        var parameter = Expression.Parameter(typeof(T), "item");
        var member = columnPath.Split('.')
            .Aggregate((Expression)parameter, Expression.PropertyOrField);
        var keySelector = Expression.Lambda(member, parameter);
        var methodCall = Expression.Call(typeof(Queryable), method, new[]
                { parameter.Type, member.Type },
            source.Expression, Expression.Quote(keySelector));

        return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
    }
}
