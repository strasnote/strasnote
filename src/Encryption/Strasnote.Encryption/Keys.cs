// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Jeebs;
using Sodium;
using static F.OptionF;

namespace Strasnote.Encryption
{
	/// <summary>
	/// Generate key pairs for public-key cryptography
	/// </summary>
	public static class Keys
	{
		/// <summary>
		/// Generate a key pair and encrypt the private key using a password
		/// </summary>
		/// <param name="password">User's password</param>
		public static Option<EncryptedKeyPair> Generate(string password) =>
			Some(
				PublicKeyBox.GenerateKeyPair,
				e => new M.UnableToGenerateNewKeyPairExceptionMsg(e)
			)
			.Bind(
				k => WithEncryptedPrivateKey(k, password)
			);

		// TODO: use pre-existing private key?  Does it need to be encrypted?

		/// <summary>
		/// Encrypt the private key and return
		/// </summary>
		/// <param name="keyPair">Key Pair</param>
		/// <param name="password">The User's password</param>
		static internal Option<EncryptedKeyPair> WithEncryptedPrivateKey(KeyPair keyPair, string password) =>
			Encrypt.PrivateKey(keyPair.PrivateKey, password)
			.Map(
				p => new EncryptedKeyPair(keyPair.PublicKey, p.key, p.nonce),
				DefaultHandler
			);

		/// <summary>Messages</summary>
		public static class M
		{
			/// <summary>Unable to generate fresh key pair</summary>
			public sealed record UnableToGenerateNewKeyPairExceptionMsg(Exception Value) : ExceptionMsg { }
		}
	}
}
