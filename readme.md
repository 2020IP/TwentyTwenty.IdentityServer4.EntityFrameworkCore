# 20|20 IdentityServer4.EntityFrameworkCore

## Deprecated: This repository is no longer in active development or maintenance.  Use the official library instead: [IdentityServer4.EntityFramework.Storage](https://github.com/IdentityServer/IdentityServer4.EntityFramework.Storage)


### Entity Framework Core persistence layer for [IdentityServer v4](https://github.com/IdentityServer/IdentityServer4)
[![NuGet](https://img.shields.io/nuget/v/TwentyTwenty.IdentityServer4.EntityFrameworkCore.svg)](https://www.nuget.org/packages/TwentyTwenty.IdentityServer4.EntityFrameworkCore/)

*CI Nuget Feed*
https://ci.appveyor.com/nuget/twentytwenty-identityserver4-e-eghymilgfl2p

### Usage
The primary key type can be configured for ClientStore and ScopeStore.
To facilitate this, when you Register your contexts make sure you use the correct key.

If you have set it up like this you can use EntityFramework Migrations against a single context to build your DB

```
public class YourDataContext : IClientConfigurationContext<Guid>, IScopeConfigurationContext<Guid>, IOperationalContext
{
	public YourDataContext(DbContextOptions<YourDataContext> options)
		: base(options)
	{ }

	public override void OnModelCreating(ModelBuilder modelBuilder)
	{
		...
		modelBuilder.ConfigureClientConfigurationContext<Guid>();
		modelBuilder.ConfigureScopeConfigurationContext<Guid>();
		modelBuilder.ConfigureOperationalContext();
		...
		base.OnModelCreating(modelBuilder);
	}
}
```
In the `Startup.cs`, register your DbContext with Entity Framework as normal
```
public void ConfigureServices(IServiceCollection services)
{
	...
	services.AddEntityFrameworkSqlServer()
		.AddDbContext<YourDatabaseContext>(o => o
			.UseSqlServer(connectionString, b =>
			b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name)))
	...
}
```
Register your datacontext(s) with the EFCore Contexts
```
public void ConfigureServices(IServiceCollection services)
{
	...
	var builder = services.AddIdentityServer(options =>
	{
		options.RequireSsl = false;
	});

	builder.ConfigureEntityFramework()
		.RegisterOperationalStores<YourDataContext>()
		.RegisterClientStore<Guid, YourDataContext>()
		.RegisterScopeStore<Guid, YourDataContext>();
	...
}
```

#### Old Usage
The primary key type can be configured for ClientStore and ScopeStore.  To facilitate this, subclass the `ClientConfigurationContext<TKey>` and `ScopeConfigurationContext<TKey>` with the desired key type.
```
public class ClientConfigurationContext : ClientConfigurationContext<Guid>
{
	public ClientConfigurationContext(DbContextOptions<ClientConfigurationContext> options)
		: base(options)
	{ }
}

public class ScopeConfigurationContext : ScopeConfigurationContext<Guid>
{
	public ScopeConfigurationContext(DbContextOptions<ScopeConfigurationContext> options)
		: base(options)
	{ }
}
```
In order to enable extensibility of the `OperationalContext`, it must be subclassed. The main reason behind this is EFCore requires the `DbContextOptions` constructor to supply a type, which we cannot set to `OperationalContext` as it will not allow this class to be extensible.
```
public class OperationalContextEx : OperationalContext
{
	public OperationalContextEx(DbContextOptions<OperationalContextEx> options)
		: base(options)
	{ }
}
```
In the `Startup.cs`, register your DbContexts with Entity Framework
```
public void ConfigureServices(IServiceCollection services)
{
	...
	services.AddEntityFrameworkSqlServer()
		.AddDbContext<ClientConfigurationContext>(o => o
			.UseSqlServer(connectionString, b =>
			b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name)))
		.AddDbContext<ScopeConfigurationContext>(o => o
			.UseSqlServer(connectionString, b =>
			b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name)))
		.AddDbContext<OperationalContext>(o => o
			.UseSqlServer(connectionString, b =>
			b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name)));
	...
}
```
Register the EFCore Contexts
```
public void ConfigureServices(IServiceCollection services)
{
	...
	var builder = services.AddIdentityServer(options =>
	{
		options.RequireSsl = false;
	});

	builder.ConfigureEntityFramework()
		.RegisterOperationalStores()
		.RegisterClientStore<Guid, ClientConfigurationContext>()
		.RegisterScopeStore<Guid, ScopeConfigurationContext>();
	...
}
```
#### Contributing
To get started, [sign the Contributor License Agreement](https://www.clahub.com/agreements/2020IP/TwentyTwenty.IdentityServer4.EntityFrameworkCore).
