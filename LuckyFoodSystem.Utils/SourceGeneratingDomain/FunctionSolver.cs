using Microsoft.CodeAnalysis;

namespace SourceGeneratingDomain
{
    public static class FunctionSolver
    {
        public static bool IsDerivedFromClass(ITypeSymbol derivedType, string className)
        {
            while (derivedType != null)
            {
                if (derivedType.TypeKind == TypeKind.Class && derivedType.Name == className)
                    return true;

                derivedType = derivedType.BaseType;
            }

            return false;
        }
    }
}
