// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Entities.Auth
{
	/// <summary>
	/// Refresh Token
	/// </summary>
	public record RefreshTokenEntity : IEntity
	{
		/// <inheritdoc/>
		[Ignore]
		public long Id
		{
			get => RefreshTokenId;
			init => RefreshTokenId = value;
		}

		/// <summary>
		/// Token ID
		/// </summary>
		public long RefreshTokenId { get; init; }

		/// <summary>
		/// Token expiry date
		/// </summary>
		public DateTime RefreshTokenExpires
		{
			get =>
				refreshTokenExpires;
			init =>
				refreshTokenExpires = value.ToUniversalTime();
		}

		private DateTime refreshTokenExpires;

		/// <summary>
		/// Token value
		/// </summary>
		public string RefreshTokenValue { get; init; } = string.Empty;

		/// <summary>
		/// User ID this Token belongs to
		/// </summary>
		public long UserId { get; init; }

		/// <summary>
		/// Create default object
		/// </summary>
		public RefreshTokenEntity() { }

		/// <summary>
		/// Create object with values
		/// </summary>
		/// <param name="token">Token value</param>
		/// <param name="expires">Token expiry date</param>
		/// <param name="userId">User ID this Token belongs to</param>
		public RefreshTokenEntity(string token, DateTime expires, long userId)
		{
			RefreshTokenValue = token;
			RefreshTokenExpires = expires;
			UserId = userId;
		}
	}
}
