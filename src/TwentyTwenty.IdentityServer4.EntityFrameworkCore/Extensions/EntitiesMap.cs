using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Extensions;
using Models = IdentityServer4.Core.Models;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities
{
    public class EntitiesKeyedMapProfile<TKey> : Profile
        where TKey : IEquatable<TKey>
    {
        protected override void Configure()
        {
            CreateMap<Scope<TKey>, Models.Scope>(MemberList.Destination)
                .ForMember(x => x.Claims, opts => opts.MapFrom(src => src.ScopeClaims.Select(x => x)));
            CreateMap<ScopeClaim<TKey>, Models.ScopeClaim>(MemberList.Destination);
            CreateMap<ScopeSecret<TKey>, Models.Secret>(MemberList.Destination)
                .ForMember(x => x.Expiration, opt => opt.MapFrom(src => src.Expiration.HasValue ? new DateTimeOffset(src.Expiration.Value, TimeSpan.Zero) : default(DateTimeOffset?)));

            CreateMap<ClientSecret<TKey>, Models.Secret>(MemberList.Destination)
                .ForMember(x => x.Expiration, opt => opt.MapFrom(src => src.Expiration.HasValue ? new DateTimeOffset(src.Expiration.Value, TimeSpan.Zero) : default(DateTimeOffset?)));
            CreateMap<Client<TKey>, Models.Client>(MemberList.Destination)
                .ForMember(x => x.UpdateAccessTokenClaimsOnRefresh, opt => opt.MapFrom(src => src.UpdateAccessTokenOnRefresh))
                .ForMember(x => x.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => x.Uri)))
                .ForMember(x => x.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => x.Uri)))
                .ForMember(x => x.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => x.Provider)))
                .ForMember(x => x.AllowedScopes, opt => opt.MapFrom(src => src.AllowedScopes.Select(x => x.Scope)))
                .ForMember(x => x.AllowedCorsOrigins, opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => x.Origin)))
                .ForMember(x => x.AllowedGrantTypes, opt => opt.MapFrom(src => src.AllowedGrantTypes.Select(x => x.GrantType)))
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new Claim(x.Type, x.Value))));

            CreateMap<Consent, Models.Consent>(MemberList.Destination)
                .ForMember(x => x.Scopes, opts => opts.MapFrom(src => src.Scopes.ParseScopes()))
                .ForMember(x => x.Subject, opts => opts.MapFrom(src => src.SubjectId));
        }
    }

    public class EntitiesMapProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Consent, Models.Consent>(MemberList.Destination)
                .ForMember(x => x.Scopes, opts => opts.MapFrom(src => src.Scopes.ParseScopes()))
                .ForMember(x => x.Subject, opts => opts.MapFrom(src => src.SubjectId));
        }
    }

    public static class EntitiesMap<TKey> where TKey : IEquatable<TKey>
    {
        static EntitiesMap()
        {
            Mapper.Configuration.AddProfile<EntitiesKeyedMapProfile<TKey>>();
        }

        public static Models.Scope ToModel(Scope<TKey> s)
        {
            if (s == null)
            {
                return null;
            }
            if (s.ScopeClaims == null)
            {
                s.ScopeClaims = new List<ScopeClaim<TKey>>();
            }
            if (s.ScopeSecrets == null)
            {
                s.ScopeSecrets = new List<ScopeSecret<TKey>>();
            }

            return Mapper.Map<Scope<TKey>, Models.Scope>(s);
        }

        public static Models.Client ToModel(Client<TKey> s)
        {
            if (s == null)
            {
                return null;
            }
            if (s.ClientSecrets == null)
            {
                s.ClientSecrets = new List<ClientSecret<TKey>>();
            }
            if (s.RedirectUris == null)
            {
                s.RedirectUris = new List<ClientRedirectUri<TKey>>();
            }
            if (s.PostLogoutRedirectUris == null)
            {
                s.PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri<TKey>>();
            }
            if (s.AllowedScopes == null)
            {
                s.AllowedScopes = new List<ClientScope<TKey>>();
            }
            if (s.IdentityProviderRestrictions == null)
            {
                s.IdentityProviderRestrictions = new List<ClientProviderRestriction<TKey>>();
            }
            if (s.Claims == null)
            {
                s.Claims = new List<ClientClaim<TKey>>();
            }
            if (s.AllowedCorsOrigins == null)
            {
                s.AllowedCorsOrigins = new List<ClientCorsOrigin<TKey>>();
            }
            if (s.AllowedGrantTypes == null)
            {
                s.AllowedGrantTypes = new List<ClientGrantType<TKey>>();
            }

            return Mapper.Map<Client<TKey>, Models.Client>(s);
        }
    }

    public static class EntitiesMap
    {
        static EntitiesMap()
        {
            Mapper.Configuration.AddProfile<EntitiesMapProfile>();
        }

        public static Models.Consent ToModel(Consent s)
        {
            if (s == null) return null;
            return Mapper.Map<Consent, Models.Consent>(s);
        }
    }

    public static class MappingExtensions
    {
        public static Models.Consent ToModel(this Consent s)
        {
            return EntitiesMap.ToModel(s);
        }

        public static Models.Scope ToModel<TKey>(this Scope<TKey> s)
            where TKey : IEquatable<TKey>
        {
            return EntitiesMap<TKey>.ToModel(s);
        }

        public static Models.Client ToModel<TKey>(this Client<TKey> s)
            where TKey : IEquatable<TKey>
        {
            return EntitiesMap<TKey>.ToModel(s);
        }
    }
}