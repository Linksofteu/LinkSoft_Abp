using System.Net.Mail;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Azure.Communication.Email;
using Microsoft.Extensions.Logging;
using LinkSoft.Abp.AzureMailing.Abstractions;
using LinkSoft.AzureMailing;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace LinkSoft.Abp.AzureMailing;

[ExposeServices(typeof(IEmailSender))]
[Dependency(lifetime: ServiceLifetime.Singleton, ReplaceServices = true, TryRegister = true)]
public class AbpAzureEmailSender(
        AbpAzureEmailSenderConfiguration _senderConfiguration,
        IBackgroundJobManager _backgroundJobManager,
        ILogger<AbpAzureEmailSender> _logger,
        IAzureEmailSender _emailSender,
        IOptions<MailingOptions> _options) 
        : EmailSenderBase(new NullTenant(), _senderConfiguration, _backgroundJobManager), ITransientDependency
{
    protected EmailMessage ConvertMailMessage(MailMessage mail)
    {
        var from = mail.From?.Address;
        _logger.LogDebug("From address: {From}, Connection string: {ConnectionString}", from, _options.Value.AzureEmailSenderConnectionString);

        var recipientAddress = new List<EmailAddress>();
        foreach (var recipient in mail.To) recipientAddress.Add(new EmailAddress(recipient.Address));

        var content = new EmailContent(mail.Subject);

        if (mail.IsBodyHtml) content.Html = mail.Body;
        else content.PlainText = mail.Body;

        if (string.IsNullOrWhiteSpace(from)) throw new FromNotNullException();

        return new EmailMessage(
            from,
            new EmailRecipients(recipientAddress),
            content);
    }

    protected async override Task SendEmailAsync(MailMessage mail)
    {
        _logger.LogDebug("Converting MailMessage to EmailMessage");
        var message = ConvertMailMessage(mail);

        _logger.LogDebug("Sending email to {MailTo} using Azure Email Sender", mail.To);
        await _emailSender.SendEmailAsync(message);
    }
}
