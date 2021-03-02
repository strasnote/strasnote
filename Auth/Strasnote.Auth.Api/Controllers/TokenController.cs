using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Strasnote.Auth.Api.Controllers
{
	[AllowAnonymous]
	public class TokenController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
