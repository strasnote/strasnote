// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using MaybeF;
using Sodium;

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
		public static Maybe<EncryptedKeyPair> Generate(string password) =>
			F.Some(
				PublicKeyBox.GenerateKeyPair,
				e => new R.UnableToGenerateNewKeyPairExceptionReason(e)
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
		static internal Maybe<EncryptedKeyPair> WithEncryptedPrivateKey(KeyPair keyPair, string password) =>
			Encrypt.PrivateKey(
				keyPair.PrivateKey, password
			)
			.Map(
				p => new EncryptedKeyPair(keyPair.PublicKey, p.key, p.nonce),
				F.DefaultHandler
			);

		/// <summary>Reasons</summary>
		public static class R
		{
			/// <summary>Unable to generate fresh key pair</summary>
			public sealed record class UnableToGenerateNewKeyPairExceptionReason(Exception Value) : IExceptionReason;
		}
	}
}
