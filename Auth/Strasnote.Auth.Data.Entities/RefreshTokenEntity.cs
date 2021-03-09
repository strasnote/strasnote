// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;

namespace Strasnote.Auth.Data.Entities
{
	public record RefreshTokenEntity
	{
		public int Id { get; init; }

		public DateTimeOffset Expires { get; private init; }

		public string Token { get; private init; }

		public long UserId { get; private init; }

		public RefreshTokenEntity(string token, DateTimeOffset expires, long userId)
		{
			Token = token;
			Expires = expires;
			UserId = userId;
		}
	}
}
