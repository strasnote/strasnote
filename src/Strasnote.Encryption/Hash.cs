// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Text;
using MaybeF;
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
		static internal Maybe<byte[]> PasswordGeneric(string password) =>
			F.Some(
				() => GenericHash.Hash(Encoding.UTF8.GetBytes(password), null, 32), // must be 32 bytes
				e => new M.GenericPasswordHashFailedExceptionMsg(e)
			);

		/// <summary>
		/// Hash a password securely using the Argon algorithm
		/// </summary>
		/// <param name="password">Password string</param>
		public static Maybe<string> PasswordArgon(string password) =>
			F.Some(
				() => PasswordHash.ArgonHashString(password, PasswordHash.StrengthArgon.Moderate),
				e => new M.ArgonPasswordHashFailedExceptionMsg(e)
			);

		/// <summary>Messages</summary>
		public static class M
		{
			/// <summary>Failed to hash password using generic hash</summary>
			public sealed record class GenericPasswordHashFailedExceptionMsg(Exception Value) : IExceptionMsg { }

			/// <summary>Failed to hash password using Argon</summary>
			public sealed record class ArgonPasswordHashFailedExceptionMsg(Exception Value) : IExceptionMsg { }
		}
	}
}
