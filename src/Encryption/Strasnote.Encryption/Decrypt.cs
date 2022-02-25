// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Text;
using System.Text.Json;
using Jeebs;
using Sodium;
using static F.OptionF;

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
		/// <param name="keyPair">Encrypted key pair</param>Hash
		/// <param name="password">User's password</param>
		static internal Option<byte[]> PrivateKey(EncryptedKeyPair keyPair, string password) =>
			Some(
				password
			)
			.Bind(
				Hash.PasswordGeneric
			)
			.Map(
				h => SecretBox.Open(keyPair.PrivateKey, keyPair.Nonce, h),
				e => new M.UnableToDecryptPrivateKeyExceptionMsg(e)
			);

		/// <summary>
		/// Decrypt a cipher using the recipient's key pair
		/// </summary>
		/// <param name="cipherText">Encrypted contents</param>
		/// <param name="recipientKeyPair">The recipient's key pair</param>
		/// <param name="password">The recipient's public key</param>
		static internal Option<byte[]> AsBytes(byte[] cipherText, EncryptedKeyPair recipientKeyPair, string password) =>
			PrivateKey(recipientKeyPair, password)
			.Map(
				k => SealedPublicKeyBox.Open(cipherText, k, recipientKeyPair.PublicKey),
				e => new M.UnableToDecryptValueExceptionMsg(e)
			);

		/// <summary>
		/// Decrypt and return as a string (usually JSON)
		/// </summary>
		/// <inheritdoc cref="AsBytes(byte[], EncryptedKeyPair, string)"/>
		public static Option<string> AsString(byte[] cipherText, EncryptedKeyPair recipientKeyPair, string password) =>
			AsBytes(cipherText, recipientKeyPair, password)
			.Map(
				b => Encoding.UTF8.GetString(b),
				e => new M.UnableToConvertBytesToStringExceptionMsg(e)
			);

		/// <summary>
		/// Decrypt as string and then deserialise into an object (assuming the value is valid JSON)
		/// </summary>
		/// <typeparam name="T">Object type</typeparam>
		/// <inheritdoc cref="AsBytes(byte[], EncryptedKeyPair, string)"/>
		public static Option<T> AsObject<T>(byte[] cipherText, EncryptedKeyPair recipientKeyPair, string password) =>
			AsString(cipherText, recipientKeyPair, password)
			.Map(
				j => JsonSerializer.Deserialize<T>(j),
				e => new M.JsonDeserialiseExceptionMsg(e)
			)
			.Bind(
				o => o switch
				{
					T =>
						Some(o),

					_ =>
						None<T, M.JsonDeserialisedToNullMsg>()
				}
			);

		/// <summary>Message</summary>
		public static class M
		{
			/// <summary><see cref="JsonSerializer.Deserialize{TValue}(string, JsonSerializerOptions?)"/> returned null</summary>
			public sealed record JsonDeserialisedToNullMsg : Msg { }

			/// <summary>Deserialising JSON failed</summary>
			public sealed record JsonDeserialiseExceptionMsg(Exception Value) : ExceptionMsg { }

			/// <summary><see cref="Encoding.UTF8"/> failed to get string from the decrypted byte array</summary>
			public sealed record UnableToConvertBytesToStringExceptionMsg(Exception Value) : ExceptionMsg { }

			/// <summary>The private key is corrupted, the nonce is wrong, or (most likely) the password is incorrect</summary>
			public sealed record UnableToDecryptPrivateKeyExceptionMsg(Exception Value) : ExceptionMsg { }

			/// <summary>The nonce is wrong, or (most likely) the password is incorrect</summary>
			public sealed record UnableToDecryptValueExceptionMsg(Exception Value) : ExceptionMsg { }
		}
	}
}
