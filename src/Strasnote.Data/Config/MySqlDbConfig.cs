// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Config
{
	/// <summary>
	/// MySQL database configuration
	/// </summary>
	public sealed record MySqlDbConfig : IDbConfig
	{
		/// <summary>
		/// Host name or IP address
		/// </summary>
		public string Host { get; init; } = string.Empty;

		/// <summary>
		/// Port number (default 3306)
		/// </summary>
		public int Port { get; set; } = 3306;

		/// <summary>
		/// User with access to the database
		/// </summary>
		public string User { get; init; } = string.Empty;

		/// <summary>
		/// User's password
		/// </summary>
		public string Pass { get; init; } = string.Empty;

		/// <summary>
		/// The name of the database to connect to
		/// </summary>
		public string Database { get; init; } = string.Empty;

		/// <summary>
		/// Additional configuration which will be added to the end of the connection string,
		/// e.g. enabling SSL
		/// </summary>
		public string? Custom { get; set; }

		/// <inheritdoc/>
		public bool IsValid =>
			!string.IsNullOrEmpty(Host)
			&& !string.IsNullOrEmpty(User)
			&& !string.IsNullOrEmpty(Pass)
			&& !string.IsNullOrEmpty(Database)
			&& Port > 0;
	}
}
