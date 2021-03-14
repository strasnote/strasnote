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
			Map(
				PublicKeyBox.GenerateKeyPair,
				e => new Msg.UnableToGenerateNewKeyPairExceptionMsg(e)
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
			Bind(
				() => Encrypt.PrivateKey(keyPair.PrivateKey, password)
			)
			.Map(
				p => new EncryptedKeyPair(keyPair.PublicKey, p.key, p.nonce)
			);

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Unable to generate fresh key pair</summary>
			public sealed record UnableToGenerateNewKeyPairExceptionMsg(Exception Exception) : IExceptionMsg { }
		}
	}
}
