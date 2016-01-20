#20|20 IdentityServer4.EntityFramework7

###Entity Framework 7 persistence layer for [IdentityServer v4](https://github.com/IdentityServer/IdentityServer4)
[![Build status](https://ci.appveyor.com/api/projects/status/wnvka7rjwx66wjk5/branch/master?svg=true)](https://ci.appveyor.com/project/2020IP/twentytwenty-identityserver4-entityframework7/branch/master)
[![NuGet](https://img.shields.io/nuget/v/TwentyTwenty.IdentityServer4.EntityFramework7.svg)](https://www.nuget.org/packages/TwentyTwenty.IdentityServer4.EntityFramework7/)

#### Usage
The primary key type can be configured for ClientStore and ScopeStore.  To facilitate this, subclass the `ClientConfigurationContext<TKey>` and `ScopeConfigurationContext<TKey>` with the desired key type.
```
public class ClientConfigurationContext : ClientConfigurationContext<Guid>
{
	public ClientConfigurationContext(DbContextOptions options)
		: base(options)
	{ }
}

public class ScopeConfigurationContext : ScopeConfigurationContext<Guid>
{
	public ScopeConfigurationContext(DbContextOptions options)
		: base(options)
	{ }
}
```
In the `Startup.cs`, register your DbContexts with Entity Framework
```
public void ConfigureServices(IServiceCollection services)
{
	...
	services.AddEntityFramework()
		.AddSqlServer()
		.AddDbContext<ClientConfigurationContext>(o => o.UseSqlServer(connectionString))
		.AddDbContext<ScopeConfigurationContext>(o => o.UseSqlServer(connectionString))
		.AddDbContext<OperationalContext>(o => o.UseSqlServer(connectionString));
	...
}
```
Configure the `IdentityServerServiceFactory` to use the EF stores.
```
public void Configure(IApplicationBuilder app)
{
	...
	var factory = new IdentityServerServiceFactory();
	factory.ConfigureEntityFramework(app.ApplicationServices)
		.RegisterOperationalStores()
		.RegisterClientStore<Guid, ClientConfigurationContext>()
		.RegisterScopeStore<Guid, ScopeConfigurationContext>();

	owinAppBuilder.UseIdentityServer(new IdentityServerOptions
	{
		...
		Factory = factory,
		...
	});
	...
}
```
#### Contributing
To get started, [sign the Contributor License Agreement](https://www.clahub.com/agreements/2020IP/TwentyTwenty.IdentityServer4.EntityFramework7).