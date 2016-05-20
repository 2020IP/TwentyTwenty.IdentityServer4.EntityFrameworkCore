using AutoMapper;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Services;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Stores;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore
{
    public class EntityFrameworkOptions<TKey>
        where TKey : IEquatable<TKey>
    {
        private IIdentityServerBuilder _builder;

        public EntityFrameworkOptions(IIdentityServerBuilder builder)
        {
            _builder = builder;

            Mapper.Initialize(cfg =>
            {
                EntitiesMap<TKey>.RegisterMappings(cfg);
                ModelsMap<TKey>.RegisterMappings(cfg);
            });
        }

        public EntityFrameworkOptions<TKey> RegisterOperationalStores()
        {
            return RegisterOperationalStores<OperationalContext>();
        }

        public EntityFrameworkOptions<TKey> RegisterOperationalStores<TOperationalContext>()
            where TOperationalContext : OperationalContext
        {
            _builder.Services.AddScoped<OperationalContext, TOperationalContext>();
            _builder.Services.AddScoped<IAuthorizationCodeStore, AuthorizationCodeStore>();
            _builder.Services.AddScoped<ITokenHandleStore, TokenHandleStore>();
            _builder.Services.AddScoped<IConsentStore, ConsentStore>();
            _builder.Services.AddScoped<IRefreshTokenStore, RefreshTokenStore>();

            return this;
        }

        public EntityFrameworkOptions<TKey> RegisterClientStore<TClientContext>()
            where TClientContext : ClientConfigurationContext<TKey>
        {
            _builder.Services.AddScoped<ClientConfigurationContext<TKey>, TClientContext>();
            _builder.Services.AddScoped<IClientStore, ClientStore<TKey>>();
            _builder.Services.AddScoped<ICorsPolicyService, ClientConfigurationCorsPolicyService<TKey>>();

            return this;
        }

        public EntityFrameworkOptions<TKey> RegisterScopeStore<TScopeContext>()
            where TScopeContext : ScopeConfigurationContext<TKey>
        {
            _builder.Services.AddScoped<ScopeConfigurationContext<TKey>, TScopeContext>();
            _builder.Services.AddScoped<IScopeStore, ScopeStore<TKey>>();

            return this;
        }
    }
}