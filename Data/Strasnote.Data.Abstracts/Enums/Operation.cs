// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// CRUD operation names (for logging queries and stored procedure names)
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
		Delete
	}
}
