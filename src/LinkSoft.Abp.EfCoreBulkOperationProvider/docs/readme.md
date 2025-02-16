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

This package is a part of larger set of packages for LinkSoft Technologies shared open source repository.

You can find the repository on [GitHub](https://github.com/Linksofteu/LinkSoft_Abp).