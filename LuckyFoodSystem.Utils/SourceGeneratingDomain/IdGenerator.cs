using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text;
using System;
using System.Linq;
using System.Diagnostics;

namespace SourceGeneratingDomain
{
    [Generator(LanguageNames.CSharp)]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class IdGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var immutableHashSet = context.SyntaxProvider
              .CreateSyntaxProvider(
                  predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                  transform: static (ctx, _) => GetSemanticTargetIdForGeneration(ctx))
              .Where(static m => m is not null)!
              .Collect();

            context.RegisterSourceOutput(immutableHashSet,
                static (spc, source) => Execute(source, spc));
        }

        static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        => node is ClassDeclarationSyntax;

        static ImmutableHashSet<ITypeSymbol> GetSemanticTargetIdForGeneration(GeneratorSyntaxContext context)
        {
            var classesWithNotifyInterfaces = context.Node.DescendantNodesAndSelf()
                .OfType<ClassDeclarationSyntax>()
                .Select(x => context.SemanticModel.GetDeclaredSymbol(x))
                .OfType<ITypeSymbol>()
                .Where(syntax => FunctionSolver.IsDerivedFromClass(syntax, "Entity") 
                                 || FunctionSolver.IsDerivedFromClass(syntax, "Aggregate"))
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
                        var source = GenerateSourceForId(typeSymbol);
                        context.AddSource($"{typeSymbol.Name}.Id.cs", SourceText.From(source, Encoding.UTF8));
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

        private static string GenerateSourceForId(ITypeSymbol typeSymbol)
        {
            return $@"
using System.ComponentModel;

namespace {typeSymbol.ContainingNamespace}
{{
  public class {typeSymbol.Name}Id
  {{        
        public Guid Value {{ get; private set; }}
        public {typeSymbol.Name}Id(Guid value) => Value = value;
        public static {typeSymbol.Name}Id CreateUnique() => new(Guid.NewGuid());
        public static {typeSymbol.Name}Id Create(Guid value) => new {typeSymbol.Name}Id(value);        
        public IEnumerable<object> GetEqualityComponents()
        {{
            yield return Value;
        }}
  }}
}}";
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
