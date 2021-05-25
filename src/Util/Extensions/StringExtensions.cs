// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Text.RegularExpressions;

namespace Strasnote.Util
{
	/// <summary>
	/// String Extension methods
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Normalise a string, replacing all non-letter and non-numeric characters with a hyphen ('-')
		/// and then converting to lower case
		/// </summary>
		/// <param name="this">Input string</param>
		public static string Normalise(this string @this) =>
			new Regex("[^a-z0-9]+", RegexOptions.IgnoreCase)
				.Replace(@this, "-")
				.Trim('-')
				.ToLower();
	}
}
