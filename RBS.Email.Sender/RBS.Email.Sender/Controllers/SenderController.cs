using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RBS.Email.Sender.Services.Interface;

namespace RBS.Email.Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly ISenderService _senderService;
        public SenderController(ISenderService senderService)
        {
            _senderService = senderService;
        }

        [HttpGet]
        public async Task<IActionResult> Send()
        {
            _senderService.Send();

            return Ok();
        }
    }
}
