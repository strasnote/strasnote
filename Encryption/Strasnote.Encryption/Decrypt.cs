// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Text;
using System.Text.Json;
using Sodium;

namespace Strasnote.Encryption
{
	/// <summary>
	/// Decrypt <see cref="SealedPublicKeyBox"/>
	/// </summary>
	public static class Decrypt
	{
		/// <summary>
		/// Decrypt a private key encrypted using a password
		/// </summary>
		/// <param name="keyPair">Encrypted key pair</param>
		/// <param name="password">User's password</param>
		static internal byte[] PrivateKey(EncryptedKeyPair keyPair, string password) =>
			SecretBox.Open(keyPair.PrivateKey, keyPair.Nonce, Hash.Password(password));

		/// <summary>
		/// Decrypt a cipher using the recipient's key pair
		/// </summary>
		/// <param name="cipherText">Encrypted contents</param>
		/// <param name="recipientKeyPair">The recipient's key pair</param>
		/// <param name="password">The recipient's public key</param>
		static internal byte[] AsBytes(byte[] cipherText, EncryptedKeyPair recipientKeyPair, string password)
		{
			var decryptedPrivateKey = PrivateKey(recipientKeyPair, password);
			return SealedPublicKeyBox.Open(cipherText, decryptedPrivateKey, recipientKeyPair.PublicKey);
		}

		/// <summary>
		/// Decrypt and return as a string (usually JSON)
		/// </summary>
		/// <inheritdoc cref="AsBytes(byte[], EncryptedKeyPair, string)"/>
		public static string AsString(byte[] cipherText, EncryptedKeyPair recipientKeyPair, string password)
		{
			var decrypted = AsBytes(cipherText, recipientKeyPair, password);
			return Encoding.UTF8.GetString(decrypted);
		}

		/// <summary>
		/// Decrypt as string and then deserialise into an object (assuming the value is valid JSON)
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <inheritdoc cref="AsBytes(byte[], EncryptedKeyPair, string)"/>
		public static T? AsObject<T>(byte[] cipherText, EncryptedKeyPair recipientKeyPair, string password)
		{
			var json = AsString(cipherText, recipientKeyPair, password);
			return JsonSerializer.Deserialize<T>(json);
		}
	}
}
