using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string recipientEmail, string subject, string body)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("MADU VACCINATIONS", _configuration["MailSettings:SenderEmail"]));
        emailMessage.To.Add(new MailboxAddress("", recipientEmail));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = body
        };

        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_configuration["MailSettings:SmtpServer"], int.Parse(_configuration["MailSettings:Port"]), false);
            await client.AuthenticateAsync(_configuration["MailSettings:SenderEmail"], _configuration["MailSettings:SenderPassword"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
