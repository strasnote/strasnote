// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Data.Abstracts
{
	public interface IDbConfig
	{
		/// <summary>
		/// Returns true if the database configuration is valid
		/// </summary>
		bool IsValid { get; }
	}
}
