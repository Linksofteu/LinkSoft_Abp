This package wraps the shared Azure mailing implementation into an Abp.io framework module.

Usage:
```csharp
[DependsOn(typeof(AzureMailingModule))]
public class AmendmentSheetsMailingModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<MailingOptions>(options =>
        {
            options.DefaultFromAddress = "noreply@domain.com";
            options.AzureEmailSenderConnectionString = "AzureCommunicationServicesConnectionString";
        });
    }
}
...
```

This package is a part of larger set of packages for LinkSoft Technologies shared open source repository.

You can find the repository on [GitHub](https://github.com/Linksofteu/LinkSoft).