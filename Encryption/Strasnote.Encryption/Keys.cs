// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

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
		public static EncryptedKeyPair Generate(string password) =>
			WithEncryptedPrivateKey(PublicKeyBox.GenerateKeyPair(), password);

		// TODO: use pre-existing private key?  Does it need to be encrypted?

		/// <summary>
		/// Encrypt the private key and return
		/// </summary>
		/// <param name="keyPair">Key Pair</param>
		/// <param name="password">The User's password</param>
		static internal EncryptedKeyPair WithEncryptedPrivateKey(KeyPair keyPair, string password)
		{
			// Encrypt the private key
			var (encryptedPrivateKey, nonce) = Encrypt.PrivateKey(keyPair.PrivateKey, password);

			// Create KeyPair
			return new(keyPair.PublicKey, encryptedPrivateKey, nonce);
		}
	}
}
