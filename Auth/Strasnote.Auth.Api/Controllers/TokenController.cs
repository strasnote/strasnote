using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
