using Microsoft.Extensions.Configuration;
using LinkSoft.Abp.AzureMailing.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace LinkSoft.Abp.AzureMailing;

[ExposeServices(typeof(AbpAzureEmailSenderConfiguration))]
public class AbpAzureEmailSenderConfiguration(
    IOptions<MailingOptions> _options,
    ILogger<AbpAzureEmailSenderConfiguration> _logger) : IEmailSenderConfiguration, ITransientDependency
{
    public Task<string> GetDefaultFromAddressAsync()
    {
        _logger.LogDebug("Getting default from address");
        var fromAddress = _options.Value.DefaultFromAddress ?? throw new FromNotNullException();
        _logger.LogDebug("Default from address: {FromAddress}", fromAddress);

        return Task.FromResult(fromAddress);
    }

    public Task<string> GetDefaultFromDisplayNameAsync()
    {
        return Task.FromResult("No Reply");
    }
}