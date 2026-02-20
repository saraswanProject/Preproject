using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Preproject.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            return Ok("Authorized Access");
        }

        [HttpGet]
        [Route("Gettwo")]
        public IActionResult Gettwo()
        {
            return Ok("Authorized Access");
        }
    }
}





