// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Mvc;

namespace Strasnote.Notes.Api.Controllers
{
	/// <summary>
	/// Base home
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class HomeController : ControllerBase
	{
		/// <summary>
		/// Test running
		/// </summary>
		[HttpGet]
		public ActionResult<string> Get()
		{
			return "Notes API running...";
		}
	}
}
