// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Security.Claims;

namespace Tests.Strasnote.Auth
{
	public class TestPrincipal : ClaimsPrincipal
	{
		public TestPrincipal(params Claim[] claims) : base(new TestIdentity(claims))
		{
		}
	}
}
