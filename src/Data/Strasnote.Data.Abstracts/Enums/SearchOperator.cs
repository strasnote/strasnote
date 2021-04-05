// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Search Operators
	/// </summary>
	public enum SearchOperator
	{
		/// <summary>
		/// None / Unknown
		/// </summary>
		None,

		/// <summary>
		/// Equal
		/// </summary>
		Equal,

		/// <summary>
		/// Not Equal
		/// </summary>
		NotEqual,

		/// <summary>
		/// Like
		/// </summary>
		Like,

		/// <summary>
		/// Less Than
		/// </summary>
		LessThan,

		/// <summary>
		/// Less Than or Equal
		/// </summary>
		LessThanOrEqual,

		/// <summary>
		/// More Than
		/// </summary>
		MoreThan,

		/// <summary>
		/// More Than or Equal
		/// </summary>
		MoreThanOrEqual
	}
}
