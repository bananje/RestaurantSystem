// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1036:Укажите параметр принудительного применения API, запрещенного для анализатора", Justification = "<Ожидание>", Scope = "type", Target = "~T:SourceGeneratingDomain.IdGenerator")]
[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1024:Символы следует сравнивать на равенство", Justification = "<Ожидание>", Scope = "member", Target = "~M:SourceGeneratingDomain.IdGenerator.GetSemanticTargetIdForGeneration(Microsoft.CodeAnalysis.GeneratorSyntaxContext)~System.Collections.Immutable.ImmutableHashSet{Microsoft.CodeAnalysis.ITypeSymbol}")]
[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1024:Символы следует сравнивать на равенство", Justification = "<Ожидание>", Scope = "member", Target = "~M:SourceGeneratingDomain.EnumerationCodeGenerator.GetSemanticTargetInheritedFromEnumerationForGeneration(Microsoft.CodeAnalysis.GeneratorSyntaxContext)~System.Collections.Immutable.ImmutableHashSet{Microsoft.CodeAnalysis.ITypeSymbol}")]
[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1036:Укажите параметр принудительного применения API, запрещенного для анализатора", Justification = "<Ожидание>", Scope = "type", Target = "~T:SourceGeneratingDomain.ValueObjectCodeGenerator")]
[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1024:Символы следует сравнивать на равенство", Justification = "<Ожидание>", Scope = "member", Target = "~M:SourceGeneratingDomain.ValueObjectCodeGenerator.GetSemanticTargetValueObjectForGeneration(Microsoft.CodeAnalysis.GeneratorSyntaxContext)~System.Collections.Immutable.ImmutableHashSet{Microsoft.CodeAnalysis.ITypeSymbol}")]
[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1036:Укажите параметр принудительного применения API, запрещенного для анализатора", Justification = "<Ожидание>", Scope = "type", Target = "~T:SourceGeneratingDomain.EnumerationCodeGenerator")]
