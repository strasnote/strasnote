// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Logging;

namespace Strasnote.Auth.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TestController : Controller
	{
		private readonly IUserRepository users;

		private readonly ILog<TestController> log;

		public TestController(IUserRepository users, ILog<TestController> log) =>
			(this.users, this.log) = (users, log);

		[HttpGet]
		[Route("id/{id}")]
		public async Task<MiniUser> GetById(long id)
		{
			log.Trace("Get user with ID: {Id}", id);
			return await users.RetrieveAsync<MiniUser>(id);
		}

		[HttpGet]
		[Route("email/{email}")]
		public async Task<MiniUser> GetByEmail(string email)
		{
			log.Trace("Get user with Email: {Email}", email);
			return await users.RetrieveByEmailAsync<MiniUser>(email);
		}

		public sealed record MiniUser(long Id, string Email)
		{
			public MiniUser() : this(0, string.Empty) { }
		}
	}
}
