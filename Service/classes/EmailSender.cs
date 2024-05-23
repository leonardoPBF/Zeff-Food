using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Implementacion necesaria para enviar emails
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Zeff_Food.Models;
using Zeff_Food.Service.Interfaces;


namespace Zeff_Food.Service.classes
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
        email.To.Add(new MailboxAddress(to, to));
        email.Subject = subject;
        email.Body = new TextPart("plain") { Text = body };

        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        email.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);               
                await client.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new InvalidOperationException(ex.Message, ex);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
        
    }
    }
    
}