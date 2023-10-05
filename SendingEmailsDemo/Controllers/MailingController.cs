using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendingEmailsDemo.Dtos;
using SendingEmailsDemo.Services;

namespace SendingEmailsDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailingController : ControllerBase
{
    private readonly IMailingService _mailingService;

    public MailingController(IMailingService mailingService)
    {
        _mailingService = mailingService;
    }

    [HttpPost("Send")]
    public async Task<IActionResult> SendMail([FromForm] MailRequestDto mailDto)
    {
        await _mailingService
            .SendEmailAsync(mailDto.ToEmail, mailDto.Subject, mailDto.Body, mailDto.Attachments);

        return Ok();
    }
}
