using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace lan_side_project.Services;
public class MailService(IConfiguration configuration)
{
    private readonly string _smtpHost = configuration.GetValue<string>("SMTP:HOST") ?? "";
    private readonly int _smtpPort = configuration.GetValue<int>("SMTP:PORT", 587);
    private readonly string _smtpUsername = configuration.GetValue<string>("SMTP:USERNAME") ?? "";
    private readonly string _smtpPassword = configuration.GetValue<string>("SMTP:PASSWORD") ?? "";
    private readonly bool _smtpEnableSsl = configuration.GetValue<bool>("SMTP:ENABLE_SSL", true);
    private readonly string _smtpDefaultFrom = configuration.GetValue<string>("SMTP:DEFAULT_FROM", "lan@example.com")!;

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpDefaultFrom),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(to);

        using var smtpClient = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
            EnableSsl = _smtpEnableSsl
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}


