using Microsoft.Extensions.Configuration;
using LinkSoft.Abp.AzureMailing.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace LinkSoft.Abp.AzureMailing;

[ExposeServices(typeof(AbpAzureEmailSenderConfiguration))]
public class AbpAzureEmailSenderConfiguration(
    IOptions<MailingOptions> _options) : IEmailSenderConfiguration, ITransientDependency
{
    public Task<string> GetDefaultFromAddressAsync()
    {
        var fromAddress = _options.Value.DefaultFromAddress ?? throw new FromNotNullException();

        return Task.FromResult(fromAddress);
    }

    public Task<string> GetDefaultFromDisplayNameAsync()
    {
        return Task.FromResult("No Reply");
    }
}