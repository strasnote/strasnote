// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;

namespace Strasnote.Data.Abstracts
{
	/// <summary>
	/// Database migrator
	/// </summary>
	public interface IMigrator
	{
		/// <summary>
		/// Check configuration values and execute migration accordingly
		/// </summary>
		Task ExecuteAsync();
	}
}
