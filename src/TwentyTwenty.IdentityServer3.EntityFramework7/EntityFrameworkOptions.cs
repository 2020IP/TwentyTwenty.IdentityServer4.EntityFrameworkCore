using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using System;
using TwentyTwenty.IdentityServer3.EntityFramework7.DbContexts;
using TwentyTwenty.IdentityServer3.EntityFramework7.Services;
using TwentyTwenty.IdentityServer3.EntityFramework7.Stores;

namespace TwentyTwenty.IdentityServer3.EntityFramework7
{
    public class EntityFrameworkOptions
    {
        private IdentityServerServiceFactory _factory;
        private IServiceProvider _services;

        public EntityFrameworkOptions(IdentityServerServiceFactory factory, IServiceProvider services)
        {
            _factory = factory;
            _services = services;
        }

        public EntityFrameworkOptions RegisterOperationalStores()
        {
            return RegisterOperationalStores<OperationalContext>();
        }

        public EntityFrameworkOptions RegisterOperationalStores<TOperationalContext>()
            where TOperationalContext : OperationalContext
        {
            _factory.RegisterScopedAspnetService<TOperationalContext, OperationalContext>();

            _factory.AuthorizationCodeStore = new Registration<IAuthorizationCodeStore, AuthorizationCodeStore>();
            _factory.TokenHandleStore = new Registration<ITokenHandleStore, TokenHandleStore>();
            _factory.ConsentStore = new Registration<IConsentStore, ConsentStore>();
            _factory.RefreshTokenStore = new Registration<IRefreshTokenStore, RefreshTokenStore>();

            return this;
        }

        public EntityFrameworkOptions RegisterClientStore<TKey, TClientContext>()
            where TKey : IEquatable<TKey>
            where TClientContext : ClientConfigurationContext<TKey>
        {
            _factory.RegisterScopedAspnetService<TClientContext, ClientConfigurationContext<TKey>>();

            _factory.ClientStore = new Registration<IClientStore, ClientStore<TKey>>();
            _factory.CorsPolicyService = new Registration<ICorsPolicyService, ClientConfigurationCorsPolicyService<TKey>>();

            return this;
        }

        public EntityFrameworkOptions RegisterScopeStore<TKey, TScopeContext>()
            where TKey : IEquatable<TKey>
            where TScopeContext : ScopeConfigurationContext<TKey>
        {
            _factory.RegisterScopedAspnetService<TScopeContext, ScopeConfigurationContext<TKey>>();

            _factory.ScopeStore = new Registration<IScopeStore, ScopeStore<TKey>>();

            return this;
        }

        /// <summary>
        /// Convienience method to return the IdentityServerServiceFactory.  Not necessary to call but useful for chaining.
        /// </summary>
        /// <returns>The IdentityServerServiceFactory</returns>
        public IdentityServerServiceFactory Build()
        {
            return _factory;
        }
    }
}