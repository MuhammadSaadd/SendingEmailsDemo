using System.ComponentModel.DataAnnotations;

namespace SendingEmailsDemo.Dtos;

public class MailRequestDto
{
    [Required]
    public string ToEmail { get; set; } = String.Empty;
    [Required]
    public string Subject { get; set; } = String.Empty;
    [Required]
    public string Body { get; set; } = String.Empty;
    public IList<IFormFile> Attachments { get; set; } = null!;
}
