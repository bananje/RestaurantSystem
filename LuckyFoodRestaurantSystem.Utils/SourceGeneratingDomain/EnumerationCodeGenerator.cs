using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SourceGeneratingDomain
{
    [Generator(LanguageNames.CSharp)]
    public class EnumerationCodeGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var immutableHashSet = context.SyntaxProvider
              .CreateSyntaxProvider(
                  predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                  transform: static (ctx, _) => GetSemanticTargetInheritedFromEnumerationForGeneration(ctx))
              .Where(static m => m is not null)!
              .Collect();

            context.RegisterSourceOutput(immutableHashSet,
                static (spc, source) => Execute(source, spc));
        }

        static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        => node is ClassDeclarationSyntax;

        static ImmutableHashSet<ITypeSymbol> GetSemanticTargetInheritedFromEnumerationForGeneration(GeneratorSyntaxContext context)
        {
            var classesWithNotifyInterfaces = context.Node.DescendantNodesAndSelf()
                .OfType<ClassDeclarationSyntax>()
                .Select(x => context.SemanticModel.GetDeclaredSymbol(x))
                .OfType<ITypeSymbol>()
                .Where(x => FunctionSolver.IsDerivedFromClass(x, "Enumeration"))
                .Where(x => !x.IsAbstract)
                .ToImmutableHashSet();

            return classesWithNotifyInterfaces;
        }

        static void Execute(ImmutableArray<ImmutableHashSet<ITypeSymbol>> array, SourceProductionContext context)
        {
            try
            {
                if (array.IsDefaultOrEmpty)
                {
                    return;
                }

                foreach (var typeSymbols in array)
                {
                    context.CancellationToken.ThrowIfCancellationRequested();

                    foreach (var typeSymbol in typeSymbols)
                    {
                        var source = GenerateSourceForEnumeration(typeSymbol);
                        context.AddSource($"{typeSymbol.Name}.Enumeration.cs", SourceText.From(source, Encoding.UTF8));
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                ExeceptionDiagnostic.ReportExceptionDiagnostic(
                  context,
                  exception,
                    e => ExeceptionDiagnostic.CreateExceptionDiagnostic(e, null));
            }

        }
       
        private static string GenerateSourceForEnumeration(ITypeSymbol typeSymbol)
        {
            return $@"
using System.ComponentModel;

namespace {typeSymbol.ContainingNamespace}
{{
  partial class {typeSymbol.Name}
  {{
   protected {typeSymbol.Name}(int id, string name) : base(id, name)
        {{
        }}

   public static {typeSymbol.Name} FromId(int id)
            => GetAll<{typeSymbol.Name}>().FirstOrDefault(x => x.Id == id)!;

   public static {typeSymbol.Name} FromName(string name)
            => GetAll<{typeSymbol.Name}>().FirstOrDefault(x => x.Name == name)!;
  }}
}}";
        }
    }
}


