// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Text.Json;
using Sodium;

namespace Strasnote.Encryption
{
	/// <summary>
	/// Encrypt values using <see cref="SealedPublicKeyBox"/>
	/// </summary>
	public static class Encrypt
	{
		/// <summary>
		/// Encrypt a private key using a password
		/// </summary>
		/// <param name="privateKey">Private key</param>
		/// <param name="password">User's password</param>
		static internal (byte[] key, byte[] nonce) PrivateKey(byte[] privateKey, string password)
		{
			var nonce = SecretBox.GenerateNonce();
			var encryptionKey = Hash.Password(password);
			return (SecretBox.Create(privateKey, nonce, encryptionKey), nonce);
		}

		/// <summary>
		/// Encrypt JSON (or any other kind of string) for a recipient
		/// </summary>
		/// <param name="json">JSON string</param>
		/// <param name="recipientKeyPair">The recipient's key pair</param>
		public static byte[] String(string json, EncryptedKeyPair recipientKeyPair) =>
			SealedPublicKeyBox.Create(json, recipientKeyPair.PublicKey);

		/// <summary>
		/// Encrypt an object for a recipient (serialises the object as JSON first)
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="obj">Object value</param>
		/// <param name="recipientKeyPair">The recipient's key pair</param>
		public static byte[] Object<T>(T obj, EncryptedKeyPair recipientKeyPair) =>
			String(JsonSerializer.Serialize(obj), recipientKeyPair);
	}
}
