// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Text.Json;
using Jeebs;
using Jeebs.Linq;
using Sodium;
using static F.OptionF;

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
		static internal Option<(byte[] key, byte[] nonce)> PrivateKey(byte[] privateKey, string password) =>
			Return(
				SecretBox.GenerateNonce,
				e => new Msg.UnableToGenerateNonceToEncryptPrivateKeyExceptionMsg(e)
			)
			.Bind(
				nonce => from hash in Hash.Password(password) select (nonce, hash)
			)
			.Map(
				p => (SecretBox.Create(privateKey, p.nonce, p.hash), p.nonce),
				e => new Msg.UnableToEncryptPrivateKeyExceptionMsg(e)
			);

		/// <summary>
		/// Encrypt JSON (or any other kind of string) for a recipient
		/// </summary>
		/// <param name="json">JSON string</param>
		/// <param name="recipientKeyPair">The recipient's key pair</param>
		public static Option<byte[]> String(string json, EncryptedKeyPair recipientKeyPair) =>
			Return(
				() => SealedPublicKeyBox.Create(json, recipientKeyPair.PublicKey),
				e => new Msg.UnableToEncryptStringExceptionMsg(e)
			);

		/// <summary>
		/// Encrypt an object for a recipient (serialises the object as JSON first)
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <param name="obj">Object value</param>
		/// <param name="recipientKeyPair">The recipient's key pair</param>
		public static Option<byte[]> Object<T>(T obj, EncryptedKeyPair recipientKeyPair) =>
			Return(
				() => JsonSerializer.Serialize(obj),
				e => new Msg.JsonSerialiseExceptionMsg(e)
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
		public static class Msg
		{
			/// <summary>Serilising object as JSON failed</summary>
			public sealed record JsonSerialiseExceptionMsg(Exception Exception) : IExceptionMsg { }

			/// <summary>Encrypting the private key failed</summary>
			public sealed record UnableToEncryptPrivateKeyExceptionMsg(Exception Exception) : IExceptionMsg { }

			/// <summary>Encrypting the (JSON) string failed</summary>
			public sealed record UnableToEncryptStringExceptionMsg(Exception Exception) : IExceptionMsg { }

			/// <summary>Generating a nonce to encrypt the private key failed</summary>
			public sealed record UnableToGenerateNonceToEncryptPrivateKeyExceptionMsg(Exception Exception) : IExceptionMsg { }
		}
	}
}
