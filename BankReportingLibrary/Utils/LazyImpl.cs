using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankReportingLibrary.Utils;

internal class LazyImpl<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T> : Lazy<T> where T : class
{
    public LazyImpl(IServiceProvider c) : base(() =>
    {
        // Try create the object from the container
        T? res = c.GetService<T>();
        if (res != null)
        {
            return res;
        }
        else
        {
            return Activator.CreateInstance<T>();
        }
    })
    { }
}