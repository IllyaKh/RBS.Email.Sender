using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RBS.Email.Sender.Common.Models;
using RBS.Email.Sender.Services.Interface;
using RBS.Email.Sender.WebApi.Models;

namespace RBS.Email.Sender.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly ISenderService _senderService;
        private readonly IMapper _mapper;

        public SenderController(ISenderService senderService,
            IMapper mapper)
        {
            _senderService = senderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(EmailRequest model)
        {
            _senderService.Send(_mapper.Map<EmailModel>(model));

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SendMessages(IEnumerable<EmailRequest> models)
        {
            throw new NotImplementedException();
        }
    }
}
