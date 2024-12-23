using LinkSoft.Abp.AzureMailing.Abstractions;
using LinkSoft.AzureMailing;
using Microsoft.Extensions.Options;

namespace LinkSoft.Abp.AzureMailing;

public class AzureEmailSenderConfiguration(IOptions<MailingOptions> _options) : IAzureEmailSenderConfiguration
{
    public string? AzureEmailSenderConnectionString
    {
        get
        {
            var connString = _options.Value.AzureEmailSenderConnectionString;
            if (string.IsNullOrWhiteSpace(connString))
            {
                throw new ConnStringNotSetException();
            }
            return connString;
        }
    }
}