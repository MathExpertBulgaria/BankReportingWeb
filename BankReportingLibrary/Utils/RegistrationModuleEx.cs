using BankReportingLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BankReportingLibrary.Utils;

public static class RegistrationModuleEx
{
    public static void AddBankReportingLibrary(this IServiceCollection builder, LibraryConfig cfg)
    {
        // Store
        config = cfg;

        // Register classes for DI
        var realClasses = from c in typeof(RegistrationModuleEx).Assembly.ExportedTypes
                          where c.IsClass && !c.IsAbstract
                          select c;
        foreach (var c in realClasses)
        {
            // Adapter
            if (c.IsAssignableTo(typeof(DbClassRoot)))
            {
                builder.TryAddScoped(c);
            }
            // Reporting
            else if (c.Namespace != null && c.Namespace.Contains("Reporting"))
            {
                builder.TryAddTransient(c);
            }
            // Validators
            else if (c.Namespace != null && c.Namespace.Contains("Validators"))
            {
                builder.TryAddTransient(c);
            }
        }

        // Register utilities
        builder.Add(new ServiceDescriptor(
                typeof(Lazy<>), typeof(LazyImpl<>), ServiceLifetime.Transient
            ));
    }

    // Store
    internal static LibraryConfig? config;
}
