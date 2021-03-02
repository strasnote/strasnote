using Microsoft.AspNetCore.Mvc;
using Strasnote.Logging;

namespace Strasnote.Auth.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HomeController : ControllerBase
	{
		private readonly ILog<HomeController> log;

		public HomeController(ILog<HomeController> log) =>
			this.log = log;

		[HttpGet]
		public ActionResult<string> Get()
		{
			return "Auth API Running...";
		}
	}
}
