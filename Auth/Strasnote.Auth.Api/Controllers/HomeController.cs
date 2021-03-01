using Microsoft.AspNetCore.Mvc;

namespace Strasnote.Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Auth API Running...";
        }
    }
}
