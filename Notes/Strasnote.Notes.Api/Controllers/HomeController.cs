using Microsoft.AspNetCore.Mvc;

namespace Strasnote.Notes.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HomeController : ControllerBase
	{
		[HttpGet]
		public ActionResult<string> Get()
		{
			return "Notes API running...";
		}
	}
}
