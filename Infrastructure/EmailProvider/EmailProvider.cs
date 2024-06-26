﻿using System.Net;
using Application.Abstraction;
using Application.Models;
using System.Net.Mail;
using Application.Models.Email;
using Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.EmailProvider
{
    public class EmailProvider : IEmailProvider
    {
        private readonly EmailOptions _emailOptions;
        private readonly ILogger<EmailProvider> _logger;
        private  MailAddress _fromAddress = null!;
        private  MailAddress _toAddress = null!;

        public EmailProvider(IOptions<EmailOptions> options, ILogger<EmailProvider> logger)
        {
            this._emailOptions = options.Value;
            this._logger = logger;
        }

        public int ResendMinutesDelay => _emailOptions.ResendMinutesDelay;
        public string ConfirmationEmailBody => _emailOptions.ConfirmationEmailBody;
        public string ResetPasswordEmailBody => _emailOptions.ResetPasswordEmailBody;

        public async Task<Result> SendAsync(EmailMessageComposerModel emailModel, CancellationToken cancellationToken)
        {

            if (emailModel is null)
            {
                throw new ArgumentNullException(nameof(emailModel),"SendAsync: emailModel is null");
            }

            _fromAddress = new MailAddress(_emailOptions.SenderEmail);
            _toAddress = new MailAddress(emailModel.Recipient.Value);

            string emailBody = emailModel.Content.ComposeBody(emailModel.Content.ConfirmationToken);
            
            using (var smtpClient = new SmtpClient()
                   {
                       Credentials = new NetworkCredential(
                           _emailOptions.SenderEmail,
                           _emailOptions.Password),
                       Host = _emailOptions.Host,
                       Port = _emailOptions.Port,
                       EnableSsl = true,
                       UseDefaultCredentials = false
                   })

            {
                using var message = new MailMessage(_fromAddress, _toAddress);
                message.Body = emailBody;

                if (emailModel.CopyTo is not null)
                {
                    message.CC.Add(new MailAddress(emailModel.CopyTo.Value));
                }

                try
                {
                    await smtpClient.SendMailAsync(message, cancellationToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Error during email sending to {emailModel.Recipient.Value}");
                    throw new Exception($"Error during email sending to {emailModel.Recipient.Value}", e);
                }

                _logger.LogInformation($"The mail has been sent to {emailModel.Recipient.Value}");
            }

            return Result.Success();
        }
    }
}
