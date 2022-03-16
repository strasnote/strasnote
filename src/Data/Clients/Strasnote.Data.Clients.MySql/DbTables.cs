// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Clients.MySql
{
	/// <inheritdoc cref="IDbTables"/>
	public sealed class DbTables : IDbTables
	{
		#region Auth

		/// <inheritdoc/>
		public string User =>
			"auth.user";

		/// <inheritdoc/>
		public string RefreshToken =>
			"auth.refresh_token";

		#endregion

		#region Notes

		/// <inheritdoc/>
		public string Folder =>
			"main.folder";

		/// <inheritdoc/>
		public string Note =>
			"main.note";

		/// <inheritdoc/>
		public string NoteTag =>
			"main.note_tag";

		/// <inheritdoc/>
		public string Tag =>
			"main.tag";

		#endregion
	}
}
