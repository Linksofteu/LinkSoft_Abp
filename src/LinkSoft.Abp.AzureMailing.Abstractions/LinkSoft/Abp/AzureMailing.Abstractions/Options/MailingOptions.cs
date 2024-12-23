namespace LinkSoft.Abp.AzureMailing.Abstractions;

public class MailingOptions
{
    public bool AllowDevEmailSender { get; set; } = true;
    public string DevEmailSenderStoragePath { get; set; } = "";
    public string? AzureEmailSenderConnectionString { get; set; } = "";
    public string DefaultFromAddress { get; set; } = "";
}