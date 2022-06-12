﻿using Microsoft.AspNetCore.Mvc;

namespace RBS.Email.Sender.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IsAliveController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public IsAliveController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult($"{_env.ApplicationName} alive. Env: {_env.EnvironmentName}.");
        }
    }
}
