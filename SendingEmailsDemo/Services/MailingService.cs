using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SendingEmailsDemo.Services;


namespace SendingEmailsDemo.Servicesl;

public class MailingService : IMailingService
{
    private readonly MailSettings _mailSettings;

    public MailingService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile>? attachments)
    {
        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.SenderEmail),
            Subject = subject,
        };

        email.To.Add(MailboxAddress.Parse(mailTo));

        var builder = new BodyBuilder();
        
        if(attachments is not null)
        {
            byte[] fileBytes;
            foreach (var attachment in attachments) 
            { 
                if(attachment.Length > 0)
                {
                    using var ms = new MemoryStream();
                    attachment.CopyTo(ms);
                    fileBytes = ms.ToArray();

                    builder.Attachments
                        .Add(attachment.Name, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }
        }

        builder.HtmlBody = body;

        email.Body = builder.ToMessageBody();

        email.From.Add(new MailboxAddress(_mailSettings.SernderName, _mailSettings.SenderEmail));

        using var smtp = new SmtpClient();

        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);

        smtp.Authenticate(_mailSettings.SenderEmail, _mailSettings.AuthKey);

        await smtp.SendAsync(email);

        smtp.Disconnect(true);
    }
}
