// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Text.Json;
using MaybeF;
using MaybeF.Linq;
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
		static internal Maybe<(byte[] key, byte[] nonce)> PrivateKey(byte[] privateKey, string password) =>
			F.Some(
				SecretBox.GenerateNonce,
				e => new M.UnableToGenerateNonceToEncryptPrivateKeyExceptionMsg(e)
			)
			.Bind(
				nonce => from hash in Hash.PasswordGeneric(password) select (nonce, hash)
			)
			.Map(
				p => (SecretBox.Create(privateKey, p.nonce, p.hash), p.nonce),
				e => new M.UnableToEncryptPrivateKeyExceptionMsg(e)
			);

		/// <summary>
		/// Encrypt JSON (or any other kind of string) for a recipient
		/// </summary>
		/// <param name="json">JSON string</param>
		/// <param name="recipientKeyPair">The recipient's key pair</param>
		public static Maybe<byte[]> String(string json, EncryptedKeyPair recipientKeyPair) =>
			F.Some(
				() => SealedPublicKeyBox.Create(json, recipientKeyPair.PublicKey),
				e => new M.UnableToEncryptStringExceptionMsg(e)
			);

		/// <summary>
		/// Encrypt an object for a recipient (serialises the object as JSON first)
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="obj">Object value</param>
		/// <param name="recipientKeyPair">The recipient's key pair</param>
		public static Maybe<byte[]> Object<T>(T obj, EncryptedKeyPair recipientKeyPair) =>
			F.Some(
				() => JsonSerializer.Serialize(obj),
				e => new M.JsonSerialiseExceptionMsg(e)
			)
			.Bind(
				x => obj switch
				{
					T =>
						String(x, recipientKeyPair),

					_ =>
						String(string.Empty, recipientKeyPair)
				}
			);

		/// <summary>Messages</summary>
		public static class M
		{
			/// <summary>Serilising object as JSON failed</summary>
			public sealed record class JsonSerialiseExceptionMsg(Exception Value) : IExceptionMsg;

			/// <summary>Encrypting the private key failed</summary>
			public sealed record class UnableToEncryptPrivateKeyExceptionMsg(Exception Value) : IExceptionMsg;

			/// <summary>Encrypting the (JSON) string failed</summary>
			public sealed record class UnableToEncryptStringExceptionMsg(Exception Value) : IExceptionMsg;

			/// <summary>Generating a nonce to encrypt the private key failed</summary>
			public sealed record class UnableToGenerateNonceToEncryptPrivateKeyExceptionMsg(Exception Value) : IExceptionMsg;
		}
	}
}
