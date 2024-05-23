using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Zeff_Food.Service.Interfaces;
using Zeff_Food.Models;

namespace Zeff_Food.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailSender _emailService;

        public EmailController(ILogger<EmailController> logger, IEmailSender emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok("Index method in EmailController");
        }

    
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            if (emailRequest == null || string.IsNullOrEmpty(emailRequest.ToEmail) || string.IsNullOrEmpty(emailRequest.Subject) || string.IsNullOrEmpty(emailRequest.Body))
            {
                return BadRequest("Invalid email request");
            }

            try
            {
                await _emailService.SendEmailAsync(emailRequest.ToEmail, emailRequest.Subject, emailRequest.Body);
                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                // Log exception (optional)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    } 
}