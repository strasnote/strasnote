// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Security.Claims;

namespace Tests.Strasnote.Auth
{
	public class TestIdentity : ClaimsIdentity
	{
		public TestIdentity(params Claim[] claims) : base(claims)
		{
		}
	}
}
