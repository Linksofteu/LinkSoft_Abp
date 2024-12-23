using Volo.Abp;

namespace LinkSoft.Abp.AzureMailing.Abstractions;

public class ConnStringNotSetException : UserFriendlyException
{
    public ConnStringNotSetException() : base("Configuration option Connection String must not be null")
    {
    }
}