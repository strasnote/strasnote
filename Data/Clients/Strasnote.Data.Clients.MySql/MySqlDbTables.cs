// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Clients.MySql
{
	/// <inheritdoc cref="IDbTables"/>
	public sealed class MySqlDbTables : IDbTables
	{
		/// <inheritdoc/>
		public string Role =>
			"auth.role";

		/// <inheritdoc/>
		public string User =>
			"auth.user";

		/// <inheritdoc/>
		public string UserRole =>
			"auth.user_role";
	}
}
