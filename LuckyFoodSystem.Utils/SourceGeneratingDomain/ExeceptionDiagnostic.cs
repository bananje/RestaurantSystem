using Microsoft.CodeAnalysis;
using System;

namespace SourceGeneratingDomain
{
    public static class ExeceptionDiagnostic
    {
        private static readonly DiagnosticDescriptor UnhandledException =
            new(
                "TS1001",
                "Unhandled exception occurred",
                "The generator caused an exception {0}: {1}",
                "Error",
                DiagnosticSeverity.Error,
                true);

        public static Diagnostic CreateExceptionDiagnostic(
           Exception exception,
           #pragma warning disable CS8632
           Location? location)
           #pragma warning restore CS8632
           => Diagnostic.Create(
               UnhandledException,
               location,
               exception?.GetType(),
               exception?.Message);

        public static void ReportExceptionDiagnostic(
            SourceProductionContext context,
            Exception exception, Func
            <Exception, Diagnostic> diagnosticFactory)
        {
            try
            {
                var diagnostic = diagnosticFactory(exception);
                context.ReportDiagnostic(diagnostic);
                var exceptionInfo = "#error " + exception.ToString().Replace("\n", "\n//");
                context.AddSource(
                    "!" + diagnostic.Descriptor.Id + "-" + Guid.NewGuid(),
                    exceptionInfo);
            }
            catch
            {
            }
        }
    }
}
