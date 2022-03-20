// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc;
using Strasnote.Logging;

namespace Strasnote.Auth.Api.Controllers
{
	[ApiController]
	[Route("/")]
	public class HomeController : ControllerBase
	{
		private readonly ILog<HomeController> log;

		public HomeController(ILog<HomeController> log) =>
			this.log = log;

		[HttpGet]
		public ActionResult<string> Get()
		{
			const string msg = "Auth API Running...";
			log.Debug(msg);
			return msg;
		}
	}
}
