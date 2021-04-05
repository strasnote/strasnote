// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Clients.MySql
{
	/// <inheritdoc cref="IDbTables"/>
	public sealed class DbTables : IDbTables
	{
		/// <inheritdoc/>
		public string RefreshToken =>
			"auth.refresh_token";

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
