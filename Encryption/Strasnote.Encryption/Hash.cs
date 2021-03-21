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
			Return(
				() => GenericHash.Hash(Encoding.UTF8.GetBytes(password), null, 32), // must be 32 bytes
				e => new Msg.GenericPasswordHashFailedException(e)
			);

		/// <summary>
		/// Hash a password securely using the Argon algorithm
		/// </summary>
		/// <param name="password">Password string</param>
		public static Option<string> PasswordArgon(string password) =>
			Return(
				() => PasswordHash.ArgonHashString(password, PasswordHash.StrengthArgon.Moderate),
				e => new Msg.ArgonPasswordHashFailedException(e)
			);

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Failed to hash password using generic hash</summary>
			public sealed record GenericPasswordHashFailedException(Exception Exception) : IExceptionMsg { }

			/// <summary>Failed to hash password using Argon</summary>
			public sealed record ArgonPasswordHashFailedException(Exception Exception) : IExceptionMsg { }
		}
	}
}
