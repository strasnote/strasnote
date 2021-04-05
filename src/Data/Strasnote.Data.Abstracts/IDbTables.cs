// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Abstracts table names for use in queries
	/// </summary>
	public interface IDbTables
	{
		/// <summary>
		/// Refresh Token table name
		/// </summary>
		string RefreshToken { get; }

		/// <summary>
		/// Role table name
		/// </summary>
		string Role { get; }

		/// <summary>
		/// User table name
		/// </summary>
		string User { get; }

		/// <summary>
		/// UserRole table name
		/// </summary>
		string UserRole { get; }
	}
}
