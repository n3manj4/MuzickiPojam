﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SignalMonitoring.API.Controllers
{
    [Route("api/[controller]"),ApiController]
    public class HomeController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetQuiz()
        {
            return Ok();
        }
    }
}