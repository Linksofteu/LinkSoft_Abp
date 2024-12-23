
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using LinkSoft.AzureMailing;

namespace LinkSoft.Abp.AzureMailing;

[DependsOn(typeof(AbpEmailingModule))]
public class AzureMailingModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        base.PreConfigureServices(context);
        context.Services.AddSingleton<IAzureEmailSenderConfiguration, LinkSoft.Abp.AzureMailing.AzureEmailSenderConfiguration>();
        context.Services.AddSingleton<IAzureEmailSender, AzureEmailSender>();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var mailingConnectionString = configuration["ConnectionStrings:Mailing"];   

        if (!string.IsNullOrEmpty(mailingConnectionString)) {
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, AbpAzureEmailSender>());
        } else {
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
        }
    }
}
