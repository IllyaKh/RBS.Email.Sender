using Microsoft.AspNetCore.Mvc;

namespace RBS.Email.Sender.WebApi.Controllers
{
    public class SenderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public Task<string> SendMessage()
        {

        }
    }
}
