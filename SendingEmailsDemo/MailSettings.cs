namespace SendingEmailsDemo;

public class MailSettings
{
    public string Host { get; set; } = String.Empty;
    public int Port { get; set; }
    public string SenderEmail { get; set; } = String.Empty;
    public string SernderName { get; set; } = String.Empty;
    public string AuthKey { get; set; } = String.Empty;
}
