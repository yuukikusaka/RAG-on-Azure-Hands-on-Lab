using Microsoft.AspNetCore.Mvc;

namespace Simple.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new { status = "ok" });
        }
    }
}
