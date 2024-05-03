using Microsoft.AspNetCore.Mvc;

namespace Broker.Controllers
{
    public class BrokerController : Controller
    {
        [Route("publish-message")]
        [HttpPost]
        public async Task<IActionResult> PublishMessage([FromBody] object message)
        {
            return Ok();
        }
    }
}
