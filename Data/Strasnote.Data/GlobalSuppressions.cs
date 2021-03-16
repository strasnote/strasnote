// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Because of the generic constraint accessing it as a static is too verbose", Scope = "member", Target = "~M:Strasnote.Data.DbContext`1.GetStoredProcedure(Strasnote.Data.Abstracts.Operation)~System.String")]
