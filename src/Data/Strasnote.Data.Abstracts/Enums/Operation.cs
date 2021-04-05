// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Database operation names (for logging queries)
	/// </summary>
	public enum Operation
	{
		/// <summary>
		/// Create operation
		/// </summary>
		Create,

		/// <summary>
		/// Retrieve operation
		/// </summary>
		Retrieve,

		/// <summary>
		/// Retrieve by ID operation
		/// </summary>
		RetrieveById,

		/// <summary>
		/// Update operation
		/// </summary>
		Update,

		/// <summary>
		/// Delete operation
		/// </summary>
		Delete,

		/// <summary>
		/// Query operation
		/// </summary>
		Query,

		/// <summary>
		/// Query Single operation
		/// </summary>
		QuerySingle
	}
}
