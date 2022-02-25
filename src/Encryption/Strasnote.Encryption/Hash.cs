// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Text;
using Jeebs;
using Sodium;
using static F.OptionF;

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
		static internal Option<byte[]> PasswordGeneric(string password) =>
			Some(
				() => GenericHash.Hash(Encoding.UTF8.GetBytes(password), null, 32), // must be 32 bytes
				e => new M.GenericPasswordHashFailedException(e)
			);

		/// <summary>
		/// Hash a password securely using the Argon algorithm
		/// </summary>
		/// <param name="password">Password string</param>
		public static Option<string> PasswordArgon(string password) =>
			Some(
				() => PasswordHash.ArgonHashString(password, PasswordHash.StrengthArgon.Moderate),
				e => new M.ArgonPasswordHashFailedException(e)
			);

		/// <summary>Messages</summary>
		public static class M
		{
			/// <summary>Failed to hash password using generic hash</summary>
			public sealed record GenericPasswordHashFailedException(Exception Value) : ExceptionMsg { }

			/// <summary>Failed to hash password using Argon</summary>
			public sealed record ArgonPasswordHashFailedException(Exception Value) : ExceptionMsg { }
		}
	}
}
