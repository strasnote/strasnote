// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc;
using Strasnote.Logging;

namespace Strasnote.Notes.Api.Controllers
{
	/// <summary>
	/// Base home
	/// </summary>
	[ApiController]
	[ApiVersion("1.0")]
	[Route("/")]
	public class HomeController : ControllerBase
	{
		private readonly ILog<HomeController> log;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		public HomeController(ILog<HomeController> log) =>
			this.log = log;

		/// <summary>
		/// Test running
		/// </summary>
		[HttpGet]
		public ActionResult<string> Get()
		{
			const string msg = "Notes API Running...";
			log.Debug(msg);
			return msg;
		}
	}
}
