To add basic support of bulk operations in an Abp project, all you have to do is to add a dependency to this package in your Entity Framework Core module and EFCoreRepository will automatically use it.

```C#
[DependsOn(typeof(EfCoreBulkOperationProviderModule))]
public class YourEfCoreModule : AbpModule
{
}
```

EFCoreRepository methods currently affected by this package:
- InsertManyAsync
- UpdateManyAsync
- DeleteManyAsync

## Known Issues

### NU1608 Warning
You will encounter the warning `NU1608: Detected package version outside of dependency constraint` related to Pomelo.EntityFrameworkCore.MySql. The package has been tested with dotnet EF Core 9.0 and seems to work just fine, you can suppress it by adding the following to your project file:

```xml
<PropertyGroup>
    <NoWarn>NU1608</NoWarn>
</PropertyGroup>
```

The issue will be fixed once the EFCore.BulkExtensions package is updated to support EF Core 9.0.

This package is a part of larger set of packages for LinkSoft Technologies shared open source repository.

You can find the repository on [GitHub](https://github.com/Linksofteu/LinkSoft_Abp).