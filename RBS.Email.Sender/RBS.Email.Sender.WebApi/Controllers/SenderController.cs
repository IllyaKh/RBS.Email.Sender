using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RBS.Email.Sender.Common.Models;
using RBS.Email.Sender.Services.Interface;
using RBS.Email.Sender.WebApi.Models;

namespace RBS.Email.Sender.WebApi.Controllers
{
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

        public async Task<IActionResult> SendMessage(EmailRequest model)
        {
            await _senderService.Send(_mapper.Map<EmailModel>(model));

            return Ok();
        }

        public async Task<IActionResult> SendMessages(IEnumerable<EmailRequest> models)
        {
            throw new NotImplementedException();
        }
    }
}
