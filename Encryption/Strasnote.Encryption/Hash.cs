// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Text;
using Sodium;

namespace Strasnote.Encryption
{
	/// <summary>
	/// Hash strings
	/// </summary>
	public static class Hash
	{
		/// <summary>
		/// Quickly hash a password so any length can be used to encrypt a private key
		/// </summary>
		/// <param name="password">Password string</param>
		static internal byte[] Password(string password) =>
			GenericHash.Hash(Encoding.UTF8.GetBytes(password), null, 32); // must be 32 bytes
	}
}
