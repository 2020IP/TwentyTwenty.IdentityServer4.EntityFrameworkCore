using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Interfaces;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Services;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Stores;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore
{
    public class EntityFrameworkOptions
    {
        private IIdentityServerBuilder _builder;

        public EntityFrameworkOptions(IIdentityServerBuilder builder)
        {
            _builder = builder;
        }

        public EntityFrameworkOptions RegisterOperationalStores()
        {
            return RegisterOperationalStores<OperationalContext>();
        }

        public EntityFrameworkOptions RegisterOperationalStores<TOperationalContext>()
            where TOperationalContext : class, IOperationalContext
        {
            _builder.Services.AddScoped<IOperationalContext, TOperationalContext>();
            _builder.Services.AddScoped<IPersistedGrantService, PersistedGrantService>();

            return this;
        }

        public EntityFrameworkOptions RegisterClientStore<TKey, TClientContext>()
            where TKey : IEquatable<TKey>
            where TClientContext : class, IClientConfigurationContext<TKey>
        {
            _builder.Services.AddScoped<IClientConfigurationContext<TKey>, TClientContext>();
            _builder.Services.AddScoped<IClientStore, ClientStore<TKey>>();
            _builder.Services.AddScoped<ICorsPolicyService, ClientConfigurationCorsPolicyService<TKey>>();

            return this;
        }

        public EntityFrameworkOptions RegisterScopeStore<TKey, TScopeContext>()
            where TKey : IEquatable<TKey>
            where TScopeContext : class, IScopeConfigurationContext<TKey>
        {
            _builder.Services.AddScoped<IScopeConfigurationContext<TKey>, TScopeContext>();
            _builder.Services.AddScoped<IScopeStore, ScopeStore<TKey>>();

            return this;
        }
    }
}