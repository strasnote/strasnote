// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Config
{
	public sealed record MySqlDbConfig : IDbConfig
	{
		public string Host { get; init; } = string.Empty;

		public int Port { get; set; } = 3306;

		public string User { get; init; } = string.Empty;

		public string Pass { get; init; } = string.Empty;

		public string Database { get; init; } = string.Empty;

		public string? Custom { get; set; }

		/// <inheritdoc/>
		public bool IsValid =>
			!string.IsNullOrEmpty(Host)
			&& !string.IsNullOrEmpty(User)
			&& !string.IsNullOrEmpty(Pass)
			&& !string.IsNullOrEmpty(Database);
	}
}
