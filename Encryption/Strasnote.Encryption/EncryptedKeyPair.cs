// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

namespace Strasnote.Encryption
{
	/// <summary>
	/// Holds a public and private key pair, where the private key is encrypted with a password
	/// </summary>
	/// <param name="PublicKey">Public Key</param>
	/// <param name="PrivateKey">Private Key</param>
	/// <param name="Nonce">Nonce used for encrypting the private key</param>
	public sealed record EncryptedKeyPair(byte[] PublicKey, byte[] PrivateKey, byte[] Nonce);
}
