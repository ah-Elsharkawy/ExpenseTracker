using ExpenseTracker.Email;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    private readonly SmtpOptions _smtpOptions;

    public EmailSender(IOptions<SmtpOptions> smtpOptions)
    {
        _smtpOptions = smtpOptions.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using (var client = new SmtpClient(_smtpOptions.Host, _smtpOptions.Port)
        {
            Credentials = new NetworkCredential(_smtpOptions.UserName, _smtpOptions.Password),
            EnableSsl = _smtpOptions.EnableSsl,
            UseDefaultCredentials = _smtpOptions.UseDefaultCredentials
        })
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpOptions.UserName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}
