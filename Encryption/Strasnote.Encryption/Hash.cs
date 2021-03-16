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
		static internal Option<byte[]> Password(string password) =>
			Return(
				() => GenericHash.Hash(Encoding.UTF8.GetBytes(password), null, 32), // must be 32 bytes
				e => new Msg.NullPasswordExceptionMsg(e)
			);

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Failed to hash password</summary>
			public sealed record NullPasswordExceptionMsg(Exception Exception) : IExceptionMsg { }
		}
	}
}
