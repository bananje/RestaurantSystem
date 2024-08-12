using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SourceGeneratingDomain
{
    [Generator(LanguageNames.CSharp)]
    public class ValueObjectCodeGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var immutableHashSet = context.SyntaxProvider
              .CreateSyntaxProvider(
                  predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                  transform: static (ctx, _) => GetSemanticTargetValueObjectForGeneration(ctx))
              .Where(static m => m is not null)!
              .Collect();

            context.RegisterSourceOutput(immutableHashSet,
                static (spc, source) => Execute(source, spc));
        }

        // поиск классов
        static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        => node is ClassDeclarationSyntax;

        // Поиск классов, которые наследуют ValueObject
        static ImmutableHashSet<ITypeSymbol> GetSemanticTargetValueObjectForGeneration(GeneratorSyntaxContext context)
        {
            var classesWithNotifyInterfaces = context.Node.DescendantNodesAndSelf()
                .OfType<ClassDeclarationSyntax>()
                .Select(x => context.SemanticModel.GetDeclaredSymbol(x))
                .OfType<ITypeSymbol>()
                .Where(x => FunctionSolver.IsDerivedFromClass(x, "ValueObject") && !x.Name.EndsWith("Id"))
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
                        var source = GenerateIdObject(typeSymbol);
                        context.AddSource($"{typeSymbol.Name}.ValueObj.cs", SourceText.From(source, Encoding.UTF8));
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

        private static string GenerateIdObject(ITypeSymbol typeSymbol)
        {
            return $@"
using System.ComponentModel;

namespace {typeSymbol.ContainingNamespace}
{{
  partial class {typeSymbol.Name}
  {{        
        public override IEnumerable<object> GetEqualityComponents()
        {{
            yield return Value;
        }}
  }}
}}";
        }
    }
}
