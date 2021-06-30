// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Clients.MySql
{
	/// <summary>
	/// Extension methods for SearchOperator: ToOperator
	/// </summary>
	public static class SearchOperatorExtensions
	{
		/// <summary>
		/// Convert a <see cref="SearchOperator"/> type to the actual MySQL operator<br/>
		/// Default value is "="
		/// </summary>
		/// <param name="this">SearchOperator</param>
		public static string ToOperator(this SearchOperator @this) =>
			@this switch
			{
				SearchOperator.NotEqual =>
					"!=",

				SearchOperator.Like =>
					"LIKE",

				SearchOperator.LessThan =>
					"<",

				SearchOperator.LessThanOrEqual =>
					"<=",

				SearchOperator.MoreThan =>
					">",

				SearchOperator.MoreThanOrEqual =>
					">=",

				SearchOperator.In =>
					"IN",

				SearchOperator.NotIn =>
					"NOT IN",

				_ =>
					"="
			};
	}
}
