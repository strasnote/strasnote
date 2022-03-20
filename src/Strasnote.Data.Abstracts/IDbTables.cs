// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Abstracts table names for use in queries
	/// </summary>
	public interface IDbTables
	{
		#region Auth

		/// <summary>
		/// Refresh Token table name
		/// </summary>
		string RefreshToken { get; }

		/// <summary>
		/// User table name
		/// </summary>
		string User { get; }

		#endregion

		#region Notes

		/// <summary>
		/// Folder table name
		/// </summary>
		string Folder { get; }

		/// <summary>
		/// Note table name
		/// </summary>
		string Note { get; }

		/// <summary>
		/// Note table name
		/// </summary>
		string NoteTag { get; }

		/// <summary>
		/// Tag table name
		/// </summary>
		string Tag { get; }

		#endregion
	}
}
