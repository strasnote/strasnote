// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.ComponentModel.DataAnnotations;

namespace Strasnote.Auth.Config
{
	public sealed class DbConfig
	{
		/// <summary>
		/// The JWT secret.
		/// </summary>
		[Required]
		public string ConnectionString { get; set; } = string.Empty;
	}
}
